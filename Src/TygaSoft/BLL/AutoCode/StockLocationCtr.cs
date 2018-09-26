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
    public partial class StockLocationCtr
    {
        private static readonly IStockLocationCtr dal = DataAccess.CreateStockLocationCtr();

        #region StockLocationCtr Member

        public int Insert(StockLocationCtrInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(StockLocationCtrInfo model)
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

        public StockLocationCtrInfo GetModel(Guid stockLocationId)
        {
            return dal.GetModel(stockLocationId);
        }

        public IList<StockLocationCtrInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<StockLocationCtrInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<StockLocationCtrInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<StockLocationCtrInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
