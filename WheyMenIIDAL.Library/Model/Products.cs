using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WheyMenDAL.Library.Model
{
    public partial class Products
    {
        public Products()
        {
            Inventory = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
