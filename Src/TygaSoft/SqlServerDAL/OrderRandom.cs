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
    public partial class OrderRandom
    {
        #region IOrderRandom Member

        public int GetMax(string pre)
        {
            var cmdText = @"select count(1) from [OrderRandom] where OrderCode like @OrderCode ";
            var parm = new SqlParameter("@OrderCode", SqlDbType.VarChar, 20);
            parm.Value = ""+ pre + "%";

            return (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm) + 1;
        }

        public bool IsExist(string orderCode)
        {
            var cmdText = @"select 1 from [OrderRandom] where OrderCode = @OrderCode ";
            var parm = new SqlParameter("@OrderCode", SqlDbType.VarChar, 20);
            parm.Value = orderCode;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm);
            if (obj != null) return true;

            return false;
        }

        #endregion
    }
}
