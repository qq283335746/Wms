using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ILogisticsDistribution
    {
        #region ILogisticsDistribution Member

        int Insert(LogisticsDistributionInfo model);

        int InsertByOutput(LogisticsDistributionInfo model);

        int Update(LogisticsDistributionInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        LogisticsDistributionInfo GetModel(Guid id);

        IList<LogisticsDistributionInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<LogisticsDistributionInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<LogisticsDistributionInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<LogisticsDistributionInfo> GetList();

        #endregion
    }
}
