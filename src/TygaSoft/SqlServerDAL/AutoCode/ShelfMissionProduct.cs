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
    public partial class ShelfMissionProduct : IShelfMissionProduct
    {
        #region IShelfMissionProduct Member

        public int Insert(ShelfMissionProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into ShelfMissionProduct (ShelfMissionId,OrderId,ProductId,StayQty,Qty,StockLocations,Status,LastUpdatedDate)
			            values
						(@ShelfMissionId,@OrderId,@ProductId,@StayQty,@Qty,@StockLocations,@Status,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@ShelfMissionId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@StayQty",SqlDbType.Float),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.ShelfMissionId;
            parms[1].Value = model.OrderId;
            parms[2].Value = model.ProductId;
            parms[3].Value = model.StayQty;
            parms[4].Value = model.Qty;
            parms[5].Value = model.StockLocations;
            parms[6].Value = model.Status;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(ShelfMissionProductInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update ShelfMissionProduct set StayQty = @StayQty,Qty = @Qty,StockLocations = @StockLocations,Status = @Status,LastUpdatedDate = @LastUpdatedDate 
			            where ShelfMissionId = @ShelfMissionId and OrderId = @OrderId and ProductId = @ProductId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@ShelfMissionId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@StayQty",SqlDbType.Float),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.ShelfMissionId;
            parms[1].Value = model.OrderId;
            parms[2].Value = model.ProductId;
            parms[3].Value = model.StayQty;
            parms[4].Value = model.Qty;
            parms[5].Value = model.StockLocations;
            parms[6].Value = model.Status;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid shelfMissionId, Guid orderId, Guid productId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from ShelfMissionProduct where ShelfMissionId = @ShelfMissionId and OrderId = @OrderId and ProductId = @ProductId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@ShelfMissionId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = shelfMissionId;
            parms[1].Value = orderId;
            parms[2].Value = productId;

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
                sb.Append(@"delete from ShelfMissionProduct where ShelfMissionId = @ShelfMissionId" + n + " ;");
                SqlParameter parm = new SqlParameter("@ShelfMissionId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public ShelfMissionProductInfo GetModel(Guid shelfMissionId, Guid orderId, Guid productId)
        {
            ShelfMissionProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 ShelfMissionId,OrderId,ProductId,StayQty,Qty,StockLocations,Status,LastUpdatedDate 
			            from ShelfMissionProduct
						where ShelfMissionId = @ShelfMissionId and OrderId = @OrderId and ProductId = @ProductId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@ShelfMissionId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = shelfMissionId;
            parms[1].Value = orderId;
            parms[2].Value = productId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new ShelfMissionProductInfo();
                        model.ShelfMissionId = reader.GetGuid(0);
                        model.OrderId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.StayQty = reader.GetDouble(3);
                        model.Qty = reader.GetDouble(4);
                        model.StockLocations = reader.GetString(5);
                        model.Status = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }

        public IList<ShelfMissionProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from ShelfMissionProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ShelfMissionProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          ShelfMissionId,OrderId,ProductId,StayQty,Qty,StockLocations,Status,LastUpdatedDate
					  from ShelfMissionProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ShelfMissionProductInfo> list = new List<ShelfMissionProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShelfMissionProductInfo model = new ShelfMissionProductInfo();
                        model.ShelfMissionId = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
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

        public IList<ShelfMissionProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			           ShelfMissionId,OrderId,ProductId,StayQty,Qty,StockLocations,Status,LastUpdatedDate
					   from ShelfMissionProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ShelfMissionProductInfo> list = new List<ShelfMissionProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShelfMissionProductInfo model = new ShelfMissionProductInfo();
                        model.ShelfMissionId = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
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

        public IList<ShelfMissionProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select ShelfMissionId,OrderId,ProductId,StayQty,Qty,StockLocations,Status,LastUpdatedDate
                        from ShelfMissionProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate ");

            IList<ShelfMissionProductInfo> list = new List<ShelfMissionProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShelfMissionProductInfo model = new ShelfMissionProductInfo();
                        model.ShelfMissionId = reader.GetGuid(0);
                        model.OrderId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.StayQty = reader.GetDouble(3);
                        model.Qty = reader.GetDouble(4);
                        model.StockLocations = reader.GetString(5);
                        model.Status = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ShelfMissionProductInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select ShelfMissionId,OrderId,ProductId,StayQty,Qty,StockLocations,Status,LastUpdatedDate 
			            from ShelfMissionProduct
					    order by LastUpdatedDate ");

            IList<ShelfMissionProductInfo> list = new List<ShelfMissionProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShelfMissionProductInfo model = new ShelfMissionProductInfo();
                        model.ShelfMissionId = reader.GetGuid(0);
                        model.OrderId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.StayQty = reader.GetDouble(3);
                        model.Qty = reader.GetDouble(4);
                        model.StockLocations = reader.GetString(5);
                        model.Status = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
