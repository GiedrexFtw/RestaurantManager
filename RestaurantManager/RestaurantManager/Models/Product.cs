using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Models
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PortionCount { get; set; }
        public string Unit { get; set; }
        public double PortionSize { get; set; }

        public override string ToString()
        {
            return String.Format("| {0, -3} | {1, -25} | {2, -5} | {3, -10} | {4, -4} |",
                this.Id, this.Name, this.PortionCount, this.Unit, this.PortionSize ) + $"\n {new string('-', 50)}";
        }
    }
}
