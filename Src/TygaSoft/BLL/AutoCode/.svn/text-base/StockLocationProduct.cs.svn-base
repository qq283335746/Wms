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
    public partial class StockLocationProduct
    {
        private static readonly IStockLocationProduct dal = DataAccess.CreateStockLocationProduct();

        #region StockLocationProduct Member

        public int Insert(StockLocationProductInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(StockLocationProductInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid stockLocationId)
        {
            return dal.Delete(stockLocationId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public StockLocationProductInfo GetModel(Guid stockLocationId)
        {
            return dal.GetModel(stockLocationId);
        }

        public IList<StockLocationProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<StockLocationProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<StockLocationProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<StockLocationProductInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
