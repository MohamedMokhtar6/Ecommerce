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
    public class CategorysController : ControllerBase
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        public CategorysController(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDto dto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var category = new Category
            {
                Name = dto.Name,
                UpdateDate = DateTime.Now
            };
            await _categoryRepository.Add(category);
            return Ok(category);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletCategory(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await _categoryRepository.FindById(id);
            if (item == null)
                return NotFound();
            await _categoryRepository.Delet(item);
            return Ok(item);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await _categoryRepository.FindById(id);
            if(item == null)
                return NotFound();
            return Ok(item);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryRepository.GetAll());
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var item = await _categoryRepository.FindById(id);
            if (item == null) {
                return NotFound();
            }
            item.Name= category.Name;
            item.UpdateDate = DateTime.Now;
            _categoryRepository.Update(item);
            return Ok(item);

        }

    }
}
