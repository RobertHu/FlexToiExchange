using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace FlexInterfaceManager.Model
{
    public class AccountModel : AccountBase
    {
        public static AccountModel Empty = new AccountModel(string.Empty, string.Empty, false);

        public AccountModel(string code, string accountCode, bool isChecked)
        {
            this.Code = code;
            this._IsChecked = IsChecked;
            this.AccountCode = accountCode;
        }
        public string Code { get; private set; }

        public string AccountCode { get; private set; }

        public string ComposedCode
        {
            get
            {
                return this.AccountCode;
            }
        }


        public override bool IsChecked
        {
            get { return this._IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    RaisePropertyChanged(() => this.IsChecked);
                }
            }
        }
        public void Update(bool isChecked)
        {
            this._IsChecked = isChecked;
            RaisePropertyChanged(() => this.IsChecked);
        }
    }
}
