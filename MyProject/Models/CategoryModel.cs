using Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CategoryModel
    {
        [Display(Name = "Mã loại")]
        [MaxLength(450)]
        public string CategoryId { get; set; }
        [Required]
        [Display(Name ="Tên loại")]
        public string CategoryName { get; set; }
        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Mã loại cha")]
        [MaxLength(450)]
        public string ParentCategoryId { get; set; }

        public CategoryModel ParentCategory { get; set; }
    }
}
