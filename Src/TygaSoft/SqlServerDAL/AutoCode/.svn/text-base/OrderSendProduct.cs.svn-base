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
    public partial class OrderSendProduct : IOrderSendProduct
    {
        #region IOrderSendProduct Member

        public int Insert(OrderSendProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderSendProduct (OrderId,ProductId,CustomerId,Qty,PickQty,Status,LastUpdatedDate)
			            values
						(@OrderId,@ProductId,@CustomerId,@Qty,@PickQty,@Status,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@PickQty",SqlDbType.Float),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.OrderId;
            parms[1].Value = model.ProductId;
            parms[2].Value = model.CustomerId;
            parms[3].Value = model.Qty;
            parms[4].Value = model.PickQty;
            parms[5].Value = model.Status;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderSendProductInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderSendProduct set Qty = @Qty,PickQty = @PickQty,Status = @Status,LastUpdatedDate = @LastUpdatedDate 
			            where OrderId = @OrderId and ProductId = @ProductId and CustomerId = @CustomerId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@Qty",SqlDbType.Float),
                                    new SqlParameter("@PickQty",SqlDbType.Float),
                                    new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                    new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.OrderId;
            parms[1].Value = model.ProductId;
            parms[2].Value = model.CustomerId;
            parms[3].Value = model.Qty;
            parms[4].Value = model.PickQty;
            parms[5].Value = model.Status;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid orderId, Guid productId, Guid customerId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderSendProduct where OrderId = @OrderId and ProductId = @ProductId and CustomerId = @CustomerId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = orderId;
            parms[1].Value = productId;
            parms[2].Value = customerId;

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
                sb.Append(@"delete from OrderSendProduct where OrderId = @OrderId" + n + " ;");
                SqlParameter parm = new SqlParameter("@OrderId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderSendProductInfo GetModel(Guid orderId, Guid productId, Guid customerId)
        {
            OrderSendProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 OrderId,ProductId,CustomerId,Qty,PickQty,Status,LastUpdatedDate 
			            from OrderSendProduct
						where OrderId = @OrderId and ProductId = @ProductId and CustomerId = @CustomerId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = orderId;
            parms[1].Value = productId;
            parms[2].Value = customerId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrderSendProductInfo();
                        model.OrderId = reader.GetGuid(0);
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.Qty = reader.GetDouble(3);
                        model.PickQty = reader.GetDouble(4);
                        model.Status = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                    }
                }
            }

            return model;
        }

        public IList<OrderSendProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderSendProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderSendProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          OrderId,ProductId,CustomerId,Qty,PickQty,Status,LastUpdatedDate
					  from OrderSendProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderSendProductInfo> list = new List<OrderSendProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderSendProductInfo model = new OrderSendProductInfo();
                        model.OrderId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.Qty = reader.GetDouble(4);
                        model.PickQty = reader.GetDouble(5);
                        model.Status = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderSendProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			           OrderId,ProductId,CustomerId,Qty,PickQty,Status,LastUpdatedDate
					   from OrderSendProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderSendProductInfo> list = new List<OrderSendProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderSendProductInfo model = new OrderSendProductInfo();
                        model.OrderId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.Qty = reader.GetDouble(4);
                        model.PickQty = reader.GetDouble(5);
                        model.Status = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderSendProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select OrderId,ProductId,CustomerId,Qty,PickQty,Status,LastUpdatedDate
                        from OrderSendProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate ");

            IList<OrderSendProductInfo> list = new List<OrderSendProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderSendProductInfo model = new OrderSendProductInfo();
                        model.OrderId = reader.GetGuid(0);
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.Qty = reader.GetDouble(3);
                        model.PickQty = reader.GetDouble(4);
                        model.Status = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderSendProductInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select OrderId,ProductId,CustomerId,Qty,PickQty,Status,LastUpdatedDate 
			            from OrderSendProduct
					    order by LastUpdatedDate ");

            IList<OrderSendProductInfo> list = new List<OrderSendProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderSendProductInfo model = new OrderSendProductInfo();
                        model.OrderId = reader.GetGuid(0);
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.Qty = reader.GetDouble(3);
                        model.PickQty = reader.GetDouble(4);
                        model.Status = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
