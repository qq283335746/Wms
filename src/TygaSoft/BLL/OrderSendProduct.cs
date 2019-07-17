using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class OrderSendProduct
    {
        #region OrderSendProduct Member

        public float[] GetTotalByOrders(string orderIds)
        {
            return dal.GetTotalByOrders(orderIds);
        }

        public IList<OrderSendProductInfo> GetListByOrderId(Guid orderId)
        {
            var sqlWhere = "and OrderId = @OrderId ";
            var parm = new SqlParameter("@OrderId", orderId);
            return dal.GetList(sqlWhere, parm);
        }

        public IList<OrderSendProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderSendProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(sqlWhere, cmdParms);
        }

        public bool IsExist(Guid orderId, Guid productId)
        {
            return dal.IsExist(orderId, productId);
        }

        #endregion
    }
}
