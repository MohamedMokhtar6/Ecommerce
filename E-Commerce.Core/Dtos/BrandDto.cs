
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Core.Dtos
{
    public class BrandDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? Poster { get; set; }
    }
}
