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
    public partial class SiteMenusAccess
    {
        private static readonly ISiteMenusAccess dal = DataAccess.CreateSiteMenusAccess();

        #region SiteMenusAccess Member

        public int Insert(SiteMenusAccessInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(SiteMenusAccessInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid applicationId, Guid accessId)
        {
            return dal.Delete(applicationId, accessId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public SiteMenusAccessInfo GetModel(Guid applicationId, Guid accessId)
        {
            return dal.GetModel(applicationId, accessId);
        }

        public IList<SiteMenusAccessInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<SiteMenusAccessInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<SiteMenusAccessInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<SiteMenusAccessInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
