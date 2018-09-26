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
    public partial class OrderPickProduct : IOrderPickProduct
    {
        #region IOrderPickProduct Member

        public IList<OrderPickProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) 
                        from OrderPickProduct opp 
                        left join OrderPicked op on op.Id = opp.OrderPickId
                        left join Product p on p.Id = opp.ProductId 
                        left join Customer c on c.Id = opp.CustomerId
                        ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderPickProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by opp.LastUpdatedDate desc) as RowNumber,
			            opp.OrderPickId,opp.OrderId,opp.ProductId,opp.CustomerId,opp.StayQty,opp.Qty,opp.StockLocations,opp.Status,opp.LastUpdatedDate
                        ,op.OrderCode,p.ProductCode,p.ProductName,c.Coded CustomerCode,c.Named CustomerName
                        from OrderPickProduct opp 
                        left join OrderPicked op on op.Id = opp.OrderPickId
                        left join Product p on p.Id = opp.ProductId
                        left join Customer c on c.Id = opp.CustomerId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderPickProductInfo> list = new List<OrderPickProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    var slpBll = new StockLocationProduct();

                    while (reader.Read())
                    {
                        var model = new OrderPickProductInfo();
                        model.OrderPickId = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.CustomerId = reader.GetGuid(4);
                        model.StayQty = reader.GetDouble(5);
                        model.Qty = reader.GetDouble(6);
                        model.StockLocations = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        model.OrderCode = reader.IsDBNull(10) ? "" : reader.GetString(10);
                        model.ProductCode = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.ProductName = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.CustomerCode = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.CustomerName = reader.IsDBNull(14) ? "" : reader.GetString(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderPickProductInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by opp.LastUpdatedDate desc) as RowNumber,
			            opp.OrderPickId,opp.OrderId,opp.ProductId,opp.CustomerId,opp.StayQty,opp.Qty,opp.StockLocations,opp.Status,opp.LastUpdatedDate
                        ,op.OrderCode,p.ProductCode,p.ProductName,c.Coded CustomerCode,c.Named CustomerName
                        from OrderPickProduct opp 
                        left join OrderPicked op on op.Id = opp.OrderPickId
                        left join Product p on p.Id = opp.ProductId
                        left join Customer c on c.Id = opp.CustomerId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<OrderPickProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    var slpBll = new StockLocationProduct();

                    while (reader.Read())
                    {
                        var model = new OrderPickProductInfo();
                        model.OrderPickId = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.CustomerId = reader.GetGuid(4);
                        model.StayQty = reader.GetDouble(5);
                        model.Qty = reader.GetDouble(6);
                        model.StockLocations = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        model.OrderCode = reader.IsDBNull(10) ? "" : reader.GetString(10);
                        model.ProductCode = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.ProductName = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.CustomerCode = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.CustomerName = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.StockLocations = slpBll.GetNameByProductId(model.ProductId);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderPickProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select opp.OrderPickId,opp.OrderId,opp.ProductId,opp.CustomerId,opp.StayQty,opp.Qty,opp.StockLocations,opp.Status,opp.LastUpdatedDate
                        ,op.OrderCode,p.ProductCode,p.ProductName,c.Coded CustomerCode,c.Named CustomerName                     
                        from OrderPickProduct opp 
                        left join OrderPicked op on op.Id = opp.OrderPickId
                        left join Product p on p.Id = opp.ProductId
                        left join Customer c on c.Id = opp.CustomerId ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate ");

            var list = new List<OrderPickProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderPickProductInfo model = new OrderPickProductInfo();
                        model.OrderPickId = reader.GetGuid(0);
                        model.OrderId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.StayQty = reader.GetDouble(4);
                        model.Qty = reader.GetDouble(5);
                        model.StockLocations = reader.GetString(6);
                        model.Status = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        model.OrderCode = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.ProductCode = reader.IsDBNull(10) ? "" : reader.GetString(10);
                        model.ProductName = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.CustomerCode = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.CustomerName = reader.IsDBNull(13) ? "" : reader.GetString(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
