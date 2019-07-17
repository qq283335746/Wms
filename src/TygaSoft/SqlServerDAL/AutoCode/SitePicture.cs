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

        public int Insert(SitePictureInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SitePicture (UserId,FileName,FileSize,FileExtension,FileDirectory,RandomFolder,FunType,LastUpdatedDate)
			            values
						(@UserId,@FileName,@FileSize,@FileExtension,@FileDirectory,@RandomFolder,@FunType,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@FileName",SqlDbType.NVarChar,100),
                                        new SqlParameter("@FileSize",SqlDbType.Int),
                                        new SqlParameter("@FileExtension",SqlDbType.VarChar,50),
                                        new SqlParameter("@FileDirectory",SqlDbType.NVarChar,100),
                                        new SqlParameter("@RandomFolder",SqlDbType.VarChar,20),
                                        new SqlParameter("@FunType",SqlDbType.VarChar,50),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.FileName;
            parms[2].Value = model.FileSize;
            parms[3].Value = model.FileExtension;
            parms[4].Value = model.FileDirectory;
            parms[5].Value = model.RandomFolder;
            parms[6].Value = model.FunType;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(SitePictureInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SitePicture (Id,UserId,FileName,FileSize,FileExtension,FileDirectory,RandomFolder,FunType,LastUpdatedDate)
			            values
						(@Id,@UserId,@FileName,@FileSize,@FileExtension,@FileDirectory,@RandomFolder,@FunType,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@FileName",SqlDbType.NVarChar,100),
                                        new SqlParameter("@FileSize",SqlDbType.Int),
                                        new SqlParameter("@FileExtension",SqlDbType.VarChar,50),
                                        new SqlParameter("@FileDirectory",SqlDbType.NVarChar,100),
                                        new SqlParameter("@RandomFolder",SqlDbType.VarChar,20),
                                        new SqlParameter("@FunType",SqlDbType.VarChar,50),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.FileName;
            parms[3].Value = model.FileSize;
            parms[4].Value = model.FileExtension;
            parms[5].Value = model.FileDirectory;
            parms[6].Value = model.RandomFolder;
            parms[7].Value = model.FunType;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(SitePictureInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update SitePicture set UserId = @UserId,FileName = @FileName,FileSize = @FileSize,FileExtension = @FileExtension,FileDirectory = @FileDirectory,RandomFolder = @RandomFolder,FunType = @FunType,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@FileName",SqlDbType.NVarChar,100),
                                    new SqlParameter("@FileSize",SqlDbType.Int),
                                    new SqlParameter("@FileExtension",SqlDbType.VarChar,50),
                                    new SqlParameter("@FileDirectory",SqlDbType.NVarChar,100),
                                    new SqlParameter("@RandomFolder",SqlDbType.VarChar,20),
                                    new SqlParameter("@FunType",SqlDbType.VarChar,50),
                                    new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.FileName;
            parms[3].Value = model.FileSize;
            parms[4].Value = model.FileExtension;
            parms[5].Value = model.FileDirectory;
            parms[6].Value = model.RandomFolder;
            parms[7].Value = model.FunType;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from SitePicture where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public bool DeleteBatch(IList<object> list)
        {
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from SitePicture where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public SitePictureInfo GetModel(Guid id)
        {
            SitePictureInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,FileName,FileSize,FileExtension,FileDirectory,RandomFolder,FunType,LastUpdatedDate 
			            from SitePicture
						where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms))
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

        public IList<SitePictureInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from SitePicture ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<SitePictureInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          Id,UserId,FileName,FileSize,FileExtension,FileDirectory,RandomFolder,FunType,LastUpdatedDate
					  from SitePicture ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SitePictureInfo> list = new List<SitePictureInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SitePictureInfo model = new SitePictureInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.FileName = reader.GetString(3);
                        model.FileSize = reader.GetInt32(4);
                        model.FileExtension = reader.GetString(5);
                        model.FileDirectory = reader.GetString(6);
                        model.RandomFolder = reader.GetString(7);
                        model.FunType = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SitePictureInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			           Id,UserId,FileName,FileSize,FileExtension,FileDirectory,RandomFolder,FunType,LastUpdatedDate
					   from SitePicture ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SitePictureInfo> list = new List<SitePictureInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SitePictureInfo model = new SitePictureInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.FileName = reader.GetString(3);
                        model.FileSize = reader.GetInt32(4);
                        model.FileExtension = reader.GetString(5);
                        model.FileDirectory = reader.GetString(6);
                        model.RandomFolder = reader.GetString(7);
                        model.FunType = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SitePictureInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,FileName,FileSize,FileExtension,FileDirectory,RandomFolder,FunType,LastUpdatedDate
                        from SitePicture ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate ");

            IList<SitePictureInfo> list = new List<SitePictureInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SitePictureInfo model = new SitePictureInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.FileName = reader.GetString(2);
                        model.FileSize = reader.GetInt32(3);
                        model.FileExtension = reader.GetString(4);
                        model.FileDirectory = reader.GetString(5);
                        model.RandomFolder = reader.GetString(6);
                        model.FunType = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SitePictureInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,FileName,FileSize,FileExtension,FileDirectory,RandomFolder,FunType,LastUpdatedDate 
			            from SitePicture
					    order by LastUpdatedDate ");

            IList<SitePictureInfo> list = new List<SitePictureInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SitePictureInfo model = new SitePictureInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.FileName = reader.GetString(2);
                        model.FileSize = reader.GetInt32(3);
                        model.FileExtension = reader.GetString(4);
                        model.FileDirectory = reader.GetString(5);
                        model.RandomFolder = reader.GetString(6);
                        model.FunType = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
