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
        public static decimal ToDecimal(this object value)
        {
            if (value == null || value.ToString() == string.Empty)
                return 0;

            decimal result = 0;

            decimal.TryParse(value.ToString(), out result);

            return result;
        }
        public static int ToInt(this object value)
        {
            if (value == null || value.ToString() == string.Empty)
                return 0;
            int result = 0;
            int.TryParse(value.ToString(), out result);
            return result;
        }

        public static string ToStringU(this object value, string tempFormat)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return string.Format("{0:" + tempFormat + "}", value);
        }

        public static string ToStringCheckZero(this object value,string tempFormat)
        {
         
            if (value.ToDecimal() ==0)
            {
                return value!.ToString();
            }
            else
            {
                return string.Format("{0:" + tempFormat + "}", value);
            }

        }
    }
}
