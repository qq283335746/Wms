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
    public partial class Category
    {
        #region ICategory Member

        public CategoryInfo GetModelByCode(string code)
        {
            CategoryInfo model = null;
            var cmdText = "select top 1 * from Category where CategoryCode = @CategoryCode ";
            var parm = new SqlParameter("@CategoryCode", SqlDbType.VarChar, 36);
            parm.Value = code;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new CategoryInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.CategoryCode = reader.GetString(3);
                        model.CategoryName = reader.GetString(4);
                        model.Step = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                    }
                }
            }

            return model;
        }

        public CategoryInfo GetRootModel()
        {
            CategoryInfo model = null;
            var cmdText = "select top 1 * from Category where ParentId = '" + Guid.Empty + "'";

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new CategoryInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.CategoryCode = reader.GetString(3);
                        model.CategoryName = reader.GetString(4);
                        model.Step = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                    }
                }
            }

            return model;
        }

        public string CreateCode(Guid Id)
        {
            var cmdText = @"select c.CategoryCode,c.ParentId,(select count(1) from Category c2 where c2.ParentId = c.Id) TotalChild
                            from Category c
                            where c.Id = @Id ";
            var parm = new SqlParameter("@Id", Id);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if(reader.GetGuid(1).Equals(Guid.Empty)) return (reader.GetInt32(2) + 1).ToString().PadLeft(3, '0');
                        return reader.GetString(0) + "." + (reader.GetInt32(2) + 1).ToString().PadLeft(3, '0');
                    }
                }
            }

            return string.Empty;
        }

        public bool IsExistProduct(object categoryId)
        {
            var cmdText = @"select 1 from [Product] where CategoryId = @CategoryId ";
            var parm = new SqlParameter("@CategoryId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(categoryId.ToString());

            object obj = SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm);
            if (obj != null) return true;

            return false;
        }

        public bool IsExistCode(string categoryCode,Guid Id)
        {
            var cmdText = "";
            SqlParameter[] parms = new SqlParameter[1];
            if (Id.Equals(Guid.Empty))
            {
                cmdText = @"select 1 from [Category] where CategoryCode = @CategoryCode ";
                parms[0] = new SqlParameter("@CategoryCode", SqlDbType.VarChar, 30);
                parms[0].Value = categoryCode;
            }
            else
            {
                cmdText = @"select 1 from [Category] where CategoryCode = @CategoryCode and Id <> @Id ";
                Array.Resize(ref parms, 2);
                parms[0] = new SqlParameter("@CategoryCode", SqlDbType.VarChar, 30);
                parms[0].Value = categoryCode;
                parms[1] = new SqlParameter("@Id", Id);
            }

            object obj = SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parms);
            if (obj != null) return true;

            return false;
        }

        #endregion
    }
}
