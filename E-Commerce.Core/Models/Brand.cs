namespace E_Commerce.Core.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Poster { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }
        public Category Category { get; set; }
        //public List<Product> products { get; set; }

    }
}
