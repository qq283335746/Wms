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
    public partial class SiteMulti : ISiteMulti
    {
        #region ISiteMulti Member

        public int Insert(SiteMultiInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SiteMulti (Coded,Named,SiteLogo,SiteTitle,CultureName,LastUpdatedDate)
			            values
						(@Coded,@Named,@SiteLogo,@SiteTitle,@CultureName,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Coded",SqlDbType.Char,6),
new SqlParameter("@Named",SqlDbType.NVarChar,30),
new SqlParameter("@SiteLogo",SqlDbType.VarChar,256),
new SqlParameter("@SiteTitle",SqlDbType.NVarChar,50),
new SqlParameter("@CultureName",SqlDbType.VarChar,20),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Coded;
            parms[1].Value = model.Named;
            parms[2].Value = model.SiteLogo;
            parms[3].Value = model.SiteTitle;
            parms[4].Value = model.CultureName;
            parms[5].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(SiteMultiInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SiteMulti (Id,Coded,Named,SiteLogo,SiteTitle,CultureName,LastUpdatedDate)
			            values
						(@Id,@Coded,@Named,@SiteLogo,@SiteTitle,@CultureName,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.Char,6),
new SqlParameter("@Named",SqlDbType.NVarChar,30),
new SqlParameter("@SiteLogo",SqlDbType.VarChar,256),
new SqlParameter("@SiteTitle",SqlDbType.NVarChar,50),
new SqlParameter("@CultureName",SqlDbType.VarChar,20),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Coded;
            parms[2].Value = model.Named;
            parms[3].Value = model.SiteLogo;
            parms[4].Value = model.SiteTitle;
            parms[5].Value = model.CultureName;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(SiteMultiInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update SiteMulti set Coded = @Coded,Named = @Named,SiteLogo = @SiteLogo,SiteTitle = @SiteTitle,CultureName = @CultureName,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.Char,6),
new SqlParameter("@Named",SqlDbType.NVarChar,30),
new SqlParameter("@SiteLogo",SqlDbType.VarChar,256),
new SqlParameter("@SiteTitle",SqlDbType.NVarChar,50),
new SqlParameter("@CultureName",SqlDbType.VarChar,20),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Coded;
            parms[2].Value = model.Named;
            parms[3].Value = model.SiteLogo;
            parms[4].Value = model.SiteTitle;
            parms[5].Value = model.CultureName;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from SiteMulti where Id = @Id ");
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
                sb.Append(@"delete from SiteMulti where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public SiteMultiInfo GetModel(Guid id)
        {
            SiteMultiInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,Coded,Named,SiteLogo,SiteTitle,CultureName,LastUpdatedDate 
			            from SiteMulti
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
                        model = new SiteMultiInfo();
                        model.Id = reader.GetGuid(0);
                        model.Coded = reader.GetString(1);
                        model.Named = reader.GetString(2);
                        model.SiteLogo = reader.GetString(3);
                        model.SiteTitle = reader.GetString(4);
                        model.CultureName = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                    }
                }
            }

            return model;
        }

        public IList<SiteMultiInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from SiteMulti ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<SiteMultiInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,Coded,Named,SiteLogo,SiteTitle,CultureName,LastUpdatedDate
					  from SiteMulti ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SiteMultiInfo> list = new List<SiteMultiInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMultiInfo model = new SiteMultiInfo();
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.SiteLogo = reader.GetString(4);
                        model.SiteTitle = reader.GetString(5);
                        model.CultureName = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteMultiInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,Coded,Named,SiteLogo,SiteTitle,CultureName,LastUpdatedDate
					   from SiteMulti ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SiteMultiInfo> list = new List<SiteMultiInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMultiInfo model = new SiteMultiInfo();
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.SiteLogo = reader.GetString(4);
                        model.SiteTitle = reader.GetString(5);
                        model.CultureName = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteMultiInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,Coded,Named,SiteLogo,SiteTitle,CultureName,LastUpdatedDate
                        from SiteMulti ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<SiteMultiInfo> list = new List<SiteMultiInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMultiInfo model = new SiteMultiInfo();
                        model.Id = reader.GetGuid(0);
                        model.Coded = reader.GetString(1);
                        model.Named = reader.GetString(2);
                        model.SiteLogo = reader.GetString(3);
                        model.SiteTitle = reader.GetString(4);
                        model.CultureName = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteMultiInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,Coded,Named,SiteLogo,SiteTitle,CultureName,LastUpdatedDate 
			            from SiteMulti
					    order by LastUpdatedDate desc ");

            IList<SiteMultiInfo> list = new List<SiteMultiInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMultiInfo model = new SiteMultiInfo();
                        model.Id = reader.GetGuid(0);
                        model.Coded = reader.GetString(1);
                        model.Named = reader.GetString(2);
                        model.SiteLogo = reader.GetString(3);
                        model.SiteTitle = reader.GetString(4);
                        model.CultureName = reader.GetString(5);
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
