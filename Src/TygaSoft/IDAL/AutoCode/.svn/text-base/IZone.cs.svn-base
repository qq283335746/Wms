using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IZone
    {
        #region IZone Member

        int Insert(ZoneInfo model);

        int InsertByOutput(ZoneInfo model);

        int Update(ZoneInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        ZoneInfo GetModel(Guid id);

        IList<ZoneInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ZoneInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ZoneInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<ZoneInfo> GetList();

        #endregion
    }
}
