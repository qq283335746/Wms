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

        float[] GetTotalByOrders(string orderIds);

        IList<OrderSendProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderSendProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms);

        bool IsExist(Guid orderId, Guid productId);

        #endregion
    }
}
