using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }= DateTime.Now;

        public bool IsActive { get; set; }= true;
        public List<CartItem> Items { get; set; } = new List<CartItem>();  
    }
}
