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
using TygaSoft.SysException;

namespace TygaSoft.SqlServerDAL
{
    public partial class StockProduct
    {
        #region IStockProduct Member

        public int UpdateWarnMsg(Guid productId,Guid customerId, string warnMsg)
        {
            var cmdText = @"update StockProduct set WarnMsg = @WarnMsg
			              where ProductId = @ProductId and CustomerId = @CustomerId ";

            SqlParameter[] parms = {
                                     new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@WarnMsg",SqlDbType.NVarChar,256)
                                   };
            parms[0].Value = productId;
            parms[1].Value = customerId;
            parms[2].Value = warnMsg;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parms);
        }

        public IList<OrderSelectProductInfo> GetSelectProductList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by sp.LastUpdatedDate desc) as RowNumber,
                        p.Id ProductId,p.ProductCode,p.ProductName,sp.Qty,sp.UnQty 
                        from StockProduct sp 
                        join Product p on p.Id = sp.ProductId
                        ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<OrderSelectProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(new OrderSelectProductInfo(reader.GetGuid(1),reader.GetString(2),reader.GetString(3),reader.GetDouble(4),reader.GetDouble(5),0));
                    }
                }
            }

            return list;
        }

        public IList<StockProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(500);
            sb.Append(@"select count(*) from StockProduct sp 
                        left join Customer c on c.Id = sp.CustomerId 
                        left join Product p on p.Id = sp.ProductId 
                       ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StockProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by sp.LastUpdatedDate desc) as RowNumber,
			          sp.ProductId,sp.CustomerId,sp.Qty,sp.UnQty,sp.FreezeQty,sp.StepCode,sp.LastStepName,sp.Status,sp.StockLocations,sp.WarnMsg,sp.LastUpdatedDate
                      ,c.Coded CustomerCode,c.Named CustomerName,p.ProductCode,p.ProductName
					  from StockProduct sp 
                      left join Customer c on c.Id = sp.CustomerId 
                      left join Product p on p.Id = sp.ProductId 
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<StockProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new StockProductInfo();
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.Qty = reader.GetDouble(3);
                        model.UnQty = reader.GetDouble(4);
                        model.FreezeQty = reader.GetDouble(5);
                        model.StepCode = reader.GetString(6);
                        model.LastStepName = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.StockLocations = reader.GetString(9);
                        model.WarnMsg = reader.GetString(10);
                        //model.LastUpdatedDate = reader.GetDateTime(11);

                        model.CustomerCode = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.CustomerName = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.ProductCode = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.ProductName = reader.IsDBNull(15) ? "" : reader.GetString(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockProductInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by sp.LastUpdatedDate desc) as RowNumber,
			          sp.ProductId,sp.CustomerId,sp.Qty,sp.UnQty,sp.FreezeQty,sp.StepCode,sp.LastStepName,sp.Status,sp.StockLocations,sp.LastUpdatedDate
                      ,c.Coded CustomerCode,c.Named CustomerName,p.ProductCode,p.ProductName
					  from StockProduct sp 
                      left join Customer c on c.Id = sp.CustomerId 
                      left join Product p on p.Id = sp.ProductId 
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<StockProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    var slpBll = new StockLocationProduct();

                    while (reader.Read())
                    {
                        var model = new StockProductInfo();
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.Qty = reader.GetDouble(3);
                        model.UnQty = reader.GetDouble(4);
                        model.FreezeQty = reader.GetDouble(5);
                        model.StepCode = reader.GetString(6);
                        model.LastStepName = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.StockLocations = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        model.CustomerCode = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.CustomerName = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.ProductCode = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.ProductName = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.StockLocationName = slpBll.GetNameByProductId(model.ProductId);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var list = new List<StockProductInfo>();

            var sb = new StringBuilder(1000);
            sb.Append(@"select sp.ProductId,sp.CustomerId,sp.Qty,sp.UnQty,sp.FreezeQty,sp.StepCode,sp.LastStepName,sp.Status,sp.StockLocations,sp.LastUpdatedDate
                      ,c.Coded CustomerCode,c.Named CustomerName,p.ProductCode,p.ProductName
					  from StockProduct sp 
                      left join Customer c on c.Id = sp.CustomerId 
                      left join Product p on p.Id = sp.ProductId 
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    var slpBll = new StockLocationProduct();

                    while (reader.Read())
                    {
                        var model = new StockProductInfo();
                        model.ProductId = reader.GetGuid(0);
                        model.CustomerId = reader.GetGuid(1);
                        model.Qty = reader.GetDouble(2);
                        model.UnQty = reader.GetDouble(3);
                        model.FreezeQty = reader.GetDouble(4);
                        model.StepCode = reader.GetString(5);
                        model.LastStepName = reader.GetString(6);
                        model.Status = reader.GetString(7);
                        model.StockLocations = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        model.CustomerCode = reader.IsDBNull(10) ? "" : reader.GetString(10);
                        model.CustomerName = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.ProductCode = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.ProductName = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.StockLocationName = slpBll.GetNameByProductId(model.ProductId);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public DataSet GetExportData()
        {
            var cmdText = @"select p.ProductCode '货品代码',p.ProductName '货品名称',c.Coded '客户代码',c.Named '客户名称',sp.Qty '数量',sp.UnQty '暂存区数量',sp.FreezeQty '冻结数量'
					      from StockProduct sp 
                          left join Customer c on c.Id = sp.CustomerId 
                          left join Product p on p.Id = sp.ProductId 
                         ";
            return SqlHelper.ExecuteDataset(SqlHelper.WmsDbConnString, CommandType.Text, cmdText);
        }

        #endregion
    }
}
