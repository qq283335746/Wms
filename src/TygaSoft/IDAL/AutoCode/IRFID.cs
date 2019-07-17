using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IRFID
    {
        #region IRFID Member

        int Insert(RFIDInfo model);

        int Update(RFIDInfo model);

        int Delete(string tID, string ePC);

        bool DeleteBatch(IList<object> list);

        RFIDInfo GetModel(string tID, string ePC);

        IList<RFIDInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<RFIDInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<RFIDInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<RFIDInfo> GetList();

        #endregion
    }
}
