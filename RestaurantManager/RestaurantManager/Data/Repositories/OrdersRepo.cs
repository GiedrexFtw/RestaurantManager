using RestaurantManager.Data.DataProviders;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantManager.Data.Repositories
{
    public class OrdersRepo : IRepo<Order>
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
            return _dataProvider.Orders.FirstOrDefault(p => p.Id == id);
        }
        public bool CreateItem(Order item)
        {
            Order lastEntry = _dataProvider.Orders.OrderByDescending(m => m.Id).FirstOrDefault();
            if(lastEntry != null)
            {
                item.Id = lastEntry.Id + 1;
            }
            else
            {
                item.Id = 1;
            }
            // Reduce the portions of products needed for order items
            foreach (var menuItem in item.MenuItems)
            {
                foreach (var product in menuItem.Products)
                {
                    if(product.PortionCount >= 1)
                    {
                        product.PortionCount--;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            _dataProvider.Orders.Add(item);

            return true;

        }
        public bool UpdateItem(Order item, int id)
        {
            Order orderToUpdate = GetItemById(id);
            if (orderToUpdate != null)
            {
                orderToUpdate.Date = item.Date;
                orderToUpdate.MenuItems = item.MenuItems;

                return true;
            }
            return false;
        }
        public bool DeleteItem(int id)
        {
            Order order = GetItemById(id);
            if (order != null)
            {
                _dataProvider.Orders.Remove(order);

                return true;
            }
            return false;
        }
    }
}
