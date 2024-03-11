using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Implementations;
using API.Data.Interfaces;
using API.DTOs;
using API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Name = request.Name,
                UrlHandle = request.CategoryUrl
            };

            await _categoryRepository.CreateCategory(category);
            
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                CategoryUrl = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
           var categories = await _categoryRepository.GetAllCategories();

           var response = new List<CategoryDto>();
           foreach (var category in categories)
           {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    CategoryUrl = category.UrlHandle
                });
           }

           return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var category = await _categoryRepository.GetCategoryById(id);

            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                CategoryUrl = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCategoryById([FromRoute] Guid id, UpdateCategoryRequestDto request)
        {
            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.CategoryUrl
            };

            category = await _categoryRepository.UpdateCategoryById(category);

            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                CategoryUrl = category.UrlHandle
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid id)
        {
            var category = await _categoryRepository.DeleteCategoryById(id);

            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                CategoryUrl = category.UrlHandle
            };

            return Ok(response);
        }
    }
}