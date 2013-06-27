using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using FlexInterfaceManager.Model;
using FlexInterface.Common;
using GalaSoft.MvvmLight.Messaging;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows;
using ValidationLibrary;
using System.Windows.Data;
using log4net;
using System.Threading.Tasks;
using FlexInterfaceManager.Util;
namespace FlexInterfaceManager.Manager
{
    public class FundManager
    {
        private ILog _Log = LogManager.GetLogger(typeof(FundManager));
        public static readonly FundManager Default = new FundManager();
        private ObservableCollection<FundData> _DataColFrom = new ObservableCollection<FundData>();
        private ObservableCollection<FundData> _DataColTo = new ObservableCollection<FundData>();
        private Regex _SpaceRegex = new Regex(@"[\r\t\n]", RegexOptions.Compiled);
        private Regex _MultipleSpaceRegex = new Regex(@"[ ]+",RegexOptions.Compiled);
        private string _UserInputFromFundNo;
        private string _UserInputToFundNo;

        private FundManager()
        {
            this.DataCol = new ObservableCollection<FundData>();
            this.FundSelectCol = new ObservableCollection<string>();
            this.FromView = CollectionViewSource.GetDefaultView(this._DataColFrom);
            this.ToView = CollectionViewSource.GetDefaultView(this._DataColTo);
            this.FundSelectView = CollectionViewSource.GetDefaultView(this.FundSelectCol);
            Init();
            Messenger.Default.Register<FundDataCheckedMsg>(this, m =>
            {
                string msg = FlexUI.FormatFundStatus(this.DataCol.Count, this.DataCol.Where(x => x.IsChecked).Count());
                Messenger.Default.Send(new UpdateFundStatusMsg(msg));
            });

            Messenger.Default.Register<UserInputFundNoRangeMsg>(this, m =>
            {
                this._UserInputFromFundNo = m.FromFundNo;
                this._UserInputToFundNo = m.ToFundNo;
            });
        }

        private void Init()
        {
            foreach (var item in Enum.GetNames(typeof(SelectTypeEnum)))
            {
                this.FundSelectCol.Add(item);
            }
        }

        public ObservableCollection<FundData> DataCol { get; set; }
       

        

        public ObservableCollection<string> FundSelectCol { get; private set; }


        public ICollectionView FromView { get; set; }
        public ICollectionView ToView { get; set; }
        public ICollectionView FundSelectView { get; set; }



        private void StartGetFundCode(QueryObject parameter)
        {
            if (Common.Service == null || Common.SessionId == null) return;

            Func<string[]> taskBody = () =>
            {
                if (Common.SynchronizationContext == null) return null;
                Common.SynchronizationContext.Send(m =>
                {
                    Messenger.Default.Send(new LoadingMsg());
                }, null);
                return Common.Service.GetAllFundNo(Common.SessionId, parameter);
            };

            Action<Task<string[]>> taskContinue = ant =>
            {
                Messenger.Default.Send(new LoadingMsg());
                if (ant.Exception != null)
                {
                    _Log.Error(ant.Exception);
                    return;
                }
                PostGetFundNo(ant.Result);
            };

            TaskUtil.TaskHelper.Create<string[]>(taskBody, taskContinue);

        }


        private void PostGetFundNoFirstStep(int recordLength)
        {
            _Log.InfoFormat("GetDepositCode: {0}",recordLength);
            this.DataCol.Clear();
            this._DataColTo.Clear();
            this._DataColFrom.Clear();
            string msg = FlexUI.FormatFundStatus(recordLength, 0);
            Messenger.Default.Send(new UpdateFundStatusMsg(msg));
        }

        private void PostGetFundNoSecondStep(string[] data)
        {
            if (data == null) return;
            Messenger.Default.Send(new UncheckedFundCheckAllButton());
            foreach (var item in data)
            {
                var model = new FundData { Code = item, IsChecked = false };
                this.DataCol.Add(model);
            }
            Regex regex = new Regex(@"^\d+$");
            if (!this.DataCol.Any(m => regex.IsMatch(m.Code))) return;
            foreach (var item in data)
            {
                var model = new FundData { Code = item, IsChecked = false };
                this._DataColTo.Add(model);
                this._DataColFrom.Add(model);
            }
        }

        private void PostGetFundNoFinalStep(string[] data)
        {
            AddSelectByRangeItem();
        }

        private void RemoveSelectByRangeItem()
        {
            if (this.FundSelectCol.Count == 2)
            {
                this.FundSelectCol.RemoveAt(1);
            }
        }

        private void AddSelectByRangeItem()
        {
            if (this.FundSelectCol.Count == 1)
            {
                this.FundSelectCol.Add(Enum.GetName(typeof(SelectTypeEnum), SelectTypeEnum.ByRange));
            }
        }


        public void PostGetFundNo(string[] data)
        {
            PostGetFundNoFirstStep(data.Length);
            PostGetFundNoSecondStep(data);
            PostGetFundNoFinalStep(data);
        }


        private string GetFundNoStrByItem()
        {
            return  this.DataCol.Where(c => c.IsChecked).Aggregate(string.Empty, (r, n) => r + "," + n.Code, r => r.Length > 0 ? r.Substring(1) : r);
        }

        private string GetFundNoStrByRange()
        {
            string fundStr = null;
            int fromFundNo;
            int toFundNo;
            try
            {
                var fundNo = ParseFundNoRange();
                fromFundNo = fundNo.Item1;
                toFundNo = fundNo.Item2;
            }
            catch (Exception ex)
            {
                _Log.Error(ex);
                return null;
            }
            Tuple<bool, string> result = XmlProccessor.GetContinuousCustomerNo(fromFundNo.ToString(), toFundNo.ToString());
            fundStr = result.Item2;
            if (!result.Item1)
            {
                MessageBox.Show("The range is too large , please shrink");
                return null;
            }
            return fundStr;
        }

        private Tuple<int, int> ParseFundNoRange()
        {
            if (this._UserInputFromFundNo.IsNullOrEmpty())
            {
                MessageBox.Show("FromFundNo can't be empty");
                throw new ArgumentNullException("FromFundNo is empty");
            }
            if (this._UserInputToFundNo.IsNullOrEmpty())
            {
                MessageBox.Show("ToFundNo can't be empty");
                throw new ArgumentNullException("ToFundNo is empty");
            }

            int fromFundNo;
            int toFundNo;
            if (!int.TryParse(EscapeSpace(this._UserInputFromFundNo), out fromFundNo))
            {
                MessageBox.Show("FromFundNo is not an integer");
                throw new ArgumentException("FromFundNo format is not correct");
            }
            if (!int.TryParse(EscapeSpace(this._UserInputToFundNo), out toFundNo))
            {
                MessageBox.Show("ToFundNo is not an integer");
                throw new ArgumentException("ToFundNo format is not correct");
            }

            if (toFundNo < fromFundNo)
            {
                MessageBox.Show("FromFundNo can't great than ToFundNo");
                throw new ArgumentException("FromFundNo can't great than ToFundNo");
            }
            return Tuple.Create(fromFundNo, toFundNo);
        }


        private string EscapeSpace(string input)
        {
            var first = this._SpaceRegex.Replace(input, "");
            var last = this._MultipleSpaceRegex.Replace(input, "");
            return last;
        }

        public bool IsByRange()
        {
           SelectTypeEnum fundSelectType = (SelectTypeEnum)Enum.Parse(typeof(SelectTypeEnum), this.FundSelectView.CurrentItem.ToString());
           return fundSelectType == SelectTypeEnum.ByRange;
        }

        public string GetFundNoStr()
        {
            string fundNoStr = null;
            if (!IsByRange())
            {
                fundNoStr = GetFundNoStrByItem();
            }
            else
            {
                fundNoStr = GetFundNoStrByRange();
            }
            return fundNoStr;
        }

        public void LoadFundCode()
        {
            try
            {
                QueryObject parameter;
                bool result = QueryStringManager.Default.GetUIParametersHelper(out parameter);
                if (result == false) return;
                StartGetFundCode(parameter);
            }
            catch (Exception e)
            {
                _Log.Error(e);
                this.DataCol.Clear();
            }
        }
    }
}
