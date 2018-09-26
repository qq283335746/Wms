using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class InfoneDeviceBorrowRecord
    {
        #region InfoneDeviceBorrowRecord Member

        public DataSet GetDsExport(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetDsExport(sqlWhere, cmdParms);
        }

        public IList<InfoneDeviceBorrowRecordInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
