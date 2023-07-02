using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rate { get; set; }
        public int Quntity { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }
        public byte[] Poster { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
