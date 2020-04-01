using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WheyMen.Domain.Model
{
    /// <summary>
    /// Products that can be part of a stores inventory
    /// </summary>
    public partial class Products
    {
        public Products()
        {
            Inventory = new HashSet<Inventory>();
        }

        /// <summary>
        /// primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        /// <summary>
        /// Unit price of product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Stores this item can be found at
        /// </summary>
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
