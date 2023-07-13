using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Dtos
{
    public class OrderInfoDto
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }

    }
}
