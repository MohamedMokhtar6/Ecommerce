using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Dtos
{
    public class OrderDto
    {
        public string UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string OrderStatus { get; set; }
        //public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public string PhoneNumber { get; set; }

    }
}
