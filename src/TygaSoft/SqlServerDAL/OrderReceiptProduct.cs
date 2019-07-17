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
    public partial class OrderReceiptProduct
    {
        #region IOrderReceiptProduct Member

        public OrderReceiptProductInfo GetOrderProductModelById(Guid Id)
        {
            OrderReceiptProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 orp.Id,orp.UserId,orp.OrderId,orp.ProductId,orp.PackageId,orp.Unit,orp.ExpectedQty,orp.ReceiptQty,orp.RecordDate,
                        orp.PreOrderCode,orp.PurchaseOrderCode,orp.Status,orp.Sort,orp.Remark,orp.LastUpdatedDate 
                        ,o.OrderCode,o.OrderType
			            from OrderReceiptProduct orp
                        join OrderReceipt o on o.Id = orp.OrderId
						where orp.Id = @Id ");

            SqlParameter[] parms = {
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = Id;

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

                        model.OrderCode = reader.IsDBNull(15) ? "" :  reader.GetString(15);
                        model.OrderType = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                    }
                }
            }

            return model;
        }

        public OrderReceiptProductInfo GetModelByProductcode(object orderId, string productCode)
        {
            OrderReceiptProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 orp.Id,orp.UserId,orp.OrderId,orp.ProductId,orp.PackageId,orp.Unit,orp.ExpectedQty,orp.ReceiptQty,orp.RecordDate,orp.PreOrderCode,orp.PurchaseOrderCode,orp.Status,orp.Sort,orp.Remark,orp.LastUpdatedDate 
			            from OrderReceiptProduct orp
                        join Product p on p.Id = orp.ProductId
						where orp.OrderId = @OrderId and p.ProductCode = @ProductCode ");

            SqlParameter[] parms = {
                new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier),
                new SqlParameter("@ProductCode", SqlDbType.VarChar,30)
            };
            parms[0].Value = Guid.Parse(orderId.ToString());
            parms[1].Value = productCode;

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

        public OrderReceiptProductInfo GetModel(object orderId,object productId)
        {
            OrderReceiptProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,OrderId,ProductId,PackageId,Unit,ExpectedQty,ReceiptQty,RecordDate,PreOrderCode,PurchaseOrderCode,Status,Sort,Remark,LastUpdatedDate 
			            from OrderReceiptProduct
						where OrderId = @OrderId and ProductId = @ProductId ");

            SqlParameter[] parms = {
                new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier),
                new SqlParameter("@ProductId", SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = orderId;
            parms[1].Value = productId;

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

        public int UpdateQty(object orderId, object productId, double receiptAmount)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update OrderReceiptProduct set ReceiptQty = @ReceiptQty, LastUpdatedDate = @LastUpdatedDate 
			            where OrderId = @OrderId and ProductId = @ProductId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@ReceiptQty",SqlDbType.Float),
                                     new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = Guid.Parse(orderId.ToString());
            parms[1].Value = Guid.Parse(productId.ToString());
            parms[2].Value = receiptAmount;
            parms[3].Value = DateTime.Now;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public bool DeleteBatchByJoin(IList<object> list)
        {
            bool result = false;
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from OrderReceiptProduct where Id = @Id" + n + " ;");
                sb.Append(@"delete from OrderReceiptProductAttr where OrderProductId = @Id" + n + " ;");
                sb.Append(@"delete from OrderReceiptProductQuality where OrderProductId = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.WmsDbConnString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int effect = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null);
                        tran.Commit();
                        if (effect > 0) result = true;
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }
            return result;
        }

        public OrderReceiptProductInfo GetModelByJoin(object Id)
        {
            OrderReceiptProductInfo model = null;
            StringBuilder sb = new StringBuilder(250);
            
            sb.Append(@"select top 1
                      orp.Id,orp.UserId,orp.OrderId,orp.ProductId,orp.PackageId,orp.Unit,orp.ExpectedQty,orp.ReceiptQty,orp.RecordDate,orp.PurchaseOrderCode,orp.Status,orp.Sort,orp.Remark,orp.LastUpdatedDate 
					  ,orpa.PackageName,orpa.SupplierName,orpa.ProduceDate,orpa.QualityStatus,orpa.PurchaseOrderCode ProductAttrPurchaseOrderCode
                      ,orpq.CheckQuantity,orpq.RejectQuantity,orpq.QCStatus,orpq.IsQCNeed 
                      ,p.ProductCode,p.ProductName,pa.PackageCode
                      from OrderReceiptProduct orp
                      left join OrderReceiptProductAttr orpa on orpa.OrderProductId = orp.Id
                      left join OrderReceiptProductQuality orpq on orpq.OrderProductId = orp.Id
                      left join Product p on p.Id = orp.ProductId
                      left join Package pa on pa.Id = orp.PackageId
                      where orp.Id = @Id
                      ");

            var parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null && reader.HasRows)
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
                        model.PurchaseOrderCode = reader.GetString(9);
                        model.Status = reader.GetString(10);
                        model.Sort = reader.GetInt32(11);
                        model.Remark = reader.GetString(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);

                        model.PackageName = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.SupplierName = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.ProduceDate = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.QualityStatus = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.ProductAttrPurchaseOrderNum = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        model.CheckQuantity = reader.IsDBNull(19) ? 0 : reader.GetDouble(19);
                        model.RejectQuantity = reader.IsDBNull(20) ? 0 : reader.GetDouble(20);
                        model.QCStatus = reader.IsDBNull(21) ? "" : reader.GetString(21);
                        model.IsQCNeed = reader.IsDBNull(22) ? false : reader.GetBoolean(22);
                        model.ProductCode = reader.IsDBNull(23) ? "" : reader.GetString(23);
                        model.ProductName = reader.IsDBNull(24) ? "" : reader.GetString(24);
                        model.PackageCode = reader.IsDBNull(25) ? "" : reader.GetString(25);
                    }
                }
            }

            return model;
        }

        public IList<OrderReceiptProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder();
            sb.Append(@"select count(*) 
                        from OrderReceiptProduct orp
                        left join OrderReceiptProductAttr orpa on orpa.OrderProductId = orp.Id
                        left join OrderReceiptProductQuality orpq on orpq.OrderProductId = orp.Id
                        left join OrderReceipt o on o.Id = orp.OrderId
                        left join Product p on p.Id = orp.ProductId
                        left join Package pa on pa.Id = orp.PackageId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderReceiptProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by orp.Sort,orp.LastUpdatedDate desc) as RowNumber,
			          orp.Id,orp.UserId,orp.OrderId,orp.ProductId,orp.PackageId,orp.Unit,orp.ExpectedQty,orp.ReceiptQty,orp.RecordDate,orp.PreOrderCode,orp.PurchaseOrderCode,orp.Status,orp.Sort,orp.Remark,orp.LastUpdatedDate 
					  ,orpa.PackageName,orpa.SupplierName,orpa.ProduceDate,orpa.QualityStatus,orpa.PurchaseOrderCode ProductAttrPurchaseOrderCode
                      ,orpq.CheckQuantity,orpq.RejectQuantity,orpq.QCStatus,orpq.IsQCNeed,o.OrderCode 
                      ,p.ProductCode,p.ProductName,pa.PackageCode
                      from OrderReceiptProduct orp
                      left join OrderReceiptProductAttr orpa on orpa.OrderProductId = orp.Id
                      left join OrderReceiptProductQuality orpq on orpq.OrderProductId = orp.Id
                      left join OrderReceipt o on o.Id = orp.OrderId
                      left join Product p on p.Id = orp.ProductId
                      left join Package pa on pa.Id = orp.PackageId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 and ISNULL(ProductCode,'') <> '' {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<OrderReceiptProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new OrderReceiptProductInfo();
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

                        model.PackageName = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.SupplierName = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.ProduceDate = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        model.QualityStatus = reader.IsDBNull(19) ? "" : reader.GetString(19);
                        model.ProductAttrPurchaseOrderNum = reader.IsDBNull(20) ? "" : reader.GetString(20);
                        model.CheckQuantity = reader.IsDBNull(21) ? 0 : reader.GetDouble(21);
                        model.RejectQuantity = reader.IsDBNull(22) ? 0 : reader.GetDouble(22);
                        model.QCStatus = reader.IsDBNull(23) ? "" : reader.GetString(23);
                        model.IsQCNeed = reader.IsDBNull(24) ? false : reader.GetBoolean(24);
                        model.OrderCode = reader.IsDBNull(25) ? "" : reader.GetString(25);
                        model.ProductCode = reader.IsDBNull(26) ? "" : reader.GetString(26);
                        model.ProductName = reader.IsDBNull(27) ? "" : reader.GetString(27);
                        model.PackageCode = reader.IsDBNull(28) ? "" : reader.GetString(28);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(1000);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by orp.Sort,orp.LastUpdatedDate desc) as RowNumber,
			          orp.Id,orp.UserId,orp.OrderId,orp.ProductId,orp.PackageId,orp.Unit,orp.ExpectedQty,orp.ReceiptQty,orp.RecordDate,orp.PurchaseOrderCode,orp.Status,orp.Sort,orp.Remark,orp.LastUpdatedDate 
					  ,orpa.PackageName,orpa.SupplierName,orpa.ProduceDate,orpa.QualityStatus,orpa.PurchaseOrderCode ProductAttrPurchaseOrderCode
                      ,orpq.CheckQuantity,orpq.RejectQuantity,orpq.QCStatus,orpq.IsQCNeed 
                      ,p.ProductCode,p.ProductName,pa.PackageCode
                      from OrderReceiptProduct orp
                      left join OrderReceiptProductAttr orpa on orpa.OrderProductId = orp.Id
                      left join OrderReceiptProductQuality orpq on orpq.OrderProductId = orp.Id
                      left join Product p on p.Id = orp.ProductId
                      left join Package pa on pa.Id = orp.PackageId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 and ISNULL(p.ProductCode,'') <> '' {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<OrderReceiptProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new OrderReceiptProductInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderId = reader.GetGuid(3);
                        model.ProductId = reader.GetGuid(4);
                        model.PackageId = reader.GetGuid(5);
                        model.Unit = reader.GetString(6);
                        model.ExpectedQty = reader.GetDouble(7);
                        model.ReceiptQty = reader.GetDouble(8);
                        model.RecordDate = reader.GetDateTime(9);
                        model.PurchaseOrderCode = reader.GetString(10);
                        model.Status = reader.GetString(11);
                        model.Sort = reader.GetInt32(12);
                        model.Remark = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        model.PackageName = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.SupplierName = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.ProduceDate = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.QualityStatus = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        model.ProductAttrPurchaseOrderNum = reader.IsDBNull(19) ? "" : reader.GetString(19);
                        model.CheckQuantity = reader.IsDBNull(20) ? 0 : reader.GetDouble(20);
                        model.RejectQuantity = reader.IsDBNull(21) ? 0 : reader.GetDouble(21);
                        model.QCStatus = reader.IsDBNull(22) ? "" : reader.GetString(22);
                        model.IsQCNeed = reader.IsDBNull(23) ? false : reader.GetBoolean(23);
                        model.ProductCode = reader.IsDBNull(24) ? "" : reader.GetString(24);
                        model.ProductName = reader.IsDBNull(25) ? "" : reader.GetString(25);
                        model.PackageCode = reader.IsDBNull(26) ? "" : reader.GetString(26);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder();

            sb.Append(@"select row_number() over(order by orp.Sort,orp.LastUpdatedDate desc) as RowNumber,
			          orp.Id,orp.UserId,orp.OrderId,orp.ProductId,orp.PackageId,orp.Unit,orp.ExpectedQty,orp.ReceiptQty,orp.RecordDate,orp.PurchaseOrderCode,orp.Status,orp.Sort,orp.Remark,orp.LastUpdatedDate 
					  ,orpa.PackageName,orpa.SupplierName,orpa.ProduceDate,orpa.QualityStatus,orpa.PurchaseOrderCode ProductAttrPurchaseOrderCode
                      ,orpq.CheckQuantity,orpq.RejectQuantity,orpq.QCStatus,orpq.IsQCNeed 
                      ,p.ProductCode,p.ProductName,pa.PackageCode
                      from OrderReceiptProduct orp
                      left join OrderReceiptProductAttr orpa on orpa.OrderProductId = orp.Id
                      left join OrderReceiptProductQuality orpq on orpq.OrderProductId = orp.Id
                      left join Product p on p.Id = orp.ProductId
                      left join Package pa on pa.Id = orp.PackageId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            var list = new List<OrderReceiptProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new OrderReceiptProductInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderId = reader.GetGuid(3);
                        model.ProductId = reader.GetGuid(4);
                        model.PackageId = reader.GetGuid(5);
                        model.Unit = reader.GetString(6);
                        model.ExpectedQty = reader.GetDouble(7);
                        model.ReceiptQty = reader.GetDouble(8);
                        model.RecordDate = reader.GetDateTime(9);
                        model.PurchaseOrderCode = reader.GetString(10);
                        model.Status = reader.GetString(11);
                        model.Sort = reader.GetInt32(12);
                        model.Remark = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        model.PackageName = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.SupplierName = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.ProduceDate = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.QualityStatus = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        model.ProductAttrPurchaseOrderNum = reader.IsDBNull(19) ? "" : reader.GetString(19);
                        model.CheckQuantity = reader.IsDBNull(20) ? 0 : reader.GetDouble(20);
                        model.RejectQuantity = reader.IsDBNull(21) ? 0 : reader.GetDouble(21);
                        model.QCStatus = reader.IsDBNull(22) ? "" : reader.GetString(22);
                        model.IsQCNeed = reader.IsDBNull(23) ? false : reader.GetBoolean(23);
                        model.ProductCode = reader.IsDBNull(24) ? "" : reader.GetString(24);
                        model.ProductName = reader.IsDBNull(25) ? "" : reader.GetString(25);
                        model.PackageCode = reader.IsDBNull(26) ? "" : reader.GetString(26);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
