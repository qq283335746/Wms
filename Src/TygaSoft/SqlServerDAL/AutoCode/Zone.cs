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
    public partial class Zone : IZone
    {
        #region IZone Member

        public int Insert(ZoneInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Zone (UserId,ZoneCode,ZoneName,Square,Descr,LastUpdatedDate)
			            values
						(@UserId,@ZoneCode,@ZoneName,@Square,@Descr,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ZoneCode",SqlDbType.VarChar,30),
                                        new SqlParameter("@ZoneName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Square",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Descr",SqlDbType.NVarChar,100),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.ZoneCode;
            parms[2].Value = model.ZoneName;
            parms[3].Value = model.Square;
            parms[4].Value = model.Descr;
            parms[5].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(ZoneInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Zone (Id,UserId,ZoneCode,ZoneName,Square,Descr,LastUpdatedDate)
			            values
						(@Id,@UserId,@ZoneCode,@ZoneName,@Square,@Descr,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ZoneCode",SqlDbType.VarChar,30),
                                        new SqlParameter("@ZoneName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Square",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Descr",SqlDbType.NVarChar,100),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ZoneCode;
            parms[3].Value = model.ZoneName;
            parms[4].Value = model.Square;
            parms[5].Value = model.Descr;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(ZoneInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Zone set UserId = @UserId,ZoneCode = @ZoneCode,ZoneName = @ZoneName,Square = @Square,Descr = @Descr,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ZoneCode",SqlDbType.VarChar,30),
                                        new SqlParameter("@ZoneName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Square",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Descr",SqlDbType.NVarChar,100),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ZoneCode;
            parms[3].Value = model.ZoneName;
            parms[4].Value = model.Square;
            parms[5].Value = model.Descr;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Zone where Id = @Id ");
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
                sb.Append(@"delete from Zone where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public ZoneInfo GetModel(Guid id)
        {
            ZoneInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,ZoneCode,ZoneName,Square,Descr,LastUpdatedDate 
			            from Zone
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
                        model = new ZoneInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ZoneCode = reader.GetString(2);
                        model.ZoneName = reader.GetString(3);
                        model.Square = reader.GetString(4);
                        model.Descr = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                    }
                }
            }

            return model;
        }

        public IList<ZoneInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Zone ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ZoneInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,ZoneCode,ZoneName,Square,Descr,LastUpdatedDate
					  from Zone ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ZoneInfo> list = new List<ZoneInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ZoneInfo model = new ZoneInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ZoneCode = reader.GetString(3);
                        model.ZoneName = reader.GetString(4);
                        model.Square = reader.GetString(5);
                        model.Descr = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ZoneInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,ZoneCode,ZoneName,Square,Descr,LastUpdatedDate
					   from Zone ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ZoneInfo> list = new List<ZoneInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ZoneInfo model = new ZoneInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ZoneCode = reader.GetString(3);
                        model.ZoneName = reader.GetString(4);
                        model.Square = reader.GetString(5);
                        model.Descr = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ZoneInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,ZoneCode,ZoneName,Square,Descr,LastUpdatedDate
                        from Zone ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<ZoneInfo> list = new List<ZoneInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ZoneInfo model = new ZoneInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ZoneCode = reader.GetString(2);
                        model.ZoneName = reader.GetString(3);
                        model.Square = reader.GetString(4);
                        model.Descr = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ZoneInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,ZoneCode,ZoneName,Square,Descr,LastUpdatedDate 
			            from Zone
					    order by LastUpdatedDate desc ");

            IList<ZoneInfo> list = new List<ZoneInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ZoneInfo model = new ZoneInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ZoneCode = reader.GetString(2);
                        model.ZoneName = reader.GetString(3);
                        model.Square = reader.GetString(4);
                        model.Descr = reader.GetString(5);
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
