using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderSend
    {
        #region IOrderSend Member

        bool IsExistProduct(object orderId);

        int SetStatus(string orderCode, int status);

        int SetStatus(string orderCode);

        OrderSendInfo GetModelByJoin(Guid id);

        IList<OrderSendInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
