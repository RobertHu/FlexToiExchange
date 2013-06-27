using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexInterfaceService.Util
{
    public static class DateTimeExtension
    {
        public static string ToStandardDatetimeString(this DateTime dt)
        {
            return dt.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public static string ToTradeDayStyle(this DateTime dt)
        {
            return dt.ToString("yyyy/MM/dd");
        }
    }
}