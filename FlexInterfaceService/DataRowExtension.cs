using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace FlexInterfaceService
{
    public static class DataRowExtension
    {
        public static T EscapeDBNULL<T>(this DataRow dr, string columnName)
        {
            return dr[columnName] == DBNull.Value ? default(T) : (T)dr[columnName];
        }
    }
}