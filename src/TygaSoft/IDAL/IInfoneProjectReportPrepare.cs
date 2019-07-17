using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IInfoneProjectReportPrepare
    {
        #region IProjectReportPrepare Member

        InfoneProjectReportPrepareInfo GetModelByJoin(Guid id);

        IList<InfoneProjectReportPrepareInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<InfoneProjectReportPrepareInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
