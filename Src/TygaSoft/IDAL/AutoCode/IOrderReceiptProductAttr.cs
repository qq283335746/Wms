using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderReceiptProductAttr
    {
        #region IOrderReceiptProductAttr Member

        int Insert(OrderReceiptProductAttrInfo model);

        int Update(OrderReceiptProductAttrInfo model);

        int Delete(Guid orderProductId);

        bool DeleteBatch(IList<object> list);

        OrderReceiptProductAttrInfo GetModel(Guid orderProductId);

        IList<OrderReceiptProductAttrInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductAttrInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductAttrInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductAttrInfo> GetList();

        #endregion
    }
}
