using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
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
            if(!ModelState.IsValid)
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
    }
}