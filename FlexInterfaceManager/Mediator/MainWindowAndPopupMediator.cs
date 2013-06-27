using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace FlexInterfaceManager.Mediator
{
    public class MainWindowAndPopupMediator
    {
        private Window _MainWindow;
        private Popup _PopupLogin;
        private Popup _PopupProgressbar;
        private string _LoginTextboxName;
        public MainWindowAndPopupMediator(Window mainWindow, Popup popupLogin, Popup popupProgressbar, string loginTextboxName)
        {
            this._MainWindow = mainWindow;
            this._PopupLogin = popupLogin;
            this._PopupProgressbar = popupProgressbar;
            this._LoginTextboxName = loginTextboxName;
            this._PopupLogin.Opened += PopupLogin_Opened;
        }

        private void PopupLogin_Opened(object sender, EventArgs e)
        {
            Popup p = (Popup)sender;
            Border border = (Border)p.Child;
            Canvas canvas = (Canvas)border.Child;
            TextBox tb = (TextBox)canvas.FindName(this._LoginTextboxName);
            tb.Focus();
            this._MainWindow.LocationChanged += WindowLocationChanged;
            this._MainWindow.SizeChanged += WindowLocationChanged;
        }

        private void WindowLocationChanged(object sender, EventArgs e)
        {
            var mi = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (this._PopupLogin.IsOpen)
            {
                mi.Invoke(this._PopupLogin, null);
            }
            if (this._PopupProgressbar.IsOpen)
            {
                mi.Invoke(this._PopupProgressbar, null);
            }
        }
    }
}
