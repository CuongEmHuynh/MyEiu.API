using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEiu.Utilities
{
    public static class ConvertUltility
    {
        public static string ToJsonString(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return JsonConvert.SerializeObject(value);
        }
    }
}
