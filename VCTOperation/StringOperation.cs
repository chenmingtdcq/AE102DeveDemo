using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCTOperation
{
    public static class StringOperation
    {
        public static bool IsNullOrWhiteSpace(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return true;
            return false;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            return false;
        }

        public static Int32 ToInt32(this string str)
        {
            Int32 result = -1;
            if (str.IsNullOrWhiteSpace())
                return result;
            Int32.TryParse(str, out result);
            return result;
        }

        public static DateTime ToDateTime(this string str)
        {
            DateTime dt = default(DateTime);
            if (str.IsNullOrWhiteSpace())
                return dt;
            DateTime.TryParse(str, out dt);
            return dt;
        }
    }
}
