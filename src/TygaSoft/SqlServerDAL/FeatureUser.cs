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
    public partial class FeatureUser
    {
        #region IFeatureUser Member

        public FeatureUserInfo GetModel(Guid userId, string typeName)
        {
            FeatureUserInfo model = null;

            var sb = new StringBuilder(300);
            sb.Append(@"select top 1 fu.UserId,fu.FeatureId,fu.TypeName
                        ,sm.Coded,sm.Named,sm.SiteLogo,sm.SiteTitle,sm.CultureName
			            from FeatureUser fu
                        left join TygaSoftAspnetDb.dbo.SiteMulti sm on sm.Id = fu.FeatureId
						where UserId = @UserId and TypeName = @TypeName ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@TypeName",SqlDbType.NVarChar,20)
                                   };
            parms[0].Value = userId;
            parms[1].Value = typeName;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new FeatureUserInfo();
                        model.UserId = reader.GetGuid(0);
                        model.FeatureId = reader.GetGuid(1);
                        model.TypeName = reader.GetString(2);

                        model.SiteCode = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        model.SiteName = reader.IsDBNull(4) ? "" : reader.GetString(4);
                        model.SiteLogo = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        model.SiteTitle = reader.IsDBNull(6) ? "" : reader.GetString(6);
                        model.CultureName = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    }
                }
            }

            return model;
        }

        #endregion
    }
}
