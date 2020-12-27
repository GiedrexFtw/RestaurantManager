using RestaurantManager.Data.DataProviders;
using RestaurantManager.Data.Repositories;
using RestaurantManager.Models;
using System;

namespace RestaurantManager
{
    class Program
    {
        // Init dependencies
        static IDataProvider _dataProvider = new FileDataProvider();
        static IRepo<Product> _productRepo = new StockRepo(_dataProvider);
        static IRepo<MenuItem> _menuItemRepo = new MenuRepo(_dataProvider);
        static IRepo<Order> _ordersRepo = new OrdersRepo(_dataProvider);
        static DisplayManager _displayManager = new DisplayManager(_dataProvider, _productRepo,
            _menuItemRepo, _ordersRepo);

        static void Main(string[] args)
        {
            // Display startup general info
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("|   Restaurant Manager Application - 2020-2021   |");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine();
            Console.WriteLine("Current stock:");
            _displayManager.DisplayStock();
            Console.WriteLine("Command list:\n");
            Console.WriteLine("display products/menuitems/orders - displays stock/menu/orders table");
            Console.WriteLine("create product/menuitem/order - creates product/menuitem/order");
            Console.WriteLine("show product/menuitem/order {id} - shows the specified by id product/menuitem/order");
            Console.WriteLine("edit product/menuitem/order {id} - edit the specified by id product/menuitem/order");
            Console.WriteLine("delete product/menuitem/order {id} - delete the specified by id product/menuitem/order");
            Console.WriteLine("exit - to exit the app");

            Console.WriteLine("Please type a command to continue:");

            bool exitTriggered = false;
            // Check for commands
            while (!exitTriggered)
            {
                string command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case ("display products"):
                        _displayManager.DisplayStock();
                        break;
                    case ("display menuitems"):
                        _displayManager.DisplayMenu();
                        break;
                    case ("display orders"):
                        _displayManager.DisplayOrders();
                        break;
                    case ("create product"):
                        break;
                    case ("create menuitem"):
                        break;
                    case ("create order"):
                        break;
                    case ("show product"):
                        break;
                    case ("show menuitem"):
                        break;
                    case ("show order"):
                        break;
                    case ("edit product"):
                        break;
                    case ("edit menuitem"):
                        break;
                    case ("edit order"):
                        break;
                    case ("delete product"):
                        break;
                    case ("delete menuitem"):
                        break;
                    case ("delete order"):
                        break;
                    case ("exit"):
                        exitTriggered = true;
                        break;
                    default:
                        Console.WriteLine("The command entered is invalid! Try again");
                        break;
                }
            }

        }
    }
}
