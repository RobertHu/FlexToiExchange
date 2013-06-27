using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;
using FlexInterface.Common;
namespace FlexInterface.Repository
{
    public class DataRepository
    {
        public static IEnumerable<string> GetAccountMt4LoginID()
        {
            using (var session = SessionManager.OpenLocalSession())
            {
                return session.Query<Account>()
                              .OrderBy(m => m.MT4LoginId)
                              .Select(m => m.MT4LoginId.ToString()).ToList();

            }
        }

        public static int GetAccountingMappingMt4LoginID(int mappingType)
        {
            using (var session = SessionManager.OpenLocalSession())
            {
                return session.Query<AccountingMapping>().Where(m => m.MappingType == mappingType).Select(m => m.MT4LoginId).FirstOrDefault();
            }
        }

        public static int GetAccountingMappingMt4LoginID(int mappingType, Guid instrumentID, Guid currencyID)
        {
            using (var session = SessionManager.OpenLocalSession())
            {
                return session.Query<AccountingMapping>()
                    .Where(m => m.MappingType == mappingType && m.InstrumentId == instrumentID && m.CurrencyId == currencyID)
                    .Select(m => m.MT4LoginId)
                    .FirstOrDefault();
            }
        }



        public static IEnumerable<string> GetAccountGroups()
        {
            using (var session = SessionManager.OpenLocalSession())
            {
                var result= session.Query<Group>().Where(m => m.GroupType == "Account")
                                             .OrderBy(m => m.Code)
                                             .Select(m => m.Code) ;
                if (result == null) yield break;
                foreach (var item in result) yield return item;
            }
        }

        public static IEnumerable<AccountData> GetAccountDataByGroup(string groupCode)
        {
            using (var session = SessionManager.OpenLocalSession())
            {
                var query = from g in session.Query<Group>()
                            join gms in session.Query<GroupMembership>() on g.ID equals gms.GroupID
                            join a in session.Query<Account>() on gms.MemberID equals a.ID
                            where g.GroupType == "Account"
                            where g.Code == groupCode
                            orderby a.Code
                            select new AccountData { Code = a.Code, MT4LoginID = a.MT4LoginId };
                if (query == null) yield break;
                foreach (var item in query) yield return item;
            }
        }

        public static double GetAccountingRate(Guid sourceCurrencyId, Guid targetCurrencyId)
        {
            using (var session = SessionManager.OpenLocalSession())
            {
               
                var query = session.Query<AccountingCurrencyRate>()
                                   .Where(m => (m.SourceCurrencyID == sourceCurrencyId && m.TargetCurrencyID == targetCurrencyId) || (m.TargetCurrencyID == sourceCurrencyId && m.SourceCurrencyID == targetCurrencyId))
                                   .Select(m => m.BookRate);
                return query.Max();
            }
        }

    }
}