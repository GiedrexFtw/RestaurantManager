using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Data.DataProviders
{
    public interface IDataProvider
    {
        public List<Product> StockProducts { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public List<Order> Orders { get; set; }

        public void SaveChanges();
    }
}
