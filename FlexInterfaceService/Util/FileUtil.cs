using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FlexInterfaceService.Util
{
    public static class FileUtil
    {
        public static void MakeFileWritable(string fileName)
        {
            if (File.Exists(fileName) == false) return;
            FileInfo fi = new FileInfo(fileName);
            if (fi.IsReadOnly) fi.IsReadOnly = false;
        }
    }
}