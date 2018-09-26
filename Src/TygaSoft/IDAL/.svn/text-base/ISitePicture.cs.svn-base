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

        SitePictureInfo GetModel(string url);

        IList<ComboboxInfo> GetCbbList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        bool IsExist(object userId, string fileName, int fileSize);

        #endregion
    }
}
