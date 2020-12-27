using RestaurantManager.Data.DataProviders;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Data.Repositories
{
    class OrdersRepo : IRepo<Order>
    {
        private readonly IDataProvider _dataProvider;

        public OrdersRepo(IDataProvider dataProvider)
        {
            this._dataProvider = dataProvider;
        }

        public IEnumerable<Order> GetList()
        {
            return _dataProvider.Orders;
        }
        public Order GetItemById(int id)
        {
            return _dataProvider.Orders.Find(p => p.Id == id);
        }
        public void CreateItem(Order item)
        {
            throw new NotImplementedException();
        }
        public void UpdateItem(Order item, int id)
        {
            throw new NotImplementedException();
        }
        public void DeleteItem(int id)
        {
            throw new NotImplementedException();
        }
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
