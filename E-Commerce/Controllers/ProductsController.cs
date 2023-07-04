﻿using E_Commerce.Core.Dtos;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Models;
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
        private new List<string> _allowesExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 2097152;

        private readonly IBaseRepository<Product> _productRepository;
        private readonly IBaseRepository<Category> _CategoryRepository;
        private readonly IBaseRepository<Brand> _brandRepository;
        public ProductsController(IBaseRepository<Product> productRepository , 
            IBaseRepository<Category> CategoryRepository, IBaseRepository<Brand> brandRepository)
        {
            _productRepository = productRepository;
            _CategoryRepository = CategoryRepository;
            _brandRepository = brandRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var product = await _productRepository.GetAllByQuery(new[] { "Brand", "Category" }, p => p.UpdateDate);
            return Ok(product);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var product = await _productRepository.FindAllByQuery(p=>p.Id==id , new[] { "Category", "Brand" });
            if (product.Count() == 0)
                return NotFound();
            return Ok(product);
        }
        [HttpGet("GetByCategoryId")]
        public async Task<IActionResult> GetByCategoryId(int CategoryId)
        {
            var product = await _productRepository.FindAllByQuery(p => p.CategoryId == CategoryId, new[] { "Category", "Brand" });
            if (product.Count() == 0)
                return NotFound();
            return Ok(product);
        }
        [HttpGet("GetByBrandId")]
        public async Task<IActionResult> GetByBrandId(int BrandId)
        {
            var product = await _productRepository.FindAllByQuery(p => p.BrandId == BrandId, new[] { "Category", "Brand" });
            if (product.Count()==0)
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

            var isVaildCaategory = await _CategoryRepository.FindById(dto.CategoryId);
            if (isVaildCaategory ==null)
                return BadRequest("Wrong Category Id");
            var isVaildBrand = await _brandRepository.FindById(dto.BrandId);
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
            await _productRepository.Add(product);
            return Ok(product);

        }
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.FindById(id);
            if (product == null)
                return BadRequest("No product id was found");
            await _productRepository.Delet(product);
            return Ok(product);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] ProductDto dto)
        {

            var product = await _productRepository.FindById(id);
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

            var isVaildCaategory = await _CategoryRepository.FindById(dto.CategoryId);
            if (isVaildCaategory==null)
                return BadRequest("Wrong Category Id");
            var brand = await _brandRepository.FindById(dto.BrandId);
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

            _productRepository.Update(product);
            return Ok(product);
        }

    }
}
