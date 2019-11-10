using Entities;
using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IProductBo:IBaseBo<ProductModel, Product>
    {
        IEnumerable<ProductModel> Top10BestSeller();
        IEnumerable<ProductViewModel> Search(string categoryId, int? supplierId, string keyword);
    }
}
