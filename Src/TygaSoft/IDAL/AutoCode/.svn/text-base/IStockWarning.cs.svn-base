using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IStockWarning
    {
        #region IStockWarning Member

        int Insert(StockWarningInfo model);

        int InsertByOutput(StockWarningInfo model);

        int Update(StockWarningInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        StockWarningInfo GetModel(Guid id);

        IList<StockWarningInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockWarningInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockWarningInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockWarningInfo> GetList();

        #endregion
    }
}
