using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IStockLocationCtr
    {
        #region IStockLocationCtr Member

        int Insert(StockLocationCtrInfo model);

        int Update(StockLocationCtrInfo model);

        int Delete(Guid stockLocationId);

        bool DeleteBatch(IList<object> list);

        StockLocationCtrInfo GetModel(Guid stockLocationId);

        IList<StockLocationCtrInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockLocationCtrInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockLocationCtrInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockLocationCtrInfo> GetList();

        #endregion
    }
}
