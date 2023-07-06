using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreateDate { get; set; } = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
        public string UpdateDate { get; set; }
        //public List<Brand> Brands { get; set; } = new List<Brand>();  
        //public List<Product> products { get; set; }

    }
}
