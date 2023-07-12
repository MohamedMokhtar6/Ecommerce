using E_Commerce.Core.Dtos;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(Guid cartId, OrderInfoDto info)
        {
            var cartItem = await _unitOfWork.CartItem.FindAllByQuery(i => i.CartId == cartId);
            var user = await _unitOfWork.Users.FindById(info.UserId);
            if (user == null)
                return BadRequest("user not found");
            if (cartItem == null)
            {
                return NotFound("no item found");
            }
            var order = new Order
            {
                Address = info.Address,
                City = info.City,
                UserId = info.UserId,
                PhoneNumber = info.PhoneNumber,
            };
            var res = await _unitOfWork.Order.Add(order);
            if(res == null)
                return BadRequest(res);
            List<OrderItem> orderList = new List<OrderItem>();
            foreach (var item in cartItem)
            {
                var x = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    OrderId= res.Id,
                    Total= item.UnitPrice*item.Quantity,
                };
                orderList.Add(x);
            }
            var orderItems = await _unitOfWork.OrderItem.AddRange(orderList);
            await _unitOfWork.CartItem.DeleteAll(cartItem);
            var cart =await _unitOfWork.Cart.FindById(cartId);
            if (cart == null)
                return NotFound("cart not found");
            await _unitOfWork.Cart.Delet(cart);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Deletorder(Guid orderId)
        {
            var order = await _unitOfWork.Order.FindById(orderId);
            if (order == null) return BadRequest("order not found");
            var orderItem= await _unitOfWork.OrderItem.FindAllByQuery(i=>i.OrderId==orderId);
            await _unitOfWork.OrderItem.DeleteAll(orderItem);
            await _unitOfWork.Order.Delet(order);
            return Ok(order);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetOrderbyId(Guid orderId)
        {
            var order = await _unitOfWork.Order.FindById(orderId);
            if (order == null) return NotFound("order not found");
            return Ok(order);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var order = await _unitOfWork.Order.GetAllByQuery(new[] { "OrderItem" });
            if (order == null) return NotFound("order not found");
            return Ok(order);
        }

    }
}
