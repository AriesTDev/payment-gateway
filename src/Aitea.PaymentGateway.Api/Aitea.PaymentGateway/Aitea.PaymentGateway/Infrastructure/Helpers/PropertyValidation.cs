using System;

namespace Aitea.PaymentGateway.Infrastructure.Helpers
{
    public static class PropertyValidation
    {
        public static bool IsValidDateTime(DateTime date) => date == default ? false : true;
    }
}
