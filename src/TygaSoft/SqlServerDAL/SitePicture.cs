using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class SitePicture : ISitePicture
    {
        #region ISitePicture Member

        public SitePictureInfo GetModel(string url)
        {
            SitePictureInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,FileName,FileSize,FileExtension,FileDirectory,RandomFolder,FunType,LastUpdatedDate 
			            from SitePicture
						where (FileDirectory+FileName) = @Url ");
            var parm = new SqlParameter("@Url", SqlDbType.NVarChar, 256);
            parm.Value = url;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new SitePictureInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.FileName = reader.GetString(2);
                        model.FileSize = reader.GetInt32(3);
                        model.FileExtension = reader.GetString(4);
                        model.FileDirectory = reader.GetString(5);
                        model.RandomFolder = reader.GetString(6);
                        model.FunType = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                    }
                }
            }

            return model;
        }

        public IList<ComboboxInfo> GetCbbList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(500);
            sb.Append(@"select count(*) from SitePicture ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ComboboxInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          Id,UserId,FileName,FileSize,FileExtension,FileDirectory,RandomFolder,FunType,LastUpdatedDate
					  from SitePicture ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<ComboboxInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(new ComboboxInfo(reader.GetGuid(1).ToString(), string.Format("{0}{1}{2}", "/wms", reader.GetString(6), reader.GetString(3))));
                    }
                }
            }

            return list;
        }

        public bool IsExist(object userId, string fileName, int fileSize)
        {
            SqlParameter[] parms = {
                                       new SqlParameter("@FileName",SqlDbType.NVarChar, 100),
                                       new SqlParameter("@FileSize",SqlDbType.Int),
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = fileName;
            parms[1].Value = fileSize;
            parms[2].Value = Guid.Parse(userId.ToString());

            string cmdText = "select 1 from [SitePicture] where lower(FileName) = @FileName and FileSize = @FileSize and UserId = @UserId ";

            object obj = SqlHelper.ExecuteScalar(SqlHelper.AspnetDbConnString, CommandType.Text, cmdText, parms);
            if (obj != null) return true;

            return false;
        }

        #endregion
    }
}
