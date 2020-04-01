using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WheyMen.Domain.Model
{
    public partial class Loc
    {
        public Loc()
        {
            Inventory = new HashSet<Inventory>();
            Order = new HashSet<Order>();
        }
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Location name
        /// </summary>
        [Display(Name="Location Name")]
        [MaxLength(50,ErrorMessage ="Max name length is 50")]
        public string Name { get; set; }

        /// <summary>
        /// collection of products and their quantities which make up stores inventory
        /// </summary>
        public virtual ICollection<Inventory> Inventory { get; set; }
        /// <summary>
        /// Orders placed on this location
        /// </summary>
        public virtual ICollection<Order> Order { get; set; }
    }
}
