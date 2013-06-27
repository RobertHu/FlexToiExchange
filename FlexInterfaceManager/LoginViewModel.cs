using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using FlexInterfaceManager.Model;


namespace FlexInterfaceManager
{
    public class LoginViewModel : ViewModelBase
    {
        private string _Name;
        private string _Pwd;
        /// <summary>
        /// Initializes a new instance of the LoginViewModel class.
        /// </summary>
        ///
        public LoginViewModel()
        {

            LoginCmd = new RelayCommand<object>(o =>
            {
                PasswordBox pwb = o as PasswordBox;
                this._Pwd = pwb.Password;
                this.ValidateAllProperty();
                if (this.HasError) return;
                if (this.Name.ToLower() == "admin")
                {
                    MessengerInstance.Send(new HideUserManageComponentMsg(true));
                }
                else
                {
                    MessengerInstance.Send(new HideUserManageComponentMsg(false));
                }
                MessengerInstance.Send(new LoginMsg(this.Name, this.Pwd));
            });

            CancelCmd = new RelayCommand(() =>
            {
                MessengerInstance.Send(new ApplicationCloseMsg());
            });
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name != value.Trim())
                {
                    _Name = value.Trim();
                    RaisePropertyChanged(() => this.Name);
                    ValidateProperty(() => this.Name, _Name);
                }
            }
        }

        public string Pwd
        {
            get
            {
                return _Pwd;
            }
            set
            {
                if (_Pwd != value.Trim())
                {
                    _Pwd = value.Trim();
                    RaisePropertyChanged(() => this.Pwd);
                    ValidateProperty(() => this.Pwd, _Pwd);
                }
            }
        }

        protected override void ValidateProperty(string propertyName, object value)
        {
            if (propertyName == GetPropertyName(() => this.Name))
            {
                if (value == null || string.Empty == value.ToString())
                {
                    this.AddError(propertyName, "LoginUserId,can't be empty");
                }
                else
                {
                    this.RemoveError(propertyName);
                }
            }
            else if (propertyName == GetPropertyName(() => this.Pwd))
            {
                if (value == null || string.Empty == value.ToString())
                {
                    this.AddError(propertyName, "Password can't be empty");
                }
                else
                {
                    this.RemoveError(propertyName);
                }
            }
        }

        public RelayCommand<object> LoginCmd { get; set; }
        public RelayCommand CancelCmd { get; set; }
    }
}
