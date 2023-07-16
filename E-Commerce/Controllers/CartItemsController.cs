using E_Commerce.Core.Dtos;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Models;
using E_Commerce.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllItemsAsync()
        {
            var items = await _unitOfWork.CartItem.GetAllByQuery( new[] { "Product" });

            return Ok(items);
        }
        [HttpGet("cartId")]
        public async Task<IActionResult> GetAllItemsAsync(Guid cartId)
        {
            var items = await _unitOfWork.CartItem.FindAllByQuery(i=>i.CartId==cartId, new[] { "Product" });

            return Ok(items);
        }
        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromForm] CartItemDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _unitOfWork.Product.FindById(itemDto.ProductId);
            var cartItem = new CartItem
            {
                ProductId = itemDto.ProductId,
                UnitPrice = product.Price,
                Quantity = itemDto.Quantity,
                CartId = itemDto.CartId
                
                

            };
            await _unitOfWork.CartItem.Add(cartItem);
            return Ok(cartItem);
        }
        [HttpPut]
        public async Task<IActionResult>UpdateQuantityAsync(int cartId,int quantity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await _unitOfWork.CartItem.FindById(cartId);
            if (item == null)
            {
                return NotFound("no Item Was Found");
            }
            item.Quantity = quantity;
            _unitOfWork.CartItem.Update(item);
            return Ok(item);
        }

        [HttpDelete("EmptyCart")]
        public async Task<IActionResult> EmptyCartAsync(Guid cartId)
        {
            var cartItems = await _unitOfWork.CartItem.FindAllByQuery(c => c.CartId == cartId);

            await _unitOfWork.CartItem.DeleteAll(cartItems);

            return Ok("Cart Is Empty Now!!");
        }  
        [HttpDelete]
        public async Task<IActionResult> DeleteCartAsync(int id)
        {
            var cartItem = await _unitOfWork.CartItem.FindById(id);
            if(cartItem == null)
            {
                return NotFound("Wrong cart Item Id");
            }

            await _unitOfWork.CartItem.Delet(cartItem);
            
            return Ok("Item Deleted!");
        }
    }
}
