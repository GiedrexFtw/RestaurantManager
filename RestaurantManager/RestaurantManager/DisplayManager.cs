using RestaurantManager.Data.DataProviders;
using RestaurantManager.Data.Repositories;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager
{
    class DisplayManager
    {
        private readonly IDataProvider _dataProvider;
        private readonly IRepo<Product> _productRepo;
        private readonly IRepo<MenuItem> _menuRepo;
        private readonly IRepo<Order> _ordersRepo;

        public DisplayManager(IDataProvider dataProvider, IRepo<Product> productRepo,
            IRepo<MenuItem> menuRepo, IRepo<Order> ordersRepo)
        {
            this._dataProvider = dataProvider;
            this._productRepo = productRepo;
            this._menuRepo = menuRepo;
            this._ordersRepo = ordersRepo;
        }

        public void DisplayStock()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("| Id | Name | Portion Count | Unit | Portion Size |");
            Console.WriteLine(new string('-', 50));
            foreach (var product in _dataProvider.StockProducts)
            {
                Console.WriteLine(product.ToString());
            }
        }
        public void DisplayMenu()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("| Id | Name | Products |");
            Console.WriteLine(new string('-', 50));
            foreach (var menuItem in _dataProvider.MenuItems)
            {
                Console.WriteLine(menuItem.ToString());
            }
        }
        public void DisplayOrders()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("| Id | Date | Menu Items |");
            Console.WriteLine(new string('-', 50));
            foreach (var order in _dataProvider.Orders)
            {
                Console.WriteLine(order.ToString());
            }
        }
    }
}
