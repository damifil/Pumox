using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;

namespace RecruitTask.Models
{
    public static class SessionFactoryBuilder
    {
        public static ISessionFactory BuildSessionFactory(string connectionStringName, bool create = false, bool update = false)
        {
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionStringName))
                .Mappings(m =>
                {

                    m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly());
                    m.FluentMappings.Conventions.Add(AutoImport.Never());
                }
                
                ).CurrentSessionContext("call").ExposeConfiguration(cfg => BuildSchema(cfg, create, update)).BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config, bool create = false, bool update = false)
        {
            if (create)
            {
                new SchemaExport(config).Create(false, true);
            }
            else
            {
                new SchemaUpdate(config).Execute(false, update);
            }
        }
    }
}
