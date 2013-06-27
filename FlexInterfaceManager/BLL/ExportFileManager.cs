using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GalaSoft.MvvmLight.Messaging;
using FlexInterfaceManager.Model;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;
using FlexInterface.Common;
using System.Threading;
using System.Windows;
using log4net;
using System.Diagnostics;
using FlexInterfaceManager.Manager;
using FlexInterfaceManager.BLL;

namespace FlexInterfaceManager
{
    public  class ExportFileManager
    {
        private  ILog _Log = LogManager.GetLogger(typeof(ExportFileManager));
        private ExportErrorHandler _ErrorHandler = new ExportErrorHandler();
       
        public ExportFileParameters GetExportFileParameters(QueryObject data)
        {
            ExportFileParameters parameters = new ExportFileParameters();
            parameters.QueryInfo = data;
            parameters.IsPL = (int)data.Type > (int)BusinessTypeEnum.Transfer;
            return parameters;
        }

        
        private void Export(object obj)
        {
            ExportFileParameters parameters = (ExportFileParameters)obj;
            this._ErrorHandler.ExportStep1(parameters.IsPL);
            QueryPageCountResult result = null;
            if (parameters.IsPL)
            {
                result = Common.Service.GetPLDataPageCount(Common.SessionId, parameters.QueryInfo);
            }
            else
            {
                result = Common.Service.GetDepositPageCount(Common.SessionId, parameters.QueryInfo);
            }

            bool isNormal = this._ErrorHandler.IsDataNormal(result);
            if (!isNormal) return;

            ExportIndex index = new ExportIndex() {  PageIndex=1, FileIndex=1, RecordIndex=0};

            while (index.PageIndex <= result.PageCount)
            {
                ExportBody(parameters, result, index);
            }
        }

        private void ExportBody(ExportFileParameters parameters, QueryPageCountResult result,ExportIndex index)
        {
            object[] data = GetData(parameters.IsPL,index);
           
            string continuousFileName = GetFileName(parameters.QueryInfo.FilePath, index, result.PageCount);
          
            using (FileStream fs = new FileStream(continuousFileName, FileMode.Create, FileAccess.Write))
            {
                int startFileIndex = index.RecordIndex * 150 + 1;
                if (parameters.IsPL)
                {
                    PLExportCenter.Default.Process((PLData[])data, startFileIndex,fs);
                }
                else
                {
                    FlexInterface.Helper.FlexPersistence.PersiteDesposit((DepositData[])data, fs,startFileIndex , parameters.QueryInfo.Type);
                }
                
                this._ErrorHandler.WhenExportPerPageSendMsgToUI(parameters.IsPL, index, result.PageCount);
                index.FileIndex++;
                index.PageIndex++;
                index.RecordIndex++;
               
            }
        }

        private object[] GetData(bool isPL,ExportIndex index)
        {
            object[] data = null;
            if (isPL)
            {
                data = Common.Service.GetPLDataByPage(Common.SessionId, index.PageIndex);
            }
            else
            {
                Debug.WriteLine("Deposit");
                data = Common.Service.GetDepositDataByPage(Common.SessionId, index.PageIndex);
            }
            return data;
        }

        private string GetFileName(string filePath,ExportIndex index, int pageCount)
        {
            string continuousFileName = string.Empty;
            if (pageCount> 1)
            {
                continuousFileName = filePath.Substring(0, filePath.Length - 4) + index.FileIndex + ".txt";
            }
            else
            {
                continuousFileName = filePath;
            }
            return continuousFileName;
        }

      

       

      

        public Action<Task> GetContinueAction(ExportFileParameters parameters)
        {
            return ant =>
            {
                Messenger.Default.Send(new ShowProgressMsg(false));
                string msg;
                if (ant.Exception != null)
                {
                    if (parameters.IsPL)
                    {
                        msg = StringLibrary.PLFailedTxtMsg;
                    }
                    else
                    {
                        msg = StringLibrary.DepositFailedTxtMsg;
                    }
                    _Log.Error(ant.Exception);
                    Messenger.Default.Send(new UpdateWarnInfoMsg(msg));
                    return;
                }
                if (parameters.IsPL)
                {
                    msg = StringLibrary.PLEndTxtMsg;
                }
                else
                {
                    msg = StringLibrary.DepositEndTxtMsg;
                }
                Messenger.Default.Send(new UpdateWarnInfoMsg(msg));
            };

        }


        public void HandGenerateInterfaceFile(ExportFileParameters parameters)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, e) =>
            {
                Common.SynchronizationContext.Send(m =>
                {
                    Messenger.Default.Send(new LoadingMsg());
                }, null);

                if (parameters.IsPL)
                {
                    e.Result = Common.Service.GetPLDataRecordCount(Common.SessionId, parameters.QueryInfo);
                }
                else
                {
                    e.Result = Common.Service.GetDepositRecordCount(Common.SessionId, parameters.QueryInfo);
                }
            };

            worker.RunWorkerCompleted += (o, e) =>
            {
                Common.SynchronizationContext.Send(m =>
                {
                    Messenger.Default.Send(new LoadingMsg());
                }, null);
                TaskUtil.TaskHelper.Create(Export, GetContinueAction(parameters), parameters);
            };

            worker.RunWorkerAsync();
        }

       

    }

    public class ExportIndex
    {
        public int FileIndex { get; set; }
        public int PageIndex { get; set; }
        public int RecordIndex { get; set; }
    }

    public class ExportFileParameters
    {
        public QueryObject QueryInfo { get; set; }
        public Action StartExportAction { get; set; }
        public bool IsPL { get; set; }
    }


}