using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public string ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category ParentCategory { get; set; }
        
    }
}
