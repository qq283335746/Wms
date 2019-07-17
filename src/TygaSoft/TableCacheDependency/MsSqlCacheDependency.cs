using System.Web.Caching;
using System.Configuration;
using TygaSoft.ICacheDependency;

namespace TygaSoft.TableCacheDependency
{
    public abstract class MsSqlCacheDependency : IMsSqlCacheDependency
    {
        // This is the separator that's used in web.config
        protected char[] configurationSeparator = new char[] { ',' };

        protected AggregateCacheDependency dependency = new AggregateCacheDependency();

        /// <summary>
        /// The constructor retrieves all related configuration and add CacheDependency object accordingly
        /// </summary>
        /// <param name="configKey">Configuration key for specific derived class implementation</param>
        protected MsSqlCacheDependency(string configKey)
        {

            string dbName = ConfigurationManager.AppSettings["CacheDatabaseName"];
            string tableConfig = ConfigurationManager.AppSettings[configKey];
            string[] tables = tableConfig.Split(configurationSeparator);

            foreach (string tableName in tables)
                dependency.Add(new SqlCacheDependency(dbName, tableName));
        }

        public AggregateCacheDependency GetDependency()
        {
            return dependency;
        }
    }
}
