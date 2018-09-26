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
    public partial class SiteMenus : ISiteMenus
    {
        #region ISiteMenus Member

        public int Insert(SiteMenusInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SiteMenus (ApplicationId,ParentId,IdStep,Title,Url,Descr,Sort,LastUpdatedDate)
			            values
						(@ApplicationId,@ParentId,@IdStep,@Title,@Url,@Descr,@Sort,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@IdStep",SqlDbType.VarChar,1000),
new SqlParameter("@Title",SqlDbType.NVarChar,20),
new SqlParameter("@Url",SqlDbType.VarChar,256),
new SqlParameter("@Descr",SqlDbType.NVarChar,50),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.ApplicationId;
            parms[1].Value = model.ParentId;
            parms[2].Value = model.IdStep;
            parms[3].Value = model.Title;
            parms[4].Value = model.Url;
            parms[5].Value = model.Descr;
            parms[6].Value = model.Sort;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(SiteMenusInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into SiteMenus (Id,ApplicationId,ParentId,IdStep,Title,Url,Descr,Sort,LastUpdatedDate)
			            values
						(@Id,@ApplicationId,@ParentId,@IdStep,@Title,@Url,@Descr,@Sort,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@IdStep",SqlDbType.VarChar,1000),
new SqlParameter("@Title",SqlDbType.NVarChar,20),
new SqlParameter("@Url",SqlDbType.VarChar,256),
new SqlParameter("@Descr",SqlDbType.NVarChar,50),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.ApplicationId;
            parms[2].Value = model.ParentId;
            parms[3].Value = model.IdStep;
            parms[4].Value = model.Title;
            parms[5].Value = model.Url;
            parms[6].Value = model.Descr;
            parms[7].Value = model.Sort;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(SiteMenusInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update SiteMenus set ApplicationId = @ApplicationId,ParentId = @ParentId,IdStep = @IdStep,Title = @Title,Url = @Url,Descr = @Descr,Sort = @Sort,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@IdStep",SqlDbType.VarChar,1000),
new SqlParameter("@Title",SqlDbType.NVarChar,20),
new SqlParameter("@Url",SqlDbType.VarChar,256),
new SqlParameter("@Descr",SqlDbType.NVarChar,50),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.ApplicationId;
            parms[2].Value = model.ParentId;
            parms[3].Value = model.IdStep;
            parms[4].Value = model.Title;
            parms[5].Value = model.Url;
            parms[6].Value = model.Descr;
            parms[7].Value = model.Sort;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from SiteMenus where Id = @Id ");
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
                sb.Append(@"delete from SiteMenus where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public SiteMenusInfo GetModel(Guid id)
        {
            SiteMenusInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 ApplicationId,Id,ParentId,IdStep,Title,Url,Descr,Sort,LastUpdatedDate 
			            from SiteMenus
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
                        model = new SiteMenusInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.IdStep = reader.GetString(3);
                        model.Title = reader.GetString(4);
                        model.Url = reader.GetString(5);
                        model.Descr = reader.GetString(6);
                        model.Sort = reader.GetInt32(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                    }
                }
            }

            return model;
        }

        public IList<SiteMenusInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from SiteMenus ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<SiteMenusInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          ApplicationId,Id,ParentId,IdStep,Title,Url,Descr,Sort,LastUpdatedDate
					  from SiteMenus ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SiteMenusInfo> list = new List<SiteMenusInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMenusInfo model = new SiteMenusInfo();
                        model.ApplicationId = reader.GetGuid(1);
                        model.Id = reader.GetGuid(2);
                        model.ParentId = reader.GetGuid(3);
                        model.IdStep = reader.GetString(4);
                        model.Title = reader.GetString(5);
                        model.Url = reader.GetString(6);
                        model.Descr = reader.GetString(7);
                        model.Sort = reader.GetInt32(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteMenusInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           ApplicationId,Id,ParentId,IdStep,Title,Url,Descr,Sort,LastUpdatedDate
					   from SiteMenus ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SiteMenusInfo> list = new List<SiteMenusInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMenusInfo model = new SiteMenusInfo();
                        model.ApplicationId = reader.GetGuid(1);
                        model.Id = reader.GetGuid(2);
                        model.ParentId = reader.GetGuid(3);
                        model.IdStep = reader.GetString(4);
                        model.Title = reader.GetString(5);
                        model.Url = reader.GetString(6);
                        model.Descr = reader.GetString(7);
                        model.Sort = reader.GetInt32(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteMenusInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select ApplicationId,Id,ParentId,IdStep,Title,Url,Descr,Sort,LastUpdatedDate
                        from SiteMenus ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<SiteMenusInfo> list = new List<SiteMenusInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMenusInfo model = new SiteMenusInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.IdStep = reader.GetString(3);
                        model.Title = reader.GetString(4);
                        model.Url = reader.GetString(5);
                        model.Descr = reader.GetString(6);
                        model.Sort = reader.GetInt32(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SiteMenusInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select ApplicationId,Id,ParentId,IdStep,Title,Url,Descr,Sort,LastUpdatedDate 
			            from SiteMenus
					    order by Sort ");

            IList<SiteMenusInfo> list = new List<SiteMenusInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SiteMenusInfo model = new SiteMenusInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.IdStep = reader.GetString(3);
                        model.Title = reader.GetString(4);
                        model.Url = reader.GetString(5);
                        model.Descr = reader.GetString(6);
                        model.Sort = reader.GetInt32(7);
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
