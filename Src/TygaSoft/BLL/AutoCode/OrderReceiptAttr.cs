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
    public partial class OrderReceiptAttr
    {
        private static readonly IOrderReceiptAttr dal = DataAccess.CreateOrderReceiptAttr();

        #region OrderReceiptAttr Member

        public int Insert(OrderReceiptAttrInfo model)
        {
            return dal.Insert(model);
        }

        public int InsertByOutput(OrderReceiptAttrInfo model)
        {
            return dal.InsertByOutput(model);
        }

        public int Update(OrderReceiptAttrInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid orderId)
        {
            return dal.Delete(orderId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public OrderReceiptAttrInfo GetModel(Guid orderId)
        {
            return dal.GetModel(orderId);
        }

        public IList<OrderReceiptAttrInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptAttrInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptAttrInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<OrderReceiptAttrInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
