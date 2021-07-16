using Forum.Entities;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            var categories = _categoryService.Get();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetById([FromRoute] int id)
        {
            var category = _categoryService.GetById(id);
            return Ok(category);
        }

        [HttpPost]
        public ActionResult CreateCategory([FromBody] CreateCategoryDto dto)
        {
            var categoryId = _categoryService.Create(dto);
            return Created($"/api/category/{categoryId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory([FromRoute] int id)
        {
            _categoryService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory([FromBody] CreateCategoryDto dto, [FromRoute] int id)
        {
            _categoryService.Update(dto, id);
            return Ok();
        }
    }
}