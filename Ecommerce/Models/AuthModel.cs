namespace Ecommerce.Models
{
    public class AuthModel
    {
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public List<String> Roles { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
