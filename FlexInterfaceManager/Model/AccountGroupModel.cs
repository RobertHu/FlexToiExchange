using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace FlexInterfaceManager.Model
{
    public class AccountGroupModel : AccountBase
    {
        public AccountGroupModel(string code, bool isChecked)
        {
            this.Accounts = new ObservableCollection<AccountModel>();
            this.Accounts.Add(AccountModel.Empty);
            this.Code = code;
            this.IsChecked = IsChecked;

        }
        public ObservableCollection<AccountModel> Accounts { get; private set; }
        public string Code { get; private set; }

        public override bool IsChecked
        {
            get { return this._IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    if (this.Accounts.Count == 1 && this.Accounts.First().Code == string.Empty)
                    {
                        LoadAccounts();
                    }
                    foreach (var item in this.Accounts)
                    {
                        item.Update(value);
                    }
                    RaisePropertyChanged(() => this.IsChecked);
                }
            }
        }

        public void Update(bool isChecked)
        {
            this._IsChecked = isChecked;
            if (this.Accounts.Count == 1 && this.Accounts.First().Code == string.Empty)
            {
                LoadAccounts();
            }
            else
            {
                foreach (var item in this.Accounts)
                {
                    item.Update(isChecked);
                }
            }
            RaisePropertyChanged(() => this.IsChecked);
        }


        private void LoadAccounts()
        {
            RaisePropertyChanged(() => this.IsExpand);
        }


    }
}
