using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IFeaturePicture
    {
        #region IFeaturePicture Member

        int Insert(FeaturePictureInfo model);

        int InsertByOutput(FeaturePictureInfo model);

        int Update(FeaturePictureInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        FeaturePictureInfo GetModel(Guid id);

        IList<FeaturePictureInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<FeaturePictureInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<FeaturePictureInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<FeaturePictureInfo> GetList();

        #endregion
    }
}
