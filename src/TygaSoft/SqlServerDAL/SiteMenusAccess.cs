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
    public partial class SiteMenusAccess
    {
        #region ISiteMenusAccess Member

        public IList<SiteMenusAccessInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select sma.ApplicationId,sma.AccessId,sma.OperationAccess,sma.AccessType
                        from SiteMenusAccess sma
                        join aspnet_Applications a on a.ApplicationId = sma.ApplicationId
                       ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by ApplicationId ");

            var list = new List<SiteMenusAccessInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new SiteMenusAccessInfo();
                        model.ApplicationId = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AccessId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.OperationAccess = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.AccessType = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
