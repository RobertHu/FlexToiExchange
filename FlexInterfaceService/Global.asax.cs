using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;
using log4net;
using System.Reflection;
using FlexInterface.Repository;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace FlexInterfaceService
{
    public class Global : System.Web.HttpApplication
    {
       
        void Application_Start(object sender, EventArgs e)
        {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Warn("Application Start");
            ConfigHelper.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            ConfigHelper.PageSize = int.Parse(ConfigurationManager.AppSettings["PAGESIZE"]);
            SessionManager.InitializeLocalSessionFactory(ConfigHelper.ConnectionString);
            log.Warn(string.Format("ConnectionString: {0}",ConfigHelper.ConnectionString));
            ManagerServiceHost.Start();
            ClientManager.Default.Start();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            log.Warn("Application End");
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
