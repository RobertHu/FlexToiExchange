using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Diagnostics;
using iExchange.Common;

namespace FlexInterfaceService
{
    public static class ManagerServiceHost
    {
        private static ServiceHost _Host = null;

        public static void Start()
        {
            ManagerServiceHost.Stop();

            try
            {
                ManagerServiceHost._Host = new ServiceHost(typeof(Service));
                ManagerServiceHost._Host.Open();
                AppDebug.LogEvent("ManagerServiceHost.Start", "Successful start", EventLogEntryType.Warning);
            }
            catch (Exception exception)
            {
                AppDebug.LogEvent("ManagerServiceHost.Start", exception.ToString(), EventLogEntryType.Error);
            }
        }

        public static void Stop()
        {
            if (ManagerServiceHost._Host != null)
            {
                try
                {
                    ManagerServiceHost._Host.Close();
                }
                catch (Exception exception)
                {
                    AppDebug.LogEvent("ManagerServiceHost.Stop", exception.ToString(), EventLogEntryType.Error);
                }
            }
        }
    }
}