
using System;
using System.Linq;
using PaymentGateway.Core.Constant;

namespace PaymentGateway.Core.Extensions
{
    public static class StringExtension
    {
        public static string ToStarred(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            var l = str.Length;
            var ss = new string('*', l);
            var cc = ss.ToCharArray();
            if (l < 4 && l > 0)
            {
                return ss;
            }
            if (l > 4)
            {
                cc[0] = str[0];
                cc[1] = str[1];
                cc[l - 2] = str[l - 2];
                cc[l - 1] = str[l - 1];

                ss = new string(cc);
                return ss;
            }

            return null;
        }

        public static string HideKey(this string str, string key)
        {
            return str.Replace(key, key.ToStarred());
        }

        public static string GetPaymentStatus(this string responseErrorCode)
        {
            if (QFPayStatusCodes.SuccessCodes.Contains(responseErrorCode))
            {
                return "Success";
            }

            if (QFPayStatusCodes.ErrorCodes.Contains(responseErrorCode))
            {
                return "Error";
            }

            if (QFPayStatusCodes.PendinCodes.Contains(responseErrorCode))
            {
                return "Pending";
            }
            throw new InvalidOperationException();
        }
    }
}
