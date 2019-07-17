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
    public partial class OrderPickProduct
    {
        private static readonly IOrderPickProduct dal = DataAccess.CreateOrderPickProduct();

        #region OrderPickProduct Member

        public int Insert(OrderPickProductInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(OrderPickProductInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid orderPickId, Guid orderId, Guid productId, Guid customerId)
        {
            return dal.Delete(orderPickId, orderId, productId, customerId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public OrderPickProductInfo GetModel(Guid orderPickId, Guid orderId, Guid productId, Guid customerId)
        {
            return dal.GetModel(orderPickId, orderId, productId, customerId);
        }

        public IList<OrderPickProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderPickProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderPickProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<OrderPickProductInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
