using Business.Interfaces;
using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Implements
{
    public class SupplierBo : BaseBo<SupplierModel, Supplier>, ISupplierBo
    {
        public SupplierBo(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
