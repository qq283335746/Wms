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
    public partial class OrderReceiptProductAttr : IOrderReceiptProductAttr
    {
        #region IOrderReceiptProductAttr Member

        public int Insert(OrderReceiptProductAttrInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderReceiptProductAttr (OrderProductId,PackageName,SupplierName,ProduceDate,QualityStatus,PurchaseOrderCode)
			            values
						(@OrderProductId,@PackageName,@SupplierName,@ProduceDate,@QualityStatus,@PurchaseOrderCode)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@OrderProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@PackageName",SqlDbType.NVarChar,20),
                                        new SqlParameter("@SupplierName",SqlDbType.NVarChar,20),
                                        new SqlParameter("@ProduceDate",SqlDbType.VarChar,20),
                                        new SqlParameter("@QualityStatus",SqlDbType.NVarChar,20),
                                        new SqlParameter("@PurchaseOrderCode",SqlDbType.VarChar,20)
                                   };
            parms[0].Value = model.OrderProductId;
            parms[1].Value = model.PackageName;
            parms[2].Value = model.SupplierName;
            parms[3].Value = model.ProduceDate;
            parms[4].Value = model.QualityStatus;
            parms[5].Value = model.PurchaseOrderCode;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderReceiptProductAttrInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderReceiptProductAttr set PackageName = @PackageName,SupplierName = @SupplierName,ProduceDate = @ProduceDate,QualityStatus = @QualityStatus,PurchaseOrderCode = @PurchaseOrderCode 
			            where OrderProductId = @OrderProductId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@OrderProductId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@PackageName",SqlDbType.NVarChar,20),
                                    new SqlParameter("@SupplierName",SqlDbType.NVarChar,20),
                                    new SqlParameter("@ProduceDate",SqlDbType.VarChar,20),
                                    new SqlParameter("@QualityStatus",SqlDbType.NVarChar,20),
                                    new SqlParameter("@PurchaseOrderCode",SqlDbType.VarChar,20)
                                   };
            parms[0].Value = model.OrderProductId;
            parms[1].Value = model.PackageName;
            parms[2].Value = model.SupplierName;
            parms[3].Value = model.ProduceDate;
            parms[4].Value = model.QualityStatus;
            parms[5].Value = model.PurchaseOrderCode;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid orderProductId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderReceiptProductAttr where OrderProductId = @OrderProductId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderProductId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = orderProductId;

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
                sb.Append(@"delete from OrderReceiptProductAttr where OrderProductId = @OrderProductId" + n + " ;");
                SqlParameter parm = new SqlParameter("@OrderProductId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderReceiptProductAttrInfo GetModel(Guid orderProductId)
        {
            OrderReceiptProductAttrInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 OrderProductId,PackageName,SupplierName,ProduceDate,QualityStatus,PurchaseOrderCode 
			            from OrderReceiptProductAttr
						where OrderProductId = @OrderProductId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderProductId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = orderProductId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrderReceiptProductAttrInfo();
                        model.OrderProductId = reader.GetGuid(0);
                        model.PackageName = reader.GetString(1);
                        model.SupplierName = reader.GetString(2);
                        model.ProduceDate = reader.GetString(3);
                        model.QualityStatus = reader.GetString(4);
                        model.PurchaseOrderCode = reader.GetString(5);
                    }
                }
            }

            return model;
        }

        public IList<OrderReceiptProductAttrInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderReceiptProductAttr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderReceiptProductAttrInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          OrderProductId,PackageName,SupplierName,ProduceDate,QualityStatus,PurchaseOrderCode
					  from OrderReceiptProductAttr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptProductAttrInfo> list = new List<OrderReceiptProductAttrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductAttrInfo model = new OrderReceiptProductAttrInfo();
                        model.OrderProductId = reader.GetGuid(1);
                        model.PackageName = reader.GetString(2);
                        model.SupplierName = reader.GetString(3);
                        model.ProduceDate = reader.GetString(4);
                        model.QualityStatus = reader.GetString(5);
                        model.PurchaseOrderCode = reader.GetString(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductAttrInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			           OrderProductId,PackageName,SupplierName,ProduceDate,QualityStatus,PurchaseOrderCode
					   from OrderReceiptProductAttr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptProductAttrInfo> list = new List<OrderReceiptProductAttrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductAttrInfo model = new OrderReceiptProductAttrInfo();
                        model.OrderProductId = reader.GetGuid(1);
                        model.PackageName = reader.GetString(2);
                        model.SupplierName = reader.GetString(3);
                        model.ProduceDate = reader.GetString(4);
                        model.QualityStatus = reader.GetString(5);
                        model.PurchaseOrderCode = reader.GetString(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductAttrInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select OrderProductId,PackageName,SupplierName,ProduceDate,QualityStatus,PurchaseOrderCode
                        from OrderReceiptProductAttr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate ");

            IList<OrderReceiptProductAttrInfo> list = new List<OrderReceiptProductAttrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductAttrInfo model = new OrderReceiptProductAttrInfo();
                        model.OrderProductId = reader.GetGuid(0);
                        model.PackageName = reader.GetString(1);
                        model.SupplierName = reader.GetString(2);
                        model.ProduceDate = reader.GetString(3);
                        model.QualityStatus = reader.GetString(4);
                        model.PurchaseOrderCode = reader.GetString(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductAttrInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select OrderProductId,PackageName,SupplierName,ProduceDate,QualityStatus,PurchaseOrderCode 
			            from OrderReceiptProductAttr
					    order by LastUpdatedDate ");

            IList<OrderReceiptProductAttrInfo> list = new List<OrderReceiptProductAttrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductAttrInfo model = new OrderReceiptProductAttrInfo();
                        model.OrderProductId = reader.GetGuid(0);
                        model.PackageName = reader.GetString(1);
                        model.SupplierName = reader.GetString(2);
                        model.ProduceDate = reader.GetString(3);
                        model.QualityStatus = reader.GetString(4);
                        model.PurchaseOrderCode = reader.GetString(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
