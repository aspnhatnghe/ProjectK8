using AutoMapper;
using Business.Interfaces;
using Entities;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Implements
{
    public class ProductBo : BaseBo<ProductModel, Product>, IProductBo
    {
        public ProductBo(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public IEnumerable<ProductViewModel> Search(string categoryId, int? supplierId, string keyword)
        {
            try
            {
                var models = new List<ProductViewModel>();

                using (var unitOfWork = NewDbContext())
                {
                    var entityProduct = unitOfWork.Repository<Product>().GetEntities();

                    if(!string.IsNullOrEmpty(categoryId))
                    {
                        entityProduct = entityProduct.Where(p => p.CategoryId == categoryId).AsQueryable();
                    }

                    if(supplierId.HasValue)
                    {
                        entityProduct = entityProduct.Where(p => p.SupplierId == supplierId).AsQueryable();
                    }

                    if(!string.IsNullOrEmpty(keyword))
                    {
                        entityProduct = entityProduct.Where(p => p.ProductName.Contains(keyword)).AsQueryable();
                    }


                    models = _mapper.Map<List<ProductViewModel>>(entityProduct);
                }

                return models;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProductModel> Top10BestSeller()
        {
            try
            {
                var models = new List<ProductModel>();

                using (var unitOfWork = NewDbContext())
                {
                    var entityProduct = unitOfWork.Repository<Product>().GetEntities();

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
