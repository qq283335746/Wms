using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IBarcodeTemplate
    {
        #region IBarcodeTemplate Member

        int Insert(BarcodeTemplateInfo model);

        int InsertByOutput(BarcodeTemplateInfo model);

        int Update(BarcodeTemplateInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        BarcodeTemplateInfo GetModel(Guid id);

        IList<BarcodeTemplateInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<BarcodeTemplateInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<BarcodeTemplateInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<BarcodeTemplateInfo> GetList();

        #endregion
    }
}
