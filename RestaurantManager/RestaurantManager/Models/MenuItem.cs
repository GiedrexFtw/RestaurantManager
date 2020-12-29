using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantManager.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }


        public override string ToString()
        {
            return String.Format("| {0, -3} | {1, -25} | {2} |",
                this.Id, this.Name, String.Join<int>(' ', Products.Select(x => x.Id))) + $"\n {new string('-', 50)}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            MenuItem menuitem = obj as MenuItem;
            if (menuitem != null)
            {
                return this.Id == menuitem.Id && this.Name == menuitem.Name && this.Products.SequenceEqual(menuitem.Products);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Products);
        }
    }
}
