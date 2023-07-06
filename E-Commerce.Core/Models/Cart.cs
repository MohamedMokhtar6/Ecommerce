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
        public string UserId { get; set; }
        public string CreatedDate{ get; set; }= DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");

        public bool IsActive { get; set; }= true;
        public List<CartItem> Items { get; set; } = new List<CartItem>();  
    }
}
