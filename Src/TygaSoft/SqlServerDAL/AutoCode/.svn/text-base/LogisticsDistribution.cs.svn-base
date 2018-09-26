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
    public partial class LogisticsDistribution : ILogisticsDistribution
    {
        #region ILogisticsDistribution Member

        public int Insert(LogisticsDistributionInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into LogisticsDistribution (UserId,OrderCode,RefOrders,CompanyId,Vehicles,TotalPackage,TotalVolume,TotalWeight,ToAddress,TypeName,Remark,DeliveryVehicleID,DriverName,DriverPhone,DeliveryStartTime,CauseAbout,DeliveryStatus,Status,RecordDate,LastUpdatedDate)
			            values
						(@UserId,@OrderCode,@RefOrders,@CompanyId,@Vehicles,@TotalPackage,@TotalVolume,@TotalWeight,@ToAddress,@TypeName,@Remark,@DeliveryVehicleID,@DriverName,@DriverPhone,@DeliveryStartTime,@CauseAbout,@DeliveryStatus,@Status,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@RefOrders",SqlDbType.VarChar,1000),
new SqlParameter("@CompanyId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Vehicles",SqlDbType.VarChar,1000),
new SqlParameter("@TotalPackage",SqlDbType.Float),
new SqlParameter("@TotalVolume",SqlDbType.Float),
new SqlParameter("@TotalWeight",SqlDbType.Float),
new SqlParameter("@ToAddress",SqlDbType.NVarChar,100),
new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@DeliveryVehicleID",SqlDbType.NVarChar,20),
new SqlParameter("@DriverName",SqlDbType.NVarChar,30),
new SqlParameter("@DriverPhone",SqlDbType.VarChar,20),
new SqlParameter("@DeliveryStartTime",SqlDbType.DateTime),
new SqlParameter("@CauseAbout",SqlDbType.NVarChar,300),
new SqlParameter("@DeliveryStatus",SqlDbType.NVarChar,20),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.OrderCode;
            parms[2].Value = model.RefOrders;
            parms[3].Value = model.CompanyId;
            parms[4].Value = model.Vehicles;
            parms[5].Value = model.TotalPackage;
            parms[6].Value = model.TotalVolume;
            parms[7].Value = model.TotalWeight;
            parms[8].Value = model.ToAddress;
            parms[9].Value = model.TypeName;
            parms[10].Value = model.Remark;
            parms[11].Value = model.DeliveryVehicleID;
            parms[12].Value = model.DriverName;
            parms[13].Value = model.DriverPhone;
            parms[14].Value = model.DeliveryStartTime;
            parms[15].Value = model.CauseAbout;
            parms[16].Value = model.DeliveryStatus;
            parms[17].Value = model.Status;
            parms[18].Value = model.RecordDate;
            parms[19].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(LogisticsDistributionInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into LogisticsDistribution (Id,UserId,OrderCode,RefOrders,CompanyId,Vehicles,TotalPackage,TotalVolume,TotalWeight,ToAddress,TypeName,Remark,DeliveryVehicleID,DriverName,DriverPhone,DeliveryStartTime,CauseAbout,DeliveryStatus,Status,RecordDate,LastUpdatedDate)
			            values
						(@Id,@UserId,@OrderCode,@RefOrders,@CompanyId,@Vehicles,@TotalPackage,@TotalVolume,@TotalWeight,@ToAddress,@TypeName,@Remark,@DeliveryVehicleID,@DriverName,@DriverPhone,@DeliveryStartTime,@CauseAbout,@DeliveryStatus,@Status,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@RefOrders",SqlDbType.VarChar,1000),
new SqlParameter("@CompanyId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Vehicles",SqlDbType.VarChar,1000),
new SqlParameter("@TotalPackage",SqlDbType.Float),
new SqlParameter("@TotalVolume",SqlDbType.Float),
new SqlParameter("@TotalWeight",SqlDbType.Float),
new SqlParameter("@ToAddress",SqlDbType.NVarChar,100),
new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@DeliveryVehicleID",SqlDbType.NVarChar,20),
new SqlParameter("@DriverName",SqlDbType.NVarChar,30),
new SqlParameter("@DriverPhone",SqlDbType.VarChar,20),
new SqlParameter("@DeliveryStartTime",SqlDbType.DateTime),
new SqlParameter("@CauseAbout",SqlDbType.NVarChar,300),
new SqlParameter("@DeliveryStatus",SqlDbType.NVarChar,20),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OrderCode;
            parms[3].Value = model.RefOrders;
            parms[4].Value = model.CompanyId;
            parms[5].Value = model.Vehicles;
            parms[6].Value = model.TotalPackage;
            parms[7].Value = model.TotalVolume;
            parms[8].Value = model.TotalWeight;
            parms[9].Value = model.ToAddress;
            parms[10].Value = model.TypeName;
            parms[11].Value = model.Remark;
            parms[12].Value = model.DeliveryVehicleID;
            parms[13].Value = model.DriverName;
            parms[14].Value = model.DriverPhone;
            parms[15].Value = model.DeliveryStartTime;
            parms[16].Value = model.CauseAbout;
            parms[17].Value = model.DeliveryStatus;
            parms[18].Value = model.Status;
            parms[19].Value = model.RecordDate;
            parms[20].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(LogisticsDistributionInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update LogisticsDistribution set UserId = @UserId,OrderCode = @OrderCode,RefOrders = @RefOrders,CompanyId = @CompanyId,Vehicles = @Vehicles,TotalPackage = @TotalPackage,TotalVolume = @TotalVolume,TotalWeight = @TotalWeight,ToAddress = @ToAddress,TypeName = @TypeName,Remark = @Remark,DeliveryVehicleID = @DeliveryVehicleID,DriverName = @DriverName,DriverPhone = @DriverPhone,DeliveryStartTime = @DeliveryStartTime,CauseAbout = @CauseAbout,DeliveryStatus = @DeliveryStatus,Status = @Status,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@RefOrders",SqlDbType.VarChar,1000),
new SqlParameter("@CompanyId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Vehicles",SqlDbType.VarChar,1000),
new SqlParameter("@TotalPackage",SqlDbType.Float),
new SqlParameter("@TotalVolume",SqlDbType.Float),
new SqlParameter("@TotalWeight",SqlDbType.Float),
new SqlParameter("@ToAddress",SqlDbType.NVarChar,100),
new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@DeliveryVehicleID",SqlDbType.NVarChar,20),
new SqlParameter("@DriverName",SqlDbType.NVarChar,30),
new SqlParameter("@DriverPhone",SqlDbType.VarChar,20),
new SqlParameter("@DeliveryStartTime",SqlDbType.DateTime),
new SqlParameter("@CauseAbout",SqlDbType.NVarChar,300),
new SqlParameter("@DeliveryStatus",SqlDbType.NVarChar,20),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OrderCode;
            parms[3].Value = model.RefOrders;
            parms[4].Value = model.CompanyId;
            parms[5].Value = model.Vehicles;
            parms[6].Value = model.TotalPackage;
            parms[7].Value = model.TotalVolume;
            parms[8].Value = model.TotalWeight;
            parms[9].Value = model.ToAddress;
            parms[10].Value = model.TypeName;
            parms[11].Value = model.Remark;
            parms[12].Value = model.DeliveryVehicleID;
            parms[13].Value = model.DriverName;
            parms[14].Value = model.DriverPhone;
            parms[15].Value = model.DeliveryStartTime;
            parms[16].Value = model.CauseAbout;
            parms[17].Value = model.DeliveryStatus;
            parms[18].Value = model.Status;
            parms[19].Value = model.RecordDate;
            parms[20].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from LogisticsDistribution where Id = @Id ");
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
                sb.Append(@"delete from LogisticsDistribution where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public LogisticsDistributionInfo GetModel(Guid id)
        {
            LogisticsDistributionInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,OrderCode,RefOrders,CompanyId,Vehicles,TotalPackage,TotalVolume,TotalWeight,ToAddress,TypeName,Remark,DeliveryVehicleID,DriverName,DriverPhone,DeliveryStartTime,CauseAbout,DeliveryStatus,Status,RecordDate,LastUpdatedDate 
			            from LogisticsDistribution
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
                        model = new LogisticsDistributionInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.RefOrders = reader.GetString(3);
                        model.CompanyId = reader.GetGuid(4);
                        model.Vehicles = reader.GetString(5);
                        model.TotalPackage = reader.GetDouble(6);
                        model.TotalVolume = reader.GetDouble(7);
                        model.TotalWeight = reader.GetDouble(8);
                        model.ToAddress = reader.GetString(9);
                        model.TypeName = reader.GetString(10);
                        model.Remark = reader.GetString(11);
                        model.DeliveryVehicleID = reader.GetString(12);
                        model.DriverName = reader.GetString(13);
                        model.DriverPhone = reader.GetString(14);
                        model.DeliveryStartTime = reader.GetDateTime(15);
                        model.CauseAbout = reader.GetString(16);
                        model.DeliveryStatus = reader.GetString(17);
                        model.Status = reader.GetString(18);
                        model.RecordDate = reader.GetDateTime(19);
                        model.LastUpdatedDate = reader.GetDateTime(20);
                    }
                }
            }

            return model;
        }

        public IList<LogisticsDistributionInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from LogisticsDistribution ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<LogisticsDistributionInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,OrderCode,RefOrders,CompanyId,Vehicles,TotalPackage,TotalVolume,TotalWeight,ToAddress,TypeName,Remark,DeliveryVehicleID,DriverName,DriverPhone,DeliveryStartTime,CauseAbout,DeliveryStatus,Status,RecordDate,LastUpdatedDate
					  from LogisticsDistribution ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<LogisticsDistributionInfo> list = new List<LogisticsDistributionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LogisticsDistributionInfo model = new LogisticsDistributionInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderCode = reader.GetString(3);
                        model.RefOrders = reader.GetString(4);
                        model.CompanyId = reader.GetGuid(5);
                        model.Vehicles = reader.GetString(6);
                        model.TotalPackage = reader.GetDouble(7);
                        model.TotalVolume = reader.GetDouble(8);
                        model.TotalWeight = reader.GetDouble(9);
                        model.ToAddress = reader.GetString(10);
                        model.TypeName = reader.GetString(11);
                        model.Remark = reader.GetString(12);
                        model.DeliveryVehicleID = reader.GetString(13);
                        model.DriverName = reader.GetString(14);
                        model.DriverPhone = reader.GetString(15);
                        model.DeliveryStartTime = reader.GetDateTime(16);
                        model.CauseAbout = reader.GetString(17);
                        model.DeliveryStatus = reader.GetString(18);
                        model.Status = reader.GetString(19);
                        model.RecordDate = reader.GetDateTime(20);
                        model.LastUpdatedDate = reader.GetDateTime(21);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<LogisticsDistributionInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,OrderCode,RefOrders,CompanyId,Vehicles,TotalPackage,TotalVolume,TotalWeight,ToAddress,TypeName,Remark,DeliveryVehicleID,DriverName,DriverPhone,DeliveryStartTime,CauseAbout,DeliveryStatus,Status,RecordDate,LastUpdatedDate
					   from LogisticsDistribution ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<LogisticsDistributionInfo> list = new List<LogisticsDistributionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LogisticsDistributionInfo model = new LogisticsDistributionInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderCode = reader.GetString(3);
                        model.RefOrders = reader.GetString(4);
                        model.CompanyId = reader.GetGuid(5);
                        model.Vehicles = reader.GetString(6);
                        model.TotalPackage = reader.GetDouble(7);
                        model.TotalVolume = reader.GetDouble(8);
                        model.TotalWeight = reader.GetDouble(9);
                        model.ToAddress = reader.GetString(10);
                        model.TypeName = reader.GetString(11);
                        model.Remark = reader.GetString(12);
                        model.DeliveryVehicleID = reader.GetString(13);
                        model.DriverName = reader.GetString(14);
                        model.DriverPhone = reader.GetString(15);
                        model.DeliveryStartTime = reader.GetDateTime(16);
                        model.CauseAbout = reader.GetString(17);
                        model.DeliveryStatus = reader.GetString(18);
                        model.Status = reader.GetString(19);
                        model.RecordDate = reader.GetDateTime(20);
                        model.LastUpdatedDate = reader.GetDateTime(21);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<LogisticsDistributionInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,OrderCode,RefOrders,CompanyId,Vehicles,TotalPackage,TotalVolume,TotalWeight,ToAddress,TypeName,Remark,DeliveryVehicleID,DriverName,DriverPhone,DeliveryStartTime,CauseAbout,DeliveryStatus,Status,RecordDate,LastUpdatedDate
                        from LogisticsDistribution ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<LogisticsDistributionInfo> list = new List<LogisticsDistributionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LogisticsDistributionInfo model = new LogisticsDistributionInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.RefOrders = reader.GetString(3);
                        model.CompanyId = reader.GetGuid(4);
                        model.Vehicles = reader.GetString(5);
                        model.TotalPackage = reader.GetDouble(6);
                        model.TotalVolume = reader.GetDouble(7);
                        model.TotalWeight = reader.GetDouble(8);
                        model.ToAddress = reader.GetString(9);
                        model.TypeName = reader.GetString(10);
                        model.Remark = reader.GetString(11);
                        model.DeliveryVehicleID = reader.GetString(12);
                        model.DriverName = reader.GetString(13);
                        model.DriverPhone = reader.GetString(14);
                        model.DeliveryStartTime = reader.GetDateTime(15);
                        model.CauseAbout = reader.GetString(16);
                        model.DeliveryStatus = reader.GetString(17);
                        model.Status = reader.GetString(18);
                        model.RecordDate = reader.GetDateTime(19);
                        model.LastUpdatedDate = reader.GetDateTime(20);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<LogisticsDistributionInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,OrderCode,RefOrders,CompanyId,Vehicles,TotalPackage,TotalVolume,TotalWeight,ToAddress,TypeName,Remark,DeliveryVehicleID,DriverName,DriverPhone,DeliveryStartTime,CauseAbout,DeliveryStatus,Status,RecordDate,LastUpdatedDate 
			            from LogisticsDistribution
					    order by LastUpdatedDate desc ");

            IList<LogisticsDistributionInfo> list = new List<LogisticsDistributionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LogisticsDistributionInfo model = new LogisticsDistributionInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.RefOrders = reader.GetString(3);
                        model.CompanyId = reader.GetGuid(4);
                        model.Vehicles = reader.GetString(5);
                        model.TotalPackage = reader.GetDouble(6);
                        model.TotalVolume = reader.GetDouble(7);
                        model.TotalWeight = reader.GetDouble(8);
                        model.ToAddress = reader.GetString(9);
                        model.TypeName = reader.GetString(10);
                        model.Remark = reader.GetString(11);
                        model.DeliveryVehicleID = reader.GetString(12);
                        model.DriverName = reader.GetString(13);
                        model.DriverPhone = reader.GetString(14);
                        model.DeliveryStartTime = reader.GetDateTime(15);
                        model.CauseAbout = reader.GetString(16);
                        model.DeliveryStatus = reader.GetString(17);
                        model.Status = reader.GetString(18);
                        model.RecordDate = reader.GetDateTime(19);
                        model.LastUpdatedDate = reader.GetDateTime(20);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
