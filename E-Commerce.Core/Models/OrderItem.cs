using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId  { get; set; }
        public double UnitPrice   { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; } 
        public Guid OrderId { get; set; } 
        public Order Order { get; set; }
    }
}
