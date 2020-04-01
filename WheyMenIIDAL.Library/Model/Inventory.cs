using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WheyMen.Domain.Model
{
    public partial class Inventory
    {
        public Inventory()
        {
            OrderItem = new HashSet<OrderItem>();
        }
        [Key]
        [Display(Name = "Inventory Item ID")]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        /// <summary>
        /// Store that the inventory row belongs to
        /// </summary>
        [Display(Name = "Store ID")]
        [Range(0,int.MaxValue)]
        public int StoreId { get; set; }

        /// <summary>
        /// Quantity of the specified item in stock
        /// </summary>
        [Display(Name = "Quantity")]
        [Range(0, int.MaxValue)]
        public int Qty { get; set; }

        /// <summary>
        /// Id of product in inventory
        /// </summary>
        [Display(Name = "Product ID")]
        [Range(0, int.MaxValue)]
        public int Pid { get; set; }

        /// <summary>
        /// Navigation property to associated product
        /// </summary>
        public virtual Products P { get; set; }
        /// <summary>
        /// Navigation property to store holding the inventory
        /// </summary>
        public virtual Loc Store { get; set; }
        /// <summary>
        /// order items which refer to this inventory item
        /// </summary>
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
