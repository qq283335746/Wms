using System.Reflection;
using System.Configuration;
using TygaSoft.ICacheDependency;

namespace TygaSoft.CacheDependencyFactory
{
    public static class DependencyAccess
    {
        private static IMsSqlCacheDependency LoadInstance(string className)
        {
            string[] paths = ConfigurationManager.AppSettings["CacheDependencyAssembly"].Split(',');
            string fullyQualifiedClass = paths[0] + "." + className;

            return (IMsSqlCacheDependency)Assembly.Load(paths[1]).CreateInstance(fullyQualifiedClass);
        }

        public static IMsSqlCacheDependency CreateMenusDependency()
        {
            return LoadInstance("Menus");
        }

    }
}
