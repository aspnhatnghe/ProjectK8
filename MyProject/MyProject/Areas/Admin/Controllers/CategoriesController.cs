﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace MyProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryBo _categoryBo;
        public CategoriesController(ICategoryBo categoryBo)
        {
            _categoryBo = categoryBo;
        }
        public IActionResult Index()
        {
            var data = _categoryBo.GetAll();
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.parentCategory = new SelectList(_categoryBo.GetAll(), "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _categoryBo.Insert(model);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {

                    ModelState.AddModelError("loi", "Có lỗi khi thực hiện.");
                    return View();
                }
            }
            return View();
        }

        public IActionResult Edit(string id)
        {
            var category = _categoryBo.GetById(id);
            if(category == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.parentCategory = new SelectList(_categoryBo.GetAll(), "CategoryId", "CategoryName", category.ParentCategoryId);

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(string id, CategoryModel model)
        {
            if(id != model.CategoryId)
            {
                ModelState.AddModelError("loi", "Sai thông tin");
                return View();
            }
            _categoryBo.Update(model, id);
            return RedirectToAction("Index");
        }
    }
}