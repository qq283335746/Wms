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
    public partial class ShelfMissionProduct
    {
        private static readonly IShelfMissionProduct dal = DataAccess.CreateShelfMissionProduct();

        #region ShelfMissionProduct Member

        public int Insert(ShelfMissionProductInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(ShelfMissionProductInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid shelfMissionId, Guid orderId, Guid productId)
        {
            return dal.Delete(shelfMissionId, orderId, productId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public ShelfMissionProductInfo GetModel(Guid shelfMissionId, Guid orderId, Guid productId)
        {
            return dal.GetModel(shelfMissionId, orderId, productId);
        }

        public IList<ShelfMissionProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<ShelfMissionProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<ShelfMissionProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<ShelfMissionProductInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
