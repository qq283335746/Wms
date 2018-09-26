using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class PandianProduct
    {
        #region IPandianProduct Member

        public int[] GetTotal(object pandianId)
        {
            var cmdText = @"select count(1) as Total from PandianProduct where PandianId = @PandianId and Status != '新建' 
                union all select count(1) as Total from PandianProduct where PandianId = @PandianId and Qty != 0 and  FailQty != 0
                union all select count(1) as Total from PandianProduct where PandianId = @PandianId and Status = '新建' ";

            var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(pandianId.ToString());

            var list = new List<int>(3);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetInt32(0));
                    }
                }
            }

            return list.ToArray();
        }

        public IList<PandianProductInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by pdp.LastUpdatedDate desc) as RowNumber,
			           pdp.PandianId,pdp.ProductId,pdp.CustomerId,pdp.UserId,pdp.Zones,pdp.StockLocations,pdp.StayQty,pdp.UpdatedZones,pdp.UpdatedStockLocations,pdp.Qty,pdp.FailQty,pdp.Status,pdp.Remark,pdp.LastUpdatedDate
					   ,p.ProductCode,p.ProductName,c.Coded CustomerCode,c.Named CustomerName,u.UserName
                       from PandianProduct pdp 
                       left join Product p on p.Id = pdp.ProductId
                       left join Customer c on c.Id = pdp.CustomerId
                       left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = pdp.UserId
                       ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<PandianProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianProductInfo model = new PandianProductInfo();
                        model.PandianId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.Zones = reader.GetString(5);
                        model.StockLocations = reader.GetString(6);
                        model.StayQty = reader.GetDouble(7);
                        model.UpdatedZones = reader.GetString(8);
                        model.UpdatedStockLocations = reader.GetString(9);
                        model.Qty = reader.GetDouble(10);
                        model.FailQty = reader.GetDouble(11);
                        model.Status = reader.GetString(12);
                        model.Remark = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        model.ProductCode = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.ProductName = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.CustomerCode = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.CustomerName = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        model.UserName = reader.IsDBNull(19) ? "" : reader.GetString(19);

                        if (!string.IsNullOrWhiteSpace(model.StockLocations))
                        {
                            var mslList = JsonConvert.DeserializeObject<List<MinStockLocationInfo>>(model.StockLocations);
                            if (mslList != null && mslList.Count > 0)
                            {
                                model.StockLocationCodes = string.Join(",", mslList.Select(m => m.StockLocationCode));
                            }
                        }

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from PandianProduct pdp
                       left join Product p on p.Id = pdp.ProductId
                       left join Customer c on c.Id = pdp.CustomerId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by pdp.LastUpdatedDate desc) as RowNumber,
			          pdp.PandianId,pdp.ProductId,pdp.CustomerId,pdp.UserId,pdp.Zones,pdp.StockLocations,pdp.StayQty,pdp.UpdatedZones,pdp.UpdatedStockLocations,pdp.Qty,pdp.FailQty,pdp.Status,pdp.Remark,pdp.LastUpdatedDate
					  ,p.ProductCode,p.ProductName,c.Coded CustomerCode,c.Named CustomerName,u.UserName
                      from PandianProduct pdp 
                      left join Product p on p.Id = pdp.ProductId
                      left join Customer c on c.Id = pdp.CustomerId
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = pdp.UserId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<PandianProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    var zList = new Zone().GetList();

                    while (reader.Read())
                    {
                        var model = new PandianProductInfo();
                        model.PandianId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.Zones = reader.GetString(5);
                        model.StockLocations = reader.GetString(6);
                        model.StayQty = reader.GetDouble(7);
                        model.UpdatedZones = reader.GetString(8);
                        model.UpdatedStockLocations = reader.GetString(9);
                        model.Qty = reader.GetDouble(10);
                        model.FailQty = reader.GetDouble(11);
                        model.Status = reader.GetString(12);
                        model.Remark = reader.GetString(13);

                        model.ProductCode = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.ProductName = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.CustomerCode = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.CustomerName = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        model.UserName = reader.IsDBNull(19) ? "" : reader.GetString(19);

                        if (!string.IsNullOrWhiteSpace(model.StockLocations))
                        {
                            var mslList = JsonConvert.DeserializeObject<List<MinStockLocationInfo>>(model.StockLocations);
                            if (mslList != null && mslList.Count > 0)
                            {
                                model.StockLocationCodes = string.Join(",", mslList.Select(m=>m.StockLocationCode));
                            }
                        }

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
