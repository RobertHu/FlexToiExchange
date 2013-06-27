using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexInterface.Common;
using System.Threading;
using System.Windows;

namespace FlexInterfaceManager.Manager
{
   public static class Common
    {
       public static IFlexService Service { get; set; }

       public static string SessionId { get; set; }

       public static SynchronizationContext SynchronizationContext { get; set; }

       public static Window MainWindow { get; set; }

    }
}
