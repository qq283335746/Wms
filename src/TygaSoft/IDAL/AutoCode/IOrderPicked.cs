using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderPicked
    {
        #region IOrderPicked Member

        int Insert(OrderPickedInfo model);

		int InsertByOutput(OrderPickedInfo model);

        int Update(OrderPickedInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        OrderPickedInfo GetModel(Guid id);

        IList<OrderPickedInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderPickedInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderPickedInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderPickedInfo> GetList();

        #endregion
    }
}
