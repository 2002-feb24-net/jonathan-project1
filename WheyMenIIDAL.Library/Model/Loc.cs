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

        public int Id { get; set; }
        [Display(Name="Location Name")]
        [MaxLength(50,ErrorMessage ="Max name length is 50")]
        public string Name { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
