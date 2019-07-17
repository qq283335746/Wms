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
    public partial class OrderSendProduct
    {
        private static readonly IOrderSendProduct dal = DataAccess.CreateOrderSendProduct();

        #region OrderSendProduct Member

        public int Insert(OrderSendProductInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(OrderSendProductInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid orderId, Guid productId, Guid customerId)
        {
            return dal.Delete(orderId, productId, customerId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public OrderSendProductInfo GetModel(Guid orderId, Guid productId, Guid customerId)
        {
            return dal.GetModel(orderId, productId, customerId);
        }

        public IList<OrderSendProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderSendProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderSendProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<OrderSendProductInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
