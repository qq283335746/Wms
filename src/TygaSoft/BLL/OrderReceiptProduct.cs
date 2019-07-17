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
    public partial class OrderReceiptProduct
    {
        #region OrderReceiptProduct Member

        public IList<OrderReceiptProductInfo> GetListByOrderId(Guid orderId)
        {
            var sqlWhere = "and OrderId = @OrderId ";
            var parm = new SqlParameter("@OrderId", orderId);
            return dal.GetList(sqlWhere, parm);
        }

        public OrderReceiptProductInfo GetOrderProductModelById(Guid Id)
        {
            return dal.GetOrderProductModelById(Id);
        }

        public OrderReceiptProductInfo GetModelByProductcode(object orderId, string productCode)
        {
            return dal.GetModelByProductcode(orderId,productCode);
        }

        public OrderReceiptProductInfo GetModel(object orderId, object productId)
        {
            return dal.GetModel(orderId, productId);
        }

        public int UpdateQty(object orderId, object productId, double receiptAmount)
        {
            return dal.UpdateQty(orderId, productId, receiptAmount);
        }

        public IList<OrderReceiptProductInfo> GetListJoinByOrderCode(string orderCode)
        {
            var sqlWhere = "and OrderId = (select Id from OrderReceipt where OrderCode = @OrderCode) ";
            var parm = new SqlParameter("@OrderCode", SqlDbType.VarChar, 20);
            parm.Value = orderCode;
            return dal.GetListByJoin(sqlWhere, parm);
        }

        public IList<OrderReceiptProductInfo> GetListJoinByOrder(object orderId)
        {
            var sqlWhere = @" and orp.OrderId = @OrderId ";
            var parm = new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(orderId.ToString());
            return dal.GetListByJoin(sqlWhere, parm);
        }

        public IList<OrderReceiptProductInfo> GetListByOrder(int pageIndex, int pageSize, out int totalRecords, object orderId)
        {
            var sqlWhere = @" and orp.OrderId = @OrderId ";
            var parm = new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(orderId.ToString());
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, parm);
        }

        public IList<OrderReceiptProductInfo> GetListByOrder(int pageIndex, int pageSize, object orderId)
        {
            var sqlWhere = @" and orp.OrderId = @OrderId ";
            var parm = new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(orderId.ToString());
            return dal.GetListByJoin(pageIndex, pageSize, sqlWhere, parm);
        }

        public IList<OrderReceiptProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(sqlWhere, cmdParms);
        }

        public bool DeleteBatchByJoin(IList<object> list)
        {
            return dal.DeleteBatchByJoin(list);
        }

        public OrderReceiptProductInfo GetModelByJoin(object Id)
        {
            return dal.GetModelByJoin(Id);
        }

        #endregion
    }
}
