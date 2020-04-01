using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WheyMen.Domain.Model
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        
        /// <summary>
        /// Customer which placed the order
        /// </summary>
        [Required(ErrorMessage ="Customer name is required")]
        public int CustId { get; set; }

        /// <summary>
        /// Location order was placed to 
        /// </summary>
        [Required(ErrorMessage ="Location name is required")]
        public int LocId { get; set; }

        /// <summary>
        /// Total value of the order
        /// </summary>
        public decimal? Total { get; set; }
        
        /// <summary>
        /// Time order wwas created client side
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Navigation property to ordering customer
        /// </summary>
        public virtual Customer Cust { get; set; }

        /// <summary>
        /// nav prop to ordered from location
        /// </summary>
        public virtual Loc Loc { get; set; }

        /// <summary>
        /// Items belonging to this order
        /// </summary>
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
