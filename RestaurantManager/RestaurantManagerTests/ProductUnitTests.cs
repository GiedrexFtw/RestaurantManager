using Moq;
using RestaurantManager;
using RestaurantManager.Data.DataProviders;
using RestaurantManager.Data.Repositories;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace RestaurantManagerTests
{
    public class ProductUnitTests
    {
        Mock<IDataProvider> dataProvider;
        IRepo<Product> productRepo;
        IRepo<MenuItem> menuItemRepo;
        IRepo<Order> orderRepo;
        DisplayManager displayManager;

        public ProductUnitTests()
        {
            dataProvider = new Mock<IDataProvider>();
            productRepo = new StockRepo(dataProvider.Object);
            menuItemRepo = new MenuRepo(dataProvider.Object);
            orderRepo = new OrdersRepo(dataProvider.Object);
            displayManager = new DisplayManager(productRepo, menuItemRepo, orderRepo);

        }
        [Fact]
        public void CreatesProductsAndAssignsIdWithGoodData()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.StockProducts = new List<Product>();
            Product product = new Product
            {
                Id = 2,
                Name = "Onion",
                PortionCount = 4,
                Unit = "lbs",
                PortionSize = 1
            };
            Product product2 = new Product
            {
                Id = 3,
                Name = "Brocoli",
                PortionCount = 1,
                Unit = "kg",
                PortionSize = 0.2
            };
            //Act
            displayManager.CreateProduct("Cucumber,1,kg,0.2");
            displayManager.CreateProduct("Onion,4,lbs,1");
            displayManager.DeleteProduct(1);
            displayManager.CreateProduct("Brocoli,1,kg,0.2");

            
            //Assert
            Assert.NotEmpty(dataProvider.Object.StockProducts);
            Assert.True(dataProvider.Object.StockProducts.Count == 2);
            Assert.False(dataProvider.Object.StockProducts.Exists(x => x.Id == 1));
            Assert.True(dataProvider.Object.StockProducts.Exists(x => x.Id == 2));
            Assert.True(dataProvider.Object.StockProducts.Exists(x => x.Id == 3));
            Assert.True(dataProvider.Object.StockProducts.Contains(product) &&
                dataProvider.Object.StockProducts.Contains(product2));
        }
        [Fact]
        public void DoesntCreateProductsWithBadData()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.StockProducts = new List<Product>();

            //Act
            displayManager.CreateProduct("Cucumber,abcd,kg,0.2");
            displayManager.CreateProduct("Onion,4,lbs,abcd");
            displayManager.CreateProduct("Onion,4,lbs,abcd");
            displayManager.CreateProduct("Brocoli,abc,kg,abcd");
            displayManager.CreateProduct("Brocoli");
            displayManager.CreateProduct("Onion, 500, lbs");

            //Assert
            Assert.Empty(dataProvider.Object.StockProducts);
        }

        [Fact]
        public void UpdatesWithGoodData()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.StockProducts = new List<Product>();
            Product product = new Product
            {
                Id = 1,
                Name = "Pineapple",
                PortionCount = 2,
                Unit = "lbs",
                PortionSize = 0.3
            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "Orange",
                PortionCount = 1,
                Unit = "lbs",
                PortionSize = 0.3
            };
            //Act
            displayManager.CreateProduct("Cucumber,1,kg,0.2");
            displayManager.CreateProduct("Brocoli,1,kg,0.2");
            displayManager.EditProduct(1, "Pineapple,2,lbs,0.3");
            displayManager.EditProduct(2, "Orange,1,lbs,0.3");


            //Assert
            Assert.NotEmpty(dataProvider.Object.StockProducts);
            Assert.True(dataProvider.Object.StockProducts.Count == 2);
            Assert.True(dataProvider.Object.StockProducts.Exists(x => x.Id == 1));
            Assert.True(dataProvider.Object.StockProducts.Exists(x => x.Id == 2));
            Assert.True(dataProvider.Object.StockProducts.Contains(product) &&
                dataProvider.Object.StockProducts.Contains(product2));
        }
        [Fact]
        public void DoesntUpdateWithBadData()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.StockProducts = new List<Product>();
            Product product = new Product
            {
                Id = 1,
                Name = "Pineapple",
                PortionCount = 2,
                Unit = "lbs",
                PortionSize = 0.3
            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "Orange",
                PortionCount = 1,
                Unit = "lbs",
                PortionSize = 0.3
            };
            //Act
            
            displayManager.CreateProduct("Pineapple,2,lbs,0.3");
            displayManager.CreateProduct("Orange,1,lbs,0.3");
            displayManager.EditProduct(1, "Cucumber,asdf,kg,asc");
            displayManager.EditProduct(200, "Brocoli,1,kg,0.2");
            displayManager.EditProduct(2, "apple,kg");


            //Assert
            Assert.NotEmpty(dataProvider.Object.StockProducts);
            Assert.True(dataProvider.Object.StockProducts.Count == 2);
            Assert.True(dataProvider.Object.StockProducts.Exists(x => x.Id == 1));
            Assert.True(dataProvider.Object.StockProducts.Exists(x => x.Id == 2));
            Assert.True(dataProvider.Object.StockProducts.Contains(product) &&
                dataProvider.Object.StockProducts.Contains(product2));
        }

        [Fact]
        public void DeletesWhenItemExists()
        {
            //Arrange
            dataProvider.SetupAllProperties();
            dataProvider.Object.StockProducts = new List<Product>();
            Product product = new Product
            {
                Id = 1,
                Name = "Pineapple",
                PortionCount = 2,
                Unit = "lbs",
                PortionSize = 0.3
            };
            Product product2 = new Product
            {
                Id = 2,
                Name = "Orange",
                PortionCount = 1,
                Unit = "lbs",
                PortionSize = 0.3
            };
            //Act

            displayManager.CreateProduct("Pineapple,2,lbs,0.3");
            displayManager.CreateProduct("Orange,1,lbs,0.3");
            displayManager.DeleteProduct(1);

            //Assert
            Assert.NotEmpty(dataProvider.Object.StockProducts);
            Assert.True(dataProvider.Object.StockProducts.Count == 1);
            Assert.True(dataProvider.Object.StockProducts.Exists(x => x.Id == 2));
            Assert.Contains(product2, dataProvider.Object.StockProducts);
            Assert.DoesNotContain(product, dataProvider.Object.StockProducts);
        }

    }
}
