using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ICompany
    {
        #region ICompany Member

        int Insert(CompanyInfo model);

        int InsertByOutput(CompanyInfo model);

        int Update(CompanyInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        CompanyInfo GetModel(Guid id);

        IList<CompanyInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<CompanyInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<CompanyInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<CompanyInfo> GetList();

        #endregion
    }
}
