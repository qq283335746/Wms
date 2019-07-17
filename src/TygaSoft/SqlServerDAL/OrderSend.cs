using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TygaSoft.DBUtility;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.SqlServerDAL
{
    public partial class OrderSend
    {
        #region IOrderSend Member

        public bool IsExistProduct(object orderId)
        {
            var cmdText = @"select 1 from [OrderSendProduct] where OrderId = @OrderId ";
            var parm = new SqlParameter("@OrderId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(orderId.ToString());

            object obj = SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm);
            if (obj != null) return true;

            return false;
        }

        public int SetStatus(string orderCode,int status)
        {
            var sb = new StringBuilder(500);
            sb.AppendFormat(@"update OrderSend set Status = {0} where 1=1 ", status);

            var Id = Guid.Empty;
            if (Guid.TryParse(orderCode, out Id)) sb.AppendFormat("and Id = '{0}' ", Id);
            else sb.AppendFormat("and OrderCode = '{0}' ", orderCode);

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString());
        }

        public int SetStatus(string orderCode)
        {
            var sb = new StringBuilder(500);
            sb.Append(@"update os set os.StayQty = t.TotalStayQty,os.Qty=t.TotalQty,os.Status=(
                        case when (t.TotalQty > 0 and (t.TotalStayQty - t.TotalQty) = 0) then 2
                        when (t.TotalQty > 0 and (t.TotalStayQty - t.TotalQty) <> 0) then 1 else os.Status end 
                        )
                        from (
                        select osp.OrderId, sum(osp.Qty) TotalStayQty,sum(osp.PickQty) TotalQty
                        from OrderSendProduct osp
                        group by osp.OrderId
                        ) t,
                        OrderSend os
                        where t.OrderId = os.Id ");

            var Id = Guid.Empty;
            if (Guid.TryParse(orderCode, out Id)) sb.AppendFormat("and os.Id = '{0}' ", Id);
            else sb.AppendFormat("and os.OrderCode = '{0}' ", orderCode);

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString());
        }

        public OrderSendInfo GetModelByJoin(Guid id)
        {
            OrderSendInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 os.Id,os.UserId,os.OrderCode,os.CustomerId,os.StayQty,os.Qty,os.Remark,os.Status,os.Sort,os.IsDisable,os.LastUpdatedDate
                        ,c.Coded CustomerCode,c.Named CustomerName
			            from OrderSend os
                        left join Customer c on c.Id = os.CustomerId
						where os.Id = @Id ");
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
                        model.OrderCode = reader.GetString(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.StayQty = reader.GetDouble(4);
                        model.Qty = reader.GetDouble(5);
                        model.Remark = reader.GetString(6);
                        model.Status = reader.GetByte(7);
                        model.Sort = reader.GetInt32(8);
                        model.IsDisable = reader.GetBoolean(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        model.CustomerCode = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.CustomerName = reader.IsDBNull(12) ? "" : reader.GetString(12);
                    }
                }
            }

            return model;
        }

        public IList<OrderSendInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderSend o 
                        left join Customer c on c.Id = o.CustomerId 
                        left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = o.UserId
                        ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderSendInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by o.Status,o.Sort,o.LastUpdatedDate desc) as RowNumber,
			          o.Id,o.UserId,o.OrderCode,o.CustomerId,o.StayQty,o.Qty,o.Remark,o.Status,o.Sort,o.IsDisable,o.LastUpdatedDate
                      ,(case o.Status when 0 then '新建' when 1 then '待完成' when 2 then '已完成' else '' end) StatusName
                      ,c.Coded CustomerCode,c.Named CustomerName,u.UserName
					  from OrderSend o 
                      left join Customer c on c.Id = o.CustomerId
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = o.UserId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderSendInfo> list = new List<OrderSendInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new OrderSendInfo();
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

                        model.StatusName = reader.GetString(12);
                        model.CustomerCode = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.CustomerName = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.UserName = reader.IsDBNull(15) ? "" : reader.GetString(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
