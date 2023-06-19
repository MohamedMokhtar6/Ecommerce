using Ecommerce.Models;

namespace Ecommerce.Dtos
{
    public class BrandDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? Poster { get; set; }
    }
}
