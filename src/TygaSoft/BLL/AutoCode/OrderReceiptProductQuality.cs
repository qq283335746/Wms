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
    public partial class OrderReceiptProductQuality
    {
        private static readonly IOrderReceiptProductQuality dal = DataAccess.CreateOrderReceiptProductQuality();

        #region OrderReceiptProductQuality Member

        public int Insert(OrderReceiptProductQualityInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(OrderReceiptProductQualityInfo model)
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

        public OrderReceiptProductQualityInfo GetModel(Guid orderProductId)
        {
            return dal.GetModel(orderProductId);
        }

        public IList<OrderReceiptProductQualityInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptProductQualityInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<OrderReceiptProductQualityInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<OrderReceiptProductQualityInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
