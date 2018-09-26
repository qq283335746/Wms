using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IPandian
    {
        #region IPandian Member

        int Insert(PandianInfo model);

        int InsertByOutput(PandianInfo model);

        int Update(PandianInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        PandianInfo GetModel(Guid id);

        IList<PandianInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<PandianInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<PandianInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<PandianInfo> GetList();

        #endregion
    }
}
