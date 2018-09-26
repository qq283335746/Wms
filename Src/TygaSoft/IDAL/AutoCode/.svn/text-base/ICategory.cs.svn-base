using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ICategory
    {
        #region ICategory Member

        int Insert(CategoryInfo model);

        int InsertByOutput(CategoryInfo model);

        int Update(CategoryInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        CategoryInfo GetModel(Guid id);

        IList<CategoryInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<CategoryInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<CategoryInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<CategoryInfo> GetList();

        #endregion
    }
}
