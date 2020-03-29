using System;
using System.Collections.Generic;

namespace WheyMenDAL.Library.Model
{
    public partial class Loc
    {
        public Loc()
        {
            Inventory = new HashSet<Inventory>();
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
