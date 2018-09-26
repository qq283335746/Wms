using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderReceipt
    {
        #region IOrderReceipt Member

        IList<CombogridInfo> GetCbgList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        int SetNext(Guid Id, string orderCode, bool isStopNext);

        object GetOrderId(string orderNum);

        bool IsExistProduct(object orderId);

        OrderReceiptInfo GetModelByJoin(object Id);

        IList<OrderReceiptInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        OrderReceiptInfo GetModel(string orderCode);

        #endregion
    }
}
