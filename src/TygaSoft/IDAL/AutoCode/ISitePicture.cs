using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface ISitePicture
    {
        #region ISitePicture Member

        int Insert(SitePictureInfo model);

        int InsertByOutput(SitePictureInfo model);

        int Update(SitePictureInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        SitePictureInfo GetModel(Guid id);

        IList<SitePictureInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SitePictureInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<SitePictureInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<SitePictureInfo> GetList();

        #endregion
    }
}
