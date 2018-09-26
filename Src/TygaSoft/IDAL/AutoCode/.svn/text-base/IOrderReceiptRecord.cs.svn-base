using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrderReceiptRecord
    {
        #region IOrderReceiptRecord Member

        int Insert(OrderReceiptRecordInfo model);

		int InsertByOutput(OrderReceiptRecordInfo model);

        int Update(OrderReceiptRecordInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        OrderReceiptRecordInfo GetModel(Guid id);

        IList<OrderReceiptRecordInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptRecordInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptRecordInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<OrderReceiptRecordInfo> GetList();

        #endregion
    }
}
