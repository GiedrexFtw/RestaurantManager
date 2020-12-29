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
            /* To migrate the app to the web easier, I would split the app to seperate parts -
             ui display logic(display manager, program) and business logic(repos) and write reusable code,
             so that it is not dependant on it's use case anymore. Another option might be to create an API and use it 
             in the future web app.*/

            // Display startup general info
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("|   Restaurant Manager Application - 2020-2021   |");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine();
            Console.WriteLine("Current stock:");
            _displayManager.DisplayStock();
            Console.WriteLine("Current menu:");
            _displayManager.DisplayMenu();
            Console.WriteLine("Current orders:");
            _displayManager.DisplayOrders();
            Console.WriteLine("\nCommand list:\n");
            Console.WriteLine("display products/menuitems/orders - displays stock/menu/orders table\n" +
                "create product/menuitem/order - creates product/menuitem/order\n" +
                "show product/menuitem/order - shows the specified by id product/menuitem/order\n" +
                "edit product/menuitem/order - edit the specified by id product/menuitem/order\n" +
                "delete product/menuitem/order - delete the specified by id product/menuitem/order\n" +
                "commands - to sho list of commands\n" +
                "exit - to exit the app and save changes to the file");
            
            // Check for commands and perform actions
            HandleCommands();

        }

        private static void HandleCommands()
        {
            bool exitTriggered = false;
            string strId = string.Empty;
            string updateStr = string.Empty;
            int id = 0;
            while (!exitTriggered)
            {
                Console.WriteLine("\nPlease type a command to continue:");
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
                        Console.WriteLine("Please provide name,portion count, unit, portion size, separated by [,] character");
                        Console.WriteLine("Example: Garlic,5,kg,0.1");
                        updateStr = Console.ReadLine();
                        _displayManager.CreateProduct(updateStr);
                        _displayManager.DisplayStock();
                        break;
                    case ("create menuitem"):
                        Console.WriteLine("Please provide name and products needed for recipe," +
                            " separated by [,] character(products should be separated by empty space character)");
                        Console.WriteLine("Example: Salad,1 2 3");
                        updateStr = Console.ReadLine();
                        _displayManager.CreateMenuItem(updateStr);
                        _displayManager.DisplayMenu();
                        break;
                    case ("create order"):
                        Console.WriteLine("Please provide menu items to order, separated by empty space ' ' character");
                        Console.WriteLine("Example: 1 2 3");
                        updateStr = Console.ReadLine();
                        _displayManager.CreateOrder(updateStr);
                        _displayManager.DisplayOrders();
                        break;
                    case ("show product"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
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
                        Console.WriteLine("Please provide name,portion count, unit, portion size, separated by [,] character");
                        Console.WriteLine("Example: Garlic,5,kg,0.1");
                        updateStr = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.EditProduct(id, updateStr);
                            _displayManager.DisplayStock();
                        }
                        break;
                    case ("edit menuitem"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        Console.WriteLine("Please provide name and products needed for recipe," +
                            " separated by [,] character(products should be separated by empty space character)");
                        Console.WriteLine("Example: Salad,1 2 3");
                        updateStr = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.EditMenuItem(id, updateStr);
                            _displayManager.DisplayMenu();
                        }
                        break;
                    case ("edit order"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        Console.WriteLine("Please provide menu items to order, separated by empty space ' ' character");
                        Console.WriteLine("Example: 1 2 3");
                        updateStr = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.EditOrder(id, updateStr);
                            _displayManager.DisplayOrders();
                        }
                        break;
                    case ("delete product"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.DeleteProduct(id);
                            _displayManager.DisplayStock();
                        }
                        break;
                    case ("delete menuitem"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.DeleteMenuItem(id);
                            _displayManager.DisplayMenu();
                        }
                        break;
                    case ("delete order"):
                        Console.WriteLine("Please provide id of an item: ");
                        strId = Console.ReadLine();
                        if (_displayManager.IsIdValid(strId, out id))
                        {
                            _displayManager.DeleteOrder(id);
                            _displayManager.DisplayOrders();
                        }
                        break;
                    case ("exit"):
                        // Exits the command loop and saves changes to files
                        exitTriggered = true;
                        _dataProvider.SaveChanges();
                        break;
                    case ("commands"):
                        Console.WriteLine("display products/menuitems/orders - displays stock/menu/orders table\n" +
                        "create product/menuitem/order - creates product/menuitem/order\n" +
                        "show product/menuitem/order - shows the specified by id product/menuitem/order\n" +
                        "edit product/menuitem/order - edit the specified by id product/menuitem/order\n" +
                        "delete product/menuitem/order - delete the specified by id product/menuitem/order\n" +
                        "commands - to sho list of commands\n" +
                        "exit - to exit the app and save changes to the file");
                        break;
                    default:
                        Console.WriteLine("The command entered is invalid! Try again");
                        break;
                }
            }
        }
    }
}
