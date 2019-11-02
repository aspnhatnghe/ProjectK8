using AutoMapper;
using Business.Interfaces;
using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Implements
{
    public class ProductBo : BaseBo<ProductModel, Product>, IProductBo
    {
        private readonly IMapper _mapper;
        public ProductBo(IMapper mapper, IServiceProvider serviceProvider) : base(mapper, serviceProvider)
        {
            _mapper = mapper;
        }

        public IEnumerable<ProductModel> Top10BestSeller()
        {
            try
            {
                var models = new List<ProductModel>();

                using (var unitOfWork = NewDbContext())
                {
                    var entityProduct = unitOfWork.Respository<Product>().GetEntities();

                    var entitiesResult = entityProduct.OrderByDescending(p => p.Price).Take(10);
                    
                    models = _mapper.Map<List<ProductModel>>(entitiesResult);
                }

                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
