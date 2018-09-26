using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IMesProduct
    {
        #region IMesProduct Member

        int Insert(MesProductInfo model);

        int InsertByOutput(MesProductInfo model);

        int Update(MesProductInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        MesProductInfo GetModel(Guid id);

        IList<MesProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<MesProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<MesProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<MesProductInfo> GetList();

        #endregion
    }
}
