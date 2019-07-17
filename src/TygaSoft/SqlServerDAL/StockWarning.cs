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
    public partial class StockWarning
    {
        #region IStockWarning Member

        public IList<StockWarningInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from StockWarning sw
                        left join Zone z on z.Id = sw.ZoneId
                        left join StockLocation sl on sl.Id = sw.StockLocationId
                     ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StockWarningInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by sw.LastUpdatedDate desc) as RowNumber,
			          sw.Id,sw.UserId,sw.ZoneId,sw.StockLocationId,sw.Coded,sw.ZoneProperty,sw.StockLocationProperty,sw.StockAmount,sw.OverdueDay,sw.MinQty,sw.MaxQty,sw.Remark,sw.Sort,sw.IsDisable,sw.LastUpdatedDate
                      ,z.ZoneName,sl.Code StockLocationCode,sl.Named StockLocationNamed
					  from StockWarning sw 
                      left join Zone z on z.Id = sw.ZoneId
                      left join StockLocation sl on sl.Id = sw.StockLocationId
                     ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<StockWarningInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new StockWarningInfo();
                        model.Id = reader.GetGuid(1);
                        model.ZoneId = reader.GetGuid(3);
                        model.StockLocationId = reader.GetGuid(4);
                        model.Coded = reader.GetString(5);
                        model.ZoneProperty = reader.GetString(6);
                        model.StockLocationProperty = reader.GetString(7);
                        model.StockAmount = reader.GetDecimal(8);
                        model.OverdueDay = reader.GetInt32(9);
                        model.MinQty = reader.GetDouble(10);
                        model.MaxQty = reader.GetDouble(11);
                        model.Remark = reader.GetString(12);
                        model.Sort = reader.GetInt32(13);
                        model.IsDisable = reader.GetBoolean(14);
                        model.SLastUpdatedDate = reader.GetDateTime(15).ToString("yyyy-MM-dd HH:mm");

                        model.ZoneName = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.StockLocationCode = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.IsDisableName = model.IsDisable ? "是" : "否";

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
