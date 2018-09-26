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
    public partial class Package : IPackage
    {
        #region IPackage Member

        public int Insert(PackageInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into Package (UserId,CustomerId,ProductId,PackageCode,TotalPiece,TotalInsidePackage,TotalBox,TotalTray,UnitXml,Remark,LastUpdatedDate)
			            values
						(@UserId,@CustomerId,@ProductId,@PackageCode,@TotalPiece,@TotalInsidePackage,@TotalBox,@TotalTray,@UnitXml,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
new SqlParameter("@PackageCode",SqlDbType.VarChar,36),
new SqlParameter("@TotalPiece",SqlDbType.Float),
new SqlParameter("@TotalInsidePackage",SqlDbType.Float),
new SqlParameter("@TotalBox",SqlDbType.Float),
new SqlParameter("@TotalTray",SqlDbType.Float),
new SqlParameter("@UnitXml",SqlDbType.NVarChar,3000),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.CustomerId;
            parms[2].Value = model.ProductId;
            parms[3].Value = model.PackageCode;
            parms[4].Value = model.TotalPiece;
            parms[5].Value = model.TotalInsidePackage;
            parms[6].Value = model.TotalBox;
            parms[7].Value = model.TotalTray;
            parms[8].Value = model.UnitXml;
            parms[9].Value = model.Remark;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(PackageInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update Package set UserId = @UserId,CustomerId = @CustomerId,ProductId = @ProductId,PackageCode = @PackageCode,TotalPiece = @TotalPiece,TotalInsidePackage = @TotalInsidePackage,TotalBox = @TotalBox,TotalTray = @TotalTray,UnitXml = @UnitXml,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
new SqlParameter("@PackageCode",SqlDbType.VarChar,36),
new SqlParameter("@TotalPiece",SqlDbType.Float),
new SqlParameter("@TotalInsidePackage",SqlDbType.Float),
new SqlParameter("@TotalBox",SqlDbType.Float),
new SqlParameter("@TotalTray",SqlDbType.Float),
new SqlParameter("@UnitXml",SqlDbType.NVarChar,3000),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.CustomerId;
            parms[3].Value = model.ProductId;
            parms[4].Value = model.PackageCode;
            parms[5].Value = model.TotalPiece;
            parms[6].Value = model.TotalInsidePackage;
            parms[7].Value = model.TotalBox;
            parms[8].Value = model.TotalTray;
            parms[9].Value = model.UnitXml;
            parms[10].Value = model.Remark;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object Id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Package where Id = @Id");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parm);
        }

        public bool DeleteBatch(IList<object> list)
        {
            bool result = false;
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from Package where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.WmsDbConnString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int effect = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null);
                        tran.Commit();
                        if (effect > 0) result = true;
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }
            return result;
        }

        public PackageInfo GetModel(object Id)
        {
            PackageInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,CustomerId,ProductId,PackageCode,TotalPiece,TotalInsidePackage,TotalBox,TotalTray,UnitXml,Remark,LastUpdatedDate 
			            from Package
						where Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new PackageInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.PackageCode = reader.GetString(4);
                        model.TotalPiece = reader.GetDouble(5);
                        model.TotalInsidePackage = reader.GetDouble(6);
                        model.TotalBox = reader.GetDouble(7);
                        model.TotalTray = reader.GetDouble(8);
                        model.UnitXml = reader.GetString(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);
                    }
                }
            }

            return model;
        }

        public IList<PackageInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from Package ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PackageInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,CustomerId,ProductId,PackageCode,TotalPiece,TotalInsidePackage,TotalBox,TotalTray,UnitXml,Remark,LastUpdatedDate
					  from Package ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PackageInfo> list = new List<PackageInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PackageInfo model = new PackageInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.ProductId = reader.GetGuid(4);
                        model.PackageCode = reader.GetString(5);
                        model.TotalPiece = reader.GetDouble(6);
                        model.TotalInsidePackage = reader.GetDouble(7);
                        model.TotalBox = reader.GetDouble(8);
                        model.TotalTray = reader.GetDouble(9);
                        model.UnitXml = reader.GetString(10);
                        model.Remark = reader.GetString(11);
                        model.LastUpdatedDate = reader.GetDateTime(12);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PackageInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,CustomerId,ProductId,PackageCode,TotalPiece,TotalInsidePackage,TotalBox,TotalTray,UnitXml,Remark,LastUpdatedDate
					   from Package ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PackageInfo> list = new List<PackageInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PackageInfo model = new PackageInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.ProductId = reader.GetGuid(4);
                        model.PackageCode = reader.GetString(5);
                        model.TotalPiece = reader.GetDouble(6);
                        model.TotalInsidePackage = reader.GetDouble(7);
                        model.TotalBox = reader.GetDouble(8);
                        model.TotalTray = reader.GetDouble(9);
                        model.UnitXml = reader.GetString(10);
                        model.Remark = reader.GetString(11);
                        model.LastUpdatedDate = reader.GetDateTime(12);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PackageInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,UserId,CustomerId,ProductId,PackageCode,TotalPiece,TotalInsidePackage,TotalBox,TotalTray,UnitXml,Remark,LastUpdatedDate
                        from Package ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<PackageInfo> list = new List<PackageInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PackageInfo model = new PackageInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.PackageCode = reader.GetString(4);
                        model.TotalPiece = reader.GetDouble(5);
                        model.TotalInsidePackage = reader.GetDouble(6);
                        model.TotalBox = reader.GetDouble(7);
                        model.TotalTray = reader.GetDouble(8);
                        model.UnitXml = reader.GetString(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PackageInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,UserId,CustomerId,ProductId,PackageCode,TotalPiece,TotalInsidePackage,TotalBox,TotalTray,UnitXml,Remark,LastUpdatedDate 
			            from Package
					    order by LastUpdatedDate desc ");

            IList<PackageInfo> list = new List<PackageInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PackageInfo model = new PackageInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.PackageCode = reader.GetString(4);
                        model.TotalPiece = reader.GetDouble(5);
                        model.TotalInsidePackage = reader.GetDouble(6);
                        model.TotalBox = reader.GetDouble(7);
                        model.TotalTray = reader.GetDouble(8);
                        model.UnitXml = reader.GetString(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
