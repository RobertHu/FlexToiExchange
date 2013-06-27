using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace FlexInterface.Repository
{
    public class SystemParameter
    {
        public virtual Guid ID { get; set; }
        public virtual Guid AccountingBaseCurrencyId { get; set; }
    }


    public class SystemParameterMap : ClassMap<SystemParameter>
    {
        public SystemParameterMap()
        {
            Id(x => x.ID);
            Map(x => x.AccountingBaseCurrencyId);
        }
    }
}