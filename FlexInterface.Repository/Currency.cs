using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace FlexInterface.Repository
{
    public class Currency
    {
        public virtual Guid ID { get; set; }

        public virtual string Name { get; set; }
    }

    public class CurrencyMap : ClassMap<Currency>
    {
        public CurrencyMap()
        {
            Id(x => x.ID);
            Map(x => x.Name);
        }
    }


    public class AccountingCurrencyRate
    {
        public virtual Guid SourceCurrencyID { get; set; }
        public virtual Guid TargetCurrencyID { get; set; }
        public virtual double BookRate { get; set; }

        public override int GetHashCode()
        {
            return (SourceCurrencyID + "|" + TargetCurrencyID).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as AccountingCurrencyRate;
            if (t == null) return false;
            return t.SourceCurrencyID == SourceCurrencyID && t.TargetCurrencyID == TargetCurrencyID;
        }
    }

    public class AccountingCurrencyRateMap : ClassMap<AccountingCurrencyRate>
    {
        public AccountingCurrencyRateMap()
        {
            CompositeId().KeyProperty(x => x.SourceCurrencyID).KeyProperty(x => x.TargetCurrencyID);
            Map(x => x.BookRate);
        }
    }
}