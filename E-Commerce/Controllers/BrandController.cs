using E_Commerce.Core.Dtos;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _UnitOfWork;
        public BrandController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand([FromForm] BrandDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (dto.Poster == null)
                return BadRequest("poster is required");
            var category = await _UnitOfWork.Category.FindById(dto.CategoryId);
            if (category == null)
                return BadRequest("category not found");
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var brand = new Brand
            {
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                Poster = dataStream.ToArray(),
                UpdateDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")
            };
            await _UnitOfWork.Brand.Add(brand);
            return Ok(brand);
        }
        [HttpDelete]
        public async Task<IActionResult> DeletBrand(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var brand = await _UnitOfWork.Brand.FindById(id);
            if (brand == null)
                return NotFound("brand not found");
            await _UnitOfWork.Brand.Delet(brand);
            return Ok(brand);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _UnitOfWork.Brand.FindByQuery(b => b.Id == id, new[] { "Category" });
            if (brand == null)
                return NotFound("brand not found");
            return Ok(brand);
        }
        [HttpGet("categoryId")]
        public async Task<IActionResult> GetByCatId(int id)
        {
            var brand = await _UnitOfWork.Brand.FindAllByQuery(b => b.CategoryId == id, new[] { "Category" });
            if (brand == null)
                return NotFound("brand not found");
            return Ok(brand);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _UnitOfWork.Brand.GetAllByQuery(new[] { "Category" }));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] BrandDto dto)
        {
            var brand = await _UnitOfWork.Brand.FindById(id);
            if (brand == null)
            {
                return NotFound($"no brand was found with id ={id}");
            }
            if (dto.Poster == null)
            {
                return BadRequest("poster is required");
            }
            var isVaildCaategory = await _UnitOfWork.Category.FindById(dto.CategoryId);
            if (isVaildCaategory == null)
                return BadRequest("Category not found");
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            brand.Name = dto.Name;
            brand.CategoryId = dto.CategoryId;
            brand.Poster = dataStream.ToArray();
            brand.UpdateDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");

            _UnitOfWork.Brand.Update(brand);
            return Ok(brand);
        }

    }
}