using Business.Interfaces;
using Entities;
using Models;
using System;


namespace Business.Implements
{
    public class UserBo : BaseBo<CustomerModel, Customer>, IUserBo
    {
        public UserBo(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
