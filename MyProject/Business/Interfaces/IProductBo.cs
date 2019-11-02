using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IProductBo:IBaseBo<ProductModel, Product>
    {
        IEnumerable<ProductModel> Top10BestSeller();
    }
}
