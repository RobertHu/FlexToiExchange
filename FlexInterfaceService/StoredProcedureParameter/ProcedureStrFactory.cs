using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlexInterfaceService.Util;
namespace FlexInterfaceService.StoredProcedureParameter
{
    public class ProcedureStrFactory
    {
        public static string CreateQueryDepositRecordSql(QueryDepositRecordParameter parameter)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("dbo.P_GetDepositForFlex");
            AppentSqlParameter(sb,parameter.FromTradeDay.ToStandardDatetimeString());
            AppentSqlParameter(sb, parameter.ToTradeDay.ToStandardDatetimeString());
            AppentSqlParameter(sb, parameter.AccountCodeStr);
            AppentSqlParameter(sb, parameter.DepositCodeStr);
            AppentSqlParameter(sb, ((int)parameter.PaymentType).ToString());
            AppentSqlParameter(sb, parameter.IsGetRecordCount.ToInt32().ToString());
            return sb.ToString(0, sb.Length - 1);
        }

        public static string CreateQueryDepositCodeSql(QueryDepositCodeParameter parameter)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("dbo.Flex_GetDepositCode");
            AppentSqlParameter(sb, parameter.FromTradeDay.ToStandardDatetimeString());
            AppentSqlParameter(sb, parameter.ToTradeDay.ToStandardDatetimeString());
            AppentSqlParameter(sb, parameter.AccountCodeStr);
            AppentSqlParameter(sb, ((int)parameter.PaymentType).ToString());
            return sb.ToString(0, sb.Length - 1);
        }

        public static string CreateQueryPlSql(PLPrameter parameter)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("dbo.P_GetMoneyFlowForFlex");
            AppentSqlParameter(sb, parameter.FromTradeDay.ToTradeDayStyle());
            AppentSqlParameter(sb, parameter.ToTradeDay.ToTradeDayStyle());
            AppentSqlParameter(sb, parameter.AccountCodeString);
            int plType = (int)parameter.Type - 3;
            AppentSqlParameter(sb, plType.ToString());
            AppentSqlParameter(sb, parameter.IsGetRecordCount.ToInt32().ToString());
            return sb.ToString(0, sb.Length - 1);
        }

        private static void AppentSqlParameter(System.Text.StringBuilder sb, string parameter)
        {
            sb.Append(string.Format("'{0}'", parameter));
            sb.Append(",");
        }
    }
}