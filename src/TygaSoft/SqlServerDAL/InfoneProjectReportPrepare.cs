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
    public partial class InfoneProjectReportPrepare
    {
        #region IProjectReportPrepare Member

        public InfoneProjectReportPrepareInfo GetModelByJoin(Guid id)
        {
            InfoneProjectReportPrepareInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 prp.Id,prp.UserId,prp.CustomerId,prp.ProjectName,prp.ProjectSource,prp.CustomerOfficial,prp.ContactMan,prp.ContactPhone,prp.SpecsModel,prp.PreQty,prp.PreAmount,prp.ProjectAbout,prp.Status,prp.Remark,prp.RecordDate,prp.LastUpdatedDate 
			            ,c.Coded CustomerCode,c.Named CustomerName,c.ShortName CustomerShortName
                        from ProjectReportPrepare prp
                        left join Customer c on c.Id = prp.CustomerId
						where prp.Id = @Id ");
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

                        model.CustomerCode = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.CustomerName = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.CustomerShortName = reader.IsDBNull(18) ? "" : reader.GetString(18);
                    }
                }
            }

            return model;
        }

        public IList<InfoneProjectReportPrepareInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from ProjectReportPrepare prp 
                         left join Customer c on c.Id = prp.CustomerId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<InfoneProjectReportPrepareInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by prp.RecordDate) as RowNumber,
			          prp.Id,prp.UserId,prp.CustomerId,prp.ProjectName,prp.ProjectSource,prp.CustomerOfficial,prp.ContactMan,prp.ContactPhone,prp.SpecsModel,prp.PreQty,prp.PreAmount,prp.ProjectAbout,prp.Status,prp.Remark,prp.RecordDate,prp.LastUpdatedDate
					  ,c.Coded CustomerCode,c.Named CustomerName,c.ShortName
                      from ProjectReportPrepare prp 
                      left join Customer c on c.Id = prp.CustomerId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<InfoneProjectReportPrepareInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new InfoneProjectReportPrepareInfo();
                        model.Id = reader.GetGuid(1);
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
                        model.SRecordDate = model.RecordDate.ToString("yyyy-MM-dd");

                        model.CustomerCode = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        model.CustomerName = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        model.CustomerShortName = reader.IsDBNull(19) ? "" : reader.GetString(19);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<InfoneProjectReportPrepareInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select prp.Id,prp.UserId,prp.CustomerId,prp.ProjectName,prp.ProjectSource,prp.CustomerOfficial,prp.ContactMan,prp.ContactPhone,prp.SpecsModel,prp.PreQty,prp.PreAmount,prp.ProjectAbout,prp.Status,prp.Remark,prp.RecordDate,prp.LastUpdatedDate
                        from ProjectReportPrepare prp ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by prp.RecordDate ");

            var list = new List<InfoneProjectReportPrepareInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new InfoneProjectReportPrepareInfo();
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
