using Ecommerce.Dtos;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDto model);
    }
}
