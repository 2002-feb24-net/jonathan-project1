using System;
using System.Collections.Generic;

namespace WheyMenDAL.Library.Model
{
    public partial class Inventory
    {
        public Inventory()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public int Qty { get; set; }
        public int Pid { get; set; }

        public virtual Products P { get; set; }
        public virtual Loc Store { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
