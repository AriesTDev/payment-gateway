using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Core.Extensions
{
   public static  class NumberExtensions
    {
        public static int ToCents(this decimal d)
        {
            return decimal.ToInt32(d * 100M);
        }
    }
}
