using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexInterfaceService.Util
{
    public static class MoneyExtension
    {
        public static string ToThousandSeparatedString(this double input)
        {
            return string.Format("{0:n2}", Math.Abs(input));
        }

        public static string ToThousandSeparatedString(this decimal input)
        {
            return string.Format("{0:n2}", Math.Abs(input));
        }
    }
}