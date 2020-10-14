using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace PaymentGateway.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static IDictionary<string, string> ToDictionary(this object model)
        {
            var jsonFormat = model.ToJsonFormat();
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonFormat);
        }

        public static string ToJsonFormat(this object model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public static string GetDataString(this IDictionary<string, string> dict)
        {
            if (dict == null)
            {
                return "";
            }
            var sb = new StringBuilder();
            foreach (var key in dict.Keys.OrderBy(p => p))
            {
                sb.AppendFormat("{0}={1}&", key, (dict[key]));
            }
            return sb.ToString().TrimEnd('&');
        }

    }
  
}
