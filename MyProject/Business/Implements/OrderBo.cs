using Business.Interfaces;
using Entities;
using Entities.Commons;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Implements
{
    public class OrderDetailBo : BaseBo<OrderDetailModel, OrderDetail>, IOrderDetailBo
    {
        public OrderDetailBo(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }

    public class OrderBo : BaseBo<OrderModel, Order>, IOrderBo
    {
        private readonly IOrderDetailBo _orderDetailBo;
        private readonly IProductBo _productBo;

        public OrderBo(IServiceProvider serviceProvider, IOrderDetailBo orderDetailBo, IProductBo productBo) : base(serviceProvider)
        {
            _orderDetailBo = orderDetailBo;
            _productBo = productBo;
        }

        public void Payment(List<CartItem> carts, string customerId)
        {
            using (var unitOfWork = NewDbContext())
            {
                unitOfWork.BeginTransaction();

                try
                {
                    var order = new OrderModel
                    {
                        CustomerId = customerId,
                        OrderDate = DateTime.Now,
                        OrderStatus = OrderStatus.Open,
                        PaymentMethod = PaymentMethod.COD
                    };
                    var orderAdded = Insert(order, unitOfWork);

                    foreach(CartItem item in carts)
                    {
                        //tạo chi tiết
                        var orderDetail = new OrderDetailModel
                        {
                            OrderId = orderAdded.OrderId,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = item.Price
                        };

                        _orderDetailBo.Insert(orderDetail, unitOfWork);

                        //cập nhật đơn hàng
                        //var product = _productBo.GetFirstWithInclude(p => p.ProductId == item.ProductId, 
                        //    //params
                        //    p => p.Supplier, p => p.Category);
                        var product = _productBo.GetById(unitOfWork, item.ProductId);
                        product.Quantity -= item.Quantity;

                        _productBo.Update(product, item.ProductId, unitOfWork);

                    }

                    unitOfWork.CommitTransaction();
                }
                catch(Exception ex)
                {
                    unitOfWork.RollbackTransaction();
                }
            }
        }
    }
}
