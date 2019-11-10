using AutoMapper;
using Entities;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerModel, Customer>().ReverseMap();

            CreateMap<Product, ProductViewModel>()
                .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.SupplierName))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.CategoryName));

            CreateMap<ProductModel, CartItem>();

            CreateMap<ProductModel, Product>()
                .ReverseMap();
            CreateMap<CategoryModel, Category>()
                .ReverseMap();
            CreateMap<SupplierModel, Supplier>().ReverseMap();

            CreateMap<RegisterViewModel, CustomerModel>();
        }
    }
}
