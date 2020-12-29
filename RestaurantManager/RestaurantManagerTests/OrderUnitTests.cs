using Moq;
using RestaurantManager;
using RestaurantManager.Data.DataProviders;
using RestaurantManager.Data.Repositories;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace RestaurantManagerTests
{
    public class OrderUnitTests
    {
        Mock<IDataProvider> dataProvider;
        IRepo<Product> productRepo;
        IRepo<MenuItem> menuItemRepo;
        IRepo<Order> orderRepo;
        DisplayManager displayManager;

        public OrderUnitTests()
        {
            dataProvider = new Mock<IDataProvider>();
            productRepo = new StockRepo(dataProvider.Object);
            menuItemRepo = new MenuRepo(dataProvider.Object);
            orderRepo = new OrdersRepo(dataProvider.Object);
            displayManager = new DisplayManager(productRepo, menuItemRepo, orderRepo);

        }
        [Fact]
        public void CreatesOrdersAndDeductsPortionsFromStock()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.MenuItems = new List<MenuItem>();
            dataProvider.Object.StockProducts = new List<Product>();
            dataProvider.Object.Orders = new List<Order>();
            displayManager.CreateProduct("Beef,1,kg,0.3"); // created with portion count of 1
            displayManager.CreateProduct("Cabbage,0,kg,0.4");            
            displayManager.CreateMenuItem("Beef steak,1");
            displayManager.CreateMenuItem("Salad,1 2");
            Order order = new Order
            {
                Id = 1,
                MenuItems = new List<MenuItem> {
                    new MenuItem
                    {
                        Id = 1,
                        Name = "Beef steak",
                        Products = new List<Product>()
                        {
                            new Product
                            {
                                Id = 1,
                                Name = "Beef",
                                PortionCount = 0, // created with 1, now dedcuted to 0
                                Unit = "kg",
                                PortionSize = 0.3
                            }
                        }
                    }
                }
            };
            //Act
            // Enough products
            displayManager.CreateOrder("1");
            // Not enough products for one of the orders
            displayManager.CreateOrder("1 2");
            // Not enough products
            displayManager.CreateOrder("2");


            //Assert
            Assert.NotEmpty(dataProvider.Object.Orders);
            Assert.True(dataProvider.Object.Orders.Count == 1);
            Assert.True(dataProvider.Object.Orders.Exists(x => x.Id == 1));
            Assert.Equal(order, dataProvider.Object.Orders[0]);
            Assert.Contains(order, dataProvider.Object.Orders);
        }
        
    }
}
