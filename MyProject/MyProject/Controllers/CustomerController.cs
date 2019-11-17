using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Entities.Commons;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
using MyProject.Helpers;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUserBo _userBo;
        private readonly IMapper _mapper;
        public CustomerController(IUserBo userBo, IMapper mapper)
        {
            _userBo = userBo;
            _mapper = mapper;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("loi", "Còn lỗi");
                return View();
            }

            var userModel = _mapper.Map<CustomerModel>(model);
            userModel.RandomKey = MyTools.GetRandom();
            userModel.Password = (model.Password + userModel.RandomKey).ToMD5();

            _userBo.Insert(userModel);
            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            try
            {
                var kh = _userBo.GetFirstWithInclude(p => p.Email == model.Email, p => p.Roles);

                if (kh.Password == (model.Password + kh.RandomKey).ToMD5())
                {
                    //thành công
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, kh.CustomerName)
                    };

                    foreach (var role in kh.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, MyTools.GetEnumDescription((Role)role.Role)));
                    }

                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }

                    return Redirect("/Customer/Profile");
                }

                ModelState.AddModelError("loi", "Sai mật khẩu");
                return View();
            }
            catch
            {
                ModelState.AddModelError("loi", "Email không tồn tại");
                return View();
            }
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Product");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize(Roles = MyRole.SuperAdmin)]
        public IActionResult SuperRole()
        {
            //var hasRole = User.IsInRole(MyRole.SuperAdmin);
            return Content("SuperRole");
        }
    }
}