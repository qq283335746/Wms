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
    public partial class Pandian
    {
        #region IPandian Member

        public bool IsExistProduct(object pandianId)
        {
            var cmdText = @"select 1 from [PandianProduct] where PandianId = @PandianId ";
            var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(pandianId.ToString());

            object obj = SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm);
            return obj != null;
        }

        public int UpdateStatus(object Id, string status)
        {
            var cmdText = string.Format(@"Update [Pandian] set [Status] = '{1}' where Id = '{0}' and [Status] != '{1}'", Id, status);
            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, cmdText);
        }

        public IList<PandianInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by pd.LastUpdatedDate desc) as RowNumber,
			           pd.Id,pd.UserId,pd.OrderCode,pd.Named,pd.AllowUsers,pd.Remark,pd.StockStartDate,pd.StockEndDate,pd.Customers,pd.Zones,pd.StockLocations,pd.TotalQty,pd.Status,pd.LastUpdatedDate
					   ,u.UserName
                       from Pandian pd 
                       left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = pd.UserId 
                       ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OrderCode = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.AllowUsers = reader.GetString(5);
                        model.Remark = reader.GetString(6);
                        model.StockStartDate = reader.GetDateTime(7);
                        model.StockEndDate = reader.GetDateTime(8);
                        model.Customers = reader.GetString(9);
                        model.Zones = reader.GetString(10);
                        model.StockLocations = reader.GetString(11);
                        model.TotalQty = reader.GetDouble(12);
                        model.Status = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        model.UserName = reader.IsDBNull(15) ? "" : reader.GetString(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Pandian pd 
                        left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = pd.UserId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by pd.LastUpdatedDate desc) as RowNumber,
			          pd.Id,pd.UserId,pd.OrderCode,pd.Named,pd.AllowUsers,pd.Remark,pd.StockStartDate,pd.StockEndDate,pd.Customers,pd.Zones,pd.StockLocations,pd.TotalQty,pd.Status,pd.LastUpdatedDate
					  ,u.UserName
                      from Pandian pd 
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = pd.UserId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new PandianInfo();
                        model.Id = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.AllowUsers = reader.GetString(5);
                        model.Remark = reader.GetString(6);
                        model.StockStartDate = reader.GetDateTime(7);
                        model.StockEndDate = reader.GetDateTime(8);
                        model.Customers = reader.GetString(9);
                        model.Zones = reader.GetString(10);
                        model.StockLocations = reader.GetString(11);
                        model.TotalQty = reader.GetDouble(12);
                        model.Status = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

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
