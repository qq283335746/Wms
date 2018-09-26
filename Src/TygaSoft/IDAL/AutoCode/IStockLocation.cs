using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IStockLocation
    {
        #region IStockLocation Member

        int Insert(StockLocationInfo model);

        int InsertByOutput(StockLocationInfo model);

        int Update(StockLocationInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        StockLocationInfo GetModel(Guid id);

        IList<StockLocationInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockLocationInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockLocationInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockLocationInfo> GetList();

        #endregion
    }
}
