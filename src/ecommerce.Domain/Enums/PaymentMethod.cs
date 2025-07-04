using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecommerce.Domain.Enums
{
    public enum PaymentMethod
    {
        Check = 1,
        BankTransfer = 2,
        Cash = 3,
        Paypal = 4,
        Payoneer = 5
    }
}
