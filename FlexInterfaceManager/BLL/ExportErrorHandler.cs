using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexInterface.Common;
using FlexInterfaceManager.Manager;
using FlexInterfaceManager.Model;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace FlexInterfaceManager.BLL
{
   public class ExportErrorHandler
    {
       public bool IsDataNormal(QueryPageCountResult result)
       {
           ReturnType type = result.Type;
           if (type == ReturnType.Normal && result.PageCount > 0) return true;
           string errorMsg = string.Empty;

           if (type == ReturnType.NoData)
           {
               errorMsg = "No Data";
           }
           else if (type == ReturnType.DataNotComplete)
           {
               errorMsg = "Some record's data is not complete ";
           }
           else if (type == ReturnType.Error)
           {
               errorMsg = "Server error";
           }
           else
           {
               errorMsg = "No Data";
           }
           DebugHelper.Log("export  " + errorMsg);
           Common.SynchronizationContext.Send(m =>
           {
               Messenger.Default.Send(new ShowProgressMsg(false));
               MessageBox.Show(errorMsg);
           }, null);

           return false;
       }
       public bool WhenExceedMaxRecordCount(int count)
       {
           if (count >= 500)
           {
               MessageBoxResult result = MessageBox.Show(string.Format(StringLibrary.ExceedMaxRecordCountWarnInfo, 500), "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
               if (result == MessageBoxResult.No) return false;
           }
           return true;
       }
       public void WhenExportPerPageSendMsgToUI(bool isPL, IntIndex index, double pageCount)
       {
           Common.SynchronizationContext.Send(ar =>
           {
               string msg = string.Empty;
               if (isPL)
               {
                   msg = StringLibrary.PLGeneratingSendMsg;
               }
               else
               {
                   msg = StringLibrary.DepositGeneratingSendMsg;
               }
               Messenger.Default.Send(new ProgressMessage(string.Format(msg, index.PageIndex), index.PageIndex / pageCount * 100));
           }, null);
       }

       public void ExportStep1(bool isPL)
       {
           Common.SynchronizationContext.Send(m =>
           {
               Messenger.Default.Send(new ShowProgressMsg(true));
               string txtMsg = string.Empty;
               string sendMsg = string.Empty;
               if (isPL)
               {
                   txtMsg = StringLibrary.PLStartTxtMsg;
                   sendMsg = StringLibrary.PLStartSendMsg;
               }
               else
               {
                   txtMsg = StringLibrary.DepositStartTxtMsg;
                   sendMsg = StringLibrary.DepositStartSendMsg;
               }
               Messenger.Default.Send(new UpdateWarnInfoMsg(txtMsg));
               Messenger.Default.Send(new ProgressMessage(sendMsg, 5));

           }, null);
       }
    }
}
