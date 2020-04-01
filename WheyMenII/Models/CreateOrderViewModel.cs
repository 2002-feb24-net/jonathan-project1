using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WheyMen.Domain.Model;

namespace WheyMenII.UI.Models
{
    public class CreateOrderViewModel
    {
        public List<Inventory> Inventory { get; set; }
        public OrderItem Item { get; set; }
    }
}
