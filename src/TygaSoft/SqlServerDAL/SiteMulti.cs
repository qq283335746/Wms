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
    public partial class SiteMulti
    {
        #region ISiteMulti Member

        public string GetCode()
        {
            var total = (int)SqlHelper.ExecuteScalar(SqlHelper.AspnetDbConnString, CommandType.Text, "select count(*) from SiteMulti ");
            return string.Format("1{0}", (total + 1).ToString().PadLeft(5, '0'));
        }

        public IList<SiteMultiInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from SiteMulti sm 
                        left join SitePicture p on (p.FileDirectory+p.FileName) = sm.SiteLogo
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<SiteMultiInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by sm.LastUpdatedDate desc) as RowNumber,
			          sm.Id,sm.Coded,sm.Named,sm.SiteLogo,sm.SiteTitle,sm.LastUpdatedDate
                      ,p.Id SiteLogoId
					  from SiteMulti sm 
                      left join SitePicture p on (p.FileDirectory+p.FileName) = sm.SiteLogo
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<SiteMultiInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new SiteMultiInfo();
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.SiteLogo = reader.GetString(4);
                        model.SiteTitle = reader.GetString(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);

                        model.SiteLogoId = reader.IsDBNull(7) ? Guid.Empty : reader.GetGuid(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
