using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ISiteMenusAccess
    {
        #region ISiteMenusAccess Member

        int Insert(SiteMenusAccessInfo model);

        int Update(SiteMenusAccessInfo model);

        int Delete(Guid applicationId, Guid accessId);

        bool DeleteBatch(IList<object> list);

        SiteMenusAccessInfo GetModel(Guid applicationId, Guid accessId);

        IList<SiteMenusAccessInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMenusAccessInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMenusAccessInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMenusAccessInfo> GetList();

        #endregion
    }
}
