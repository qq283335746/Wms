using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysHelper;

namespace TygaSoft.SqlServerDAL
{
    public partial class OrderReceipt
    {
        #region IOrderReceipt Member

        public IList<CombogridInfo> GetCbgList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from OrderReceipt o
                        left join Customer c on o.CustomerId = c.Id
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<CombogridInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by o.Sort,o.LastUpdatedDate desc) as RowNumber,
			          o.Id,o.OrderCode,o.RecordDate,o.CustomerId,c.Coded CustomerCode,c.Named CustomerName
					  from OrderReceipt o
                      left join Customer c on o.CustomerId = c.Id
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<CombogridInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(new CombogridInfo(reader.GetGuid(1).ToString(), reader.GetString(2), reader.GetDateTime(3).ToString("yyyy-MM-dd HH:mm"), reader.GetGuid(4), reader.IsDBNull(5) ? "" : reader.GetString(5), reader.IsDBNull(6) ? "" : reader.GetString(6)));
                    }
                }
            }

            return list;
        }

        public int SetNext(Guid Id, string orderCode, bool isStopNext)
        {
            if (Id.Equals(Guid.Empty) && string.IsNullOrWhiteSpace(orderCode)) throw new ArgumentException(MC.Request_Params_InvalidError);

            string cmdText = "";
            SqlParameter[] parms = null;
            if (!Id.Equals(Guid.Empty))
            {
                cmdText = "update OrderReceipt set IsStopNext = @IsStopNext where Id = @Id ";
                parms = new SqlParameter[] {
                    new SqlParameter("@Id", SqlDbType.UniqueIdentifier),
                    new SqlParameter("@IsStopNext", SqlDbType.Bit)
                };
                parms[0].Value = Id;
                parms[1].Value = isStopNext;
            }
            else
            {
                cmdText = "update OrderReceipt set IsStopNext = @IsStopNext where OrderCode = @OrderCode ";
                parms = new SqlParameter[] {
                    new SqlParameter("@OrderCode", SqlDbType.VarChar,20),
                    new SqlParameter("@IsStopNext", SqlDbType.Bit)
                };
                parms[0].Value = orderCode;
                parms[1].Value = isStopNext;
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parms);
        }

        public object GetOrderId(string orderNum)
        {
            string cmdText = @"select top 1 orb.Id from OrderReceipt orb where orb.OrderCode = @OrderCode";

            SqlParameter parm = new SqlParameter("@OrderCode", SqlDbType.VarChar, 20);
            parm.Value = orderNum;

            return SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm);
        }

        public bool IsExistProduct(object orderId)
        {
            var cmdText = @"select 1 from [OrderReceiptProduct] where OrderId = @OrderId ";
            var parm = new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(orderId.ToString());

            object obj = SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm);
            if (obj != null) return true;

            return false;
        }

        public OrderReceiptInfo GetModelByJoin(object Id)
        {
            OrderReceiptInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 o.Id,o.UserId,o.CustomerId,o.SupplierId,o.OrderCode,o.OrderType,o.PreOrderCode,o.PurchaseOrderCode,o.TypeName,o.SettlementDate,o.RecordDate,o.IsStopNext,o.Status,o.Sort,o.Remark,o.LastUpdatedDate 
                        ,ora.LastTakeDate,ora.ExpectTakeDate,ora.SendDate,ora.PlanSendDate,ora.RMA,ora.ExpectVolume,ora.GW,ora.CustomAttr  
			            ,c.Coded CustomerCode,c.Named CustomerName
                        from OrderReceipt o
                        join OrderReceiptAttr ora on ora.OrderId = o.Id
                        left join Customer c on c.Id = o.CustomerId
						where o.Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrderReceiptInfo();
                        model.Id = reader.GetGuid(0);
                        //model.UserId = reader.GetGuid(1);
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

                        model.SSettlementDate = model.SettlementDate.ToString("yyyy-MM-dd");
                        model.SRecordDate = model.RecordDate.ToString("yyyy-MM-dd");

                        model.SLastTakeDate = reader.GetDateTime(16).ToString("yyyy-MM-dd");
                        model.SExpectTakeDate = reader.GetDateTime(17).ToString("yyyy-MM-dd");
                        model.SSendDate = reader.GetDateTime(18).ToString("yyyy-MM-dd");
                        model.SPlanSendDate = reader.GetDateTime(19).ToString("yyyy-MM-dd");
                        model.RMA = reader.GetString(20);
                        model.ExpectVolume = reader.GetDouble(21);
                        model.GW = reader.GetDouble(22);
                        model.CustomAttr = reader.GetString(23);

                        model.CustomerCode = reader.IsDBNull(24) ? "" : reader.GetString(24);
                        model.CustomerName = reader.IsDBNull(25) ? "" : reader.GetString(25);
                    }
                }
            }

            return model;
        }

        public IList<OrderReceiptInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from OrderReceipt orb
                        left join Customer c on c.Id = orb.CustomerId 
                        left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = orb.UserId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderReceiptInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by orb.Status,orb.Sort,orb.LastUpdatedDate desc) as RowNumber,
			          orb.Id,orb.UserId,orb.CustomerId,orb.SupplierId,orb.OrderCode,orb.OrderType,orb.PurchaseOrderCode,orb.TypeName,orb.SettlementDate,orb.RecordDate,orb.IsStopNext,orb.Status,orb.Sort,orb.Remark,orb.LastUpdatedDate
                      ,c.Coded CustomerCode,c.Named CustomerName,u.UserName
					  from OrderReceipt orb
                      left join Customer c on c.Id = orb.CustomerId 
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = orb.UserId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<OrderReceiptInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new OrderReceiptInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.SupplierId = reader.GetGuid(4);
                        model.OrderCode = reader.GetString(5);
                        model.OrderType = reader.GetInt32(6);
                        model.PurchaseOrderCode = reader.GetString(7);
                        model.TypeName = reader.GetString(8);
                        model.SettlementDate = reader.GetDateTime(9);
                        model.RecordDate = reader.GetDateTime(10);
                        model.IsStopNext = reader.GetBoolean(11);
                        model.Status = reader.GetByte(12);
                        model.Sort = reader.GetInt32(13);
                        model.Remark = reader.GetString(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);
                        model.SSettlementDate = model.SettlementDate.ToString("yyyy-MM-dd");

                        model.CustomerCode = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.CustomerName = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.UserName = reader.IsDBNull(18) ? "" : reader.GetString(18);

                        model.StatusName = Enum.GetName(typeof(EnumData.EnumOrderStatus), model.Status);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public OrderReceiptInfo GetModel(string orderCode)
        {
            OrderReceiptInfo model = null;

            var sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,CustomerId,SupplierId,OrderCode,OrderType,PreOrderCode,PurchaseOrderCode,TypeName,SettlementDate,RecordDate,IsStopNext,Status,Sort,Remark,LastUpdatedDate 
			            from OrderReceipt
						where OrderCode = @OrderCode ");
            var parm = new SqlParameter("@OrderCode", SqlDbType.VarChar, 20);
            parm.Value = orderCode;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parm))
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

                        model.StatusName = Enum.GetName(typeof(EnumData.EnumOrderStatus), model.Status);
                    }
                }
            }

            return model;
        }

        #endregion
    }
}
