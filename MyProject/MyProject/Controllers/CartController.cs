using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Helpers;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Models;

namespace MyProject.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductBo _productBo;
        private readonly IOrderBo _orderBo;
        private readonly IMapper _mapper;
        private readonly IPaymentBo _paymentBo;
        
        public CartController(IProductBo productBo, IMapper mapper, IPaymentBo paymentBo, IOrderBo orderBo)
        {
            _productBo = productBo;
            _mapper = mapper;
            _paymentBo = paymentBo;
            _orderBo = orderBo;
        }

        public List<CartItem> CartItems
        {
            get
            {
                var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (cart == null)
                {
                    cart = new List<CartItem>();
                }

                return cart;
            }
        }

        public IActionResult AddToCart(int productId, int quantity)
        {
            var carts = CartItems;
            var cartItem = carts.SingleOrDefault(p => p.ProductId == productId);

            if (cartItem != null)//đã có
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                var hh = _productBo.GetById(productId);
                cartItem = _mapper.Map<CartItem>(hh);
                cartItem.Quantity = quantity;

                carts.Add(cartItem);
            }
            HttpContext.Session.Set("GioHang", carts);

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View(CartItems);
        }

        public IActionResult Payment1()
        {
            if(CartItems.Count == 0)
            {
                return Redirect("/Product");
            }
            _orderBo.Payment(CartItems, null);
            return Json(true);
        }

        public IActionResult Payment2()
        {
            if (CartItems.Count == 0)
            {
                return Redirect("/Product");
            }
            _paymentBo.Payment(CartItems, null);
            return Json(true);
        }
    }
}
