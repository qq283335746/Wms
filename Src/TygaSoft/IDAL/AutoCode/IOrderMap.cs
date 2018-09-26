using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderMap
    {
        #region IOrderMap Member

        int Insert(OrderMapInfo model);

        int Update(OrderMapInfo model);

        int Delete(string orderCode);

        bool DeleteBatch(IList<object> list);

        OrderMapInfo GetModel(string orderCode);

        IList<OrderMapInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderMapInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderMapInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderMapInfo> GetList();

        #endregion
    }
}
