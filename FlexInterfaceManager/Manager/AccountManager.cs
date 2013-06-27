using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FlexInterfaceManager.Model;
using FlexInterface.Common;
using System.Windows;
using System.Windows.Data;

namespace FlexInterfaceManager.Manager
{
    public class AccountManager
    {
        public static readonly AccountManager Default = new AccountManager();
        private AccountManager()
        {
            this.AccountGroupCol = new ObservableCollection<TopAccountGroupModel>();
        }


        private TopAccountGroupModel _TotalAccountGroupModel = new TopAccountGroupModel("All", false);
        public ObservableCollection<TopAccountGroupModel> AccountGroupCol { get; private set; }

        public TopAccountGroupModel GetTotalAccountGroupModel()
        {
            return this._TotalAccountGroupModel;
        }

        private string GetAllSelectCustomerCodeString()
        {
            if (this._TotalAccountGroupModel == null)
            {
                return string.Empty;
            }
            else
            {
                return this._TotalAccountGroupModel.GetAllCheckedCodeString();
            }
        }


        public string GetAccountInfo()
        {
           return GetAllSelectCustomerCodeString();
        }

    }
}
