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
    public partial class OrderMap
    {
        private static readonly IOrderMap dal = DataAccess.CreateOrderMap();

        #region OrderMap Member

        public int Insert(OrderMapInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(OrderMapInfo model)
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

        public OrderMapInfo GetModel(string orderCode)
        {
            return dal.GetModel(orderCode);
        }

        public IList<OrderMapInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderMapInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderMapInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<OrderMapInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
