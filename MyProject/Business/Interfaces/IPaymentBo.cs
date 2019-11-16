using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IPaymentBo
    {
        void Payment(List<CartItem> carts, string customerId);
    }
}
