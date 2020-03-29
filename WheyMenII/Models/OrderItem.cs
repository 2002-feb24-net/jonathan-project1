using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WheyMenDAL.Library.Model
{
    public partial class OrderItem
    {
        public int Oid { get; set; }
        public int Qty { get; set; }
        public int Id { get; set; }
        [ForeignKey("pid")]
        public int? Pid { get; set; }

        public virtual Order O { get; set; }
        public virtual Inventory P { get; set; }
    }
}
