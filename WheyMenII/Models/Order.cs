using System;
using System.Collections.Generic;

namespace WheyMenDAL.Library.Model
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int CustId { get; set; }
        public int LocId { get; set; }
        public decimal? Total { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual Loc Loc { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
