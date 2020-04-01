using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WheyMen.Domain.Model
{
    public partial class OrderItem
    {
        public int Oid { get; set; }
        [Display(Name="Quantity")]
        public int Qty { get; set; }
        public int Id { get; set; }
        [ForeignKey("pid")]
        public int Pid { get; set; }

        public virtual Order O { get; set; }
        public virtual Inventory P { get; set; }

        /// <summary>
        /// Checks if request is for gte 50% of the available quantity or lt 100 and lt available qty
        /// </summary>
        /// <param name="item">object containing requested quantity</param>
        /// <param name="qty">available item quantity</param>
        /// <returns></returns>
        public bool ValidateQuantity( int qty)
        {
            return this.Qty < qty * .5 || (this.Qty < 100 && this.Qty < qty);
        }
    }
}
