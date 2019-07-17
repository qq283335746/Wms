using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ISiteMenus
    {
        #region ISiteMenus Member

        int Insert(SiteMenusInfo model);

        int InsertByOutput(SiteMenusInfo model);

        int Update(SiteMenusInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        SiteMenusInfo GetModel(Guid id);

        IList<SiteMenusInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMenusInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMenusInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMenusInfo> GetList();

        #endregion
    }
}
