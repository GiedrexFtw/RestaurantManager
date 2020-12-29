using Moq;
using RestaurantManager;
using RestaurantManager.Data.DataProviders;
using RestaurantManager.Data.Repositories;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RestaurantManagerTests
{
    public class MenuItemUnitTests
    {
        Mock<IDataProvider> dataProvider;
        IRepo<Product> productRepo;
        IRepo<MenuItem> menuItemRepo;
        IRepo<Order> orderRepo;
        DisplayManager displayManager;

        public MenuItemUnitTests()
        {
            dataProvider = new Mock<IDataProvider>();
            productRepo = new StockRepo(dataProvider.Object);
            menuItemRepo = new MenuRepo(dataProvider.Object);
            orderRepo = new OrdersRepo(dataProvider.Object);
            displayManager = new DisplayManager(productRepo, menuItemRepo, orderRepo);

        }
        [Fact]
        public void CreatesMenuItemsAndAssignsIdWithGoodData()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.MenuItems = new List<MenuItem>();
            dataProvider.Object.StockProducts = new List<Product>();
            displayManager.CreateProduct("Beef,1,kg,0.3");
            displayManager.CreateProduct("Cabbage,0,kg,0.4");
            MenuItem menuitem = new MenuItem
            {
                Id = 2,
                Name = "Beef steak",
                Products = new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Beef",
                        PortionCount = 1,
                        Unit = "kg",
                        PortionSize = 0.3
                    }
                }
            };
            MenuItem menuitem2 = new MenuItem
            {
                Id = 3,
                Name = "Salad",
                Products = new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Beef",
                        PortionCount = 1,
                        Unit = "kg",
                        PortionSize = 0.3
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Cabbage",
                        PortionCount = 0,
                        Unit = "kg",
                        PortionSize = 0.4
                    }
                }
            };
            //Act
            displayManager.CreateMenuItem("Pizza,1 2");
            displayManager.CreateMenuItem("Beef steak,1");
            displayManager.DeleteMenuItem(1);
            displayManager.CreateMenuItem("Salad,1 2");


            //Assert
            Assert.NotEmpty(dataProvider.Object.MenuItems);
            Assert.True(dataProvider.Object.MenuItems.Count == 2);
            Assert.False(dataProvider.Object.MenuItems.Exists(x => x.Id == 1));
            Assert.True(dataProvider.Object.MenuItems.Exists(x => x.Id == 2));
            Assert.True(dataProvider.Object.MenuItems.Exists(x => x.Id == 3));
            Assert.True(dataProvider.Object.MenuItems.Contains(menuitem) &&
                dataProvider.Object.MenuItems.Contains(menuitem2));
        }
        [Fact]
        public void DoesntCreateMenuItemsWithBadData()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.MenuItems = new List<MenuItem>();
            dataProvider.Object.StockProducts = new List<Product>();
            
            //Act
            displayManager.CreateMenuItem("Pizza");
            displayManager.CreateMenuItem("1 2 4");


            //Assert
            Assert.Empty(dataProvider.Object.MenuItems);
        }

        [Fact]
        public void UpdatesWithGoodData()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.MenuItems = new List<MenuItem>();
            dataProvider.Object.StockProducts = new List<Product>();
            displayManager.CreateProduct("Beef,1,kg,0.3");
            displayManager.CreateProduct("Cabbage,0,kg,0.4");
            MenuItem menuitem = new MenuItem
            {
                Id = 1,
                Name = "Beef steak with cabbage",
                Products = new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Beef",
                        PortionCount = 1,
                        Unit = "kg",
                        PortionSize = 0.3
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Cabbage",
                        PortionCount = 0,
                        Unit = "kg",
                        PortionSize = 0.4
                    }
                }
            };
            MenuItem menuitem2 = new MenuItem
            {
                Id = 2,
                Name = "Salad with meat",
                Products = new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Beef",
                        PortionCount = 1,
                        Unit = "kg",
                        PortionSize = 0.3
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Cabbage",
                        PortionCount = 0,
                        Unit = "kg",
                        PortionSize = 0.4
                    }
                }
            };
            //Act
            displayManager.CreateMenuItem("Beef steak,1");
            displayManager.CreateMenuItem("Salad,1 2");
            displayManager.EditMenuItem(1,"Beef steak with cabbage,1 2");
            displayManager.EditMenuItem(2,"Salad with meat,1 2");


            //Assert
            Assert.NotEmpty(dataProvider.Object.MenuItems);
            Assert.True(dataProvider.Object.MenuItems.Count == 2);
            Assert.True(dataProvider.Object.MenuItems.Exists(x => x.Id == 1));
            Assert.True(dataProvider.Object.MenuItems.Exists(x => x.Id == 2));
            Assert.True(dataProvider.Object.MenuItems.Contains(menuitem) &&
                dataProvider.Object.MenuItems.Contains(menuitem2));
        }
        [Fact]
        public void DoesntUpdateWithBadData()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.MenuItems = new List<MenuItem>();
            dataProvider.Object.StockProducts = new List<Product>();
            displayManager.CreateProduct("Beef,1,kg,0.3");
            displayManager.CreateProduct("Cabbage,0,kg,0.4");
            MenuItem menuitem = new MenuItem
            {
                Id = 1,
                Name = "Beef steak",
                Products = new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Beef",
                        PortionCount = 1,
                        Unit = "kg",
                        PortionSize = 0.3
                    }
                }
            };
            MenuItem menuitem2 = new MenuItem
            {
                Id = 2,
                Name = "Salad",
                Products = new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Beef",
                        PortionCount = 1,
                        Unit = "kg",
                        PortionSize = 0.3
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Cabbage",
                        PortionCount = 0,
                        Unit = "kg",
                        PortionSize = 0.4
                    }
                }
            };
            //Act
            displayManager.CreateMenuItem("Beef steak,1");
            displayManager.CreateMenuItem("Salad,1 2");
            displayManager.EditMenuItem(1000, "Beef steak ,1 2");
            displayManager.EditMenuItem(2, "Salad with meat and spices");


            //Assert
            Assert.NotEmpty(dataProvider.Object.MenuItems);
            Assert.True(dataProvider.Object.MenuItems.Count == 2);
            Assert.True(dataProvider.Object.MenuItems.Exists(x => x.Id == 1));
            Assert.True(dataProvider.Object.MenuItems.Exists(x => x.Id == 2));
            Assert.True(dataProvider.Object.MenuItems.Contains(menuitem) &&
                dataProvider.Object.MenuItems.Contains(menuitem2));
        }

        [Fact]
        public void DeletesWhenItemExists()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.MenuItems = new List<MenuItem>();
            dataProvider.Object.StockProducts = new List<Product>();
            displayManager.CreateProduct("Beef,1,kg,0.3");
            displayManager.CreateProduct("Cabbage,0,kg,0.4");
            MenuItem menuitem = new MenuItem
            {
                Id = 1,
                Name = "Beef steak with cabbage",
                Products = new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Beef",
                        PortionCount = 1,
                        Unit = "kg",
                        PortionSize = 0.3
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Cabbage",
                        PortionCount = 0,
                        Unit = "kg",
                        PortionSize = 0.4
                    }
                }
            };
            MenuItem menuitem2 = new MenuItem
            {
                Id = 2,
                Name = "Salad with meat",
                Products = new List<Product>()
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Beef",
                        PortionCount = 1,
                        Unit = "kg",
                        PortionSize = 0.3
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Cabbage",
                        PortionCount = 0,
                        Unit = "kg",
                        PortionSize = 0.4
                    }
                }
            };
            //Act
            displayManager.CreateMenuItem("Beef steak with cabbage,1 2");
            displayManager.CreateMenuItem("Salad with meat,1 2");
            displayManager.DeleteMenuItem(1);
            displayManager.DeleteMenuItem(2);


            //Assert
            Assert.Empty(dataProvider.Object.MenuItems);
        }
    }
}
