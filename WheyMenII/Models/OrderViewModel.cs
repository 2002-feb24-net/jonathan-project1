using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WheyMen.Domain.Model;

namespace WheyMenII.UI.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        [Required(ErrorMessage = "Username is required")]
        [MaxLength(30, ErrorMessage = "Maximum username length is 30")]
        [MinLength(3, ErrorMessage = "Minimum username length is 3")]
        public string Username { get; set; }

        /// <summary>
        /// Password, between 3 and 100 characters
        /// </summary>
        [Display(Name = "Password")]
        [MinLength(3, ErrorMessage = "Minium password length is 3")]
        [MaxLength(100, ErrorMessage = "Maximum password length is 100")]
        [ScaffoldColumn(false)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Pwd { get; set; }

        public int Id { get; set; }

        /// <summary>
        /// Customer which placed the order
        /// </summary>
        [Required(ErrorMessage = "Customer name is required")]
        public int CustId { get; set; }

        /// <summary>
        /// Location order was placed to 
        /// </summary>
        [Required(ErrorMessage = "Location name is required")]
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
        /// nav prop to ordered from location
        /// </summary>
        public virtual Loc Loc { get; set; }
        /// <summary>
        /// Navigation property to ordering customer
        /// </summary>
        public virtual Customer Cust { get; set; }

        /// <summary>
        /// Items belonging to this order
        /// </summary>
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
