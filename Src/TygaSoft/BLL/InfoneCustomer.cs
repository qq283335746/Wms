using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class InfoneCustomer
    {
        #region Customer Member

        public IList<InfoneCustomerInfo> GetCustomersByProjectId(Guid projectId)
        {
            var sqlWhere = new StringBuilder(@"and Id in (select prp.CustomerId from ProjectReportPrepare prp where prp.Id = @ProjectId group by prp.CustomerId) ");
            var parm = new SqlParameter("@ProjectId", SqlDbType.UniqueIdentifier);
            parm.Value = projectId;
            return dal.GetList(sqlWhere.ToString(), parm);
        }

        public IList<InfoneCustomerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
