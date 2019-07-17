using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderPickProduct
    {
        #region IOrderPickProduct Member

        int Insert(OrderPickProductInfo model);

        int Update(OrderPickProductInfo model);

        int Delete(Guid orderPickId, Guid orderId, Guid productId, Guid customerId);

        bool DeleteBatch(IList<object> list);

        OrderPickProductInfo GetModel(Guid orderPickId, Guid orderId, Guid productId, Guid customerId);

        IList<OrderPickProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderPickProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderPickProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderPickProductInfo> GetList();

        #endregion
    }
}
