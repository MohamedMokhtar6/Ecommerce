using Ecommerce.Data;
using Ecommerce.Dtos;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BrandController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var brands = await _context.brands.OrderBy(b => b.Name).Include(b => b.Category).ToListAsync();
            return Ok(brands);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetAllByIdAsync(int id)
        {
            var brands = await _context.brands.Include(b => b.Category).SingleOrDefaultAsync(b=>b.Id==id);
            return Ok(brands);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGenraAsync([FromForm] BrandDto dto)
        {
            if (dto.Poster == null)
            {
                return BadRequest("poster is reqouird");
            }
            var isVaildCaategory = await _context.categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!isVaildCaategory)
                return BadRequest("Wrong Category Id");
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var brand = new Brand 
            {
                Name = dto.Name ,
                CategoryId = dto.CategoryId ,
                Poster = dataStream.ToArray(),
                UpdateDate = DateTime.Now,
            };
            await _context.brands.AddAsync(brand);
            _context.SaveChanges();
            return Ok(brand);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] BrandDto dto)
        {
            var brand = await _context.brands.SingleOrDefaultAsync(b => b.Id == id);
            if (brand == null)
            {
                return NotFound($"no brand was found with id ={id}");
            }
            if (dto.Poster == null)
            {
                return BadRequest("poster is reqouird");
            }
            var isVaildCaategory = await _context.categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!isVaildCaategory)
                return BadRequest("Wrong Category Id");
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            brand.Name = dto.Name;
            brand.CategoryId = dto.CategoryId;
            brand.Poster = dataStream.ToArray();
            brand.UpdateDate = DateTime.Now;

            _context.SaveChanges();
            return Ok(brand);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var brand = await _context.brands.SingleOrDefaultAsync(b => b.Id == id);
            if (brand == null)
            {
                return NotFound($"No brand was found with id:{id}");
            }
            _context.brands.Remove(brand);
            _context.SaveChanges();
            return Ok(brand);
        }
    }
}
