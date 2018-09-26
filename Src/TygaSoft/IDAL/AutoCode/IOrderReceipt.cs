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

        int Insert(OrderReceiptInfo model);

        int InsertByOutput(OrderReceiptInfo model);

        int Update(OrderReceiptInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        OrderReceiptInfo GetModel(Guid id);

        IList<OrderReceiptInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptInfo> GetList();

        #endregion
    }
}
