using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IOrderBo : IBaseBo<OrderModel, Order>
    {
        void Payment(List<CartItem> carts, string customerId);
    }

    public interface IOrderDetailBo : IBaseBo<OrderDetailModel, OrderDetail>
    {

    }
}
