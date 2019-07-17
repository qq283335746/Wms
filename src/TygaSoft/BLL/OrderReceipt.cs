using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class OrderReceipt
    {
        #region OrderReceipt Member

        public IList<CombogridInfo> GetCbgOrderReceipt(int pageIndex, int pageSize,out int totalRecords, int orderType)
        {
            var sqlWhere = @"and IsStopNext = @IsStopNext and OrderType = @OrderType ";
            SqlParameter[] parms = {
                new SqlParameter("@IsStopNext",SqlDbType.Bit),
                new SqlParameter("@OrderType",orderType)
            };
            parms[0].Value = false;

            return dal.GetCbgList(pageIndex, pageSize, out totalRecords, sqlWhere, parms);
        }

        public int SetNext(Guid Id, string orderCode, bool isStopNext)
        {
            return dal.SetNext(Id, orderCode, isStopNext);
        }

        public object GetOrderId(string orderNum)
        {
            var orderId = dal.GetOrderId(orderNum);
            if (orderId == null) throw new ArgumentNullException(orderNum);
            return orderId;
        }

        public bool IsExistProduct(object orderId)
        {
            return dal.IsExistProduct(orderId);
        }

        public OrderReceiptInfo GetModelByJoin(object Id)
        {
            return dal.GetModelByJoin(Id);
        }

        public OrderReceiptInfo GetModel(string orderCode)
        {
            return dal.GetModel(orderCode);
        }

        public IList<OrderReceiptInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
