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
        static DisplayManager _displayManager = new DisplayManager( _productRepo,
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
            _displayManager.DisplayMenu();
            _displayManager.DisplayOrders();
            Console.WriteLine("Command list:\n");
            Console.WriteLine("display products/menuitems/orders - displays stock/menu/orders table");
            Console.WriteLine("create product/menuitem/order - creates product/menuitem/order");
            Console.WriteLine("show product/menuitem/order {id} - shows the specified by id product/menuitem/order");
            Console.WriteLine("edit product/menuitem/order {id} - edit the specified by id product/menuitem/order");
            Console.WriteLine("delete product/menuitem/order {id} - delete the specified by id product/menuitem/order");
            Console.WriteLine("exit - to exit the app");

            bool exitTriggered = false;
            string strId = string.Empty;
            string updateStr = string.Empty;
            int id = 0;

            // Check for commands
            while (!exitTriggered)
            {            
                Console.WriteLine("Please type a command to continue:");
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
                        Console.WriteLine("Please provide information to add, separated by [,] character");
                        updateStr = Console.ReadLine();
                        _displayManager.CreateProduct(updateStr);
                        break;
                    case ("create menuitem"):
                        Console.WriteLine("Please provide information to add, separated by [,] character");
                        updateStr = Console.ReadLine();
                        _displayManager.CreateMenuItem(updateStr);
                        break;
                    case ("create order"):
                        Console.WriteLine("Please provide information to add, separated by [,] character");
                        updateStr = Console.ReadLine();
                        _displayManager.CreateOrder(updateStr);
                        break;
                    case ("show product"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        if(_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.ShowProduct(id);
                        }

                        break;
                    case ("show menuitem"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.ShowMenuItem(id);
                        }
                        break;
                    case ("show order"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.ShowOrder(id);
                        }
                        break;
                    case ("edit product"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        Console.WriteLine("Please provide information to update, separated by [,] character");
                        updateStr = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.EditProduct(id, updateStr);
                        }
                        break;
                    case ("edit menuitem"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        Console.WriteLine("Please provide information to update, separated by [,] character");
                        updateStr = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.EditMenuItem(id, updateStr);
                        }
                        break;
                    case ("edit order"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        Console.WriteLine("Please provide information to update, separated by [,] character");
                        updateStr = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.EditOrder(id, updateStr);
                        }
                        break;
                    case ("delete product"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.DeleteProduct(id);
                        }
                        break;
                    case ("delete menuitem"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.DeleteMenuItem(id);
                        }
                        break;
                    case ("delete order"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.DeleteOrder(id);
                        }
                        break;
                    case ("exit"):
                        exitTriggered = true;
                        _dataProvider.SaveChanges();
                        break;
                    default:
                        Console.WriteLine("The command entered is invalid! Try again");
                        break;
                }
            }

        }
    }
}
