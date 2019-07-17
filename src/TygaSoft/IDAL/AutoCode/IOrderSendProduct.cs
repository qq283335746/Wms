using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderSendProduct
    {
        #region IOrderSendProduct Member

        int Insert(OrderSendProductInfo model);

        int Update(OrderSendProductInfo model);

        int Delete(Guid orderId, Guid productId, Guid customerId);

        bool DeleteBatch(IList<object> list);

        OrderSendProductInfo GetModel(Guid orderId, Guid productId, Guid customerId);

        IList<OrderSendProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderSendProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderSendProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderSendProductInfo> GetList();

        #endregion
    }
}
