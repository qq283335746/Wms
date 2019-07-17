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
    public partial class OrderReceiptRecord
    {
        #region IOrderReceiptRecord Member

        public IList<OrderReceiptRecordInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(1500);
            sb.Append(@"select count(*) from Order_Receipt_Record orr 
                        left join Order_Receipt_Base orb on orb.Id = orr.OrderId
                        left join Customer c on c.Id = orb.CustomerId 
                        left join Product p on p.Id = orr.ProductId 
                        left join Package pk on pk.Id = orr.PackageId 
                        left join StockLocation sl on sl.Id = orr.StockLocationId 
                       ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderReceiptRecordInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by orr.LastUpdatedDate desc) as RowNumber,
			          orr.Id,orr.OrderId,orr.UserId,orr.ProductId,orr.PackageId,orr.StockLocationId,orr.Unit,orr.Qty,orr.LPN,orr.LastUpdatedDate
                      ,orb.OrderNum,(case orb.Status when 0 then '其它' when 1 then '新建' else '其它' end) OrderStatus,c.Coded CustomerCode,c.Named CustomerName,p.ProductCode,p.ProductName,pk.PackageCode
                      ,sl.Code StockLocationCode,sl.Named StockLocationName
					  from Order_Receipt_Record orr
                      left join Order_Receipt_Base orb on orb.Id = orr.OrderId
                      left join Customer c on c.Id = orb.CustomerId 
                      left join Product p on p.Id = orr.ProductId 
                      left join Package pk on pk.Id = orr.PackageId 
                      left join StockLocation sl on sl.Id = orr.StockLocationId 
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptRecordInfo> list = new List<OrderReceiptRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptRecordInfo model = new OrderReceiptRecordInfo();
                        model.Id = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.UserId = reader.GetGuid(3);
                        model.ProductId = reader.GetGuid(4);
                        model.PackageId = reader.GetGuid(5);
                        model.StockLocationId = reader.GetGuid(6);
                        model.Unit = reader.GetString(7);
                        model.Qty = reader.GetDouble(8);
                        model.LPN = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        model.OrderNum = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.OrderStatus = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.CustomerCode = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.CustomerName = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.ProductCode = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.ProductName = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.PackageCode = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.StockLocationCode = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        model.StockLocationName = reader.IsDBNull(19) ? "" : reader.GetString(19);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
