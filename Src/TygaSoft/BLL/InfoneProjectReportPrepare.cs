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
    public partial class InfoneProjectReportPrepare
    {
        #region ProjectReportPrepare Member

        public IList<InfoneProjectReportPrepareInfo> GetProjectsByCustomerId(Guid customerId)
        {
            var parm = new SqlParameter("@CustomerId", SqlDbType.UniqueIdentifier);
            parm.Value = customerId;
            return dal.GetListByJoin("and prp.CustomerId = @CustomerId ", parm);
        }

        public InfoneProjectReportPrepareInfo GetModelByJoin(Guid id)
        {
            return dal.GetModelByJoin(id);
        }

        public IList<InfoneProjectReportPrepareInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<InfoneProjectReportPrepareInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(sqlWhere, cmdParms);
        }

        #endregion
    }
}
