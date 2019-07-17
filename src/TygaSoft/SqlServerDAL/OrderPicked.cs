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
    public partial class OrderPicked : IOrderPicked
    {
        #region IOrderPicked Member

        public IList<OrderPickedInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderPicked op 
                        left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = op.UserId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderPickedInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by op.Status,op.Sort,op.LastUpdatedDate desc) as RowNumber,
			          op.Id,op.UserId,op.OrderCode,op.TotalStayQty,op.TotalQty,op.Status,op.Remark,op.Sort,op.IsDisable,op.LastUpdatedDate
                      ,u.UserName
					  from OrderPicked op 
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = op.UserId ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<OrderPickedInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new OrderPickedInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderCode = reader.GetString(3);
                        model.TotalStayQty = reader.GetDouble(4);
                        model.TotalQty = reader.GetDouble(5);
                        model.Status = reader.GetByte(6);
                        model.Remark = reader.GetString(7);
                        model.Sort = reader.GetInt32(8);
                        model.IsDisable = reader.GetBoolean(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        model.UserName = reader.IsDBNull(11) ? "" : reader.GetString(11);

                        model.StatusName = Enum.GetName(typeof(EnumData.EnumOrderStatus), model.Status);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public void SetTotalProduct(string orderCode)
        {
            var sb = new StringBuilder(500);
            sb.AppendFormat(@"update o set o.TotalStayQty = t.TotalStayQty,o.TotalQty=t.TotalQty,o.Status=(
                        case when (t.TotalStayQty - t.TotalQty) = 0 then {0}
                        when (t.TotalStayQty - t.TotalQty) > 0 then {1} else {2} end 
                        )
                        from (
                        select opp.OrderPickId, sum(opp.StayQty) TotalStayQty,sum(opp.Qty) TotalQty
                        from OrderPickProduct opp
                        group by opp.OrderPickId
                        ) t,
                        OrderPicked o
                        where t.OrderPickId = o.Id ",(byte)EnumData.EnumOrderStatus.已完成, (byte)EnumData.EnumOrderStatus.待完成, (byte)EnumData.EnumOrderStatus.新建);

            var Id = Guid.Empty;
            if (Guid.TryParse(orderCode, out Id)) sb.AppendFormat("and o.Id = '{0}' ", Id);
            else sb.AppendFormat("and o.OrderCode = '{0}' ", orderCode);

            SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString());
        }

        #endregion
    }
}
