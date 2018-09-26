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

        public int Insert(OrderPickProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderPickProduct (OrderPickId,OrderId,ProductId,CustomerId,StayQty,Qty,StockLocations,Status,LastUpdatedDate)
			            values
						(@OrderPickId,@OrderId,@ProductId,@CustomerId,@StayQty,@Qty,@StockLocations,@Status,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@OrderPickId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@StayQty",SqlDbType.Float),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.OrderPickId;
            parms[1].Value = model.OrderId;
            parms[2].Value = model.ProductId;
            parms[3].Value = model.CustomerId;
            parms[4].Value = model.StayQty;
            parms[5].Value = model.Qty;
            parms[6].Value = model.StockLocations;
            parms[7].Value = model.Status;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderPickProductInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderPickProduct set StayQty = @StayQty,Qty = @Qty,StockLocations = @StockLocations,Status = @Status,LastUpdatedDate = @LastUpdatedDate 
			            where OrderPickId = @OrderPickId and OrderId = @OrderId and ProductId = @ProductId and CustomerId = @CustomerId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@OrderPickId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@StayQty",SqlDbType.Float),
                                    new SqlParameter("@Qty",SqlDbType.Float),
                                    new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                    new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                    new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.OrderPickId;
            parms[1].Value = model.OrderId;
            parms[2].Value = model.ProductId;
            parms[3].Value = model.CustomerId;
            parms[4].Value = model.StayQty;
            parms[5].Value = model.Qty;
            parms[6].Value = model.StockLocations;
            parms[7].Value = model.Status;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid orderPickId, Guid orderId, Guid productId, Guid customerId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderPickProduct where OrderPickId = @OrderPickId and OrderId = @OrderId and ProductId = @ProductId and CustomerId = @CustomerId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderPickId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = orderPickId;
            parms[1].Value = orderId;
            parms[2].Value = productId;
            parms[3].Value = customerId;

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
                sb.Append(@"delete from OrderPickProduct where OrderPickId = @OrderPickId" + n + " ;");
                SqlParameter parm = new SqlParameter("@OrderPickId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderPickProductInfo GetModel(Guid orderPickId, Guid orderId, Guid productId, Guid customerId)
        {
            OrderPickProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 OrderPickId,OrderId,ProductId,CustomerId,StayQty,Qty,StockLocations,Status,LastUpdatedDate 
			            from OrderPickProduct
						where OrderPickId = @OrderPickId and OrderId = @OrderId and ProductId = @ProductId and CustomerId = @CustomerId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderPickId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = orderPickId;
            parms[1].Value = orderId;
            parms[2].Value = productId;
            parms[3].Value = customerId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrderPickProductInfo();
                        model.OrderPickId = reader.GetGuid(0);
                        model.OrderId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.StayQty = reader.GetDouble(4);
                        model.Qty = reader.GetDouble(5);
                        model.StockLocations = reader.GetString(6);
                        model.Status = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                    }
                }
            }

            return model;
        }

        public IList<OrderPickProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderPickProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderPickProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          OrderPickId,OrderId,ProductId,CustomerId,StayQty,Qty,StockLocations,Status,LastUpdatedDate
					  from OrderPickProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderPickProductInfo> list = new List<OrderPickProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderPickProductInfo model = new OrderPickProductInfo();
                        model.OrderPickId = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.CustomerId = reader.GetGuid(4);
                        model.StayQty = reader.GetDouble(5);
                        model.Qty = reader.GetDouble(6);
                        model.StockLocations = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderPickProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			           OrderPickId,OrderId,ProductId,CustomerId,StayQty,Qty,StockLocations,Status,LastUpdatedDate
					   from OrderPickProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderPickProductInfo> list = new List<OrderPickProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderPickProductInfo model = new OrderPickProductInfo();
                        model.OrderPickId = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.CustomerId = reader.GetGuid(4);
                        model.StayQty = reader.GetDouble(5);
                        model.Qty = reader.GetDouble(6);
                        model.StockLocations = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderPickProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select OrderPickId,OrderId,ProductId,CustomerId,StayQty,Qty,StockLocations,Status,LastUpdatedDate
                        from OrderPickProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate ");

            IList<OrderPickProductInfo> list = new List<OrderPickProductInfo>();

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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderPickProductInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select OrderPickId,OrderId,ProductId,CustomerId,StayQty,Qty,StockLocations,Status,LastUpdatedDate 
			            from OrderPickProduct
					    order by LastUpdatedDate ");

            IList<OrderPickProductInfo> list = new List<OrderPickProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
