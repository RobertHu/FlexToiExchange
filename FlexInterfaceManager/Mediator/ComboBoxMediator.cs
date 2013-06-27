using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using FlexInterfaceManager.Model;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using FlexInterface.Common;

namespace FlexInterfaceManager.Mediator
{
    public class ComboBoxMediator
    {
        private ComboBox _cbDepositType;
        private ComboBox _cbFundSelectType;
        private GroupBox _gbFundSelectByItemPanel;
        private StackPanel _spFundSelectByRangePanel;
        private Grid _gridFundByItem;
        private StackPanel _spFundByRange;
        private Button _btnQueryFund;
         
        public ComboBoxMediator(ControlParameter parameter)
        {
            this._cbDepositType = parameter.DepositWithDrawTranferType;
            this._cbFundSelectType = parameter.DepositCodeSelectType;
            this._gbFundSelectByItemPanel = parameter.DepositCodeSelectByItemPanel;
            this._spFundSelectByRangePanel = parameter.DepositCodeSelectByRangePanel;
            this._gridFundByItem = parameter.DepositCodeSelectByItem;
            this._spFundByRange = parameter.DepositCodeSelectByRange;
            this._btnQueryFund = parameter.QueryFundBtn;
            this._cbDepositType.SelectionChanged += cbDepositType_SelectionChanged;
            this._cbFundSelectType.SelectionChanged += cbFundSelectType_SelectionChanged;
        }

        private void cbFundSelectType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count == 0) return;
            SelectTypeEnum item = (SelectTypeEnum)Enum.Parse(typeof(SelectTypeEnum), e.AddedItems[0].ToString());
            if (item == SelectTypeEnum.Selection)
            {
                this._gridFundByItem.Visibility = Visibility.Visible;
                this._spFundByRange.Visibility = Visibility.Collapsed;
            }
            else
            {
                this._gridFundByItem.Visibility = Visibility.Collapsed;
                this._spFundByRange.Visibility = Visibility.Visible;
            }

        }

        private void cbDepositType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BusinessTypeEnum item = (BusinessTypeEnum)Enum.Parse(typeof(BusinessTypeEnum), e.AddedItems[0].ToString());
            if ((int)item <= (int)BusinessTypeEnum.Transfer)
            {
                this._gbFundSelectByItemPanel.IsEnabled = true;
                this._spFundSelectByRangePanel.IsEnabled = true;
                this._btnQueryFund.IsEnabled = true;
            }
            else
            {
                this._gbFundSelectByItemPanel.IsEnabled = false;
                this._spFundSelectByRangePanel.IsEnabled = false;
                this._btnQueryFund.IsEnabled = false;
            }

        }


    }
}
