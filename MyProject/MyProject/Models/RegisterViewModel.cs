using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [RegularExpression("[A-Za-z0-9]+")]
        [DataType(DataType.Password)]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
