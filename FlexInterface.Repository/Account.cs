using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace FlexInterface.Repository
{
    public class Account
    {
        public virtual Guid ID { get; set; }
        public virtual string Code { get; set; }
        public virtual Guid CustomerID { get; set; }
        public virtual int MT4LoginId { get; set; }
    }

    public class AccountMapping : ClassMap<Account>
    {
        public AccountMapping()
        {
            Id(x => x.ID);
            Map(x => x.MT4LoginId);
            Map(x => x.CustomerID);
            Map(x => x.Code);
        }
    }


    public class AccountingMapping
    {
        public virtual int MappingType { get; set; }
        public virtual Guid InstrumentId { get; set; }
        public virtual Guid CurrencyId { get; set; }
        public virtual int MT4LoginId { get; set; }
        public override int GetHashCode()
        {
            return (MappingType + "|" + InstrumentId + "|" + CurrencyId).GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as AccountingMapping;
            if (t == null) return false;
            if (MappingType == t.MappingType && InstrumentId == t.InstrumentId && CurrencyId == t.CurrencyId) return true;
            return false;
        }
    }

    public class AccountingMappingMaps : ClassMap<AccountingMapping>
    {
        public AccountingMappingMaps()
        {
            CompositeId().KeyProperty(x => x.MappingType)
                         .KeyProperty(x => x.InstrumentId)
                         .KeyProperty(x => x.CurrencyId);
            Map(x => x.MT4LoginId);
        }
    }



    public class Group
    {
        public virtual Guid ID { get; set; }
        public virtual string Code { get; set; }
        public virtual string GroupType { get; set; }
    }

    public class GroupMap : ClassMap<Group>
    {
        public GroupMap()
        {
            Id(x => x.ID);
            Map(x => x.Code);
            Map(x => x.GroupType);
        }
    }


    public class GroupMembership
    {
        public virtual Guid GroupID { get; set; }
        public virtual Guid MemberID { get; set; }
        public override int GetHashCode()
        {
            return (GroupID + "|" + MemberID).GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as GroupMembership;
            if (t == null) return false;
            if (GroupID == t.GroupID && MemberID == t.MemberID) return true;
            return false;
        }
    }


    public class GroupMembershipMap : ClassMap<GroupMembership>
    {
        public GroupMembershipMap()
        {
            CompositeId().KeyProperty(x => x.GroupID).KeyProperty(x => x.MemberID);
        }
    }


    public class AccountingCurrencyRateHistory
    {
        public virtual Guid SourceCurrencyID { get; set; }
        public virtual Guid TargetCurrencyID { get; set; }
        public virtual double BookRate { get; set; }
        public virtual DateTime UpdateTime { get; set; }

        public override int GetHashCode()
        {
            return (SourceCurrencyID + "|" + TargetCurrencyID + "|" + UpdateTime).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as AccountingCurrencyRateHistory;
            if (t == null) return false;
            return t.SourceCurrencyID == SourceCurrencyID && t.TargetCurrencyID == TargetCurrencyID && t.UpdateTime == UpdateTime;
        }
    }

    public class AccountingCurrencyRateHistoryMap : ClassMap<AccountingCurrencyRateHistory>
    {
        public AccountingCurrencyRateHistoryMap()
        {
            CompositeId().KeyProperty(x => x.SourceCurrencyID)
                         .KeyProperty(x => x.TargetCurrencyID)
                         .KeyProperty(x => x.UpdateTime);

            Map(x => x.BookRate);
        }
    }
}