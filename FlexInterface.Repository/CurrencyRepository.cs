using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
namespace FlexInterface.Repository
{
    public static class CurrencyRepository
    {
        public static string GetCurrencyName(Guid id)
        {
            using (var session = SessionManager.OpenLocalSession())
            {
                return session.Get<Currency>(id).Name;
            }
        }

        public static Guid? GetAccountingBaseCurrency()
        {
            using (var session = SessionManager.OpenLocalSession())
            {
                return session.Query<SystemParameter>().Single().AccountingBaseCurrencyId;
            }
        }
    }
}