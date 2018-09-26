using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IMesOrder
    {
        #region IMesOrder Member

        int Insert(MesOrderInfo model);

        int InsertByOutput(MesOrderInfo model);

        int Update(MesOrderInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        MesOrderInfo GetModel(Guid id);

        IList<MesOrderInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<MesOrderInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<MesOrderInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<MesOrderInfo> GetList();

        #endregion
    }
}
