using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FlexInterface.Common;
using log4net;
using ValidationLibrary;
using System.Threading;
using System.Configuration;
using System.Diagnostics;

namespace FlexInterfaceManager
{
    public static class MainWindowHelper
    {
        public static readonly int MaxCount = int.Parse(ConfigurationManager.AppSettings["MAXCOUNT"]);
        private static ILog _Log = LogManager.GetLogger(typeof(MainWindowHelper));
        public static string FormatString(string voucherType, string voucherNo, DateTime voucherDate, string accountNo, DateTime fileBeginDate, DateTime fileEndDate, string currency, decimal amountOrigin, decimal currencyRate, decimal amount, string dc)
        {
            return string.Format("{0}{1}{2}{3}    {4}{5}{6}   {7}  {8}   {9}{10}", voucherType, voucherNo,
                     voucherDate.ToString("dd/MM/yyyy"), string.Format("{0,10}", accountNo), fileBeginDate.ToString("dd/MM/yyyy"), fileEndDate.ToString("dd/MM/yyyy"), currency,
                     string.Format("{0,15:F2}", amountOrigin), string.Format("{0,10:F5}", currencyRate), string.Format("{0,15:F2}", Math.Abs(amount)), dc);
        }

        public static void WriteToFile(string content, FileStream fs, bool newline)
        {
            TextWriter tw = new StreamWriter(fs);
            tw.Write(content);
            tw.WriteLine();
            if (newline)
            {
                tw.WriteLine();
                tw.WriteLine();
                tw.WriteLine();
            }
            tw.Flush();
        }
        public static void GeneratedContent(PLData[] data, FileStream fs, int startIndex, IFlexService service, string sessionId)
        {
            string voucherType = "HVHV";
            foreach (var item in data)
            {
                string line1 = MainWindowHelper.FormatString(voucherType, XmlProccessor.GetFormatString(startIndex.ToString(), false), item.TradeDay
                    , item.CustomerCode, item.TradeDay, item.TradeDay, item.CurrencyName,
                    Math.Abs(item.Amount), item.CurrencyRate, Math.Abs(item.Amount) * item.CurrencyRate, "D");
                MainWindowHelper.WriteToFile(line1, fs, false);

                string line2 = MainWindowHelper.FormatString(voucherType, XmlProccessor.GetFormatString(startIndex.ToString(), false), item.TradeDay,
                    item.AccountingCode, item.TradeDay, item.TradeDay, item.CurrencyName,
                    Math.Abs(item.Amount), item.CurrencyRate, Math.Abs(item.Amount) * item.CurrencyRate, "C");
                MainWindowHelper.WriteToFile(line2, fs, true);
                startIndex++;
            }

        }

    }
}
