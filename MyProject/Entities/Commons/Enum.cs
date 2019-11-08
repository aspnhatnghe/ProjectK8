using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entities.Commons
{
    public enum Role
    {
        [Description("Administrator")]
        Administrator,
        [Description("Customer")]
        Customer,
        [Description("Accountant")]
        Accountant,
        [Description("Guest")]
        Guest
    }

    public enum OrderStatus
    {
        [Description("Open")]
        Open = 0,
        [Description("Confirmed")]
        Confirmed = 1,
        [Description("Paid")]
        Paid = 2,
        [Description("Done")]
        Done = 3,
        [Description("Cancel")]
        Cancel = 4
    }

    public enum PaymentMethod
    {
        COD,
        CreditCard,
        Tranfer,
        InternetBanking,
        SMSBanking,
        MobileBanking
    }

    public enum CouponStatus
    {
        [Description("New")]
        New,
        [Description("Used")]
        Used,
        [Description("Expired")]
        Expired
    }
}
