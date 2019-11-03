using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            return View(_categoryBo.GetAll());
        }
        public IActionResult Create()
        {
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
            return View();
        }
    }
}