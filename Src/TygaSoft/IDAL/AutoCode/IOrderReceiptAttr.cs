using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderReceiptAttr
    {
        #region IOrderReceiptAttr Member

        int Insert(OrderReceiptAttrInfo model);

		int InsertByOutput(OrderReceiptAttrInfo model);

        int Update(OrderReceiptAttrInfo model);

        int Delete(Guid orderId);

        bool DeleteBatch(IList<object> list);

        OrderReceiptAttrInfo GetModel(Guid orderId);

        IList<OrderReceiptAttrInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptAttrInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptAttrInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptAttrInfo> GetList();

        #endregion
    }
}
