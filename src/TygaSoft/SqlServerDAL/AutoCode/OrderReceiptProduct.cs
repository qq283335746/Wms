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
    public partial class OrderReceiptProduct : IOrderReceiptProduct
    {
        #region IOrderReceiptProduct Member

        public int Insert(OrderReceiptProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderReceiptProduct (UserId,OrderId,ProductId,PackageId,Unit,ExpectedQty,ReceiptQty,RecordDate,PreOrderCode,PurchaseOrderCode,Status,Sort,Remark,LastUpdatedDate)
			            values
						(@UserId,@OrderId,@ProductId,@PackageId,@Unit,@ExpectedQty,@ReceiptQty,@RecordDate,@PreOrderCode,@PurchaseOrderCode,@Status,@Sort,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@PackageId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Unit",SqlDbType.NVarChar,10),
                                        new SqlParameter("@ExpectedQty",SqlDbType.Float),
                                        new SqlParameter("@ReceiptQty",SqlDbType.Float),
                                        new SqlParameter("@RecordDate",SqlDbType.DateTime),
                                        new SqlParameter("@PreOrderCode",SqlDbType.VarChar,20),
                                        new SqlParameter("@PurchaseOrderCode",SqlDbType.VarChar,20),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.OrderId;
            parms[2].Value = model.ProductId;
            parms[3].Value = model.PackageId;
            parms[4].Value = model.Unit;
            parms[5].Value = model.ExpectedQty;
            parms[6].Value = model.ReceiptQty;
            parms[7].Value = model.RecordDate;
            parms[8].Value = model.PreOrderCode;
            parms[9].Value = model.PurchaseOrderCode;
            parms[10].Value = model.Status;
            parms[11].Value = model.Sort;
            parms[12].Value = model.Remark;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(OrderReceiptProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderReceiptProduct (Id,UserId,OrderId,ProductId,PackageId,Unit,ExpectedQty,ReceiptQty,RecordDate,PreOrderCode,PurchaseOrderCode,Status,Sort,Remark,LastUpdatedDate)
			            values
						(@Id,@UserId,@OrderId,@ProductId,@PackageId,@Unit,@ExpectedQty,@ReceiptQty,@RecordDate,@PreOrderCode,@PurchaseOrderCode,@Status,@Sort,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@PackageId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Unit",SqlDbType.NVarChar,10),
                                        new SqlParameter("@ExpectedQty",SqlDbType.Float),
                                        new SqlParameter("@ReceiptQty",SqlDbType.Float),
                                        new SqlParameter("@RecordDate",SqlDbType.DateTime),
                                        new SqlParameter("@PreOrderCode",SqlDbType.VarChar,20),
                                        new SqlParameter("@PurchaseOrderCode",SqlDbType.VarChar,20),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OrderId;
            parms[3].Value = model.ProductId;
            parms[4].Value = model.PackageId;
            parms[5].Value = model.Unit;
            parms[6].Value = model.ExpectedQty;
            parms[7].Value = model.ReceiptQty;
            parms[8].Value = model.RecordDate;
            parms[9].Value = model.PreOrderCode;
            parms[10].Value = model.PurchaseOrderCode;
            parms[11].Value = model.Status;
            parms[12].Value = model.Sort;
            parms[13].Value = model.Remark;
            parms[14].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderReceiptProductInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderReceiptProduct set UserId = @UserId,OrderId = @OrderId,ProductId = @ProductId,PackageId = @PackageId,Unit = @Unit,ExpectedQty = @ExpectedQty,ReceiptQty = @ReceiptQty,RecordDate = @RecordDate,PreOrderCode = @PreOrderCode,PurchaseOrderCode = @PurchaseOrderCode,Status = @Status,Sort = @Sort,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@PackageId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Unit",SqlDbType.NVarChar,10),
                                        new SqlParameter("@ExpectedQty",SqlDbType.Float),
                                        new SqlParameter("@ReceiptQty",SqlDbType.Float),
                                        new SqlParameter("@RecordDate",SqlDbType.DateTime),
                                        new SqlParameter("@PreOrderCode",SqlDbType.VarChar,20),
                                        new SqlParameter("@PurchaseOrderCode",SqlDbType.VarChar,20),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OrderId;
            parms[3].Value = model.ProductId;
            parms[4].Value = model.PackageId;
            parms[5].Value = model.Unit;
            parms[6].Value = model.ExpectedQty;
            parms[7].Value = model.ReceiptQty;
            parms[8].Value = model.RecordDate;
            parms[9].Value = model.PreOrderCode;
            parms[10].Value = model.PurchaseOrderCode;
            parms[11].Value = model.Status;
            parms[12].Value = model.Sort;
            parms[13].Value = model.Remark;
            parms[14].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderReceiptProduct where Id = @Id ");
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
                sb.Append(@"delete from OrderReceiptProduct where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderReceiptProductInfo GetModel(Guid id)
        {
            OrderReceiptProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,OrderId,ProductId,PackageId,Unit,ExpectedQty,ReceiptQty,RecordDate,PreOrderCode,PurchaseOrderCode,Status,Sort,Remark,LastUpdatedDate 
			            from OrderReceiptProduct
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
                        model = new OrderReceiptProductInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.PackageId = reader.GetGuid(4);
                        model.Unit = reader.GetString(5);
                        model.ExpectedQty = reader.GetDouble(6);
                        model.ReceiptQty = reader.GetDouble(7);
                        model.RecordDate = reader.GetDateTime(8);
                        model.PreOrderCode = reader.GetString(9);
                        model.PurchaseOrderCode = reader.GetString(10);
                        model.Status = reader.GetString(11);
                        model.Sort = reader.GetInt32(12);
                        model.Remark = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);
                    }
                }
            }

            return model;
        }

        public IList<OrderReceiptProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderReceiptProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderReceiptProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          Id,UserId,OrderId,ProductId,PackageId,Unit,ExpectedQty,ReceiptQty,RecordDate,PreOrderCode,PurchaseOrderCode,Status,Sort,Remark,LastUpdatedDate
					  from OrderReceiptProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptProductInfo> list = new List<OrderReceiptProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductInfo model = new OrderReceiptProductInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderId = reader.GetGuid(3);
                        model.ProductId = reader.GetGuid(4);
                        model.PackageId = reader.GetGuid(5);
                        model.Unit = reader.GetString(6);
                        model.ExpectedQty = reader.GetDouble(7);
                        model.ReceiptQty = reader.GetDouble(8);
                        model.RecordDate = reader.GetDateTime(9);
                        model.PreOrderCode = reader.GetString(10);
                        model.PurchaseOrderCode = reader.GetString(11);
                        model.Status = reader.GetString(12);
                        model.Sort = reader.GetInt32(13);
                        model.Remark = reader.GetString(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           Id,UserId,OrderId,ProductId,PackageId,Unit,ExpectedQty,ReceiptQty,RecordDate,PreOrderCode,PurchaseOrderCode,Status,Sort,Remark,LastUpdatedDate
					   from OrderReceiptProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptProductInfo> list = new List<OrderReceiptProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductInfo model = new OrderReceiptProductInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderId = reader.GetGuid(3);
                        model.ProductId = reader.GetGuid(4);
                        model.PackageId = reader.GetGuid(5);
                        model.Unit = reader.GetString(6);
                        model.ExpectedQty = reader.GetDouble(7);
                        model.ReceiptQty = reader.GetDouble(8);
                        model.RecordDate = reader.GetDateTime(9);
                        model.PreOrderCode = reader.GetString(10);
                        model.PurchaseOrderCode = reader.GetString(11);
                        model.Status = reader.GetString(12);
                        model.Sort = reader.GetInt32(13);
                        model.Remark = reader.GetString(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,OrderId,ProductId,PackageId,Unit,ExpectedQty,ReceiptQty,RecordDate,PreOrderCode,PurchaseOrderCode,Status,Sort,Remark,LastUpdatedDate
                        from OrderReceiptProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<OrderReceiptProductInfo> list = new List<OrderReceiptProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductInfo model = new OrderReceiptProductInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.PackageId = reader.GetGuid(4);
                        model.Unit = reader.GetString(5);
                        model.ExpectedQty = reader.GetDouble(6);
                        model.ReceiptQty = reader.GetDouble(7);
                        model.RecordDate = reader.GetDateTime(8);
                        model.PreOrderCode = reader.GetString(9);
                        model.PurchaseOrderCode = reader.GetString(10);
                        model.Status = reader.GetString(11);
                        model.Sort = reader.GetInt32(12);
                        model.Remark = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,OrderId,ProductId,PackageId,Unit,ExpectedQty,ReceiptQty,RecordDate,PreOrderCode,PurchaseOrderCode,Status,Sort,Remark,LastUpdatedDate 
			            from OrderReceiptProduct
					    order by Sort ");

            IList<OrderReceiptProductInfo> list = new List<OrderReceiptProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductInfo model = new OrderReceiptProductInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OrderId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.PackageId = reader.GetGuid(4);
                        model.Unit = reader.GetString(5);
                        model.ExpectedQty = reader.GetDouble(6);
                        model.ReceiptQty = reader.GetDouble(7);
                        model.RecordDate = reader.GetDateTime(8);
                        model.PreOrderCode = reader.GetString(9);
                        model.PurchaseOrderCode = reader.GetString(10);
                        model.Status = reader.GetString(11);
                        model.Sort = reader.GetInt32(12);
                        model.Remark = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
