using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace FlexInterface.Repository
{
    public class Deposit
    {
        public virtual Guid ID { get; set; }
        public virtual string Code { get; set; }
        public virtual Guid AccountID { get; set; }
        public virtual Guid CurrencyID { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual Guid TargetCurrencyID { get; set; }
        public virtual decimal TargetAmount { get; set; }
        public virtual DateTime UpdateTime { get; set; }
        public virtual Guid BankId { get; set; }
        public virtual decimal AmountRecevided { get; set; }
        public virtual bool Marker { get; set; }
        public virtual string AccountingDesription { get; set; }
        public virtual long MarginReference { get; set; }
    }

    public class DepositMap : ClassMap<Deposit>
    {
        public DepositMap()
        {
            Id(x => x.ID);
            Map(x => x.Code);
            Map(x => x.AccountID);
            Map(x => x.CurrencyID);
            Map(x => x.Amount);
            Map(x => x.TargetCurrencyID);
            Map(x => x.TargetAmount);
            Map(x => x.UpdateTime);
            Map(x => x.BankId);
            Map(x => x.AmountRecevided);
            Map(x => x.Marker);
            Map(x => x.AccountingDesription);
            Map(x => x.MarginReference);
        }
    }
}