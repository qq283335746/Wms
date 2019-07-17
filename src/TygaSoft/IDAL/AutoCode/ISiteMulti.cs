using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ISiteMulti
    {
        #region ISiteMulti Member

        int Insert(SiteMultiInfo model);

        int InsertByOutput(SiteMultiInfo model);

        int Update(SiteMultiInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        SiteMultiInfo GetModel(Guid id);

        IList<SiteMultiInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMultiInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMultiInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<SiteMultiInfo> GetList();

        #endregion
    }
}
