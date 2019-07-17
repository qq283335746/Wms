using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IPandianProduct
    {
        #region IPandianProduct Member

        int Insert(PandianProductInfo model);

        int Update(PandianProductInfo model);

        int Delete(Guid pandianId, Guid productId, Guid customerId);

        bool DeleteBatch(IList<object> list);

        PandianProductInfo GetModel(Guid pandianId, Guid productId, Guid customerId);

        IList<PandianProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<PandianProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<PandianProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<PandianProductInfo> GetList();

        #endregion
    }
}
