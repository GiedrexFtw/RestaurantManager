using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RestaurantManager.Data.DataProviders
{
    class FileDataProvider : IDataProvider
    {
        public List<Product> StockProducts { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public List<Order> Orders { get; set; }

        private readonly string _stocksFileName = @"../../../Data/Files/Stock.csv";
        private readonly string _menuFileName = @"../../../Data/Files/Menu.csv";
        private readonly string _ordersFileName = @"../../../Data/Files/Orders.csv";

        public FileDataProvider()
        {
            StockProducts = new List<Product>();
            MenuItems = new List<MenuItem>();
            Orders = new List<Order>();
            PopulateStockProducts();
            PopulateMenuItems();
            PopulateOrders();
        }
        private void PopulateStockProducts()
        {
            using StreamReader reader = new StreamReader(path: this._stocksFileName, encoding: Encoding.Default);
            string line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                string[] productLineData = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                Product product = new Product
                {
                    Id = int.TryParse(productLineData[0], out int productId) ? productId : 0,
                    Name = productLineData[1],
                    PortionCount = int.TryParse(productLineData[2], out int productPortionCount) ? productPortionCount : 0,
                    Unit = productLineData[3],
                    PortionSize = double.TryParse(productLineData[4], out double productPortionSize) ? productPortionSize : 0
                };
                if (product.Id != 0)
                    this.StockProducts.Add(product);

            }
        }
        private void PopulateMenuItems()
        {
            using StreamReader reader = new StreamReader(path: this._menuFileName, encoding: Encoding.Default);
            string line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                string[] menuItemLineData = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                List<int> productList = menuItemLineData[2].Split(' ')
                    .Select(productIdString => int.TryParse(productIdString, out int productId) ? productId : 0)
                    .ToList();
                MenuItem menuItem = new MenuItem
                {
                    Id = int.TryParse(menuItemLineData[0], out int menuItemId) ? menuItemId : 0,
                    Name = menuItemLineData[1],
                    Products = StockProducts.Where(p => productList.Contains(p.Id)).ToList()
                };
                if (menuItem.Id != 0 && menuItem.Products.Count() != 0)
                    this.MenuItems.Add(menuItem);
            }
        }
        private void PopulateOrders()
        {
            using StreamReader reader = new StreamReader(path: this._ordersFileName, encoding: Encoding.Default);
            string line;
            while (!string.IsNullOrEmpty(line = reader.ReadLine()))
            {
                string[] ordersLineData = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                List<int> menuItemsList = ordersLineData[2].Split(' ')
                    .Select(menuItemIdString => int.TryParse(menuItemIdString, out int menuItemId) ? menuItemId : 0)
                    .ToList();
                Order order = new Order
                {
                    Id = int.TryParse(ordersLineData[0], out int menuItemId) ? menuItemId : 0,
                    Date = DateTime.TryParse(ordersLineData[1], out DateTime date) ? date : DateTime.Now,
                    MenuItems = MenuItems.Where(p => menuItemsList.Contains(p.Id)).ToList()
                };
                if (order.Id != 0 && order.MenuItems.Count() != 0)
                    this.Orders.Add(order);
            }
        }
    }
}
