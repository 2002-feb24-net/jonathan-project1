using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WheyMenDAL.Library.Model
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

        [Display(Name = "Store ID")]
        [Range(0,int.MaxValue)]
        public int StoreId { get; set; }

        [Display(Name = "Quantity")]
        [Range(0, int.MaxValue)]
        public int Qty { get; set; }

        [Display(Name = "Product ID")]
        [Range(0, int.MaxValue)]
        public int Pid { get; set; }

        public virtual Products P { get; set; }
        public virtual Loc Store { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
