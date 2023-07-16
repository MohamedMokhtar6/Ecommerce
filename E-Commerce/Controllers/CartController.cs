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
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
     
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var carts=await _unitOfWork.Cart.GetAllByQuery(new[] { "Items" });     
            return Ok(carts);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetbByUser(string userId)
        {
            var user = await _unitOfWork.Users.FindById(userId);
            if (user == null)
            {
                return NotFound("user not found");
            }
            var carts = await _unitOfWork.Cart.FindByQuery(c=>c.UserId==userId,new[] { "Items" });
            return Ok(carts);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CartDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cart=new Cart { UserId = dto.UserId };
            await _unitOfWork.Cart.Add(cart);
            return Ok(cart);
        }


    }
}
