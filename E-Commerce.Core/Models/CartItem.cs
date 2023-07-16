using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models
{
    public class CartItem
    {
        public int Id   { get; set; }
        public Guid CartId  { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        [JsonIgnore]
        public Cart Cart { get; set; }

    }
}
