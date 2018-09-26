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
    public partial class OrderReceiptAttr : IOrderReceiptAttr
    {
        #region IOrderReceiptAttr Member

        public int Insert(OrderReceiptAttrInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderReceiptAttr (LastTakeDate,ExpectTakeDate,SendDate,PlanSendDate,RMA,ExpectVolume,GW,CustomAttr)
			            values
						(@LastTakeDate,@ExpectTakeDate,@SendDate,@PlanSendDate,@RMA,@ExpectVolume,@GW,@CustomAttr)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@LastTakeDate",SqlDbType.DateTime),
                                        new SqlParameter("@ExpectTakeDate",SqlDbType.DateTime),
                                        new SqlParameter("@SendDate",SqlDbType.DateTime),
                                        new SqlParameter("@PlanSendDate",SqlDbType.DateTime),
                                        new SqlParameter("@RMA",SqlDbType.VarChar,20),
                                        new SqlParameter("@ExpectVolume",SqlDbType.Float),
                                        new SqlParameter("@GW",SqlDbType.Float),
                                        new SqlParameter("@CustomAttr",SqlDbType.NVarChar,3000)
                                   };
            parms[0].Value = model.LastTakeDate;
            parms[1].Value = model.ExpectTakeDate;
            parms[2].Value = model.SendDate;
            parms[3].Value = model.PlanSendDate;
            parms[4].Value = model.RMA;
            parms[5].Value = model.ExpectVolume;
            parms[6].Value = model.GW;
            parms[7].Value = model.CustomAttr;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(OrderReceiptAttrInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderReceiptAttr (OrderId,LastTakeDate,ExpectTakeDate,SendDate,PlanSendDate,RMA,ExpectVolume,GW,CustomAttr)
			            values
						(@OrderId,@LastTakeDate,@ExpectTakeDate,@SendDate,@PlanSendDate,@RMA,@ExpectVolume,@GW,@CustomAttr)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@LastTakeDate",SqlDbType.DateTime),
                                        new SqlParameter("@ExpectTakeDate",SqlDbType.DateTime),
                                        new SqlParameter("@SendDate",SqlDbType.DateTime),
                                        new SqlParameter("@PlanSendDate",SqlDbType.DateTime),
                                        new SqlParameter("@RMA",SqlDbType.VarChar,20),
                                        new SqlParameter("@ExpectVolume",SqlDbType.Float),
                                        new SqlParameter("@GW",SqlDbType.Float),
                                        new SqlParameter("@CustomAttr",SqlDbType.NVarChar,3000)
                                   };
            parms[0].Value = model.OrderId;
            parms[1].Value = model.LastTakeDate;
            parms[2].Value = model.ExpectTakeDate;
            parms[3].Value = model.SendDate;
            parms[4].Value = model.PlanSendDate;
            parms[5].Value = model.RMA;
            parms[6].Value = model.ExpectVolume;
            parms[7].Value = model.GW;
            parms[8].Value = model.CustomAttr;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderReceiptAttrInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderReceiptAttr set LastTakeDate = @LastTakeDate,ExpectTakeDate = @ExpectTakeDate,SendDate = @SendDate,PlanSendDate = @PlanSendDate,RMA = @RMA,ExpectVolume = @ExpectVolume,GW = @GW,CustomAttr = @CustomAttr 
			            where OrderId = @OrderId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@LastTakeDate",SqlDbType.DateTime),
                                        new SqlParameter("@ExpectTakeDate",SqlDbType.DateTime),
                                        new SqlParameter("@SendDate",SqlDbType.DateTime),
                                        new SqlParameter("@PlanSendDate",SqlDbType.DateTime),
                                        new SqlParameter("@RMA",SqlDbType.VarChar,20),
                                        new SqlParameter("@ExpectVolume",SqlDbType.Float),
                                        new SqlParameter("@GW",SqlDbType.Float),
                                        new SqlParameter("@CustomAttr",SqlDbType.NVarChar,3000)
                                   };
            parms[0].Value = model.OrderId;
            parms[1].Value = model.LastTakeDate;
            parms[2].Value = model.ExpectTakeDate;
            parms[3].Value = model.SendDate;
            parms[4].Value = model.PlanSendDate;
            parms[5].Value = model.RMA;
            parms[6].Value = model.ExpectVolume;
            parms[7].Value = model.GW;
            parms[8].Value = model.CustomAttr;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid orderId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderReceiptAttr where OrderId = @OrderId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = orderId;

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
                sb.Append(@"delete from OrderReceiptAttr where OrderId = @OrderId" + n + " ;");
                SqlParameter parm = new SqlParameter("@OrderId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderReceiptAttrInfo GetModel(Guid orderId)
        {
            OrderReceiptAttrInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 OrderId,LastTakeDate,ExpectTakeDate,SendDate,PlanSendDate,RMA,ExpectVolume,GW,CustomAttr 
			            from OrderReceiptAttr
						where OrderId = @OrderId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = orderId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrderReceiptAttrInfo();
                        model.OrderId = reader.GetGuid(0);
                        model.LastTakeDate = reader.GetDateTime(1);
                        model.ExpectTakeDate = reader.GetDateTime(2);
                        model.SendDate = reader.GetDateTime(3);
                        model.PlanSendDate = reader.GetDateTime(4);
                        model.RMA = reader.GetString(5);
                        model.ExpectVolume = reader.GetDouble(6);
                        model.GW = reader.GetDouble(7);
                        model.CustomAttr = reader.GetString(8);
                    }
                }
            }

            return model;
        }

        public IList<OrderReceiptAttrInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderReceiptAttr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderReceiptAttrInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          OrderId,LastTakeDate,ExpectTakeDate,SendDate,PlanSendDate,RMA,ExpectVolume,GW,CustomAttr
					  from OrderReceiptAttr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptAttrInfo> list = new List<OrderReceiptAttrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptAttrInfo model = new OrderReceiptAttrInfo();
                        model.OrderId = reader.GetGuid(1);
                        model.LastTakeDate = reader.GetDateTime(2);
                        model.ExpectTakeDate = reader.GetDateTime(3);
                        model.SendDate = reader.GetDateTime(4);
                        model.PlanSendDate = reader.GetDateTime(5);
                        model.RMA = reader.GetString(6);
                        model.ExpectVolume = reader.GetDouble(7);
                        model.GW = reader.GetDouble(8);
                        model.CustomAttr = reader.GetString(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptAttrInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           OrderId,LastTakeDate,ExpectTakeDate,SendDate,PlanSendDate,RMA,ExpectVolume,GW,CustomAttr
					   from OrderReceiptAttr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderReceiptAttrInfo> list = new List<OrderReceiptAttrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptAttrInfo model = new OrderReceiptAttrInfo();
                        model.OrderId = reader.GetGuid(1);
                        model.LastTakeDate = reader.GetDateTime(2);
                        model.ExpectTakeDate = reader.GetDateTime(3);
                        model.SendDate = reader.GetDateTime(4);
                        model.PlanSendDate = reader.GetDateTime(5);
                        model.RMA = reader.GetString(6);
                        model.ExpectVolume = reader.GetDouble(7);
                        model.GW = reader.GetDouble(8);
                        model.CustomAttr = reader.GetString(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptAttrInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select OrderId,LastTakeDate,ExpectTakeDate,SendDate,PlanSendDate,RMA,ExpectVolume,GW,CustomAttr
                        from OrderReceiptAttr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<OrderReceiptAttrInfo> list = new List<OrderReceiptAttrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptAttrInfo model = new OrderReceiptAttrInfo();
                        model.OrderId = reader.GetGuid(0);
                        model.LastTakeDate = reader.GetDateTime(1);
                        model.ExpectTakeDate = reader.GetDateTime(2);
                        model.SendDate = reader.GetDateTime(3);
                        model.PlanSendDate = reader.GetDateTime(4);
                        model.RMA = reader.GetString(5);
                        model.ExpectVolume = reader.GetDouble(6);
                        model.GW = reader.GetDouble(7);
                        model.CustomAttr = reader.GetString(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderReceiptAttrInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select OrderId,LastTakeDate,ExpectTakeDate,SendDate,PlanSendDate,RMA,ExpectVolume,GW,CustomAttr 
			            from OrderReceiptAttr
					    order by LastUpdatedDate desc ");

            IList<OrderReceiptAttrInfo> list = new List<OrderReceiptAttrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderReceiptAttrInfo model = new OrderReceiptAttrInfo();
                        model.OrderId = reader.GetGuid(0);
                        model.LastTakeDate = reader.GetDateTime(1);
                        model.ExpectTakeDate = reader.GetDateTime(2);
                        model.SendDate = reader.GetDateTime(3);
                        model.PlanSendDate = reader.GetDateTime(4);
                        model.RMA = reader.GetString(5);
                        model.ExpectVolume = reader.GetDouble(6);
                        model.GW = reader.GetDouble(7);
                        model.CustomAttr = reader.GetString(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
