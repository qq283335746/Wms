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
    public partial class OrderReceipt : IOrderReceipt
    {
        #region IOrderReceipt Member

        public int Insert(OrderReceiptInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderReceipt (UserId,CustomerId,SupplierId,OrderCode,OrderType,PreOrderCode,PurchaseOrderCode,TypeName,SettlementDate,RecordDate,IsStopNext,Status,Sort,Remark,LastUpdatedDate)
			            values
						(@UserId,@CustomerId,@SupplierId,@OrderCode,@OrderType,@PreOrderCode,@PurchaseOrderCode,@TypeName,@SettlementDate,@RecordDate,@IsStopNext,@Status,@Sort,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@SupplierId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@OrderType",SqlDbType.Int),
new SqlParameter("@PreOrderCode",SqlDbType.VarChar,20),
new SqlParameter("@PurchaseOrderCode",SqlDbType.VarChar,20),
new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
new SqlParameter("@SettlementDate",SqlDbType.DateTime),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@IsStopNext",SqlDbType.Bit),
new SqlParameter("@Status",SqlDbType.TinyInt),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.CustomerId;
            parms[2].Value = model.SupplierId;
            parms[3].Value = model.OrderCode;
            parms[4].Value = model.OrderType;
            parms[5].Value = model.PreOrderCode;
            parms[6].Value = model.PurchaseOrderCode;
            parms[7].Value = model.TypeName;
            parms[8].Value = model.SettlementDate;
            parms[9].Value = model.RecordDate;
            parms[10].Value = model.IsStopNext;
            parms[11].Value = model.Status;
            parms[12].Value = model.Sort;
            parms[13].Value = model.Remark;
            parms[14].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(OrderReceiptInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderReceipt (Id,UserId,CustomerId,SupplierId,OrderCode,OrderType,PreOrderCode,PurchaseOrderCode,TypeName,SettlementDate,RecordDate,IsStopNext,Status,Sort,Remark,LastUpdatedDate)
			            values
						(@Id,@UserId,@CustomerId,@SupplierId,@OrderCode,@OrderType,@PreOrderCode,@PurchaseOrderCode,@TypeName,@SettlementDate,@RecordDate,@IsStopNext,@Status,@Sort,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@SupplierId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@OrderType",SqlDbType.Int),
new SqlParameter("@PreOrderCode",SqlDbType.VarChar,20),
new SqlParameter("@PurchaseOrderCode",SqlDbType.VarChar,20),
new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
new SqlParameter("@SettlementDate",SqlDbType.DateTime),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@IsStopNext",SqlDbType.Bit),
new SqlParameter("@Status",SqlDbType.TinyInt),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.CustomerId;
            parms[3].Value = model.SupplierId;
            parms[4].Value = model.OrderCode;
            parms[5].Value = model.OrderType;
            parms[6].Value = model.PreOrderCode;
            parms[7].Value = model.PurchaseOrderCode;
            parms[8].Value = model.TypeName;
            parms[9].Value = model.SettlementDate;
            parms[10].Value = model.RecordDate;
            parms[11].Value = model.IsStopNext;
            parms[12].Value = model.Status;
            parms[13].Value = model.Sort;
            parms[14].Value = model.Remark;
            parms[15].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderReceiptInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderReceipt set UserId = @UserId,CustomerId = @CustomerId,SupplierId = @SupplierId,OrderCode = @OrderCode,OrderType = @OrderType,PreOrderCode = @PreOrderCode,PurchaseOrderCode = @PurchaseOrderCode,TypeName = @TypeName,SettlementDate = @SettlementDate,RecordDate = @RecordDate,IsStopNext = @IsStopNext,Status = @Status,Sort = @Sort,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@SupplierId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@OrderType",SqlDbType.Int),
new SqlParameter("@PreOrderCode",SqlDbType.VarChar,20),
new SqlParameter("@PurchaseOrderCode",SqlDbType.VarChar,20),
new SqlParameter("@TypeName",SqlDbType.NVarChar,20),
new SqlParameter("@SettlementDate",SqlDbType.DateTime),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@IsStopNext",SqlDbType.Bit),
new SqlParameter("@Status",SqlDbType.TinyInt),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.CustomerId;
            parms[3].Value = model.SupplierId;
            parms[4].Value = model.OrderCode;
            parms[5].Value = model.OrderType;
            parms[6].Value = model.PreOrderCode;
            parms[7].Value = model.PurchaseOrderCode;
            parms[8].Value = model.TypeName;
            parms[9].Value = model.SettlementDate;
            parms[10].Value = model.RecordDate;
            parms[11].Value = model.IsStopNext;
            parms[12].Value = model.Status;
            parms[13].Value = model.Sort;
            parms[14].Value = model.Remark;
            parms[15].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderReceipt where Id = @Id ");
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
                sb.Append(@"delete from OrderReceipt where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderReceiptInfo GetModel(Guid id)
        {
            OrderReceiptInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,CustomerId,SupplierId,OrderCode,OrderType,PreOrderCode,PurchaseOrderCode,TypeName,SettlementDate,RecordDate,IsStopNext,Status,Sort,Remark,LastUpdatedDate 
			            from OrderReceipt
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
                        model = new OrderReceiptInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.SupplierId = reader.GetGuid(3);
                        model.OrderCode = reader.GetString(4);
                        model.OrderType = reader.GetInt32(5);
                        model.PreOrderCode = reader.GetString(6);
                        model.PurchaseOrderCode = reader.GetString(7);
                        model.TypeName = reader.GetString(8);
                        model.SettlementDate = reader.GetDateTime(9);
                        model.RecordDate = reader.GetDateTime(10);
                        model.IsStopNext = reader.GetBoolean(11);
                        model.Status = reader.GetByte(12);
                        model.Sort = reader.GetInt32(13);
                        model.Remark = reader.GetString(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);
                    }
                }
            }

            return model;
        }

        public IList<OrderReceiptInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderReceipt ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderReceiptInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Status,Sort) as RowNumber,
			          Id,UserId,CustomerId,SupplierId,OrderCode,OrderType,PreOrderCode,PurchaseOrderCode,TypeName,SettlementDate,RecordDate,IsStopNext,Status,Sort,Remark,LastUpdatedDate
					  from OrderReceipt ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptInfo> list = new List<OrderReceiptInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptInfo model = new OrderReceiptInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.SupplierId = reader.GetGuid(4);
                        model.OrderCode = reader.GetString(5);
                        model.OrderType = reader.GetInt32(6);
                        model.PreOrderCode = reader.GetString(7);
                        model.PurchaseOrderCode = reader.GetString(8);
                        model.TypeName = reader.GetString(9);
                        model.SettlementDate = reader.GetDateTime(10);
                        model.RecordDate = reader.GetDateTime(11);
                        model.IsStopNext = reader.GetBoolean(12);
                        model.Status = reader.GetByte(13);
                        model.Sort = reader.GetInt32(14);
                        model.Remark = reader.GetString(15);
                        model.LastUpdatedDate = reader.GetDateTime(16);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Status,Sort) as RowNumber,
			           Id,UserId,CustomerId,SupplierId,OrderCode,OrderType,PreOrderCode,PurchaseOrderCode,TypeName,SettlementDate,RecordDate,IsStopNext,Status,Sort,Remark,LastUpdatedDate
					   from OrderReceipt ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptInfo> list = new List<OrderReceiptInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptInfo model = new OrderReceiptInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.SupplierId = reader.GetGuid(4);
                        model.OrderCode = reader.GetString(5);
                        model.OrderType = reader.GetInt32(6);
                        model.PreOrderCode = reader.GetString(7);
                        model.PurchaseOrderCode = reader.GetString(8);
                        model.TypeName = reader.GetString(9);
                        model.SettlementDate = reader.GetDateTime(10);
                        model.RecordDate = reader.GetDateTime(11);
                        model.IsStopNext = reader.GetBoolean(12);
                        model.Status = reader.GetByte(13);
                        model.Sort = reader.GetInt32(14);
                        model.Remark = reader.GetString(15);
                        model.LastUpdatedDate = reader.GetDateTime(16);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,CustomerId,SupplierId,OrderCode,OrderType,PreOrderCode,PurchaseOrderCode,TypeName,SettlementDate,RecordDate,IsStopNext,Status,Sort,Remark,LastUpdatedDate
                        from OrderReceipt ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("Status,Sort ");

            IList<OrderReceiptInfo> list = new List<OrderReceiptInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptInfo model = new OrderReceiptInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.SupplierId = reader.GetGuid(3);
                        model.OrderCode = reader.GetString(4);
                        model.OrderType = reader.GetInt32(5);
                        model.PreOrderCode = reader.GetString(6);
                        model.PurchaseOrderCode = reader.GetString(7);
                        model.TypeName = reader.GetString(8);
                        model.SettlementDate = reader.GetDateTime(9);
                        model.RecordDate = reader.GetDateTime(10);
                        model.IsStopNext = reader.GetBoolean(11);
                        model.Status = reader.GetByte(12);
                        model.Sort = reader.GetInt32(13);
                        model.Remark = reader.GetString(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,CustomerId,SupplierId,OrderCode,OrderType,PreOrderCode,PurchaseOrderCode,TypeName,SettlementDate,RecordDate,IsStopNext,Status,Sort,Remark,LastUpdatedDate 
			            from OrderReceipt
					    Status,Sort ");

            IList<OrderReceiptInfo> list = new List<OrderReceiptInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptInfo model = new OrderReceiptInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.SupplierId = reader.GetGuid(3);
                        model.OrderCode = reader.GetString(4);
                        model.OrderType = reader.GetInt32(5);
                        model.PreOrderCode = reader.GetString(6);
                        model.PurchaseOrderCode = reader.GetString(7);
                        model.TypeName = reader.GetString(8);
                        model.SettlementDate = reader.GetDateTime(9);
                        model.RecordDate = reader.GetDateTime(10);
                        model.IsStopNext = reader.GetBoolean(11);
                        model.Status = reader.GetByte(12);
                        model.Sort = reader.GetInt32(13);
                        model.Remark = reader.GetString(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
