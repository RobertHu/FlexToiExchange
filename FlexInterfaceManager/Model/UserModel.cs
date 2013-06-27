using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace FlexInterfaceManager.Model
{
    public class UserModel : ViewModelBase
    {

        private string _LoginID;
        private string _Pwd;
        private DateTime _LastModifiedDate;
        public Guid ID { get; set; }
        public string LoginID
        {
            get { return this._LoginID; }
            set
            {
                if (this._LoginID != value)
                {
                    this._LoginID = value.Trim();
                    RaisePropertyChanged(() => this.LoginID);
                    this.ValidateProperty(() => this.LoginID, value.Trim()); 
                }
            }
        }

        public string Pwd
        {
            get { return this._Pwd; }
            set
            {
                if (this._Pwd != value)
                {
                    this._Pwd = value.Trim();
                    RaisePropertyChanged(() => this.Pwd);
                    this.ValidateProperty(() => this.Pwd, value.Trim());
                }
            }
        }


        public DateTime LastModifiedDate
        {
            get { return this._LastModifiedDate; }
            set
            {
                if (this._LastModifiedDate != value)
                {
                    this._LastModifiedDate = value;
                    RaisePropertyChanged(() => this.LastModifiedDate);
                }
            }
        }


        protected override void ValidateProperty(string propertyName, object value)
        {
            if (propertyName == GetPropertyName(() => this.LoginID))
            {
                if (value == null || string.IsNullOrEmpty(value.ToString().Trim()))
                {
                    this.AddError(propertyName, "LoginID can't be empty!");
                }
                else
                {
                    this.RemoveError(propertyName);
                }
            }
            else if (propertyName == GetPropertyName(() => this.Pwd))
            {
                if (value == null || string.IsNullOrEmpty(value.ToString().Trim()))
                {
                    this.AddError(propertyName, "Password can't be empty!");
                }
                else
                {
                    this.RemoveError(propertyName);
                }
            }
        }
    }
}
