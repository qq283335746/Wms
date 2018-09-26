using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ISiteRoles
    {
        #region ISiteRoles Member

        SiteRolesInfo GetAspnetModel(string appName, string name);

        List<SiteRolesInfo> GetAspnetList(string appName, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
