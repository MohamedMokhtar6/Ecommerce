namespace E_Commerce.Core.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Poster { get; set; }
        public int CategoryId { get; set; }
        public string CreateDate { get; set; } = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
        public string UpdateDate { get; set; }
        public Category Category { get; set; }
        //public List<Product> products { get; set; }

    }
}
