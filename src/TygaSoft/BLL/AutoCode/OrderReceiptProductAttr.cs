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
    public partial class OrderReceiptProductAttr
    {
        private static readonly IOrderReceiptProductAttr dal = DataAccess.CreateOrderReceiptProductAttr();

        #region OrderReceiptProductAttr Member

        public int Insert(OrderReceiptProductAttrInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(OrderReceiptProductAttrInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid orderProductId)
        {
            return dal.Delete(orderProductId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public OrderReceiptProductAttrInfo GetModel(Guid orderProductId)
        {
            return dal.GetModel(orderProductId);
        }

        public IList<OrderReceiptProductAttrInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptProductAttrInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptProductAttrInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<OrderReceiptProductAttrInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
