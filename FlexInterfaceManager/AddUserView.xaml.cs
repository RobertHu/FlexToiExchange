using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using FlexInterfaceManager.Model;
using FlexInterface.Common;


namespace FlexInterfaceManager
{
    /// <summary>
    /// Interaction logic for AddUserForm.xaml
    /// </summary>
    public partial class AddUserView : Window
    {
        private IFlexService _Service;
        private string _SessionId;
        public AddUserView(IFlexService service,string sessionId)
        {
            InitializeComponent();
            this._Service = service;
            this._SessionId = sessionId;
            this.DataContext = new UserModel();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            UserModel model = this.DataContext as UserModel;
            model.Pwd = this.pwb.Password;
            model.ValidateAllProperty();
            if (model.HasError) return;
            User user = new User();
            user.Name = model.LoginID;
            user.Pwd = model.Pwd;
            try
            {
                if (_Service.AddUser(this._SessionId,user))
                {
                    MessageBox.Show("Success!");
                }
                else
                {
                    MessageBox.Show("The user with the same LoginID has already existed!");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Network not connected!");
               
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
