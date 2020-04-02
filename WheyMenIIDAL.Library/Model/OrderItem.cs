using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace WheyMen.Domain.Model
{
    /// <summary>
    /// Order lines
    /// </summary>
    public partial class OrderItem
    {
        public int Id { get; set; }
        /// <summary>
        /// Order the item is associated to
        /// </summary>
        public int Oid { get; set; }

        /// <summary>
        /// Quantity purchased
        /// </summary>
        [Display(Name="Quantity")]
        public int Qty { get; set; }

       
        /// <summary>
        /// Product purchased
        /// </summary>
        [ForeignKey("pid")]
        public int Pid { get; set; }

        /// <summary>
        /// Nav prop to order
        /// </summary>
        public virtual Order O { get; set; }

        /// <summary>
        /// nav propr to inventory purchased from
        /// </summary>
        public virtual Inventory P { get; set; }

        /// <summary>
        /// Checks if request is for gte 50% of the available quantity or lt 100 and lt available qty
        /// </summary>
        /// <param name="item">object containing requested quantity</param>
        /// <param name="qty">available item quantity</param>
        /// <returns></returns>
        public bool ValidateQuantity( int qty)
        {
            Debug.WriteLine(this.Qty>0);
            return this.Qty >0 && (this.Qty < qty * .5 || (this.Qty < 100 && this.Qty < qty));
        }
    }
}
