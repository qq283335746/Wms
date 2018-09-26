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
    public partial class OrderReceiptProductQuality : IOrderReceiptProductQuality
    {
        #region IOrderReceiptProductQuality Member

        public int Insert(OrderReceiptProductQualityInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderReceiptProductQuality (OrderProductId,CheckQuantity,RejectQuantity,QCStatus,IsQCNeed)
			            values
						(@OrderProductId,@CheckQuantity,@RejectQuantity,@QCStatus,@IsQCNeed)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@OrderProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CheckQuantity",SqlDbType.Float),
                                        new SqlParameter("@RejectQuantity",SqlDbType.Float),
                                        new SqlParameter("@QCStatus",SqlDbType.NVarChar,20),
                                        new SqlParameter("@IsQCNeed",SqlDbType.Bit)
                                   };
            parms[0].Value = model.OrderProductId;
            parms[1].Value = model.CheckQuantity;
            parms[2].Value = model.RejectQuantity;
            parms[3].Value = model.QCStatus;
            parms[4].Value = model.IsQCNeed;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderReceiptProductQualityInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderReceiptProductQuality set CheckQuantity = @CheckQuantity,RejectQuantity = @RejectQuantity,QCStatus = @QCStatus,IsQCNeed = @IsQCNeed 
			            where OrderProductId = @OrderProductId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@OrderProductId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@CheckQuantity",SqlDbType.Float),
                                    new SqlParameter("@RejectQuantity",SqlDbType.Float),
                                    new SqlParameter("@QCStatus",SqlDbType.NVarChar,20),
                                    new SqlParameter("@IsQCNeed",SqlDbType.Bit)
                                   };
            parms[0].Value = model.OrderProductId;
            parms[1].Value = model.CheckQuantity;
            parms[2].Value = model.RejectQuantity;
            parms[3].Value = model.QCStatus;
            parms[4].Value = model.IsQCNeed;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid orderProductId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderReceiptProductQuality where OrderProductId = @OrderProductId ");
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
                sb.Append(@"delete from OrderReceiptProductQuality where OrderProductId = @OrderProductId" + n + " ;");
                SqlParameter parm = new SqlParameter("@OrderProductId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderReceiptProductQualityInfo GetModel(Guid orderProductId)
        {
            OrderReceiptProductQualityInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 OrderProductId,CheckQuantity,RejectQuantity,QCStatus,IsQCNeed 
			            from OrderReceiptProductQuality
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
                        model = new OrderReceiptProductQualityInfo();
                        model.OrderProductId = reader.GetGuid(0);
                        model.CheckQuantity = reader.GetDouble(1);
                        model.RejectQuantity = reader.GetDouble(2);
                        model.QCStatus = reader.GetString(3);
                        model.IsQCNeed = reader.GetBoolean(4);
                    }
                }
            }

            return model;
        }

        public IList<OrderReceiptProductQualityInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderReceiptProductQuality ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderReceiptProductQualityInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          OrderProductId,CheckQuantity,RejectQuantity,QCStatus,IsQCNeed
					  from OrderReceiptProductQuality ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptProductQualityInfo> list = new List<OrderReceiptProductQualityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductQualityInfo model = new OrderReceiptProductQualityInfo();
                        model.OrderProductId = reader.GetGuid(1);
                        model.CheckQuantity = reader.GetDouble(2);
                        model.RejectQuantity = reader.GetDouble(3);
                        model.QCStatus = reader.GetString(4);
                        model.IsQCNeed = reader.GetBoolean(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductQualityInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			           OrderProductId,CheckQuantity,RejectQuantity,QCStatus,IsQCNeed
					   from OrderReceiptProductQuality ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptProductQualityInfo> list = new List<OrderReceiptProductQualityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductQualityInfo model = new OrderReceiptProductQualityInfo();
                        model.OrderProductId = reader.GetGuid(1);
                        model.CheckQuantity = reader.GetDouble(2);
                        model.RejectQuantity = reader.GetDouble(3);
                        model.QCStatus = reader.GetString(4);
                        model.IsQCNeed = reader.GetBoolean(5);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductQualityInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select OrderProductId,CheckQuantity,RejectQuantity,QCStatus,IsQCNeed
                        from OrderReceiptProductQuality ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate ");

            IList<OrderReceiptProductQualityInfo> list = new List<OrderReceiptProductQualityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductQualityInfo model = new OrderReceiptProductQualityInfo();
                        model.OrderProductId = reader.GetGuid(0);
                        model.CheckQuantity = reader.GetDouble(1);
                        model.RejectQuantity = reader.GetDouble(2);
                        model.QCStatus = reader.GetString(3);
                        model.IsQCNeed = reader.GetBoolean(4);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptProductQualityInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select OrderProductId,CheckQuantity,RejectQuantity,QCStatus,IsQCNeed 
			            from OrderReceiptProductQuality
					    order by LastUpdatedDate ");

            IList<OrderReceiptProductQualityInfo> list = new List<OrderReceiptProductQualityInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptProductQualityInfo model = new OrderReceiptProductQualityInfo();
                        model.OrderProductId = reader.GetGuid(0);
                        model.CheckQuantity = reader.GetDouble(1);
                        model.RejectQuantity = reader.GetDouble(2);
                        model.QCStatus = reader.GetString(3);
                        model.IsQCNeed = reader.GetBoolean(4);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
