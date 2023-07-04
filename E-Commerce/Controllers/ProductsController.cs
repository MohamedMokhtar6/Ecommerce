using E_Commerce.Core.Dtos;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Models;
using E_Commerce.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private new List<string> _allowesExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 2097152;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           

        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync( int? pageresult, int? pageNumber)
        {
            var prodNumber = await _unitOfWork.Product.count();
            int? pageCount = pageresult.HasValue ? Convert.ToInt32(Math.Ceiling((decimal)prodNumber / (decimal)pageresult)) : null;
            var product = await _unitOfWork.Product.GetAllByQuery((pageNumber - 1) * (int?)pageresult
               , (int?)pageresult, new[] { "Brand", "Category" }, p => p.Id);
            return Ok(product);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Product.FindAllByQuery(p => p.Id == id, new[] { "Category", "Brand" });
            if (product.Count() == 0)
                return NotFound();
            return Ok(product);
        }
        [HttpGet("GetByCategoryId")]
        public async Task<IActionResult> GetByCategoryId(int CategoryId)
        {
            var product = await _unitOfWork.Product.FindAllByQuery(p => p.CategoryId == CategoryId, new[] { "Category", "Brand" });
            if (product.Count() == 0)
                return NotFound();
            return Ok(product);
        }
        [HttpGet("GetByBrandId")]
        public async Task<IActionResult> GetByBrandId(int BrandId)
        {
            var product = await _unitOfWork.Product.FindAllByQuery(p => p.BrandId == BrandId, new[] { "Category", "Brand" });
            if (product.Count() == 0)
                return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] ProductDto dto)
        {
            if (dto.Poster == null)
            {
                return BadRequest("poster is required");
            }
            if (!_allowesExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max Allowed images size 2mb");

            var isVaildCaategory = await _unitOfWork.Category.FindById(dto.CategoryId);
            if (isVaildCaategory == null)
                return BadRequest("Wrong Category Id");
            var isVaildBrand = await _unitOfWork.Brand.FindById(dto.BrandId);
            if (isVaildBrand == null)
                return BadRequest("Wrong brand Id");

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
                UpdateDate = DateTime.Now,
                BrandId = dto.BrandId,
            };
            await _unitOfWork.Product.Add(product);
            return Ok(product);

        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Product.FindById(id);
            if (product == null)
                return BadRequest("No product id was found");
            await _unitOfWork.Product.Delet(product);
            return Ok(product);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] ProductDto dto)
        {

            var product = await _unitOfWork.Product.FindById(id);
            if (product == null)
            {
                return NotFound($"no product was found with id ={id}");
            }
            if (dto.Poster != null)
            {
                if (!_allowesExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max Allowed images size 2MB");

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
                product.Poster = dataStream.ToArray();
            }

            var isVaildCaategory = await _unitOfWork.Category.FindById(dto.CategoryId);
            if (isVaildCaategory == null)
                return BadRequest("Wrong Category Id");
            var brand = await _unitOfWork.Brand.FindById(dto.BrandId);
            if (brand == null)
                return BadRequest("Wrong brand Id");
            product.Price = dto.Price;
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Rate = dto.Rate;
            product.Quntity = dto.Quntity;
            product.CategoryId = dto.CategoryId;
            product.UpdateDate = DateTime.Now;
            product.BrandId = dto.BrandId;

            _unitOfWork.Product.Update(product);
            return Ok(product);
        }
        [HttpGet("search")]
        public async Task<IActionResult> search(string productName)
        {
            if (productName == null) return BadRequest("no search query");
            var products = await _unitOfWork.Product.FindAllByQuery(p => p.Name.Contains(productName) || p.Description.Contains(productName)
            , new[] { "Category", "Brand" }, p => p.Name);
            if (products.Count() == 0) return NotFound();
            return Ok(products);
        }


    }
}