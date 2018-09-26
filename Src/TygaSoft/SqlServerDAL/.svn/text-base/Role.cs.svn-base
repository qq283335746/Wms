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
    public class Role : IRole
    {
        #region 成员方法

        public RoleInfo GetModel(string roleName)
        {
            RoleInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 RoleId,RoleName
			            from  aspnet_Roles
						where LoweredRoleName = @RoleName ");

            var parm = new SqlParameter("@RoleName", SqlDbType.NVarChar, 256);
            parm.Value = roleName;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new RoleInfo();
                        model.RoleId = reader.GetGuid(0);
                        model.RoleName = reader.GetString(1);
                    }
                }
            }

            return model;
        }

        public List<RoleInfo> GetList()
        {
            string cmdText = "select RoleId,RoleName from aspnet_Roles ";

            List<RoleInfo> list = new List<RoleInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AspnetDbConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RoleInfo model = new RoleInfo();
                        model.RoleId = reader["RoleId"];
                        model.RoleName = reader.GetString(1).Trim();

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public int Update(RoleInfo model)
        {
            string cmdText = @"update aspnet_Roles set RoleName = @RoleName,LoweredRoleName = @LoweredRoleName where RoleId = @RoleId";

            SqlParameter[] parms = {
                                     new SqlParameter("@RoleId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@RoleName",SqlDbType.NVarChar,256),
                                     new SqlParameter("@LoweredRoleName",SqlDbType.NVarChar,256)
                                   };
            parms[0].Value = Guid.Parse(model.RoleId.ToString());
            parms[1].Value = model.RoleName;
            parms[2].Value = model.RoleName.ToLower();

            return SqlHelper.ExecuteNonQuery(SqlHelper.AspnetDbConnString, CommandType.Text, cmdText, parms);
        }

        #endregion
    }
}
