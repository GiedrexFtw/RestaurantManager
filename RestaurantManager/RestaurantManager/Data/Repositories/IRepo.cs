using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Data.Repositories
{
    public interface IRepo<T>
    {
        public IEnumerable<T> GetList();
        public T GetItemById(int id);
        public bool CreateItem(T item);
        public bool UpdateItem(T item, int id);
        public bool DeleteItem(int id);
    }
}
