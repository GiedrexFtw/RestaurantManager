using RestaurantManager.Data.DataProviders;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _dataProvider.MenuItems.FirstOrDefault(p => p.Id == id);
        }
        public bool CreateItem(MenuItem item)
        {
            MenuItem lastEntry = _dataProvider.MenuItems.OrderByDescending(m => m.Id).FirstOrDefault();
            if (lastEntry != null)
            {
                item.Id = lastEntry.Id + 1;
                _dataProvider.MenuItems.Add(item);

                return true;
            }

            return false;
        }
        public bool UpdateItem(MenuItem item, int id)
        {
            MenuItem menuItemToUpdate = GetItemById(id);
            if (menuItemToUpdate != null)
            {
                menuItemToUpdate.Name = item.Name;
                menuItemToUpdate.Products = item.Products;

                return true;
            }
            return false;
        }
        public bool DeleteItem(int id)
        {
            MenuItem menuItem = GetItemById(id);
            if (menuItem != null)
            {
                _dataProvider.MenuItems.Remove(menuItem);

                return true;
            }
            return false;
        }
    }
}
