using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductBo _productBo;
        public ProductController(IProductBo productBo)
        {
            _productBo = productBo;
        }
        public IActionResult Index(string categoryId, int? supplierId, string keyword)
        {
            var data = _productBo.Search(categoryId, supplierId, keyword);
            return View(data);
        }
    }
}