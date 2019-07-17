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
    public partial class OrderReceiptRecord : IOrderReceiptRecord
    {
        #region IOrderReceiptRecord Member

        public int Insert(OrderReceiptRecordInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderReceiptRecord (OrderId,UserId,ProductId,PackageId,StockLocationId,Unit,Qty,LPN,LastUpdatedDate)
			            values
						(@OrderId,@UserId,@ProductId,@PackageId,@StockLocationId,@Unit,@Qty,@LPN,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@PackageId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Unit",SqlDbType.NVarChar,10),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@LPN",SqlDbType.VarChar,36),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.OrderId;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ProductId;
            parms[3].Value = model.PackageId;
            parms[4].Value = model.StockLocationId;
            parms[5].Value = model.Unit;
            parms[6].Value = model.Qty;
            parms[7].Value = model.LPN;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(OrderReceiptRecordInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderReceiptRecord (Id,OrderId,UserId,ProductId,PackageId,StockLocationId,Unit,Qty,LPN,LastUpdatedDate)
			            values
						(@Id,@OrderId,@UserId,@ProductId,@PackageId,@StockLocationId,@Unit,@Qty,@LPN,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@PackageId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Unit",SqlDbType.NVarChar,10),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@LPN",SqlDbType.VarChar,36),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.OrderId;
            parms[2].Value = model.UserId;
            parms[3].Value = model.ProductId;
            parms[4].Value = model.PackageId;
            parms[5].Value = model.StockLocationId;
            parms[6].Value = model.Unit;
            parms[7].Value = model.Qty;
            parms[8].Value = model.LPN;
            parms[9].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderReceiptRecordInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderReceiptRecord set OrderId = @OrderId,UserId = @UserId,ProductId = @ProductId,PackageId = @PackageId,StockLocationId = @StockLocationId,Unit = @Unit,Qty = @Qty,LPN = @LPN,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@PackageId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Unit",SqlDbType.NVarChar,10),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@LPN",SqlDbType.VarChar,36),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.OrderId;
            parms[2].Value = model.UserId;
            parms[3].Value = model.ProductId;
            parms[4].Value = model.PackageId;
            parms[5].Value = model.StockLocationId;
            parms[6].Value = model.Unit;
            parms[7].Value = model.Qty;
            parms[8].Value = model.LPN;
            parms[9].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderReceiptRecord where Id = @Id ");
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
                sb.Append(@"delete from OrderReceiptRecord where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderReceiptRecordInfo GetModel(Guid id)
        {
            OrderReceiptRecordInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,OrderId,UserId,ProductId,PackageId,StockLocationId,Unit,Qty,LPN,LastUpdatedDate 
			            from OrderReceiptRecord
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
                        model = new OrderReceiptRecordInfo();
                        model.Id = reader.GetGuid(0);
                        model.OrderId = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.PackageId = reader.GetGuid(4);
                        model.StockLocationId = reader.GetGuid(5);
                        model.Unit = reader.GetString(6);
                        model.Qty = reader.GetDouble(7);
                        model.LPN = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);
                    }
                }
            }

            return model;
        }

        public IList<OrderReceiptRecordInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderReceiptRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderReceiptRecordInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,OrderId,UserId,ProductId,PackageId,StockLocationId,Unit,Qty,LPN,LastUpdatedDate
					  from OrderReceiptRecord ");
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptRecordInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,OrderId,UserId,ProductId,PackageId,StockLocationId,Unit,Qty,LPN,LastUpdatedDate
					   from OrderReceiptRecord ");
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptRecordInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,OrderId,UserId,ProductId,PackageId,StockLocationId,Unit,Qty,LPN,LastUpdatedDate
                        from OrderReceiptRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<OrderReceiptRecordInfo> list = new List<OrderReceiptRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptRecordInfo model = new OrderReceiptRecordInfo();
                        model.Id = reader.GetGuid(0);
                        model.OrderId = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.PackageId = reader.GetGuid(4);
                        model.StockLocationId = reader.GetGuid(5);
                        model.Unit = reader.GetString(6);
                        model.Qty = reader.GetDouble(7);
                        model.LPN = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptRecordInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,OrderId,UserId,ProductId,PackageId,StockLocationId,Unit,Qty,LPN,LastUpdatedDate 
			            from OrderReceiptRecord
					    order by LastUpdatedDate desc ");

            IList<OrderReceiptRecordInfo> list = new List<OrderReceiptRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptRecordInfo model = new OrderReceiptRecordInfo();
                        model.Id = reader.GetGuid(0);
                        model.OrderId = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.PackageId = reader.GetGuid(4);
                        model.StockLocationId = reader.GetGuid(5);
                        model.Unit = reader.GetString(6);
                        model.Qty = reader.GetDouble(7);
                        model.LPN = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
