using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IProduct
    {
        #region IProduct Member

        int Insert(ProductInfo model);

        int InsertByOutput(ProductInfo model);

        int Update(ProductInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        ProductInfo GetModel(Guid id);

        IList<ProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<ProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<ProductInfo> GetList();

        #endregion
    }
}
