using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        public int Quntity { get; set; }
        public IFormFile? Poster { get; set; }
    }
}
