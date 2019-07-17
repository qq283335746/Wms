using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IShelfMissionProduct
    {
        #region IShelfMissionProduct Member

        IList<ShelfMissionProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ShelfMissionProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms);

        IList<ShelfMissionProductInfo> GetListByScanned(Guid shelfMissionId);

        #endregion
    }
}
