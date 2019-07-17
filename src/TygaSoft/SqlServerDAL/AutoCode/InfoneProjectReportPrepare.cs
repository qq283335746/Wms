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
    public partial class InfoneProjectReportPrepare : IInfoneProjectReportPrepare
    {
        #region IProjectReportPrepare Member

        public int Insert(InfoneProjectReportPrepareInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into ProjectReportPrepare (UserId,CustomerId,ProjectName,ProjectSource,CustomerOfficial,ContactMan,ContactPhone,SpecsModel,PreQty,PreAmount,ProjectAbout,Status,Remark,RecordDate,LastUpdatedDate)
			            values
						(@UserId,@CustomerId,@ProjectName,@ProjectSource,@CustomerOfficial,@ContactMan,@ContactPhone,@SpecsModel,@PreQty,@PreAmount,@ProjectAbout,@Status,@Remark,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ProjectName",SqlDbType.NVarChar,50),
new SqlParameter("@ProjectSource",SqlDbType.NVarChar,50),
new SqlParameter("@CustomerOfficial",SqlDbType.NVarChar,30),
new SqlParameter("@ContactMan",SqlDbType.NVarChar,20),
new SqlParameter("@ContactPhone",SqlDbType.VarChar,20),
new SqlParameter("@SpecsModel",SqlDbType.NVarChar,100),
new SqlParameter("@PreQty",SqlDbType.Int),
new SqlParameter("@PreAmount",SqlDbType.Decimal),
new SqlParameter("@ProjectAbout",SqlDbType.NVarChar),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.CustomerId;
            parms[2].Value = model.ProjectName;
            parms[3].Value = model.ProjectSource;
            parms[4].Value = model.CustomerOfficial;
            parms[5].Value = model.ContactMan;
            parms[6].Value = model.ContactPhone;
            parms[7].Value = model.SpecsModel;
            parms[8].Value = model.PreQty;
            parms[9].Value = model.PreAmount;
            parms[10].Value = model.ProjectAbout;
            parms[11].Value = model.Status;
            parms[12].Value = model.Remark;
            parms[13].Value = model.RecordDate;
            parms[14].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(InfoneProjectReportPrepareInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into ProjectReportPrepare (Id,UserId,CustomerId,ProjectName,ProjectSource,CustomerOfficial,ContactMan,ContactPhone,SpecsModel,PreQty,PreAmount,ProjectAbout,Status,Remark,RecordDate,LastUpdatedDate)
			            values
						(@Id,@UserId,@CustomerId,@ProjectName,@ProjectSource,@CustomerOfficial,@ContactMan,@ContactPhone,@SpecsModel,@PreQty,@PreAmount,@ProjectAbout,@Status,@Remark,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ProjectName",SqlDbType.NVarChar,50),
new SqlParameter("@ProjectSource",SqlDbType.NVarChar,50),
new SqlParameter("@CustomerOfficial",SqlDbType.NVarChar,30),
new SqlParameter("@ContactMan",SqlDbType.NVarChar,20),
new SqlParameter("@ContactPhone",SqlDbType.VarChar,20),
new SqlParameter("@SpecsModel",SqlDbType.NVarChar,100),
new SqlParameter("@PreQty",SqlDbType.Int),
new SqlParameter("@PreAmount",SqlDbType.Decimal),
new SqlParameter("@ProjectAbout",SqlDbType.NVarChar),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.CustomerId;
            parms[3].Value = model.ProjectName;
            parms[4].Value = model.ProjectSource;
            parms[5].Value = model.CustomerOfficial;
            parms[6].Value = model.ContactMan;
            parms[7].Value = model.ContactPhone;
            parms[8].Value = model.SpecsModel;
            parms[9].Value = model.PreQty;
            parms[10].Value = model.PreAmount;
            parms[11].Value = model.ProjectAbout;
            parms[12].Value = model.Status;
            parms[13].Value = model.Remark;
            parms[14].Value = model.RecordDate;
            parms[15].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(InfoneProjectReportPrepareInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update ProjectReportPrepare set UserId = @UserId,CustomerId = @CustomerId,ProjectName = @ProjectName,ProjectSource = @ProjectSource,CustomerOfficial = @CustomerOfficial,ContactMan = @ContactMan,ContactPhone = @ContactPhone,SpecsModel = @SpecsModel,PreQty = @PreQty,PreAmount = @PreAmount,ProjectAbout = @ProjectAbout,Status = @Status,Remark = @Remark,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ProjectName",SqlDbType.NVarChar,50),
new SqlParameter("@ProjectSource",SqlDbType.NVarChar,50),
new SqlParameter("@CustomerOfficial",SqlDbType.NVarChar,30),
new SqlParameter("@ContactMan",SqlDbType.NVarChar,20),
new SqlParameter("@ContactPhone",SqlDbType.VarChar,20),
new SqlParameter("@SpecsModel",SqlDbType.NVarChar,100),
new SqlParameter("@PreQty",SqlDbType.Int),
new SqlParameter("@PreAmount",SqlDbType.Decimal),
new SqlParameter("@ProjectAbout",SqlDbType.NVarChar),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.CustomerId;
            parms[3].Value = model.ProjectName;
            parms[4].Value = model.ProjectSource;
            parms[5].Value = model.CustomerOfficial;
            parms[6].Value = model.ContactMan;
            parms[7].Value = model.ContactPhone;
            parms[8].Value = model.SpecsModel;
            parms[9].Value = model.PreQty;
            parms[10].Value = model.PreAmount;
            parms[11].Value = model.ProjectAbout;
            parms[12].Value = model.Status;
            parms[13].Value = model.Remark;
            parms[14].Value = model.RecordDate;
            parms[15].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from ProjectReportPrepare where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public bool DeleteBatch(IList<object> list)
        {
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from ProjectReportPrepare where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public InfoneProjectReportPrepareInfo GetModel(Guid id)
        {
            InfoneProjectReportPrepareInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,CustomerId,ProjectName,ProjectSource,CustomerOfficial,ContactMan,ContactPhone,SpecsModel,PreQty,PreAmount,ProjectAbout,Status,Remark,RecordDate,LastUpdatedDate 
			            from ProjectReportPrepare
						where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new InfoneProjectReportPrepareInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.ProjectName = reader.GetString(3);
                        model.ProjectSource = reader.GetString(4);
                        model.CustomerOfficial = reader.GetString(5);
                        model.ContactMan = reader.GetString(6);
                        model.ContactPhone = reader.GetString(7);
                        model.SpecsModel = reader.GetString(8);
                        model.PreQty = reader.GetInt32(9);
                        model.PreAmount = reader.GetDecimal(10);
                        model.ProjectAbout = reader.GetString(11);
                        model.Status = reader.GetString(12);
                        model.Remark = reader.GetString(13);
                        model.RecordDate = reader.GetDateTime(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);
                    }
                }
            }

            return model;
        }

        public IList<InfoneProjectReportPrepareInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from ProjectReportPrepare ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<InfoneProjectReportPrepareInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,CustomerId,ProjectName,ProjectSource,CustomerOfficial,ContactMan,ContactPhone,SpecsModel,PreQty,PreAmount,ProjectAbout,Status,Remark,RecordDate,LastUpdatedDate
					  from ProjectReportPrepare ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<InfoneProjectReportPrepareInfo> list = new List<InfoneProjectReportPrepareInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneProjectReportPrepareInfo model = new InfoneProjectReportPrepareInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.ProjectName = reader.GetString(4);
                        model.ProjectSource = reader.GetString(5);
                        model.CustomerOfficial = reader.GetString(6);
                        model.ContactMan = reader.GetString(7);
                        model.ContactPhone = reader.GetString(8);
                        model.SpecsModel = reader.GetString(9);
                        model.PreQty = reader.GetInt32(10);
                        model.PreAmount = reader.GetDecimal(11);
                        model.ProjectAbout = reader.GetString(12);
                        model.Status = reader.GetString(13);
                        model.Remark = reader.GetString(14);
                        model.RecordDate = reader.GetDateTime(15);
                        model.LastUpdatedDate = reader.GetDateTime(16);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<InfoneProjectReportPrepareInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,CustomerId,ProjectName,ProjectSource,CustomerOfficial,ContactMan,ContactPhone,SpecsModel,PreQty,PreAmount,ProjectAbout,Status,Remark,RecordDate,LastUpdatedDate
					   from ProjectReportPrepare ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<InfoneProjectReportPrepareInfo> list = new List<InfoneProjectReportPrepareInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneProjectReportPrepareInfo model = new InfoneProjectReportPrepareInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.ProjectName = reader.GetString(4);
                        model.ProjectSource = reader.GetString(5);
                        model.CustomerOfficial = reader.GetString(6);
                        model.ContactMan = reader.GetString(7);
                        model.ContactPhone = reader.GetString(8);
                        model.SpecsModel = reader.GetString(9);
                        model.PreQty = reader.GetInt32(10);
                        model.PreAmount = reader.GetDecimal(11);
                        model.ProjectAbout = reader.GetString(12);
                        model.Status = reader.GetString(13);
                        model.Remark = reader.GetString(14);
                        model.RecordDate = reader.GetDateTime(15);
                        model.LastUpdatedDate = reader.GetDateTime(16);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<InfoneProjectReportPrepareInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,CustomerId,ProjectName,ProjectSource,CustomerOfficial,ContactMan,ContactPhone,SpecsModel,PreQty,PreAmount,ProjectAbout,Status,Remark,RecordDate,LastUpdatedDate
                        from ProjectReportPrepare ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<InfoneProjectReportPrepareInfo> list = new List<InfoneProjectReportPrepareInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneProjectReportPrepareInfo model = new InfoneProjectReportPrepareInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.ProjectName = reader.GetString(3);
                        model.ProjectSource = reader.GetString(4);
                        model.CustomerOfficial = reader.GetString(5);
                        model.ContactMan = reader.GetString(6);
                        model.ContactPhone = reader.GetString(7);
                        model.SpecsModel = reader.GetString(8);
                        model.PreQty = reader.GetInt32(9);
                        model.PreAmount = reader.GetDecimal(10);
                        model.ProjectAbout = reader.GetString(11);
                        model.Status = reader.GetString(12);
                        model.Remark = reader.GetString(13);
                        model.RecordDate = reader.GetDateTime(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<InfoneProjectReportPrepareInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,CustomerId,ProjectName,ProjectSource,CustomerOfficial,ContactMan,ContactPhone,SpecsModel,PreQty,PreAmount,ProjectAbout,Status,Remark,RecordDate,LastUpdatedDate 
			            from ProjectReportPrepare
					    order by LastUpdatedDate desc ");

            IList<InfoneProjectReportPrepareInfo> list = new List<InfoneProjectReportPrepareInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneProjectReportPrepareInfo model = new InfoneProjectReportPrepareInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.ProjectName = reader.GetString(3);
                        model.ProjectSource = reader.GetString(4);
                        model.CustomerOfficial = reader.GetString(5);
                        model.ContactMan = reader.GetString(6);
                        model.ContactPhone = reader.GetString(7);
                        model.SpecsModel = reader.GetString(8);
                        model.PreQty = reader.GetInt32(9);
                        model.PreAmount = reader.GetDecimal(10);
                        model.ProjectAbout = reader.GetString(11);
                        model.Status = reader.GetString(12);
                        model.Remark = reader.GetString(13);
                        model.RecordDate = reader.GetDateTime(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
