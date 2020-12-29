using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PortionCount { get; set; }
        public string Unit { get; set; }
        public double PortionSize { get; set; }

        public override string ToString()
        {
            return String.Format("| {0, -3} | {1, -25} | {2, -13} | {3, -10} | {4, -12} |",
                this.Id, this.Name, this.PortionCount, this.Unit, this.PortionSize ) + $"\n {new string('-', 77)}";
        }
        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            Product product = obj as Product;
            if(product != null)
            {
                return this.Id == product.Id && this.Name == product.Name && 
                    this.PortionCount == product.PortionCount &&
                    this.Unit == product.Unit && this.PortionSize == product.PortionSize;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, PortionCount, Unit, PortionSize);
        }
    }
}
