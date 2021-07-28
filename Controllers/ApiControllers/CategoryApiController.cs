using Forum.Entities;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin, Moderator")]
    

    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryApiController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            var categories = _categoryService.Get();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<Category> GetById([FromRoute] int id)
        {
            var category = _categoryService.GetById(id);
            return Ok(category);
        }

        [HttpPost]
        public ActionResult CreateCategory([FromBody] CreateOrUpdateCategoryDto dto)
        {
            var categoryId = _categoryService.Create(dto);
            return Created($"/api/category/{categoryId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory([FromRoute] int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory([FromBody] CreateOrUpdateCategoryDto dto, [FromRoute] int id)
        {
            _categoryService.Update(dto, id);
            return Ok();
        }
    }
}