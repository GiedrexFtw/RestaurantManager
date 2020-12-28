using RestaurantManager.Data.DataProviders;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _dataProvider.StockProducts.FirstOrDefault(p => p.Id == id);
        }
        public bool CreateItem(Product item)
        {
            Product lastEntry = _dataProvider.StockProducts.OrderByDescending(m => m.Id).FirstOrDefault();
            if(lastEntry != null)
            {
                item.Id = lastEntry.Id + 1;
                _dataProvider.StockProducts.Add(item);

                return true;
            }

            return false;
        }
        public bool UpdateItem(Product item, int id)
        {
            Product productToUpdate = GetItemById(id);
            if (productToUpdate != null)
            {
                productToUpdate.Name = item.Name;
                productToUpdate.PortionCount = item.PortionCount;
                productToUpdate.Unit = item.Unit;
                productToUpdate.PortionSize = item.PortionSize;

                return true;
            }
            return false;
        }
        public bool DeleteItem(int id)
        {
            Product product = GetItemById(id);
            if(product != null)
            {
                _dataProvider.StockProducts.Remove(product);

                return true;
            }
            return false;
        }
    }
}
