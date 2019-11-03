using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using OfficeOpenXml;

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

        public IActionResult ExportExcel()
        {
            //chuẩn bị dữ liệu
            var data = _categoryBo.GetAll();

            //trả về trình duyệt file <==> MemoryStream
            var stream = new MemoryStream();

            using (var excel = new ExcelPackage(stream))
            {
                var sheet = excel.Workbook.Worksheets.Add("CategoryData");

                //đổ data vào sheet
                //sheet.Cells["A1"]
                //sheet.Cells[rowIndex, colIndex].Value = "AAA";//index (row/col) begin 1
                sheet.Cells.LoadFromCollection(data, true);

                excel.Save();
            }

            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Category{DateTime.Now.ToLongTimeString()}.xlsx");
        }

        public IActionResult ImportExcel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ImportExcel(IFormFile myFile)
        {
            if(myFile != null)
            {
                List<CategoryModel> categories = new List<CategoryModel>();

                var stream = new MemoryStream();
                myFile.CopyTo(stream);

                using (var excel = new ExcelPackage(stream))
                {
                    //đúng template
                    var sheet = excel.Workbook.Worksheets[0];
                    int rowCount = sheet.Dimension.Rows;

                    //duyệt qua từng dòng để xử lý
                    for(int i = 2; i <= rowCount; i++)
                    {
                        try
                        {
                            categories.Add(new CategoryModel {
                                CategoryName = sheet.Cells[i, 2].Value.ToString(),
                                Description = sheet.Cells[i, 3].Value.ToString()
                            });
                        }
                        catch { }
                    }
                }

                foreach (var item in categories)
                {
                    var categoryAdded = _categoryBo.Insert(item);
                }
            }

            
            return View();
        }
    }
}