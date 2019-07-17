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
    public partial class OrderSend : IOrderSend
    {
        #region IOrderSend Member

        public int Insert(OrderSendInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderSend (UserId,OrderCode,CustomerId,StayQty,Qty,Remark,Status,Sort,IsDisable,LastUpdatedDate)
			            values
						(@UserId,@OrderCode,@CustomerId,@StayQty,@Qty,@Remark,@Status,@Sort,@IsDisable,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@StayQty",SqlDbType.Float),
new SqlParameter("@Qty",SqlDbType.Float),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@Status",SqlDbType.TinyInt),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@IsDisable",SqlDbType.Bit),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.OrderCode;
            parms[2].Value = model.CustomerId;
            parms[3].Value = model.StayQty;
            parms[4].Value = model.Qty;
            parms[5].Value = model.Remark;
            parms[6].Value = model.Status;
            parms[7].Value = model.Sort;
            parms[8].Value = model.IsDisable;
            parms[9].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(OrderSendInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderSend (Id,UserId,OrderCode,CustomerId,StayQty,Qty,Remark,Status,Sort,IsDisable,LastUpdatedDate)
			            values
						(@Id,@UserId,@OrderCode,@CustomerId,@StayQty,@Qty,@Remark,@Status,@Sort,@IsDisable,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@StayQty",SqlDbType.Float),
new SqlParameter("@Qty",SqlDbType.Float),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@Status",SqlDbType.TinyInt),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@IsDisable",SqlDbType.Bit),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OrderCode;
            parms[3].Value = model.CustomerId;
            parms[4].Value = model.StayQty;
            parms[5].Value = model.Qty;
            parms[6].Value = model.Remark;
            parms[7].Value = model.Status;
            parms[8].Value = model.Sort;
            parms[9].Value = model.IsDisable;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderSendInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderSend set UserId = @UserId,OrderCode = @OrderCode,CustomerId = @CustomerId,StayQty = @StayQty,Qty = @Qty,Remark = @Remark,Status = @Status,Sort = @Sort,IsDisable = @IsDisable,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@StayQty",SqlDbType.Float),
new SqlParameter("@Qty",SqlDbType.Float),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@Status",SqlDbType.TinyInt),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@IsDisable",SqlDbType.Bit),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OrderCode;
            parms[3].Value = model.CustomerId;
            parms[4].Value = model.StayQty;
            parms[5].Value = model.Qty;
            parms[6].Value = model.Remark;
            parms[7].Value = model.Status;
            parms[8].Value = model.Sort;
            parms[9].Value = model.IsDisable;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderSend where Id = @Id ");
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
                sb.Append(@"delete from OrderSend where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderSendInfo GetModel(Guid id)
        {
            OrderSendInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,OrderCode,CustomerId,StayQty,Qty,Remark,Status,Sort,IsDisable,LastUpdatedDate 
			            from OrderSend
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
                        model = new OrderSendInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.StayQty = reader.GetDouble(4);
                        model.Qty = reader.GetDouble(5);
                        model.Remark = reader.GetString(6);
                        model.Status = reader.GetByte(7);
                        model.Sort = reader.GetInt32(8);
                        model.IsDisable = reader.GetBoolean(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);
                    }
                }
            }

            return model;
        }

        public IList<OrderSendInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderSend ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderSendInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Status,Sort) as RowNumber,
			          Id,UserId,OrderCode,CustomerId,StayQty,Qty,Remark,Status,Sort,IsDisable,LastUpdatedDate
					  from OrderSend ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderSendInfo> list = new List<OrderSendInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderSendInfo model = new OrderSendInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderCode = reader.GetString(3);
                        model.CustomerId = reader.GetGuid(4);
                        model.StayQty = reader.GetDouble(5);
                        model.Qty = reader.GetDouble(6);
                        model.Remark = reader.GetString(7);
                        model.Status = reader.GetByte(8);
                        model.Sort = reader.GetInt32(9);
                        model.IsDisable = reader.GetBoolean(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderSendInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Status,Sort) as RowNumber,
			           Id,UserId,OrderCode,CustomerId,StayQty,Qty,Remark,Status,Sort,IsDisable,LastUpdatedDate
					   from OrderSend ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderSendInfo> list = new List<OrderSendInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderSendInfo model = new OrderSendInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderCode = reader.GetString(3);
                        model.CustomerId = reader.GetGuid(4);
                        model.StayQty = reader.GetDouble(5);
                        model.Qty = reader.GetDouble(6);
                        model.Remark = reader.GetString(7);
                        model.Status = reader.GetByte(8);
                        model.Sort = reader.GetInt32(9);
                        model.IsDisable = reader.GetBoolean(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderSendInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,OrderCode,CustomerId,StayQty,Qty,Remark,Status,Sort,IsDisable,LastUpdatedDate
                        from OrderSend ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("Status,Sort ");

            IList<OrderSendInfo> list = new List<OrderSendInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderSendInfo model = new OrderSendInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.StayQty = reader.GetDouble(4);
                        model.Qty = reader.GetDouble(5);
                        model.Remark = reader.GetString(6);
                        model.Status = reader.GetByte(7);
                        model.Sort = reader.GetInt32(8);
                        model.IsDisable = reader.GetBoolean(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderSendInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,OrderCode,CustomerId,StayQty,Qty,Remark,Status,Sort,IsDisable,LastUpdatedDate 
			            from OrderSend
					    Status,Sort ");

            IList<OrderSendInfo> list = new List<OrderSendInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderSendInfo model = new OrderSendInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.StayQty = reader.GetDouble(4);
                        model.Qty = reader.GetDouble(5);
                        model.Remark = reader.GetString(6);
                        model.Status = reader.GetByte(7);
                        model.Sort = reader.GetInt32(8);
                        model.IsDisable = reader.GetBoolean(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
