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
    /// Interaction logic for UpdateUserForm.xaml
    /// </summary>
    public partial class UpdateUserView : Window
    {
        private UserModel _CurrentUser;
        private IFlexService _Service;
        private string _SessionId;

        public UpdateUserView(UserModel model,IFlexService service,string sessionID)
        {
            InitializeComponent();
            this._CurrentUser = new UserModel { ID = model.ID, LoginID = model.LoginID, Pwd = model.Pwd, LastModifiedDate = model.LastModifiedDate };
            this._Service = service;
            this._SessionId = sessionID;
            if (model.LoginID.ToLower() == "admin")
            {
                this.tbxName.IsEnabled = false;
            }
            this.DataContext = _CurrentUser;
        }



        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            this._CurrentUser.Pwd = this.pwb.Password;
            this._CurrentUser.ValidateAllProperty();
            if (this._CurrentUser.HasError) return;
            try
            {
                if (_Service.UpdateUser(_SessionId, new User { Id = this._CurrentUser.ID, Name = this._CurrentUser.LoginID, Pwd = this._CurrentUser.Pwd }))
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
