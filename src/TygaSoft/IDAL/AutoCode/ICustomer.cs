using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ICustomer
    {
        #region ICustomer Member

        int Insert(CustomerInfo model);

        int InsertByOutput(CustomerInfo model);

        int Update(CustomerInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        CustomerInfo GetModel(Guid id);

        IList<CustomerInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<CustomerInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<CustomerInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<CustomerInfo> GetList();

        #endregion
    }
}
