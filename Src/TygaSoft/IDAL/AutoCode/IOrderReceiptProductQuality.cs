using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderReceiptProductQuality
    {
        #region IOrderReceiptProductQuality Member

        int Insert(OrderReceiptProductQualityInfo model);

        int Update(OrderReceiptProductQualityInfo model);

        int Delete(Guid orderProductId);

        bool DeleteBatch(IList<object> list);

        OrderReceiptProductQualityInfo GetModel(Guid orderProductId);

        IList<OrderReceiptProductQualityInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductQualityInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductQualityInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptProductQualityInfo> GetList();

        #endregion
    }
}
