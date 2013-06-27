using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexInterfaceService
{
    public class CommonService
    {
        public int GetPageCount(int recordCount)
        {
            return recordCount / ConfigHelper.PageSize + (recordCount % ConfigHelper.PageSize == 0 ? 0 : 1);
        }
    }
}