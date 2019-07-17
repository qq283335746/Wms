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
    public partial class OrderPicked
    {
        private static readonly IOrderPicked dal = DataAccess.CreateOrderPicked();

        #region OrderPicked Member

        public int Insert(OrderPickedInfo model)
        {
            return dal.Insert(model);
        }

        public int InsertByOutput(OrderPickedInfo model)
        {
            return dal.InsertByOutput(model);
        }

        public int Update(OrderPickedInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid id)
        {
            return dal.Delete(id);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public OrderPickedInfo GetModel(Guid id)
        {
            return dal.GetModel(id);
        }

        public IList<OrderPickedInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderPickedInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderPickedInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<OrderPickedInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
