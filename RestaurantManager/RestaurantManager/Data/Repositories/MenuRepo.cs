using RestaurantManager.Data.DataProviders;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Data.Repositories
{
    class MenuRepo : IRepo<MenuItem>
    {
        private readonly IDataProvider _dataProvider;

        public MenuRepo(IDataProvider dataProvider)
        {
            this._dataProvider = dataProvider;
        }

        public IEnumerable<MenuItem> GetList()
        {
            return _dataProvider.MenuItems;
        }
        public MenuItem GetItemById(int id)
        {
            return _dataProvider.MenuItems.Find(p => p.Id == id);
        }
        public void CreateItem(MenuItem item)
        {
            throw new NotImplementedException();
        }
        public void UpdateItem(MenuItem item, int id)
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
