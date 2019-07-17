using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IFeatureUser
    {
        #region IFeatureUser Member

        int Insert(FeatureUserInfo model);

        int Update(FeatureUserInfo model);

        int Delete(Guid userId, Guid featureId);

        bool DeleteBatch(IList<object> list);

        FeatureUserInfo GetModel(Guid userId, Guid featureId);

        IList<FeatureUserInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<FeatureUserInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<FeatureUserInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<FeatureUserInfo> GetList();

        #endregion
    }
}
