using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace FlexInterfaceManager.Model
{
    public class TopAccountGroupModel : AccountBase
    {
        public TopAccountGroupModel(string code, bool isChecked)
        {
            this.AccountGroups = new ObservableCollection<AccountGroupModel>();
            this.Code = code;
            this.IsChecked = IsChecked;
        }

        public string GetAllCheckedCodeString()
        {
            var result1 = this.AccountGroups
                            .Where(g=>g.Accounts.Count>0)
                            .Select(g => g.Accounts)
                            .Aggregate((IEnumerable<AccountModel>)new List<AccountModel>(), (acc, m) => acc.Concat(m));
            
            var result2 =  result1.Where(a => a.IsChecked);
            var result = result2.Aggregate(string.Empty, (r, n) => 
                {
                    if (string.IsNullOrEmpty(n.AccountCode))
                    {
                        return r;
                    }
                    return r + "," + n.AccountCode;
                }, r => r.Length > 0 ? r.Substring(1) : r);
            DebugHelper.Log(result);
            return result;
        }
        public ObservableCollection<AccountGroupModel> AccountGroups { get; private set; }
        public string Code { get; private set; }


        public override bool IsChecked
        {
            get { return this._IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    foreach (var item in this.AccountGroups)
                    {
                        item.Update(value);
                    }
                    RaisePropertyChanged(() => this.IsChecked);
                }
            }
        }
    }
}
