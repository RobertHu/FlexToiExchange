using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Extension;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FlexInterface.Common;
using Microsoft.Win32;
using FlexInterfaceManager.Util;
using System.Windows;
using FlexInterfaceManager.Model;
using System.Windows.Data;
namespace FlexInterfaceManager.Manager
{
    public class QueryStringManager : ViewModelBase
    {
        public static readonly QueryStringManager Default = new QueryStringManager();
        private DateTime _FromTradeDay = DateTime.Now.ToStartDay();
        private DateTime _ToTradeDay = DateTime.Now.ToEndDay();

       

        private QueryStringManager()
        {
            this.BusinessTypeCol = new ObservableCollection<string>();
            this.BusinessTypeView = CollectionViewSource.GetDefaultView(this.BusinessTypeCol);
            foreach (var item in Enum.GetNames(typeof(BusinessTypeEnum)))
            {
                this.BusinessTypeCol.Add(item);
            }

        }


        public ObservableCollection<string> BusinessTypeCol { get; private set; }

        public ICollectionView BusinessTypeView { get; set; }




        public DateTime FromTradeDay
        {
            get { return _FromTradeDay; }
            set
            {
                if (_FromTradeDay != value)
                {
                    _FromTradeDay = value.ToStartDay();
                    RaisePropertyChanged(() => this.FromTradeDay);
                }
            }
        }
        public DateTime ToTradeDay
        {
            get { return _ToTradeDay; }
            set
            {
                if (_ToTradeDay != value)
                {
                    _ToTradeDay = value.ToEndDay();
                    RaisePropertyChanged(() => this.ToTradeDay);
                }
            }
        }

      
        public bool GetAllParameters(out QueryObject target)
        {
            target = null;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".text";
            dlg.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> isShow = dlg.ShowDialog();
            if (isShow == false) return false;
            bool result = GetUIParametersHelper(out target,true);
            if (result == false) return false;
            target.FilePath = dlg.FileName;
            return true;
        }

        public bool GetUIParametersHelper(out QueryObject parameter,bool isGetFundCode=false)
        {
            parameter = null;
            if (this.BusinessTypeView.CurrentItem == null) return false;
            DateTime fromDt = this.FromTradeDay;
            DateTime toDt = this.ToTradeDay;
            BusinessTypeEnum plType = EnumUtil.ParseString<BusinessTypeEnum>(this.BusinessTypeView.CurrentItem.ToString());
            if (fromDt > toDt)
            {
                MessageBox.Show("FromTradeDay  can't great than ToTradeDay!");
                return false;
            }
            string fundNoStr = string.Empty;
            string accountStr = string.Empty;
            try
            {
                accountStr = GetAccountStr();
                if (isGetFundCode)
                {
                    fundNoStr = GetFundCode(plType);
                }
            }
            catch (Exception e)
            {
                DebugHelper.Log(e.ToString());
                return false;
            }
            parameter = new QueryObject();
            parameter.Type = plType;
            parameter.BeginDatetime = fromDt;
            parameter.EndDatetime = toDt;
            parameter.AccountStr = accountStr;
            parameter.DepositQueryStr = fundNoStr;
            return true;
        }

        private string GetAccountStr()
        {
            string accountStr = AccountManager.Default.GetAccountInfo();
            if (accountStr == null)
            {
                throw new ArgumentNullException("accountStr", "accountStr is null");
            }
            return accountStr;
        }


        private string GetFundCode(BusinessTypeEnum plType)
        {
            string fundNoStr = string.Empty;
            if (plType > BusinessTypeEnum.Transfer) return fundNoStr;
            fundNoStr = FundManager.Default.GetFundNoStr();
            if (fundNoStr == string.Empty)
            {
                MessageBox.Show("FundNo can't be empty");
                throw new ArgumentException("fundNoStr is empty", "fundNoStr");
            }
            else if (fundNoStr == null)
            {
                throw new ArgumentNullException("fundNoStr", "fundNoStr is null");
            }
            return fundNoStr;
        }


    }
}
