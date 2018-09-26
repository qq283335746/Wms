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
    public partial class OrderReceiptProduct
    {
        private static readonly IOrderReceiptProduct dal = DataAccess.CreateOrderReceiptProduct();

        #region OrderReceiptProduct Member

        public int Insert(OrderReceiptProductInfo model)
        {
            return dal.Insert(model);
        }

        public int InsertByOutput(OrderReceiptProductInfo model)
        {
            return dal.InsertByOutput(model);
        }

        public int Update(OrderReceiptProductInfo model)
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

        public OrderReceiptProductInfo GetModel(Guid id)
        {
            return dal.GetModel(id);
        }

        public IList<OrderReceiptProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<OrderReceiptProductInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
