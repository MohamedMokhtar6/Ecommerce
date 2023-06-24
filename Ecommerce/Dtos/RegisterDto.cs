using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos
{
    public class RegisterDto
    {
        [Required,StringLength(50)]
        public string User_Name { get; set; }
        [Required,StringLength(50)]
        public string Password { get; set; }

        [Required,StringLength(100)]
        public string Email { get; set; }
    }
}
