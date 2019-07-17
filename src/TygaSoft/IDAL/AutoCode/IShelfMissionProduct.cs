using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IShelfMissionProduct
    {
        #region IShelfMissionProduct Member

        int Insert(ShelfMissionProductInfo model);

        int Update(ShelfMissionProductInfo model);

        int Delete(Guid shelfMissionId, Guid orderId, Guid productId);

        bool DeleteBatch(IList<object> list);

        ShelfMissionProductInfo GetModel(Guid shelfMissionId, Guid orderId, Guid productId);

        IList<ShelfMissionProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ShelfMissionProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ShelfMissionProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<ShelfMissionProductInfo> GetList();

        #endregion
    }
}
