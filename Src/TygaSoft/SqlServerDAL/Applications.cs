using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class Applications: IApplications
    {
        #region IApplication Member

        public Guid GetAspnetAppId(string appName)
        {
            string cmdText = @"select ApplicationId from aspnet_Applications where LoweredApplicationName = @AppName ";
            SqlParameter parm = new SqlParameter("@AppName", SqlDbType.NVarChar, 256);
            parm.Value = appName.ToLower();

            return (Guid)SqlHelper.ExecuteScalar(SqlHelper.AspnetDbConnString, CommandType.Text, cmdText, parm);
        }

        #endregion
    }
}
