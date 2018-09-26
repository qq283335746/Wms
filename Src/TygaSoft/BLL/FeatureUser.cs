using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class FeatureUser
    {
        #region FeatureUser Member

        public void DoFeatureUser(Guid userId,Guid featureId, string typeName,bool isSingle)
        {
            SqlParameter[] parms = {
                new SqlParameter("@UserId",userId),
                new SqlParameter("@TypeName",SqlDbType.NVarChar,20)
            };
            parms[1].Value = typeName;
            var bll = new FeatureUser();
            var list = bll.GetList("and UserId = @UserId and TypeName = @TypeName ", parms);

            var isExist = false;
            foreach (var item in list)
            {
                if (!item.FeatureId.Equals(featureId))
                {
                    if (isSingle)
                    {
                        bll.Delete(userId, item.FeatureId);
                    }
                }
                else isExist = true;
            }
            if (!isExist)
            {
                var fuInfo = new FeatureUserInfo(userId, featureId, typeName, DateTime.Now);
                bll.Insert(fuInfo);
            }
        }

        public FeatureUserInfo GetModel(Guid userId, string typeName)
        {
            return dal.GetModel(userId, typeName);
        }

        #endregion
    }
}
