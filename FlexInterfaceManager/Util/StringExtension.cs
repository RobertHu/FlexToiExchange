using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexInterfaceManager.Util
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string source)
        {
            return String.IsNullOrEmpty(source);
        }
    }
}
