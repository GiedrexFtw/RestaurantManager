using RestaurantManager.Data.DataProviders;
using RestaurantManager.Data.Repositories;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RestaurantManager
{
    class DisplayManager
    {
        // Dependency inject required repos for data manipulation
        private readonly IRepo<Product> _productRepo;
        private readonly IRepo<MenuItem> _menuRepo;
        private readonly IRepo<Order> _ordersRepo;

        public DisplayManager(IRepo<Product> productRepo,
            IRepo<MenuItem> menuRepo, IRepo<Order> ordersRepo)
        {
            this._productRepo = productRepo;
            this._menuRepo = menuRepo;
            this._ordersRepo = ordersRepo;
        }

        //Display functions
        public void DisplayStock()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("| Id | Name | Portion Count | Unit | Portion Size |");
            Console.WriteLine(new string('-', 50));
            foreach (var product in _productRepo.GetList())
            {
                Console.WriteLine(product.ToString());
            }
        }
        public void DisplayMenu()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("| Id | Name | Products |");
            Console.WriteLine(new string('-', 50));
            foreach (var menuItem in _menuRepo.GetList())
            {
                Console.WriteLine(menuItem.ToString());
            }
        }
        public void DisplayOrders()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("| Id | Date | Menu Items |");
            Console.WriteLine(new string('-', 50));
            foreach (var order in _ordersRepo.GetList())
            {
                Console.WriteLine(order.ToString());
            }
        }
        // Create functions
        public void CreateProduct(string propertiesStr)
        {
            string[] properties = propertiesStr.Split(',');
            //Check if all required data is provided
            if(properties.Length != typeof(Product).GetProperties().Length - 1)
            {
                Console.WriteLine("Required data not provided! You must provide all the fields!");
                return;
            }
            Product product = new Product
            {
                Name = properties[0],
                PortionCount = int.TryParse(properties[1], out int portionCount)
                ? portionCount : -1,
                Unit = properties[2],
                PortionSize = double.TryParse(properties[3], out double portionSize)
                ? portionSize : -1
            };
            if (product.PortionCount != -1 && product.PortionSize != -1)
            {
                if (_productRepo.CreateItem(product))
                {
                    Console.WriteLine("Successfully added!");
                }
                else
                {
                    Console.WriteLine("Something went wrong. Couldn't add the item");
                }
                
            }
            else
            {
                Console.WriteLine("Data provided is incorrect!");
            }
        }
        public void CreateMenuItem(string propertiesStr)
        {
            //Populate related products
            string[] properties = propertiesStr.Split(',');
            List<int> productIds = properties[1].Split(' ')
                .Select(str => int.TryParse(str, out int strId) ? strId : -1).ToList();
            List<Product> products = _productRepo.GetList().Where(p => productIds.Contains(p.Id)).ToList();
            //Check if all required data is provided
            if (properties.Length != typeof(MenuItem).GetProperties().Length - 1)
            {
                Console.WriteLine("Required data not provided! You must provide all the fields!");
                return;
            }
            MenuItem menuItem = new MenuItem
            {
                Name = properties[0],
                Products = products
            };
            if (products.Count != 0)
            {
                if (_menuRepo.CreateItem(menuItem))
                {
                    Console.WriteLine("Item successfully added!");
                }
                else
                {
                    Console.WriteLine("Something went wrong. Couldnt add the item");
                }
            }
            else
            {
                Console.WriteLine("Data entered is invalid!");
            }
        }
        public void CreateOrder(string propertiesStr)
        {
            //Populate related menu items
            string[] properties = propertiesStr.Split(',');
            List<int> menuItemIds = properties[0].Split(' ')
                .Select(str => int.TryParse(str, out int strId) ? strId : -1).ToList();
            List<MenuItem> menuItems = _menuRepo.GetList().Where(p => menuItemIds.Contains(p.Id)).ToList();
            //Check if all required data is provided
            if (properties.Length != typeof(Order).GetProperties().Length - 2)
            {
                Console.WriteLine("Required data not provided! You must provide all the fields!");
                return;
            }
            Order order = new Order
            {
                MenuItems = menuItems
            };
            if (_ordersRepo.CreateItem(order))
            {
                Console.WriteLine("Item successfully added!");
            }
            else
            {
                Console.WriteLine("Out of products to finish the order. Order cancelled");
            }
        }
        public void ShowProduct(int id)
        {
            Product product =_productRepo.GetItemById(id);
            if(product != null)
            {
                Console.WriteLine(product.ToString());
            }
            else
            {
                Console.WriteLine("Item with this id doesn't exist!");
            }
            
        }
        public void ShowMenuItem(int id)
        {
            MenuItem menuItem = _menuRepo.GetItemById(id);
            if (menuItem != null)
            {
                Console.WriteLine(menuItem.ToString());
            }
            else
            {
                Console.WriteLine("Item with this id doesn't exist!");
            }
        }
        public void ShowOrder(int id)
        {
            Order order = _ordersRepo.GetItemById(id);
            if (order != null)
            {
                Console.WriteLine(order.ToString());
            }
            else
            {
                Console.WriteLine("Item with this id doesn't exist!");
            }
        }
            
        public bool IsIdValid(string strId, out int id)
        {
            return int.TryParse(strId, out id);
        }

        public void EditProduct(int id, string propertiesStr)
        {
            string[] properties = propertiesStr.Split(',');
            if (properties.Length != typeof(Product).GetProperties().Length - 1)
            {
                Console.WriteLine("Required data not provided! You must provide all the fields!");
                return;
            }
            Product updatedProduct = new Product
            {
                Name = properties[0],
                PortionCount = int.TryParse(properties[1], out int portionCount)
                ? portionCount : -1,
                Unit = properties[2],
                PortionSize = double.TryParse(properties[3], out double portionSize)
                ? portionSize : -1
            };
            if(updatedProduct.PortionCount != -1 && updatedProduct.PortionSize != -1)
            {
                if(_productRepo.UpdateItem(updatedProduct, id))
                {
                    Console.WriteLine("Item successfully updated!");
                }
                else
                {
                    Console.WriteLine("Failed to update. Item with such id doesn't exist!");
                }
            }
            else
            {
                Console.WriteLine("Data entered is invalid!");
            }
        }
        public void EditMenuItem(int id, string propertiesStr)
        {
            string[] properties = propertiesStr.Split(',');
            List<int> productIds = properties[1].Split(' ')
                .Select(str => int.TryParse(str, out int strId) ? strId : -1).ToList();
            List<Product> products = _productRepo.GetList().Where(p => productIds.Contains(p.Id)).ToList();
            if (properties.Length != typeof(MenuItem).GetProperties().Length - 1)
            {
                Console.WriteLine("Required data not provided! You must provide all the fields!");
                return;
            }
            MenuItem menuItem = new MenuItem
            {
                Name = properties[0],
                Products = products
            };
            if (products.Count != 0)
            {
                if(_menuRepo.UpdateItem(menuItem, id))
                {
                    Console.WriteLine("Item successfully updated!");
                }
                else
                {
                    Console.WriteLine("Failed to update. Item with such id doesn't exist!");
                }
            }
            else
            {
                Console.WriteLine("Data entered is invalid!");
            }
        }
        public void EditOrder(int id, string propertiesStr)
        {
            string[] properties = propertiesStr.Split(',');
            List<int> menuItemIds = properties[0].Split(' ')
                .Select(str => int.TryParse(str, out int strId) ? strId : -1).ToList();
            List<MenuItem> menuItems = _menuRepo.GetList().Where(p => menuItemIds.Contains(p.Id)).ToList();
            if (properties.Length != typeof(Order).GetProperties().Length - 2)
            {
                Console.WriteLine("Required data not provided! You must provide all the fields!");
                return;
            }
            Order order = new Order
            {
                MenuItems = menuItems
            };
            if (_ordersRepo.UpdateItem(order, id))
            {
                Console.WriteLine("Item successfully updated!");
            }
            else
            {
                Console.WriteLine("Failed to update. Item with such id doesn't exist!");
            }
        }
        public void DeleteProduct(int id)
        {
            if (_productRepo.DeleteItem(id))
            {
                Console.WriteLine("Successfully deleted!");
            }
            else
            {
                Console.WriteLine("Item to delete not found!");
            }

        }
        public void DeleteMenuItem(int id)
        {
            if (_menuRepo.DeleteItem(id))
            {
                Console.WriteLine("Successfully deleted!");
            }
            else
            {
                Console.WriteLine("Item to delete not found!");
            }
        }
        public void DeleteOrder(int id)
        {
            if (_ordersRepo.DeleteItem(id))
            {
                Console.WriteLine("Successfully deleted!");
            }
            else
            {
                Console.WriteLine("Item to delete not found!");
            }
        }
    }
}
