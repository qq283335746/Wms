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

        OrderReceiptProductInfo GetOrderProductModelById(Guid Id);

        OrderReceiptProductInfo GetModelByProductcode(object orderId, string productCode);

        OrderReceiptProductInfo GetModel(object orderId, object productId);

        int UpdateQty(object orderId, object productId, double receiptAmount);

        bool DeleteBatchByJoin(IList<object> list);

        OrderReceiptProductInfo GetModelByJoin(object Id);

        IList<OrderReceiptProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
