using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Aitea.PaymentGateway.Core.Constant
{
    public static class QFPayPaymentTypes
    {
        private static readonly ReadOnlyDictionary<string, string> QFPayPaymentType
            = new ReadOnlyDictionary<string, string>(
                new Dictionary<string, string>()
                {
                    {"alipaympm", "800101"},
                    {"alipaycpm", "800108"},
                    {"alipayonline","801114"},
                    {"wechatmpm", "800201"},
                    {"wechatcpm", "800208"},
                    {"wechatonline","800214"}
                }
            );

        public static string GetQFPayPaymentTypeCode(this string paymentType)
        {
            return QFPayPaymentType.FirstOrDefault(a => a.Key.Equals(paymentType, StringComparison.OrdinalIgnoreCase))
                .Value;
        }
    }
}
