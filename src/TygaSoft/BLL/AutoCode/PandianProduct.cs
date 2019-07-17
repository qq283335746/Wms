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
    public partial class PandianProduct
    {
        private static readonly IPandianProduct dal = DataAccess.CreatePandianProduct();

        #region PandianProduct Member

        public int Insert(PandianProductInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(PandianProductInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid pandianId, Guid productId, Guid customerId)
        {
            return dal.Delete(pandianId, productId, customerId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public PandianProductInfo GetModel(Guid pandianId, Guid productId, Guid customerId)
        {
            return dal.GetModel(pandianId, productId, customerId);
        }

        public IList<PandianProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<PandianProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<PandianProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<PandianProductInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
