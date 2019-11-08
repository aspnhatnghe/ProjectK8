using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Supplier")]
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public ICollection<Product> Products { get; set; }
        public Supplier()
        {
            Products = new HashSet<Product>();
        }
    }
}