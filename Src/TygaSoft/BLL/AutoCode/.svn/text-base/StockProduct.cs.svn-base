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
    public partial class StockProduct
    {
        private static readonly IStockProduct dal = DataAccess.CreateStockProduct();

        #region StockProduct Member

        public int Insert(StockProductInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(StockProductInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid productId, Guid customerId)
        {
            return dal.Delete(productId, customerId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public StockProductInfo GetModel(Guid productId, Guid customerId)
        {
            return dal.GetModel(productId, customerId);
        }

        public IList<StockProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<StockProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<StockProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<StockProductInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
