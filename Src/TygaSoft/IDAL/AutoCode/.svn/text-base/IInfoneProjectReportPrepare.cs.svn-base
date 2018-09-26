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

        int Insert(InfoneProjectReportPrepareInfo model);

        int InsertByOutput(InfoneProjectReportPrepareInfo model);

        int Update(InfoneProjectReportPrepareInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        InfoneProjectReportPrepareInfo GetModel(Guid id);

        IList<InfoneProjectReportPrepareInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<InfoneProjectReportPrepareInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<InfoneProjectReportPrepareInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<InfoneProjectReportPrepareInfo> GetList();

        #endregion
    }
}
