using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IMesCategory
    {
        #region IMesCategory Member

        int Insert(MesCategoryInfo model);

        int InsertByOutput(MesCategoryInfo model);

        int Update(MesCategoryInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        MesCategoryInfo GetModel(Guid id);

        IList<MesCategoryInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<MesCategoryInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<MesCategoryInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<MesCategoryInfo> GetList();

        #endregion
    }
}
