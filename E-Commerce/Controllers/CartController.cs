using E_Commerce.Core.Dtos;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IBaseRepository<Cart> _cartRepository;
   
        public CartController(IBaseRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
     
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var carts=await _cartRepository.GetAllByQuery(new[] { "Items" });
     
            return Ok(carts);
        } 
       
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] CartDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cart=new Cart { UserId = dto.UserId };
            await _cartRepository.Add(cart);
            return Ok(cart);
        }


    }
}
