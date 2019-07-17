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
    public partial class FeatureUser
    {
        private static readonly IFeatureUser dal = DataAccess.CreateFeatureUser();

        #region FeatureUser Member

        public int Insert(FeatureUserInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(FeatureUserInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid userId, Guid featureId)
        {
            return dal.Delete(userId, featureId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public FeatureUserInfo GetModel(Guid userId, Guid featureId)
        {
            return dal.GetModel(userId, featureId);
        }

        public IList<FeatureUserInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<FeatureUserInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<FeatureUserInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<FeatureUserInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
