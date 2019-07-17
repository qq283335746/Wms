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
    public partial class Category : ICategory
    {
        #region ICategory Member

        public int Insert(CategoryInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Category (UserId,ParentId,CategoryCode,CategoryName,Step,Sort,Remark,LastUpdatedDate)
			            values
						(@UserId,@ParentId,@CategoryCode,@CategoryName,@Step,@Sort,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CategoryCode",SqlDbType.VarChar,30),
new SqlParameter("@CategoryName",SqlDbType.NVarChar,30),
new SqlParameter("@Step",SqlDbType.VarChar,1000),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.ParentId;
            parms[2].Value = model.CategoryCode;
            parms[3].Value = model.CategoryName;
            parms[4].Value = model.Step;
            parms[5].Value = model.Sort;
            parms[6].Value = model.Remark;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(CategoryInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Category (Id,UserId,ParentId,CategoryCode,CategoryName,Step,Sort,Remark,LastUpdatedDate)
			            values
						(@Id,@UserId,@ParentId,@CategoryCode,@CategoryName,@Step,@Sort,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CategoryCode",SqlDbType.VarChar,30),
new SqlParameter("@CategoryName",SqlDbType.NVarChar,30),
new SqlParameter("@Step",SqlDbType.VarChar,1000),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ParentId;
            parms[3].Value = model.CategoryCode;
            parms[4].Value = model.CategoryName;
            parms[5].Value = model.Step;
            parms[6].Value = model.Sort;
            parms[7].Value = model.Remark;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(CategoryInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Category set UserId = @UserId,ParentId = @ParentId,CategoryCode = @CategoryCode,CategoryName = @CategoryName,Step = @Step,Sort = @Sort,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CategoryCode",SqlDbType.VarChar,30),
new SqlParameter("@CategoryName",SqlDbType.NVarChar,30),
new SqlParameter("@Step",SqlDbType.VarChar,1000),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ParentId;
            parms[3].Value = model.CategoryCode;
            parms[4].Value = model.CategoryName;
            parms[5].Value = model.Step;
            parms[6].Value = model.Sort;
            parms[7].Value = model.Remark;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Category where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public bool DeleteBatch(IList<object> list)
        {
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from Category where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public CategoryInfo GetModel(Guid id)
        {
            CategoryInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,ParentId,CategoryCode,CategoryName,Step,Sort,Remark,LastUpdatedDate 
			            from Category
						where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
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

        public IList<CategoryInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Category ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<CategoryInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          Id,UserId,ParentId,CategoryCode,CategoryName,Step,Sort,Remark,LastUpdatedDate
					  from Category ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<CategoryInfo> list = new List<CategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryInfo model = new CategoryInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ParentId = reader.GetGuid(3);
                        model.CategoryCode = reader.GetString(4);
                        model.CategoryName = reader.GetString(5);
                        model.Step = reader.GetString(6);
                        model.Sort = reader.GetInt32(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<CategoryInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           Id,UserId,ParentId,CategoryCode,CategoryName,Step,Sort,Remark,LastUpdatedDate
					   from Category ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<CategoryInfo> list = new List<CategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryInfo model = new CategoryInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ParentId = reader.GetGuid(3);
                        model.CategoryCode = reader.GetString(4);
                        model.CategoryName = reader.GetString(5);
                        model.Step = reader.GetString(6);
                        model.Sort = reader.GetInt32(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<CategoryInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,ParentId,CategoryCode,CategoryName,Step,Sort,Remark,LastUpdatedDate
                        from Category ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<CategoryInfo> list = new List<CategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryInfo model = new CategoryInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.CategoryCode = reader.GetString(3);
                        model.CategoryName = reader.GetString(4);
                        model.Step = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<CategoryInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,ParentId,CategoryCode,CategoryName,Step,Sort,Remark,LastUpdatedDate 
			            from Category
					    order by Sort ");

            IList<CategoryInfo> list = new List<CategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryInfo model = new CategoryInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.CategoryCode = reader.GetString(3);
                        model.CategoryName = reader.GetString(4);
                        model.Step = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
