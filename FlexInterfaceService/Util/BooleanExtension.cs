using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexInterfaceService.Util
{
    public static class BooleanExtension
    {
        public static int ToInt32(this bool source)
        {
            return source ? 1 : 0;
        }
    }
}