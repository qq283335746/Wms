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
    public partial class OrderRandom
    {
        private static readonly IOrderRandom dal = DataAccess.CreateOrderRandom();

        #region OrderRandom Member

        public int Insert(OrderRandomInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(OrderRandomInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(string orderCode)
        {
            return dal.Delete(orderCode);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public OrderRandomInfo GetModel(string orderCode)
        {
            return dal.GetModel(orderCode);
        }

        public IList<OrderRandomInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderRandomInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderRandomInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<OrderRandomInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
