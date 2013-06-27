using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Diagnostics;
using System;

namespace FlexInterface.Repository
{
    public class SessionManager
    {
        private static ISessionFactory LocalSessionFactory { get; set; }

        public static void InitializeLocalSessionFactory(string connectionstr)
        {
            LocalSessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(connectionstr)
                              .ShowSql()
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<SessionManager>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .BuildSessionFactory();
        }

        public static ISession OpenLocalSession()
        {
            return LocalSessionFactory.OpenSession();
        }

    }
}