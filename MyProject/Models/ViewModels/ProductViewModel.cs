using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public string SupplierName { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
    }
}
