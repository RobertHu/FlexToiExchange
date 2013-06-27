using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace FlexInterface.Repository
{
    public class Customer
    {
        public virtual Guid ID { get; set; }
        public virtual string Name { get; set; }
    }

    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(m => m.ID);
            Map(x => x.Name);
        }
    }




    public class Bank
    {
        public virtual Guid ID { get; set; }
        public virtual Guid CurrencyID { get; set; }
        public virtual string MT4LoginId { get; set; }
    }


    public class BankMap : ClassMap<Bank>
    {
        public BankMap()
        {
            Id(x => x.ID);
            Map(x => x.CurrencyID);
            Map(x => x.MT4LoginId);
        }
    }
}