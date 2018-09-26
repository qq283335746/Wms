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
    public partial class StockLocation
    {
        private static readonly IStockLocation dal = DataAccess.CreateStockLocation();

        #region StockLocation Member

        public int Insert(StockLocationInfo model)
        {
            return dal.Insert(model);
        }

        public int InsertByOutput(StockLocationInfo model)
        {
            return dal.InsertByOutput(model);
        }

        public int Update(StockLocationInfo model)
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

        public StockLocationInfo GetModel(Guid id)
        {
            return dal.GetModel(id);
        }

        public IList<StockLocationInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<StockLocationInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<StockLocationInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<StockLocationInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
