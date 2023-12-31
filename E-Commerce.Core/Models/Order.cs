﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public string PhoneNumber { get; set; }
    }
}
