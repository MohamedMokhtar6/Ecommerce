using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Dtos;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorysController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategorysController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _context.categories.OrderBy(g => g.Name).ToListAsync();
            return Ok(categories);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetAllByIdAsync(int id)
        {
            var category = await _context.categories.SingleOrDefaultAsync(c => c.Id == id);
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGenraAsync(CategoryDto dto)
        {
            var category = new Category 
            {
                Name = dto.Name,
                UpdateDate = DateTime.Now
        };
            await _context.categories.AddAsync(category);
            _context.SaveChanges();
            return Ok(category);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] CategoryDto dto)
        {
            var category=await _context.categories.SingleOrDefaultAsync(g=>g.Id==id);
            if (category == null)
            {
                return NotFound($"no Category was found with id ={id}");
            }
            category.Name = dto.Name;
            category.UpdateDate = DateTime.Now;
            _context.SaveChanges();
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAsync(int id)
        {
            var category=await _context.categories.SingleOrDefaultAsync(gen=>gen.Id==id);
            if(category == null)
            {
                return NotFound($"No Category was found with id:{id}");
            }
            _context.categories.Remove(category);
            _context.SaveChanges();
            return Ok(category);
        }

    }
}
