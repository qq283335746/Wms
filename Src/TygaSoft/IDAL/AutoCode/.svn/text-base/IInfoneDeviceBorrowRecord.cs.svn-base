using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IInfoneDeviceBorrowRecord
    {
        #region IDeviceBorrowRecord Member

        int Insert(InfoneDeviceBorrowRecordInfo model);

        int InsertByOutput(InfoneDeviceBorrowRecordInfo model);

        int Update(InfoneDeviceBorrowRecordInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        InfoneDeviceBorrowRecordInfo GetModel(Guid id);

        IList<InfoneDeviceBorrowRecordInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<InfoneDeviceBorrowRecordInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<InfoneDeviceBorrowRecordInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<InfoneDeviceBorrowRecordInfo> GetList();

        #endregion
    }
}
