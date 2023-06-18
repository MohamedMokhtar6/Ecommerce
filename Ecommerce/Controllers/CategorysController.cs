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
            var genres = await _context.categories.OrderBy(g => g.Name).ToListAsync();
            return Ok(genres);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGenraAsync(CategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
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
                return NotFound($"no genra was found with id ={id}");
            }
            category.Name = dto.Name;
            _context.SaveChanges();
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteAsync(int id)
        {
            var category=await _context.categories.SingleOrDefaultAsync(gen=>gen.Id==id);
            if(category == null)
            {
                return NotFound($"No genra was found with id:{id}");
            }
            _context.categories.Remove(category);
            _context.SaveChanges();
            return Ok(category);
        }

    }
}
