using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace FlexInterfaceManager.Model
{


    public class AccountSelectData
    {
        public string Code { get; set; }
    }

    public class FundData:ViewModelBase
    {
        
        private bool _IsChecked;
        public string Code { get; set; }
        public bool IsChecked
        {
            get
            {
                return _IsChecked;
            }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    RaisePropertyChanged(() => this.IsChecked);
                    Messenger.Default.Send(new FundDataCheckedMsg());
                }
            }
        }
        public void Update(bool isChecked)
        {
            if (this.IsChecked != isChecked)
            {
                this._IsChecked = isChecked;
                RaisePropertyChanged(() => this.IsChecked);
            }
        }
    }

    public enum SelectTypeEnum
    {
        Selection,
        ByRange
    }

}
