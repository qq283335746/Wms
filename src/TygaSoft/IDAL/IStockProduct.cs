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
        
        int UpdateWarnMsg(Guid productId, Guid customerId, string warnMsg);

        IList<OrderSelectProductInfo> GetSelectProductList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockProductInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<StockProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms);

        DataSet GetExportData();

        #endregion
    }
}
