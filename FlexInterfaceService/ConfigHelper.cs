using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;

namespace FlexInterfaceService
{
    public class ConfigHelper
    {
        public static string ConnectionString { get; set; }
        public static int PageSize { get; set; }
        public static readonly string DepositString = "Deposit";
        public static readonly string PLString = "PL";
        public static readonly string FileExtension = ".txt";
        public static readonly string DepositDirectoryPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, DepositString);
        public static readonly string PLDirectoryPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, PLString);
    }
}