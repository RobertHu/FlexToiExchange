using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlexInterfaceManager.Util
{
    public static class EnumExtension
    {
        
    }


    public static class EnumUtil
    {
        public static T ParseString<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
