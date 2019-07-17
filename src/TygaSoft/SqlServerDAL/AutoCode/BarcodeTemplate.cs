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
    public partial class BarcodeTemplate : IBarcodeTemplate
    {
        #region IBarcodeTemplate Member

        public int Insert(BarcodeTemplateInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into BarcodeTemplate (UserId,Title,JContent,IsDefault,TypeName,LastUpdatedDate)
			            values
						(@UserId,@Title,@JContent,@IsDefault,@TypeName,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Title",SqlDbType.NVarChar,256),
                                        new SqlParameter("@JContent",SqlDbType.NVarChar),
                                        new SqlParameter("@IsDefault",SqlDbType.Bit),
                                        new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.Title;
            parms[2].Value = model.JContent;
            parms[3].Value = model.IsDefault;
            parms[4].Value = model.TypeName;
            parms[5].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(BarcodeTemplateInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into BarcodeTemplate (Id,UserId,Title,JContent,IsDefault,TypeName,LastUpdatedDate)
			            values
						(@Id,@UserId,@Title,@JContent,@IsDefault,@TypeName,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Title",SqlDbType.NVarChar,256),
                                        new SqlParameter("@JContent",SqlDbType.NVarChar),
                                        new SqlParameter("@IsDefault",SqlDbType.Bit),
                                        new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.Title;
            parms[3].Value = model.JContent;
            parms[4].Value = model.IsDefault;
            parms[5].Value = model.TypeName;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(BarcodeTemplateInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update BarcodeTemplate set UserId = @UserId,Title = @Title,JContent = @JContent,IsDefault = @IsDefault,TypeName = @TypeName,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Title",SqlDbType.NVarChar,256),
                                        new SqlParameter("@JContent",SqlDbType.NVarChar),
                                        new SqlParameter("@IsDefault",SqlDbType.Bit),
                                        new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.Title;
            parms[3].Value = model.JContent;
            parms[4].Value = model.IsDefault;
            parms[5].Value = model.TypeName;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from BarcodeTemplate where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public bool DeleteBatch(IList<object> list)
        {
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from BarcodeTemplate where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public BarcodeTemplateInfo GetModel(Guid id)
        {
            BarcodeTemplateInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,Title,JContent,IsDefault,TypeName,LastUpdatedDate 
			            from BarcodeTemplate
						where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new BarcodeTemplateInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.Title = reader.GetString(2);
                        model.JContent = reader.GetString(3);
                        model.IsDefault = reader.GetBoolean(4);
                        model.TypeName = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                    }
                }
            }

            return model;
        }

        public IList<BarcodeTemplateInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from BarcodeTemplate ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<BarcodeTemplateInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,Title,JContent,IsDefault,TypeName,LastUpdatedDate
					  from BarcodeTemplate ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<BarcodeTemplateInfo> list = new List<BarcodeTemplateInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BarcodeTemplateInfo model = new BarcodeTemplateInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.Title = reader.GetString(3);
                        model.JContent = reader.GetString(4);
                        model.IsDefault = reader.GetBoolean(5);
                        model.TypeName = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<BarcodeTemplateInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,Title,JContent,IsDefault,TypeName,LastUpdatedDate
					   from BarcodeTemplate ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<BarcodeTemplateInfo> list = new List<BarcodeTemplateInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BarcodeTemplateInfo model = new BarcodeTemplateInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.Title = reader.GetString(3);
                        model.JContent = reader.GetString(4);
                        model.IsDefault = reader.GetBoolean(5);
                        model.TypeName = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<BarcodeTemplateInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,Title,JContent,IsDefault,TypeName,LastUpdatedDate
                        from BarcodeTemplate ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<BarcodeTemplateInfo> list = new List<BarcodeTemplateInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BarcodeTemplateInfo model = new BarcodeTemplateInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.Title = reader.GetString(2);
                        model.JContent = reader.GetString(3);
                        model.IsDefault = reader.GetBoolean(4);
                        model.TypeName = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<BarcodeTemplateInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,Title,JContent,IsDefault,TypeName,LastUpdatedDate 
			            from BarcodeTemplate
					    order by LastUpdatedDate desc ");

            IList<BarcodeTemplateInfo> list = new List<BarcodeTemplateInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BarcodeTemplateInfo model = new BarcodeTemplateInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.Title = reader.GetString(2);
                        model.JContent = reader.GetString(3);
                        model.IsDefault = reader.GetBoolean(4);
                        model.TypeName = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
