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
    public partial class InfoneDeviceRepairRecord
    {
        #region IDeviceRepairRecord Member

        public IList<InfoneDeviceRepairRecordInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(250);
            sb.Append(@"select count(*) from DeviceRepairRecord drr
                        left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = drr.UserId 
                        ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<InfoneDeviceRepairRecordInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by drr.LastUpdatedDate desc) as RowNumber,
			          drr.Id,drr.UserId,drr.RecordDate,drr.Customer,drr.SerialNumber,drr.DeviceModel,drr.FaultCause,drr.SolveMethod,
                      drr.CustomerProblem,drr.DevicePart,drr.TreatmentSituation,drr.WhetherFix,drr.HandoverPerson,drr.IsBack,drr.BackDate,
                      drr.RegisteredPerson,drr.Remark,drr.LastUpdatedDate
                      ,UserName
					  from DeviceRepairRecord drr 
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = drr.UserId 
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<InfoneDeviceRepairRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new InfoneDeviceRepairRecordInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.RecordDate = reader.GetDateTime(3);
                        model.Customer = reader.GetString(4);
                        model.SerialNumber = reader.GetString(5);
                        model.DeviceModel = reader.GetString(6);
                        model.FaultCause = reader.GetString(7);
                        model.SolveMethod = reader.GetString(8);
                        model.CustomerProblem = reader.GetString(9);
                        model.DevicePart = reader.GetString(10);
                        model.TreatmentSituation = reader.GetString(11);
                        model.WhetherFix = reader.GetString(12);
                        model.HandoverPerson = reader.GetString(13);
                        model.IsBack = reader.GetBoolean(14);
                        model.BackDate = reader.GetDateTime(15);
                        model.RegisteredPerson = reader.GetString(16);
                        model.Remark = reader.GetString(17);
                        model.LastUpdatedDate = reader.GetDateTime(18);

                        model.UserName = reader.IsDBNull(19) ? "" : reader.GetString(19);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public DataSet GetDsByExport(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(1000);

            sb.Append(@"select 
			          drr.Id,drr.UserId,drr.RecordDate,drr.Customer,drr.SerialNumber,drr.DeviceModel,drr.FaultCause,drr.SolveMethod,
                      drr.CustomerProblem,drr.DevicePart,drr.TreatmentSituation,drr.WhetherFix,drr.HandoverPerson,drr.IsBack,drr.BackDate,
                      drr.RegisteredPerson,drr.Remark,drr.LastUpdatedDate
                      ,UserName
					  from DeviceRepairRecord drr 
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = drr.UserId 
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by drr.LastUpdatedDate desc ");

            return SqlHelper.ExecuteDataset(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms);
        }

        #endregion
    }
}
