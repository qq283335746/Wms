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
    public partial class MesProduct : IMesProduct
    {
        #region IMesProduct Member

        public bool IsExistCode(string code, Guid Id)
        {
            var cmdText = @"select 1 from [MesProduct] where LOWER(Coded) = @Coded and Id <> @Id ";
            SqlParameter[] parms = {
                new SqlParameter("@Coded", SqlDbType.VarChar,36),
                new SqlParameter("@Id", SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = code;
            parms[1].Value = Id;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parms);
            if (obj != null) return true;

            return false;
        }

        #endregion
    }
}
