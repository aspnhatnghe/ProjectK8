using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using MyProject.Helpers;

namespace MyProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ProductsController : Controller
    {
        private readonly IProductBo _productBo;
        private readonly ICategoryBo _categoryBo;
        private readonly ISupplierBo _supplierBo;
        public ProductsController(IProductBo productBo, ICategoryBo categoryBo, ISupplierBo supplierBo)
        {
            _productBo = productBo;
            _categoryBo = categoryBo;
            _supplierBo = supplierBo;
        }
        public IActionResult Index()
        {
            var data = _productBo.GetAll();                        
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(_categoryBo.GetAll(), "CategoryId", "CategoryName");
            ViewBag.Supplier = new SelectList(_supplierBo.GetAll(), "SupplierId", "SupplierName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel model, IFormFile fHinh)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("loi", "Còn lỗi");
                return View();
            }
            var fileName = MyTools.UploadFile(fHinh, "products");
            model.Image = fileName;

            var productCreated = _productBo.Insert(model);
            return RedirectToAction("Edit", new { id = productCreated.ProductId});
        }

        public IActionResult Edit(int id)
        {
            var product = _productBo.GetById(id);
            //var pro = _productBo.GetFirstWithInclude(p => p.ProductId == id, p => p.Category, p => p.Supplier, p =>p.OrderDetails);

            ViewBag.Category = new SelectList(_categoryBo.GetAll(), "CategoryId", "CategoryName");
            ViewBag.Supplier = new SelectList(_supplierBo.GetAll(), "SupplierId", "SupplierName");

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProductModel model, IFormFile fHinh)
        {
            if(id != model.ProductId)
            {
                return View();
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("loi", "Còn lỗi");
                return View();
            }
            var fileName = MyTools.UploadFile(fHinh, "products");
            if (!string.IsNullOrEmpty(fileName))
            {
                model.Image = fileName;
            }

            var productCreated = _productBo.Update(model, id);
            return RedirectToAction("Edit", new { id = productCreated.ProductId });
        }
    }
}