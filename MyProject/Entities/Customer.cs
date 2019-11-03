using Entities.Commons;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("User")]
    public class Customer
    {
        [Key]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string RandomKey { get; set; }
        public bool Status { get; set; } /*1: Active, 0: InActive*/
        public Role Role { get; set; }

        public ICollection<Order> Orders { get; set; }
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
    }
}