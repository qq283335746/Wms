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
    public partial class ShelfMission : IShelfMission
    {
        #region IShelfMission Member

        public IList<ShelfMissionInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(500);
            sb.Append(@"select count(*) from ShelfMission sm
                         left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = sm.UserId
                        ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ShelfMissionInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by sm.Sort,sm.LastUpdatedDate desc) as RowNumber,
			          sm.Id,sm.UserId,sm.OrderCode,sm.TotalStayQty,sm.TotalQty,Status,sm.Remark,sm.Sort,sm.IsDisable,sm.LastUpdatedDate
                      ,u.UserName
					  from ShelfMission sm
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = sm.UserId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<ShelfMissionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShelfMissionInfo model = new ShelfMissionInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderCode = reader.GetString(3);
                        model.TotalStayQty = reader.GetDouble(4);
                        model.TotalQty = reader.GetDouble(5);
                        model.Status = reader.GetString(6);
                        model.Remark = reader.GetString(7);
                        model.Sort = reader.GetInt32(8);
                        model.IsDisable = reader.GetBoolean(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        model.UserName = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.TotalViewQty = model.TotalStayQty - model.TotalQty;

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public void SetTotalProduct(string orderCode)
        {
            var sb = new StringBuilder(500);
            sb.Append(@"update sm set sm.TotalStayQty = t.TotalStayQty,sm.TotalQty=t.TotalQty,sm.Status=(
                        case when (t.TotalStayQty - t.TotalQty) = 0 then '已完成'
                        when (t.TotalQty > 0 and (t.TotalStayQty - t.TotalQty) > 0) then '待完成' else '新建' end 
                        ),sm.Sort=(
                        case when (t.TotalStayQty - t.TotalQty) = 0 then 2
                        when (t.TotalQty > 0 and (t.TotalStayQty - t.TotalQty) > 0) then 1 else 0 end 
                        )
                        from (
                        select smp.ShelfMissionId, sum(smp.StayQty) TotalStayQty,sum(smp.Qty) TotalQty
                        from ShelfMissionProduct smp
                        group by smp.ShelfMissionId
                        ) t,
                        ShelfMission sm
                        where t.ShelfMissionId = sm.Id ");

            var Id = Guid.Empty;
            if (Guid.TryParse(orderCode, out Id)) sb.AppendFormat("and sm.Id = '{0}' ", Id);
            else sb.AppendFormat("and sm.OrderCode = '{0}' ", orderCode);

            SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString());
        }

        #endregion
    }
}
