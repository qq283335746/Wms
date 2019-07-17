using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IStockLocationProduct
    {
        #region IStockLocationProduct Member

        int Insert(StockLocationProductInfo model);

        int Update(StockLocationProductInfo model);

        int Delete(Guid stockLocationId);

        bool DeleteBatch(IList<object> list);

        StockLocationProductInfo GetModel(Guid stockLocationId);

        IList<StockLocationProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockLocationProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockLocationProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockLocationProductInfo> GetList();

        #endregion
    }
}
