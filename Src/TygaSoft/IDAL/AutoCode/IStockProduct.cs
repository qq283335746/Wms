using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IStockProduct
    {
        #region IStockProduct Member

        int Insert(StockProductInfo model);

        int Update(StockProductInfo model);

        int Delete(Guid productId, Guid customerId);

        bool DeleteBatch(IList<object> list);

        StockProductInfo GetModel(Guid productId, Guid customerId);

        IList<StockProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockProductInfo> GetList();

        #endregion
    }
}
