using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderRandom
    {
        #region IOrderRandom Member

        int Insert(OrderRandomInfo model);

        int Update(OrderRandomInfo model);

        int Delete(string orderCode);

        bool DeleteBatch(IList<object> list);

        OrderRandomInfo GetModel(string orderCode);

        IList<OrderRandomInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderRandomInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderRandomInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderRandomInfo> GetList();

        #endregion
    }
}
