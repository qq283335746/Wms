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
    public partial class FeatureUser : IFeatureUser
    {
        #region IFeatureUser Member

        public int Insert(FeatureUserInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into FeatureUser (UserId,FeatureId,TypeName,LastUpdatedDate)
			            values
						(@UserId,@FeatureId,@TypeName,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@FeatureId",SqlDbType.UniqueIdentifier),
new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.FeatureId;
            parms[2].Value = model.TypeName;
            parms[3].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(FeatureUserInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update FeatureUser set TypeName = @TypeName,LastUpdatedDate = @LastUpdatedDate 
			            where UserId = @UserId and FeatureId = @FeatureId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@FeatureId",SqlDbType.UniqueIdentifier),
new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.FeatureId;
            parms[2].Value = model.TypeName;
            parms[3].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid userId, Guid featureId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from FeatureUser where UserId = @UserId and FeatureId = @FeatureId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@FeatureId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userId;
            parms[1].Value = featureId;

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
                sb.Append(@"delete from FeatureUser where UserId = @UserId" + n + " ;");
                SqlParameter parm = new SqlParameter("@UserId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public FeatureUserInfo GetModel(Guid userId, Guid featureId)
        {
            FeatureUserInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 UserId,FeatureId,TypeName,LastUpdatedDate 
			            from FeatureUser
						where UserId = @UserId and FeatureId = @FeatureId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@FeatureId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userId;
            parms[1].Value = featureId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new FeatureUserInfo();
                        model.UserId = reader.GetGuid(0);
                        model.FeatureId = reader.GetGuid(1);
                        model.TypeName = reader.GetString(2);
                        model.LastUpdatedDate = reader.GetDateTime(3);
                    }
                }
            }

            return model;
        }

        public IList<FeatureUserInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from FeatureUser ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<FeatureUserInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          UserId,FeatureId,TypeName,LastUpdatedDate
					  from FeatureUser ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<FeatureUserInfo> list = new List<FeatureUserInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FeatureUserInfo model = new FeatureUserInfo();
                        model.UserId = reader.GetGuid(1);
                        model.FeatureId = reader.GetGuid(2);
                        model.TypeName = reader.GetString(3);
                        model.LastUpdatedDate = reader.GetDateTime(4);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<FeatureUserInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           UserId,FeatureId,TypeName,LastUpdatedDate
					   from FeatureUser ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<FeatureUserInfo> list = new List<FeatureUserInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FeatureUserInfo model = new FeatureUserInfo();
                        model.UserId = reader.GetGuid(1);
                        model.FeatureId = reader.GetGuid(2);
                        model.TypeName = reader.GetString(3);
                        model.LastUpdatedDate = reader.GetDateTime(4);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<FeatureUserInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select UserId,FeatureId,TypeName,LastUpdatedDate
                        from FeatureUser ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<FeatureUserInfo> list = new List<FeatureUserInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FeatureUserInfo model = new FeatureUserInfo();
                        model.UserId = reader.GetGuid(0);
                        model.FeatureId = reader.GetGuid(1);
                        model.TypeName = reader.GetString(2);
                        model.LastUpdatedDate = reader.GetDateTime(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<FeatureUserInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select UserId,FeatureId,TypeName,LastUpdatedDate 
			            from FeatureUser
					    order by LastUpdatedDate desc ");

            IList<FeatureUserInfo> list = new List<FeatureUserInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FeatureUserInfo model = new FeatureUserInfo();
                        model.UserId = reader.GetGuid(0);
                        model.FeatureId = reader.GetGuid(1);
                        model.TypeName = reader.GetString(2);
                        model.LastUpdatedDate = reader.GetDateTime(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
