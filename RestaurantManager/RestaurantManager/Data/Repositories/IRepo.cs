using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Data.Repositories
{
    interface IRepo<T>
    {
        public IEnumerable<T> GetList();
        public T GetItemById(int id);
        public void CreateItem(T item);
        public void UpdateItem(T item, int id);
        public void DeleteItem(int id);
        public void SaveChanges();
    }
}
