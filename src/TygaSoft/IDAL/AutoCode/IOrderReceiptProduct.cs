using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderReceiptProduct
    {
        #region IOrderReceiptProduct Member

        int Insert(OrderReceiptProductInfo model);

        int InsertByOutput(OrderReceiptProductInfo model);

        int Update(OrderReceiptProductInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        OrderReceiptProductInfo GetModel(Guid id);

        IList<OrderReceiptProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductInfo> GetList();

        #endregion
    }
}
