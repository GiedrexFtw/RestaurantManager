using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RestaurantManager.Models
{
    class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public List<MenuItem> MenuItems { get; set; }

        public override string ToString()
        {
            return String.Format("| {0, -3} | {1, -25} | {2} |",
                this.Id, this.Date, String.Join<int>(' ', MenuItems.Select(x => x.Id))) + $"\n {new string('-', 50)}";
        }
    }
}
