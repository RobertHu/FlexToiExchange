using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace FlexInterfaceManager.Mediator
{
    public class ControlParameter
    {
        public ComboBox DepositWithDrawTranferType { get; set; }
        public ComboBox DepositCodeSelectType { get; set; }
        public GroupBox DepositCodeSelectByItemPanel { get; set; }
        public StackPanel DepositCodeSelectByRangePanel { get; set; }
        public Grid DepositCodeSelectByItem{ get; set; }
        public StackPanel DepositCodeSelectByRange{ get; set; }
        public Button QueryFundBtn { get; set; }

    }
}
