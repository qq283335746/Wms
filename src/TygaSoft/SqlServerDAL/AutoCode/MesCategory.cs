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
    public partial class MesCategory : IMesCategory
    {
        #region IMesCategory Member

        public int Insert(MesCategoryInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into MesCategory (UserId,ParentId,Coded,Named,Step,WorkStation,StandardHours,Barcode,Sort,Remark,LastUpdatedDate)
			            values
						(@UserId,@ParentId,@Coded,@Named,@Step,@WorkStation,@StandardHours,@Barcode,@Sort,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,36),
new SqlParameter("@Named",SqlDbType.NVarChar,100),
new SqlParameter("@Step",SqlDbType.VarChar,1000),
new SqlParameter("@WorkStation",SqlDbType.NVarChar,256),
new SqlParameter("@StandardHours",SqlDbType.Float),
new SqlParameter("@Barcode",SqlDbType.VarChar,36),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.ParentId;
            parms[2].Value = model.Coded;
            parms[3].Value = model.Named;
            parms[4].Value = model.Step;
            parms[5].Value = model.WorkStation;
            parms[6].Value = model.StandardHours;
            parms[7].Value = model.Barcode;
            parms[8].Value = model.Sort;
            parms[9].Value = model.Remark;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(MesCategoryInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into MesCategory (Id,UserId,ParentId,Coded,Named,Step,WorkStation,StandardHours,Barcode,Sort,Remark,LastUpdatedDate)
			            values
						(@Id,@UserId,@ParentId,@Coded,@Named,@Step,@WorkStation,@StandardHours,@Barcode,@Sort,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,36),
new SqlParameter("@Named",SqlDbType.NVarChar,100),
new SqlParameter("@Step",SqlDbType.VarChar,1000),
new SqlParameter("@WorkStation",SqlDbType.NVarChar,256),
new SqlParameter("@StandardHours",SqlDbType.Float),
new SqlParameter("@Barcode",SqlDbType.VarChar,36),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ParentId;
            parms[3].Value = model.Coded;
            parms[4].Value = model.Named;
            parms[5].Value = model.Step;
            parms[6].Value = model.WorkStation;
            parms[7].Value = model.StandardHours;
            parms[8].Value = model.Barcode;
            parms[9].Value = model.Sort;
            parms[10].Value = model.Remark;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(MesCategoryInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update MesCategory set UserId = @UserId,ParentId = @ParentId,Coded = @Coded,Named = @Named,Step = @Step,WorkStation = @WorkStation,StandardHours = @StandardHours,Barcode = @Barcode,Sort = @Sort,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,36),
new SqlParameter("@Named",SqlDbType.NVarChar,100),
new SqlParameter("@Step",SqlDbType.VarChar,1000),
new SqlParameter("@WorkStation",SqlDbType.NVarChar,256),
new SqlParameter("@StandardHours",SqlDbType.Float),
new SqlParameter("@Barcode",SqlDbType.VarChar,36),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ParentId;
            parms[3].Value = model.Coded;
            parms[4].Value = model.Named;
            parms[5].Value = model.Step;
            parms[6].Value = model.WorkStation;
            parms[7].Value = model.StandardHours;
            parms[8].Value = model.Barcode;
            parms[9].Value = model.Sort;
            parms[10].Value = model.Remark;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from MesCategory where Id = @Id ");
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
                sb.Append(@"delete from MesCategory where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public MesCategoryInfo GetModel(Guid id)
        {
            MesCategoryInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,ParentId,Coded,Named,Step,WorkStation,StandardHours,Barcode,Sort,Remark,LastUpdatedDate 
			            from MesCategory
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
                        model = new MesCategoryInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.Step = reader.GetString(5);
                        model.WorkStation = reader.GetString(6);
                        model.StandardHours = reader.GetDouble(7);
                        model.Barcode = reader.GetString(8);
                        model.Sort = reader.GetInt32(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);
                    }
                }
            }

            return model;
        }

        public IList<MesCategoryInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from MesCategory ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<MesCategoryInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          Id,UserId,ParentId,Coded,Named,Step,WorkStation,StandardHours,Barcode,Sort,Remark,LastUpdatedDate
					  from MesCategory ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<MesCategoryInfo> list = new List<MesCategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MesCategoryInfo model = new MesCategoryInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ParentId = reader.GetGuid(3);
                        model.Coded = reader.GetString(4);
                        model.Named = reader.GetString(5);
                        model.Step = reader.GetString(6);
                        model.WorkStation = reader.GetString(7);
                        model.StandardHours = reader.GetDouble(8);
                        model.Barcode = reader.GetString(9);
                        model.Sort = reader.GetInt32(10);
                        model.Remark = reader.GetString(11);
                        model.LastUpdatedDate = reader.GetDateTime(12);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<MesCategoryInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           Id,UserId,ParentId,Coded,Named,Step,WorkStation,StandardHours,Barcode,Sort,Remark,LastUpdatedDate
					   from MesCategory ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<MesCategoryInfo> list = new List<MesCategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MesCategoryInfo model = new MesCategoryInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ParentId = reader.GetGuid(3);
                        model.Coded = reader.GetString(4);
                        model.Named = reader.GetString(5);
                        model.Step = reader.GetString(6);
                        model.WorkStation = reader.GetString(7);
                        model.StandardHours = reader.GetDouble(8);
                        model.Barcode = reader.GetString(9);
                        model.Sort = reader.GetInt32(10);
                        model.Remark = reader.GetString(11);
                        model.LastUpdatedDate = reader.GetDateTime(12);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<MesCategoryInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,ParentId,Coded,Named,Step,WorkStation,StandardHours,Barcode,Sort,Remark,LastUpdatedDate
                        from MesCategory ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<MesCategoryInfo> list = new List<MesCategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MesCategoryInfo model = new MesCategoryInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.Step = reader.GetString(5);
                        model.WorkStation = reader.GetString(6);
                        model.StandardHours = reader.GetDouble(7);
                        model.Barcode = reader.GetString(8);
                        model.Sort = reader.GetInt32(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<MesCategoryInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,ParentId,Coded,Named,Step,WorkStation,StandardHours,Barcode,Sort,Remark,LastUpdatedDate 
			            from MesCategory
					    order by Sort ");

            IList<MesCategoryInfo> list = new List<MesCategoryInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MesCategoryInfo model = new MesCategoryInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.Step = reader.GetString(5);
                        model.WorkStation = reader.GetString(6);
                        model.StandardHours = reader.GetDouble(7);
                        model.Barcode = reader.GetString(8);
                        model.Sort = reader.GetInt32(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
