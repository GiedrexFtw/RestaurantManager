using RestaurantManager.Data.DataProviders;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Data.Repositories
{
    class StockRepo : IRepo<Product>
    {
        private readonly IDataProvider _dataProvider;

        public StockRepo(IDataProvider dataProvider)
        {
            this._dataProvider = dataProvider;
        }

        public IEnumerable<Product> GetList()
        {
            return _dataProvider.StockProducts;
        }
        public Product GetItemById(int id)
        {
            return _dataProvider.StockProducts.Find(p => p.Id == id);
        }
        public void CreateItem(Product item)
        {
            throw new NotImplementedException();
        }
        public void UpdateItem(Product item, int id)
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
