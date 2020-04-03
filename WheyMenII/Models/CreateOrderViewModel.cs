using System.Collections.Generic;
using WheyMen.Domain.Model;

namespace WheyMenII.UI.Models
{
    public class CreateOrderViewModel
    {
        public List<Inventory> Inventory { get; set; }
        public OrderItem Item { get; set; }
    }
}
