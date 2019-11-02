using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("Product")]
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }

        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}