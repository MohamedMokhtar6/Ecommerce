using E_Commerce.Core.Dtos;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly IBaseRepository<CartItem> _cartItemRepository;
        private readonly IBaseRepository<Product> _productRepository;
        public CartItemsController(IBaseRepository<Product> productRepository, IBaseRepository<CartItem> cartItemRepository)
        {
            _productRepository = productRepository;
            _cartItemRepository = cartItemRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllItemsAsync()
        {
            var items = await _cartItemRepository.GetAll();

            return Ok(items);
        }
        [HttpPost("Item")]
        public async Task<IActionResult> AddItemAsync([FromForm] CartItemDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productRepository.FindById(itemDto.ProductId);
            var cartItem = new CartItem
            {
                ProductId = itemDto.ProductId,
                UnitPrice = product.Price,
                Quantity = itemDto.Quantity,
                CartId = itemDto.CartId

            };
            await _cartItemRepository.Add(cartItem);
            return Ok(cartItem);
        }
    }
}
