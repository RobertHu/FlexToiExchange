using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FlexInterface.Common
{

    [DataContract]
    public class DepositData
    {
        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public string AccountCode { get; set; }

        [DataMember]
        public string AccountName { get; set; }

        [DataMember]
        public string MT4Code { get; set; }

        [DataMember]
        public string TransactionNo { get; set; }

        [DataMember]
        public string SourceCustomerCode { get; set; }

        [DataMember]
        public string SourceCustomerName { get; set; }

        [DataMember]
        public string SourceCustomerAliasCode { get; set; }

        [DataMember]
        public string TargetCustomerCode { get; set; }
        [DataMember]
        public decimal SourceAmount { get; set; }
        [DataMember]
        public Guid SourceCurrency { get; set; }
        [DataMember]
        public decimal SourceCurrencyRate { get; set; }
        [DataMember]
        public string SourceCurrencyName { get; set; }
        [DataMember]
        public string TargetMt4 { get; set; }
        [DataMember]
        public decimal TargetAmount { get; set; }
        [DataMember]
        public Guid TargetCurrency { get; set; }
        [DataMember]
        public decimal TargetCurrencyRate { get; set; }
        [DataMember]
        public string TargetCurrencyName { get; set; }

        [DataMember]
        public string BankCode { get; set; }

        [DataMember]
        public string BankName { get; set; }

        [DataMember]
        public DateTime UpdateTime { get; set; }
        [DataMember]
        public long MarginReference { get; set; }

        [DataMember]
        public string BaseCurrencyName { get; set; }

        [DataMember]
        public string iExchangePlMT4LoginID { get; set; }

        [DataMember]
        public DepositData Transfer { get; set; }

        [DataMember]
        public string AccountingDesription { get; set; }


        [DataMember]
        public DepositFormatEnum FormatType { get; set; }


        [DataMember]
        public double MonthlyRate { get; set; }

        [DataMember]
        public double DayTradeRate { get; set; }

        [DataMember]
        public string Desc5 { get; set; }

        [DataMember]
        public string Desc6 { get; set; }

        [DataMember]
        public string Desc7 { get; set; }

        [DataMember]
        public string Desc8 { get; set; }

        [DataMember]
        public string Desc9 { get; set; }
    }




    [DataContract]
    public class LoginResult
    {
        [DataMember]
        public bool IsSuccess { get; set; }
        [DataMember]
        public string SessionId { get; set; }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Pwd { get; set; }

        [DataMember]
        public DateTime LastModifedDate { get; set; }
        [DataMember]
        public UserOperateEnum Operation { get; set; }
    }

    public enum UserOperateEnum
    {
        Added,
        Deleted,
        Updated
    }


    public enum DepositFormatEnum
    {
        None,
        First,
        Second,
        Third,
        End
    }

   

}
