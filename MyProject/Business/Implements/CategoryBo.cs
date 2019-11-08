using AutoMapper;
using Business.Interfaces;
using Entities;
using Models;
using System;

namespace Business.Implements
{
    public class CategoryBo : BaseBo<CategoryModel, Category>, ICategoryBo
    {
        public CategoryBo(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
