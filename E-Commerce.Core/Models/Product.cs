using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Rate { get; set; }
        public int Quntity { get; set; }
        public string CreateDate { get; set; } = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
        public string UpdateDate { get; set; } 
        [MaxLength(2500)]
        public string Description { get; set; }
        public byte[] Poster { get; set; }
        public int CategoryId { get; set; }
        public Category Category{ get; set; }
        public int? BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}
