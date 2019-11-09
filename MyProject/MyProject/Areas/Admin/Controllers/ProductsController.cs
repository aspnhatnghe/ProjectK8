using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    }
}