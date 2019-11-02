using AutoMapper;
using Business.Interfaces;
using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Implements
{
    public class CategoryBo : BaseBo<CategoryModel, Category>, ICategoryBo
    {
        public CategoryBo(IMapper mapper, IServiceProvider serviceProvider) : base(mapper, serviceProvider)
        {
        }
    }
}
