using AutoMapper;
using Forum.Entities;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Authorize(Roles = "Admin, Moderator")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var categories = _categoryService.Get();
            return View(categories);
        }
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            var category = _categoryService.GetById(id);
            return View(category);  
        }

        
        public ActionResult Create(CreateOrUpdateCategoryDto dto)
        {
            if(ModelState.IsValid)
            {     
                _categoryService.Create(dto) ;
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int? id)
        {
            var category = _categoryService.GetById(id);
            return View(category);
        }

        public ActionResult EditSave(Category category, int? id)
        {
            _categoryService.Update(category, id);          
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);
            return View(category);
        }

        public ActionResult DeleteConfirmed(int id)
        {
            var category = _categoryService.GetById(id);
            _categoryService.DeleteConfirm(category);
            return RedirectToAction("Index");
        }    
    }
}
