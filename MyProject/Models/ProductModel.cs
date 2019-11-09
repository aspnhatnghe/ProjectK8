using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Image { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public int SupplierId { get; set; }        
        public Supplier Supplier { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
