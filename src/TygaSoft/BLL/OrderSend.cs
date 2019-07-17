using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class OrderSend
    {
        #region OrderSend Member

        public bool IsExistProduct(object orderId)
        {
            return dal.IsExistProduct(orderId);
        }

        public int SetStatus(string orderCode, int status)
        {
            return dal.SetStatus(orderCode, status);
        }

        public int SetStatus(string orderCode)
        {
            return dal.SetStatus(orderCode);
        }

        public OrderSendInfo GetModelByJoin(Guid id)
        {
            return dal.GetModelByJoin(id);
        }

        public IList<OrderSendInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
