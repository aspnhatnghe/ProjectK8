using AutoMapper;
using Business.Interfaces;
using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Implements
{
    public class ProductBo : BaseBo<ProductModel, Product>, IProductBo
    {
        public ProductBo(IMapper mapper, IServiceProvider serviceProvider) : base(mapper, serviceProvider)
        {
        }
    }
}
