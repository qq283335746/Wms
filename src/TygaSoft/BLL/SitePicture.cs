using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class SitePicture
    {
        #region SitePicture Member

        public SitePictureInfo GetModel(string url)
        {
            return dal.GetModel(url);
        }

        public IList<ComboboxInfo> GetCbbList(int pageIndex, int pageSize, out int totalRecords, string funName)
        {
            var sqlWhere = "and FunType = @FunType";
            var parm = new SqlParameter("@FunType", SqlDbType.VarChar, 50);
            parm.Value = funName;

            return dal.GetCbbList(pageIndex, pageSize, out totalRecords, sqlWhere, parm);
        }

        public bool IsExist(object userId, string fileName, int fileSize)
        {
            return dal.IsExist(userId, fileName, fileSize);
        }

        #endregion
    }
}
