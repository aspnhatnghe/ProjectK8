using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;

namespace MyProject.Areas.Admin.Controllers
{
    [Area("admin")]
    public class StatisticsController : Controller
    {
        private readonly MyDbContext _context;
        public StatisticsController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(DateTime? fromDate, DateTime? toDate)
        {
            if (!toDate.HasValue) toDate = DateTime.Now;
            if (!fromDate.HasValue) fromDate = toDate.Value.AddDays(-7);

            toDate = DateTime.Parse(toDate.Value.ToString("MM/dd/yyyy 00:00:00.000")).AddDays(1).AddMilliseconds(-1);// now() + 23:59:59.999
            fromDate = DateTime.Parse(fromDate.Value.ToString("MM/dd/yyyy 00:00:00.000"));

            //DateTime.UtcNow --> database save UTC time

            var data = _context.OrderDetails
                .Where(cthd => cthd.Order.OrderDate >= fromDate && cthd.Order.OrderDate <= toDate)
                .GroupBy(cthd => cthd.Product)
                .Select(cthd => new ProductRevenueVM {
                    ProductId = cthd.Key.ProductId,
                    ProductName = cthd.Key.ProductName,
                    Revenue = cthd.Sum(p => p.Quantity * p.Price)
                });

            //return Json(data);
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            return View(data);
        }
    }
}