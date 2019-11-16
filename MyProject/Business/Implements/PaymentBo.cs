using Business.Interfaces;
using Entities;
using Entities.Commons;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Implements
{
    public class PaymentBo : IPaymentBo
    {
        private readonly IOrderDetailBo _orderDetailBo;
        private readonly IProductBo _productBo;
        private readonly IOrderBo _orderBo;
        protected readonly IServiceProvider _serviceProvider;

        public PaymentBo(IOrderDetailBo _orderDetailBo, IProductBo _productBo, IOrderBo _orderBo, IServiceProvider serviceProvider)
        {
            this._orderBo = _orderBo;
            this._orderDetailBo = _orderDetailBo;
            this._productBo = _productBo;
            _serviceProvider = serviceProvider;
        }

        public void Payment(List<CartItem> carts, string customerId)
        {
            var newContext = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope()
                .ServiceProvider.GetRequiredService<MyDbContext>();            

            using (var unitOfWork = new UnitOfWork(newContext))
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
                    var orderAdded = _orderBo.Insert(order, unitOfWork);

                    foreach (CartItem item in carts)
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

                        var product = _productBo.GetById(unitOfWork, item.ProductId);
                        product.Quantity -= item.Quantity;

                        _productBo.Update(product, item.ProductId, unitOfWork);

                    }

                    unitOfWork.CommitTransaction();
                }
                catch (Exception ex)
                {
                    unitOfWork.RollbackTransaction();
                }
            }
        }
    }
}
