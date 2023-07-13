using E_Commerce.Core.Dtos;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Models;
using E_Commerce.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private class UserNameResponse
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string userName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAll();
            var result = users.Select(u => new UserNameResponse {Id=u.Id, FirstName = u.FirstName, LastName = u.LastName, userName=u.UserName, Email=u.Email ,PhoneNumber=u.PhoneNumber }).OrderBy(u=>u.FirstName).ToList();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _unitOfWork.Users.FindById(id);
            if (user == null)
                return NotFound("user not found");
            return Ok(user);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletAsync(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _unitOfWork.Users.FindById(id);
            if (user == null)
                return NotFound("user not found");
            await _unitOfWork.Users.Delet(user);
            return Ok(user);
        }
    }
}
