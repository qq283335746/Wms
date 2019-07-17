using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using TygaSoft.DBUtility;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.SqlServerDAL
{
    public partial class ShelfMissionProduct : IShelfMissionProduct
    {
        public IList<ShelfMissionProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*)
                      from ShelfMissionProduct smp
                      left join Product p on p.Id = smp.ProductId 
                      left join OrderReceipt o on o.Id = smp.OrderId ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ShelfMissionProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by smp.LastUpdatedDate desc) as RowNumber,
                      smp.ShelfMissionId,smp.OrderId,smp.ProductId,smp.StayQty,smp.Qty,smp.StockLocations,smp.Status,smp.LastUpdatedDate
			          ,p.ProductCode,p.ProductName,o.OrderCode
                      from ShelfMissionProduct smp
                      left join Product p on p.Id = smp.ProductId 
                      left join OrderReceipt o on o.Id = smp.OrderId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<ShelfMissionProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new ShelfMissionProductInfo();
                        model.ShelfMissionId = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.StayQty = reader.GetDouble(4);
                        model.Qty = reader.GetDouble(5);
                        model.StockLocations = reader.GetString(6);
                        model.Status = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        model.ProductCode = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.ProductName = reader.IsDBNull(10) ? "" : reader.GetString(10);
                        model.OrderReceiptCode = reader.IsDBNull(11) ? "" : reader.GetString(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ShelfMissionProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);

            sb.Append(@"select smp.ShelfMissionId,smp.OrderId,smp.ProductId,smp.StayQty,smp.Qty,smp.StockLocations,smp.Status,smp.LastUpdatedDate
			          ,p.ProductCode,p.ProductName,o.OrderCode
                      from ShelfMissionProduct smp
                      left join Product p on p.Id = smp.ProductId 
                      left join OrderReceipt o on o.Id = smp.OrderId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            var list = new List<ShelfMissionProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    var slList = new StockLocation().GetList();

                    while (reader.Read())
                    {
                        var model = new ShelfMissionProductInfo();
                        model.ShelfMissionId = reader.GetGuid(0);
                        model.OrderId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.StayQty = reader.GetDouble(3);
                        model.Qty = reader.GetDouble(4);
                        model.StockLocations = reader.GetString(5);
                        model.Status = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        model.ProductCode = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.ProductName = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.OrderReceiptCode = reader.IsDBNull(10) ? "" : reader.GetString(10);

                        var currStockLocations = new List<string>();
                        if (!string.IsNullOrWhiteSpace(model.StockLocations))
                        {
                            var dicSl = new Dictionary<string, float>();
                            var slItems = JsonConvert.DeserializeObject<Dictionary<string, float>>(model.StockLocations);

                            foreach (var slItem in slItems)
                            {
                                var slInfo = slList.FirstOrDefault(m => m.Id.ToString() == slItem.Key);
                                if (slInfo != null)
                                {
                                    currStockLocations.Add(slInfo.Code);
                                }
                            }
                        }
                        if (currStockLocations.Count > 0) model.StockLocations = string.Join(",", currStockLocations);
                        else model.StockLocations = "";

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ShelfMissionProductInfo> GetListByScanned(Guid shelfMissionId)
        {
            var list = new List<ShelfMissionProductInfo>();

            var cmdText = @"select p.Id ProductId,p.ProductCode,smp.Qty
                            from ShelfMission sm 
                            join ShelfMissionProduct smp on smp.ShelfMissionId = sm.Id
                            join Product p on p.Id = smp.ProductId
                            where sm.Id = @Id";
            var parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = shelfMissionId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new ShelfMissionProductInfo();
                        model.ProductId = reader.GetGuid(0);
                        model.ProductCode = reader.GetString(1);
                        model.Qty = reader.GetDouble(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }
    }
}
