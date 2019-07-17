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
    public partial class OrderReceiptRecord
    {
        private static readonly IOrderReceiptRecord dal = DataAccess.CreateOrderReceiptRecord();

        #region OrderReceiptRecord Member

        public int Insert(OrderReceiptRecordInfo model)
        {
            return dal.Insert(model);
        }

        public int InsertByOutput(OrderReceiptRecordInfo model)
        {
            return dal.InsertByOutput(model);
        }

        public int Update(OrderReceiptRecordInfo model)
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

        public OrderReceiptRecordInfo GetModel(Guid id)
        {
            return dal.GetModel(id);
        }

        public IList<OrderReceiptRecordInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptRecordInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptRecordInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<OrderReceiptRecordInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
