using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyProject.DataModels;
using MyProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.ViewComponents
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryBo _categoryRepo;
        public CategoryMenu(ICategoryBo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        /*Chỉ có 1 action duy nhất là Invoke()*/
        public IViewComponentResult Invoke()
        {         
            return View("Default", _categoryRepo.GetAll());
        }
    }
}
