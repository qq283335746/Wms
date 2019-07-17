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
    public partial class LogisticsDistribution
    {
        #region ILogisticsDistribution Member

        public LogisticsDistributionInfo GetModel(string orderCode)
        {
            LogisticsDistributionInfo model = null;

            var sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,OrderCode,RefOrders,CompanyId,Vehicles,TotalPackage,TotalVolume,TotalWeight,ToAddress,TypeName,Remark,DeliveryVehicleID,DriverName,DriverPhone,DeliveryStartTime,CauseAbout,DeliveryStatus,Status,RecordDate,LastUpdatedDate 
			            from LogisticsDistribution
						where OrderCode = @OrderCode ");
            var parm = new SqlParameter("@OrderCode", SqlDbType.VarChar, 20);
            parm.Value = orderCode;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parm))
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

        public IList<LogisticsDistributionInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from LogisticsDistribution lgd 
                        left join Company c on c.Id = lgd.CompanyId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<LogisticsDistributionInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by lgd.LastUpdatedDate desc) as RowNumber,
			          lgd.Id,lgd.UserId,lgd.OrderCode,lgd.RefOrders,lgd.CompanyId,lgd.Vehicles,lgd.TotalPackage,lgd.TotalVolume,lgd.TotalWeight,lgd.ToAddress,lgd.TypeName,lgd.Remark,lgd.DeliveryVehicleID,lgd.DriverName,lgd.DriverPhone,lgd.DeliveryStartTime,lgd.CauseAbout,lgd.DeliveryStatus,lgd.Status,lgd.RecordDate,lgd.LastUpdatedDate
					  ,c.Coded CompanyCode,c.Named CompanyName
                      from LogisticsDistribution lgd 
                      left join Company c on c.Id = lgd.CompanyId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<LogisticsDistributionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    var vList = new Vehicle().GetList();
                    while (reader.Read())
                    {
                        var model = new LogisticsDistributionInfo();
                        model.Id = reader.GetGuid(1);
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

                        model.CompanyCode = reader.IsDBNull(22) ? "" : reader.GetString(22);
                        model.CompanyName = reader.IsDBNull(23) ? "" : reader.GetString(23);
                        model.VehicleID = string.Join(",", vList.Where(m=> model.Vehicles.Contains(m.Id.ToString())).Select(m=>m.VehicleID));

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
