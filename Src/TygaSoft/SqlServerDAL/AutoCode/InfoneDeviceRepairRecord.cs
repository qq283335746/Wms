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
    public partial class InfoneDeviceRepairRecord : IInfoneDeviceRepairRecord
    {
        #region IDeviceRepairRecord Member

        public int Insert(InfoneDeviceRepairRecordInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into DeviceRepairRecord (UserId,RecordDate,Customer,SerialNumber,DeviceModel,FaultCause,SolveMethod,CustomerProblem,DevicePart,TreatmentSituation,WhetherFix,HandoverPerson,IsBack,BackDate,RegisteredPerson,Remark,LastUpdatedDate)
			            values
						(@UserId,@RecordDate,@Customer,@SerialNumber,@DeviceModel,@FaultCause,@SolveMethod,@CustomerProblem,@DevicePart,@TreatmentSituation,@WhetherFix,@HandoverPerson,@IsBack,@BackDate,@RegisteredPerson,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@Customer",SqlDbType.NVarChar,30),
new SqlParameter("@SerialNumber",SqlDbType.VarChar,36),
new SqlParameter("@DeviceModel",SqlDbType.VarChar,20),
new SqlParameter("@FaultCause",SqlDbType.NVarChar,100),
new SqlParameter("@SolveMethod",SqlDbType.NVarChar,100),
new SqlParameter("@CustomerProblem",SqlDbType.NVarChar,100),
new SqlParameter("@DevicePart",SqlDbType.NVarChar,100),
new SqlParameter("@TreatmentSituation",SqlDbType.NVarChar,100),
new SqlParameter("@WhetherFix",SqlDbType.NVarChar,20),
new SqlParameter("@HandoverPerson",SqlDbType.NVarChar,20),
new SqlParameter("@IsBack",SqlDbType.Bit),
new SqlParameter("@BackDate",SqlDbType.DateTime),
new SqlParameter("@RegisteredPerson",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.RecordDate;
            parms[2].Value = model.Customer;
            parms[3].Value = model.SerialNumber;
            parms[4].Value = model.DeviceModel;
            parms[5].Value = model.FaultCause;
            parms[6].Value = model.SolveMethod;
            parms[7].Value = model.CustomerProblem;
            parms[8].Value = model.DevicePart;
            parms[9].Value = model.TreatmentSituation;
            parms[10].Value = model.WhetherFix;
            parms[11].Value = model.HandoverPerson;
            parms[12].Value = model.IsBack;
            parms[13].Value = model.BackDate;
            parms[14].Value = model.RegisteredPerson;
            parms[15].Value = model.Remark;
            parms[16].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(InfoneDeviceRepairRecordInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into DeviceRepairRecord (Id,UserId,RecordDate,Customer,SerialNumber,DeviceModel,FaultCause,SolveMethod,CustomerProblem,DevicePart,TreatmentSituation,WhetherFix,HandoverPerson,IsBack,BackDate,RegisteredPerson,Remark,LastUpdatedDate)
			            values
						(@Id,@UserId,@RecordDate,@Customer,@SerialNumber,@DeviceModel,@FaultCause,@SolveMethod,@CustomerProblem,@DevicePart,@TreatmentSituation,@WhetherFix,@HandoverPerson,@IsBack,@BackDate,@RegisteredPerson,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@Customer",SqlDbType.NVarChar,30),
new SqlParameter("@SerialNumber",SqlDbType.VarChar,36),
new SqlParameter("@DeviceModel",SqlDbType.VarChar,20),
new SqlParameter("@FaultCause",SqlDbType.NVarChar,100),
new SqlParameter("@SolveMethod",SqlDbType.NVarChar,100),
new SqlParameter("@CustomerProblem",SqlDbType.NVarChar,100),
new SqlParameter("@DevicePart",SqlDbType.NVarChar,100),
new SqlParameter("@TreatmentSituation",SqlDbType.NVarChar,100),
new SqlParameter("@WhetherFix",SqlDbType.NVarChar,20),
new SqlParameter("@HandoverPerson",SqlDbType.NVarChar,20),
new SqlParameter("@IsBack",SqlDbType.Bit),
new SqlParameter("@BackDate",SqlDbType.DateTime),
new SqlParameter("@RegisteredPerson",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.RecordDate;
            parms[3].Value = model.Customer;
            parms[4].Value = model.SerialNumber;
            parms[5].Value = model.DeviceModel;
            parms[6].Value = model.FaultCause;
            parms[7].Value = model.SolveMethod;
            parms[8].Value = model.CustomerProblem;
            parms[9].Value = model.DevicePart;
            parms[10].Value = model.TreatmentSituation;
            parms[11].Value = model.WhetherFix;
            parms[12].Value = model.HandoverPerson;
            parms[13].Value = model.IsBack;
            parms[14].Value = model.BackDate;
            parms[15].Value = model.RegisteredPerson;
            parms[16].Value = model.Remark;
            parms[17].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(InfoneDeviceRepairRecordInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update DeviceRepairRecord set UserId = @UserId,RecordDate = @RecordDate,Customer = @Customer,SerialNumber = @SerialNumber,DeviceModel = @DeviceModel,FaultCause = @FaultCause,SolveMethod = @SolveMethod,CustomerProblem = @CustomerProblem,DevicePart = @DevicePart,TreatmentSituation = @TreatmentSituation,WhetherFix = @WhetherFix,HandoverPerson = @HandoverPerson,IsBack = @IsBack,BackDate = @BackDate,RegisteredPerson = @RegisteredPerson,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@Customer",SqlDbType.NVarChar,30),
new SqlParameter("@SerialNumber",SqlDbType.VarChar,36),
new SqlParameter("@DeviceModel",SqlDbType.VarChar,20),
new SqlParameter("@FaultCause",SqlDbType.NVarChar,100),
new SqlParameter("@SolveMethod",SqlDbType.NVarChar,100),
new SqlParameter("@CustomerProblem",SqlDbType.NVarChar,100),
new SqlParameter("@DevicePart",SqlDbType.NVarChar,100),
new SqlParameter("@TreatmentSituation",SqlDbType.NVarChar,100),
new SqlParameter("@WhetherFix",SqlDbType.NVarChar,20),
new SqlParameter("@HandoverPerson",SqlDbType.NVarChar,20),
new SqlParameter("@IsBack",SqlDbType.Bit),
new SqlParameter("@BackDate",SqlDbType.DateTime),
new SqlParameter("@RegisteredPerson",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.RecordDate;
            parms[3].Value = model.Customer;
            parms[4].Value = model.SerialNumber;
            parms[5].Value = model.DeviceModel;
            parms[6].Value = model.FaultCause;
            parms[7].Value = model.SolveMethod;
            parms[8].Value = model.CustomerProblem;
            parms[9].Value = model.DevicePart;
            parms[10].Value = model.TreatmentSituation;
            parms[11].Value = model.WhetherFix;
            parms[12].Value = model.HandoverPerson;
            parms[13].Value = model.IsBack;
            parms[14].Value = model.BackDate;
            parms[15].Value = model.RegisteredPerson;
            parms[16].Value = model.Remark;
            parms[17].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from DeviceRepairRecord where Id = @Id ");
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
                sb.Append(@"delete from DeviceRepairRecord where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public InfoneDeviceRepairRecordInfo GetModel(Guid id)
        {
            InfoneDeviceRepairRecordInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,RecordDate,Customer,SerialNumber,DeviceModel,FaultCause,SolveMethod,CustomerProblem,DevicePart,TreatmentSituation,WhetherFix,HandoverPerson,IsBack,BackDate,RegisteredPerson,Remark,LastUpdatedDate 
			            from DeviceRepairRecord
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
                        model = new InfoneDeviceRepairRecordInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.RecordDate = reader.GetDateTime(2);
                        model.Customer = reader.GetString(3);
                        model.SerialNumber = reader.GetString(4);
                        model.DeviceModel = reader.GetString(5);
                        model.FaultCause = reader.GetString(6);
                        model.SolveMethod = reader.GetString(7);
                        model.CustomerProblem = reader.GetString(8);
                        model.DevicePart = reader.GetString(9);
                        model.TreatmentSituation = reader.GetString(10);
                        model.WhetherFix = reader.GetString(11);
                        model.HandoverPerson = reader.GetString(12);
                        model.IsBack = reader.GetBoolean(13);
                        model.BackDate = reader.GetDateTime(14);
                        model.RegisteredPerson = reader.GetString(15);
                        model.Remark = reader.GetString(16);
                        model.LastUpdatedDate = reader.GetDateTime(17);
                    }
                }
            }

            return model;
        }

        public IList<InfoneDeviceRepairRecordInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from DeviceRepairRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<InfoneDeviceRepairRecordInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,RecordDate,Customer,SerialNumber,DeviceModel,FaultCause,SolveMethod,CustomerProblem,DevicePart,TreatmentSituation,WhetherFix,HandoverPerson,IsBack,BackDate,RegisteredPerson,Remark,LastUpdatedDate
					  from DeviceRepairRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<InfoneDeviceRepairRecordInfo> list = new List<InfoneDeviceRepairRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneDeviceRepairRecordInfo model = new InfoneDeviceRepairRecordInfo();
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<InfoneDeviceRepairRecordInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,RecordDate,Customer,SerialNumber,DeviceModel,FaultCause,SolveMethod,CustomerProblem,DevicePart,TreatmentSituation,WhetherFix,HandoverPerson,IsBack,BackDate,RegisteredPerson,Remark,LastUpdatedDate
					   from DeviceRepairRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<InfoneDeviceRepairRecordInfo> list = new List<InfoneDeviceRepairRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneDeviceRepairRecordInfo model = new InfoneDeviceRepairRecordInfo();
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<InfoneDeviceRepairRecordInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,RecordDate,Customer,SerialNumber,DeviceModel,FaultCause,SolveMethod,CustomerProblem,DevicePart,TreatmentSituation,WhetherFix,HandoverPerson,IsBack,BackDate,RegisteredPerson,Remark,LastUpdatedDate
                        from DeviceRepairRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<InfoneDeviceRepairRecordInfo> list = new List<InfoneDeviceRepairRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneDeviceRepairRecordInfo model = new InfoneDeviceRepairRecordInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.RecordDate = reader.GetDateTime(2);
                        model.Customer = reader.GetString(3);
                        model.SerialNumber = reader.GetString(4);
                        model.DeviceModel = reader.GetString(5);
                        model.FaultCause = reader.GetString(6);
                        model.SolveMethod = reader.GetString(7);
                        model.CustomerProblem = reader.GetString(8);
                        model.DevicePart = reader.GetString(9);
                        model.TreatmentSituation = reader.GetString(10);
                        model.WhetherFix = reader.GetString(11);
                        model.HandoverPerson = reader.GetString(12);
                        model.IsBack = reader.GetBoolean(13);
                        model.BackDate = reader.GetDateTime(14);
                        model.RegisteredPerson = reader.GetString(15);
                        model.Remark = reader.GetString(16);
                        model.LastUpdatedDate = reader.GetDateTime(17);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<InfoneDeviceRepairRecordInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,RecordDate,Customer,SerialNumber,DeviceModel,FaultCause,SolveMethod,CustomerProblem,DevicePart,TreatmentSituation,WhetherFix,HandoverPerson,IsBack,BackDate,RegisteredPerson,Remark,LastUpdatedDate 
			            from DeviceRepairRecord
					    order by LastUpdatedDate desc ");

            IList<InfoneDeviceRepairRecordInfo> list = new List<InfoneDeviceRepairRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneDeviceRepairRecordInfo model = new InfoneDeviceRepairRecordInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.RecordDate = reader.GetDateTime(2);
                        model.Customer = reader.GetString(3);
                        model.SerialNumber = reader.GetString(4);
                        model.DeviceModel = reader.GetString(5);
                        model.FaultCause = reader.GetString(6);
                        model.SolveMethod = reader.GetString(7);
                        model.CustomerProblem = reader.GetString(8);
                        model.DevicePart = reader.GetString(9);
                        model.TreatmentSituation = reader.GetString(10);
                        model.WhetherFix = reader.GetString(11);
                        model.HandoverPerson = reader.GetString(12);
                        model.IsBack = reader.GetBoolean(13);
                        model.BackDate = reader.GetDateTime(14);
                        model.RegisteredPerson = reader.GetString(15);
                        model.Remark = reader.GetString(16);
                        model.LastUpdatedDate = reader.GetDateTime(17);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
