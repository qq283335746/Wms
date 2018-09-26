using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IShelfMission
    {
        #region IShelfMission Member

        int Insert(ShelfMissionInfo model);

        int InsertByOutput(ShelfMissionInfo model);

        int Update(ShelfMissionInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        ShelfMissionInfo GetModel(Guid id);

        IList<ShelfMissionInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ShelfMissionInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ShelfMissionInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<ShelfMissionInfo> GetList();

        #endregion
    }
}
