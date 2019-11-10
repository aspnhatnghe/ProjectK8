using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class CartItem : ProductViewModel
    {
        public int Quantity { get; set; }
        public double Total => Quantity * Price;
    }
}
