using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace FlexInterfaceManager.Model
{
    public abstract class AccountBase : ViewModelBase
    {
        protected bool _IsChecked;
        protected bool _IsExpand;

        public bool IsExpand
        {
            get
            {
                return _IsExpand;
            }
            set
            {
                if (_IsExpand != value)
                {
                    _IsExpand = value;
                    RaisePropertyChanged(() => this.IsExpand);
                }
            }
        }

        public abstract bool IsChecked { get; set; }

    }
}
