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
    public partial class Pandian : IPandian
    {
        #region IPandian Member

        public int Insert(PandianInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Pandian (UserId,OrderCode,Named,AllowUsers,Remark,StockStartDate,StockEndDate,Customers,Zones,StockLocations,TotalQty,Status,LastUpdatedDate)
			            values
						(@UserId,@OrderCode,@Named,@AllowUsers,@Remark,@StockStartDate,@StockEndDate,@Customers,@Zones,@StockLocations,@TotalQty,@Status,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderCode",SqlDbType.VarChar,36),
                                        new SqlParameter("@Named",SqlDbType.NVarChar,256),
                                        new SqlParameter("@AllowUsers",SqlDbType.VarChar,1000),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,300),
                                        new SqlParameter("@StockStartDate",SqlDbType.DateTime),
                                        new SqlParameter("@StockEndDate",SqlDbType.DateTime),
                                        new SqlParameter("@Customers",SqlDbType.VarChar,1000),
                                        new SqlParameter("@Zones",SqlDbType.VarChar,1000),
                                        new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@TotalQty",SqlDbType.Float),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.OrderCode;
            parms[2].Value = model.Named;
            parms[3].Value = model.AllowUsers;
            parms[4].Value = model.Remark;
            parms[5].Value = model.StockStartDate;
            parms[6].Value = model.StockEndDate;
            parms[7].Value = model.Customers;
            parms[8].Value = model.Zones;
            parms[9].Value = model.StockLocations;
            parms[10].Value = model.TotalQty;
            parms[11].Value = model.Status;
            parms[12].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(PandianInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Pandian (Id,UserId,OrderCode,Named,AllowUsers,Remark,StockStartDate,StockEndDate,Customers,Zones,StockLocations,TotalQty,Status,LastUpdatedDate)
			            values
						(@Id,@UserId,@OrderCode,@Named,@AllowUsers,@Remark,@StockStartDate,@StockEndDate,@Customers,@Zones,@StockLocations,@TotalQty,@Status,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderCode",SqlDbType.VarChar,36),
                                        new SqlParameter("@Named",SqlDbType.NVarChar,256),
                                        new SqlParameter("@AllowUsers",SqlDbType.VarChar,1000),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,300),
                                        new SqlParameter("@StockStartDate",SqlDbType.DateTime),
                                        new SqlParameter("@StockEndDate",SqlDbType.DateTime),
                                        new SqlParameter("@Customers",SqlDbType.VarChar,1000),
                                        new SqlParameter("@Zones",SqlDbType.VarChar,1000),
                                        new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@TotalQty",SqlDbType.Float),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OrderCode;
            parms[3].Value = model.Named;
            parms[4].Value = model.AllowUsers;
            parms[5].Value = model.Remark;
            parms[6].Value = model.StockStartDate;
            parms[7].Value = model.StockEndDate;
            parms[8].Value = model.Customers;
            parms[9].Value = model.Zones;
            parms[10].Value = model.StockLocations;
            parms[11].Value = model.TotalQty;
            parms[12].Value = model.Status;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(PandianInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Pandian set UserId = @UserId,OrderCode = @OrderCode,Named = @Named,AllowUsers = @AllowUsers,Remark = @Remark,StockStartDate = @StockStartDate,StockEndDate = @StockEndDate,Customers = @Customers,Zones = @Zones,StockLocations = @StockLocations,TotalQty = @TotalQty,Status = @Status,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@OrderCode",SqlDbType.VarChar,36),
                                    new SqlParameter("@Named",SqlDbType.NVarChar,256),
                                    new SqlParameter("@AllowUsers",SqlDbType.VarChar,1000),
                                    new SqlParameter("@Remark",SqlDbType.NVarChar,300),
                                    new SqlParameter("@StockStartDate",SqlDbType.DateTime),
                                    new SqlParameter("@StockEndDate",SqlDbType.DateTime),
                                    new SqlParameter("@Customers",SqlDbType.VarChar,1000),
                                    new SqlParameter("@Zones",SqlDbType.VarChar,1000),
                                    new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                    new SqlParameter("@TotalQty",SqlDbType.Float),
                                    new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                    new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OrderCode;
            parms[3].Value = model.Named;
            parms[4].Value = model.AllowUsers;
            parms[5].Value = model.Remark;
            parms[6].Value = model.StockStartDate;
            parms[7].Value = model.StockEndDate;
            parms[8].Value = model.Customers;
            parms[9].Value = model.Zones;
            parms[10].Value = model.StockLocations;
            parms[11].Value = model.TotalQty;
            parms[12].Value = model.Status;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Pandian where Id = @Id ");
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
                sb.Append(@"delete from Pandian where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public PandianInfo GetModel(Guid id)
        {
            PandianInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,OrderCode,Named,AllowUsers,Remark,StockStartDate,StockEndDate,Customers,Zones,StockLocations,TotalQty,Status,LastUpdatedDate 
			            from Pandian
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
                        model = new PandianInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.AllowUsers = reader.GetString(4);
                        model.Remark = reader.GetString(5);
                        model.StockStartDate = reader.GetDateTime(6);
                        model.StockEndDate = reader.GetDateTime(7);
                        model.Customers = reader.GetString(8);
                        model.Zones = reader.GetString(9);
                        model.StockLocations = reader.GetString(10);
                        model.TotalQty = reader.GetDouble(11);
                        model.Status = reader.GetString(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);
                    }
                }
            }

            return model;
        }

        public IList<PandianInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,OrderCode,Named,AllowUsers,Remark,StockStartDate,StockEndDate,Customers,Zones,StockLocations,TotalQty,Status,LastUpdatedDate
					  from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderCode = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.AllowUsers = reader.GetString(5);
                        model.Remark = reader.GetString(6);
                        model.StockStartDate = reader.GetDateTime(7);
                        model.StockEndDate = reader.GetDateTime(8);
                        model.Customers = reader.GetString(9);
                        model.Zones = reader.GetString(10);
                        model.StockLocations = reader.GetString(11);
                        model.TotalQty = reader.GetDouble(12);
                        model.Status = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,OrderCode,Named,AllowUsers,Remark,StockStartDate,StockEndDate,Customers,Zones,StockLocations,TotalQty,Status,LastUpdatedDate
					   from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderCode = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.AllowUsers = reader.GetString(5);
                        model.Remark = reader.GetString(6);
                        model.StockStartDate = reader.GetDateTime(7);
                        model.StockEndDate = reader.GetDateTime(8);
                        model.Customers = reader.GetString(9);
                        model.Zones = reader.GetString(10);
                        model.StockLocations = reader.GetString(11);
                        model.TotalQty = reader.GetDouble(12);
                        model.Status = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,OrderCode,Named,AllowUsers,Remark,StockStartDate,StockEndDate,Customers,Zones,StockLocations,TotalQty,Status,LastUpdatedDate
                        from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.AllowUsers = reader.GetString(4);
                        model.Remark = reader.GetString(5);
                        model.StockStartDate = reader.GetDateTime(6);
                        model.StockEndDate = reader.GetDateTime(7);
                        model.Customers = reader.GetString(8);
                        model.Zones = reader.GetString(9);
                        model.StockLocations = reader.GetString(10);
                        model.TotalQty = reader.GetDouble(11);
                        model.Status = reader.GetString(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,OrderCode,Named,AllowUsers,Remark,StockStartDate,StockEndDate,Customers,Zones,StockLocations,TotalQty,Status,LastUpdatedDate 
			            from Pandian
					    order by LastUpdatedDate desc ");

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.AllowUsers = reader.GetString(4);
                        model.Remark = reader.GetString(5);
                        model.StockStartDate = reader.GetDateTime(6);
                        model.StockEndDate = reader.GetDateTime(7);
                        model.Customers = reader.GetString(8);
                        model.Zones = reader.GetString(9);
                        model.StockLocations = reader.GetString(10);
                        model.TotalQty = reader.GetDouble(11);
                        model.Status = reader.GetString(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
