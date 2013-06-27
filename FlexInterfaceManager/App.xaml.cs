using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using log4net;
using System.Globalization;
using System.Threading;

namespace FlexInterfaceManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ILog log = LogManager.GetLogger(typeof(App));
            try
            {
                CultureInfo ci = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
                ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
                Thread.CurrentThread.CurrentCulture = ci;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Network not connected!");
                log.ErrorFormat("Application Start up Error  {0}", ex);
            }
        }
    }
}
