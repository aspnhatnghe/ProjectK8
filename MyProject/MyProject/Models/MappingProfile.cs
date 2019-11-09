using AutoMapper;
using Entities;
using Models;
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
            CreateMap<ProductModel, Product>()
                .ReverseMap();
            CreateMap<CategoryModel, Category>()
                .ReverseMap();
            CreateMap<SupplierModel, Supplier>().ReverseMap();
        }
    }
}
