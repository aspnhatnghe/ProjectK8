using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class SupplierModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public ICollection<ProductModel> Products { get; set; }
        public SupplierModel()
        {
            Products = new HashSet<ProductModel>();
        }
    }
}
