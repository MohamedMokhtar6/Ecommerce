using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Dtos;
using Ecommerce.Migrations;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private new List<string> _allowesExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 2097152;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var product = await _context.products.Include(p=>p.Category).ToListAsync();
            return Ok(product);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _context.products.Include(p => p.Category).SingleOrDefaultAsync(p=>p.Id==id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpGet("GetByGenraId")]
        public async Task<IActionResult> GetByGenraId(int CategoryId)
        {
            var product = await _context.products.Where(p=>p.CategoryId==CategoryId).Include(p => p.Category).ToListAsync();
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] ProductDto dto)
        {
            if (dto.Poster == null)
            {
                return BadRequest("poster is reqouird");
            }
            if (!_allowesExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max Allowed images size 2mb");

            var isVaildCaategory = await _context.categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!isVaildCaategory)
                return BadRequest("Wrong Category Id");
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var product = new Product
            {
                Price = dto.Price,
                Quntity = dto.Quntity,
                Description = dto.Description,
                Rate = dto.Rate,
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                Poster = dataStream.ToArray(),
            };
            await _context.AddAsync(product);
            _context.SaveChanges();
            return Ok(product);

        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] ProductDto dto)
        {

            var product = await _context.products.SingleOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound($"no product was found with id ={id}");
            }
            if(dto.Poster != null)
            {
                if (!_allowesExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max Allowed images size 2MB");

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                product.Poster = dataStream.ToArray();
            }

            var isVaildCaategory = await _context.categories.AnyAsync(c => c.Id == dto.CategoryId);
            if (!isVaildCaategory)
                return BadRequest("Wrong Caategory Id");
            product.Price = dto.Price;
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Rate = dto.Rate;
            product.Quntity = dto.Quntity;
            product.CategoryId = dto.CategoryId;

            _context.Update(product);
            _context.SaveChanges();
            return Ok(product);
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product = await _context.products.FindAsync(id);
            if (product == null)
                return BadRequest("No product id was found");
            _context.products.Remove(product);
            _context.SaveChanges();
            return Ok(product);
        }
    }
}
