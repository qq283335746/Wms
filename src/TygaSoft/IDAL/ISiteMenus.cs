using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ISiteMenus
    {
        #region ISiteMenus Member

        IList<SiteMenusInfo> GetListByParentName(string parentName);

        IList<SiteMenusInfo> GetMenusAccess(string appName, string[] accessIds, bool isAdministrators);

        List<SiteMenusInfo> GetMenusAccess(Guid appId, Guid accessId, bool isAdministrators);

        #endregion
    }
}
