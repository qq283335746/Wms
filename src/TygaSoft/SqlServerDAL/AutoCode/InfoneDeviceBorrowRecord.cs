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
    public partial class InfoneDeviceBorrowRecord : IInfoneDeviceBorrowRecord
    {
        #region IInfoneDeviceBorrowRecord Member

        public int Insert(InfoneDeviceBorrowRecordInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into DeviceBorrowRecord (UserId,Customer,CustomerContact,SerialNumber,DeviceModel,DevicePart,PartStatus,ProjectAbout,SaleMan,SendOrderCode,IsBack,BackDate,Register,Remark,FunType,RecordDate,LastUpdatedDate)
			            values
						(@UserId,@Customer,@CustomerContact,@SerialNumber,@DeviceModel,@DevicePart,@PartStatus,@ProjectAbout,@SaleMan,@SendOrderCode,@IsBack,@BackDate,@Register,@Remark,@FunType,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Customer",SqlDbType.NVarChar,30),
new SqlParameter("@CustomerContact",SqlDbType.NVarChar,100),
new SqlParameter("@SerialNumber",SqlDbType.VarChar,36),
new SqlParameter("@DeviceModel",SqlDbType.VarChar,100),
new SqlParameter("@DevicePart",SqlDbType.NVarChar,256),
new SqlParameter("@PartStatus",SqlDbType.NVarChar,256),
new SqlParameter("@ProjectAbout",SqlDbType.NVarChar,256),
new SqlParameter("@SaleMan",SqlDbType.NVarChar,20),
new SqlParameter("@SendOrderCode",SqlDbType.VarChar,36),
new SqlParameter("@IsBack",SqlDbType.Bit),
new SqlParameter("@BackDate",SqlDbType.DateTime),
new SqlParameter("@Register",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@FunType",SqlDbType.NVarChar,20),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.Customer;
            parms[2].Value = model.CustomerContact;
            parms[3].Value = model.SerialNumber;
            parms[4].Value = model.DeviceModel;
            parms[5].Value = model.DevicePart;
            parms[6].Value = model.PartStatus;
            parms[7].Value = model.ProjectAbout;
            parms[8].Value = model.SaleMan;
            parms[9].Value = model.SendOrderCode;
            parms[10].Value = model.IsBack;
            parms[11].Value = model.BackDate;
            parms[12].Value = model.Register;
            parms[13].Value = model.Remark;
            parms[14].Value = model.FunType;
            parms[15].Value = model.RecordDate;
            parms[16].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(InfoneDeviceBorrowRecordInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into DeviceBorrowRecord (Id,UserId,Customer,CustomerContact,SerialNumber,DeviceModel,DevicePart,PartStatus,ProjectAbout,SaleMan,SendOrderCode,IsBack,BackDate,Register,Remark,FunType,RecordDate,LastUpdatedDate)
			            values
						(@Id,@UserId,@Customer,@CustomerContact,@SerialNumber,@DeviceModel,@DevicePart,@PartStatus,@ProjectAbout,@SaleMan,@SendOrderCode,@IsBack,@BackDate,@Register,@Remark,@FunType,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Customer",SqlDbType.NVarChar,30),
new SqlParameter("@CustomerContact",SqlDbType.NVarChar,100),
new SqlParameter("@SerialNumber",SqlDbType.VarChar,36),
new SqlParameter("@DeviceModel",SqlDbType.VarChar,100),
new SqlParameter("@DevicePart",SqlDbType.NVarChar,256),
new SqlParameter("@PartStatus",SqlDbType.NVarChar,256),
new SqlParameter("@ProjectAbout",SqlDbType.NVarChar,256),
new SqlParameter("@SaleMan",SqlDbType.NVarChar,20),
new SqlParameter("@SendOrderCode",SqlDbType.VarChar,36),
new SqlParameter("@IsBack",SqlDbType.Bit),
new SqlParameter("@BackDate",SqlDbType.DateTime),
new SqlParameter("@Register",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@FunType",SqlDbType.NVarChar,20),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.Customer;
            parms[3].Value = model.CustomerContact;
            parms[4].Value = model.SerialNumber;
            parms[5].Value = model.DeviceModel;
            parms[6].Value = model.DevicePart;
            parms[7].Value = model.PartStatus;
            parms[8].Value = model.ProjectAbout;
            parms[9].Value = model.SaleMan;
            parms[10].Value = model.SendOrderCode;
            parms[11].Value = model.IsBack;
            parms[12].Value = model.BackDate;
            parms[13].Value = model.Register;
            parms[14].Value = model.Remark;
            parms[15].Value = model.FunType;
            parms[16].Value = model.RecordDate;
            parms[17].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(InfoneDeviceBorrowRecordInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update DeviceBorrowRecord set UserId = @UserId,Customer = @Customer,CustomerContact = @CustomerContact,SerialNumber = @SerialNumber,DeviceModel = @DeviceModel,DevicePart = @DevicePart,PartStatus = @PartStatus,ProjectAbout = @ProjectAbout,SaleMan = @SaleMan,SendOrderCode = @SendOrderCode,IsBack = @IsBack,BackDate = @BackDate,Register = @Register,Remark = @Remark,FunType = @FunType,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Customer",SqlDbType.NVarChar,30),
new SqlParameter("@CustomerContact",SqlDbType.NVarChar,100),
new SqlParameter("@SerialNumber",SqlDbType.VarChar,36),
new SqlParameter("@DeviceModel",SqlDbType.VarChar,100),
new SqlParameter("@DevicePart",SqlDbType.NVarChar,256),
new SqlParameter("@PartStatus",SqlDbType.NVarChar,256),
new SqlParameter("@ProjectAbout",SqlDbType.NVarChar,256),
new SqlParameter("@SaleMan",SqlDbType.NVarChar,20),
new SqlParameter("@SendOrderCode",SqlDbType.VarChar,36),
new SqlParameter("@IsBack",SqlDbType.Bit),
new SqlParameter("@BackDate",SqlDbType.DateTime),
new SqlParameter("@Register",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@FunType",SqlDbType.NVarChar,20),
new SqlParameter("@RecordDate",SqlDbType.DateTime),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.Customer;
            parms[3].Value = model.CustomerContact;
            parms[4].Value = model.SerialNumber;
            parms[5].Value = model.DeviceModel;
            parms[6].Value = model.DevicePart;
            parms[7].Value = model.PartStatus;
            parms[8].Value = model.ProjectAbout;
            parms[9].Value = model.SaleMan;
            parms[10].Value = model.SendOrderCode;
            parms[11].Value = model.IsBack;
            parms[12].Value = model.BackDate;
            parms[13].Value = model.Register;
            parms[14].Value = model.Remark;
            parms[15].Value = model.FunType;
            parms[16].Value = model.RecordDate;
            parms[17].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from DeviceBorrowRecord where Id = @Id ");
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
                sb.Append(@"delete from DeviceBorrowRecord where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public InfoneDeviceBorrowRecordInfo GetModel(Guid id)
        {
            InfoneDeviceBorrowRecordInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,Customer,CustomerContact,SerialNumber,DeviceModel,DevicePart,PartStatus,ProjectAbout,SaleMan,SendOrderCode,IsBack,BackDate,Register,Remark,FunType,RecordDate,LastUpdatedDate 
			            from DeviceBorrowRecord
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
                        model = new InfoneDeviceBorrowRecordInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.Customer = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.CustomerContact = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.SerialNumber = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.DeviceModel = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.DevicePart = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.PartStatus = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.ProjectAbout = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.SaleMan = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.SendOrderCode = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.IsBack = reader.IsDBNull(11) ? false : reader.GetBoolean(11);
                        model.BackDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                        model.Register = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.Remark = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.FunType = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.RecordDate = reader.IsDBNull(16) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(16);
                        model.LastUpdatedDate = reader.IsDBNull(17) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(17);
                    }
                }
            }

            return model;
        }

        public IList<InfoneDeviceBorrowRecordInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from DeviceBorrowRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<InfoneDeviceBorrowRecordInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,Customer,CustomerContact,SerialNumber,DeviceModel,DevicePart,PartStatus,ProjectAbout,SaleMan,SendOrderCode,IsBack,BackDate,Register,Remark,FunType,RecordDate,LastUpdatedDate
					  from DeviceBorrowRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<InfoneDeviceBorrowRecordInfo> list = new List<InfoneDeviceBorrowRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneDeviceBorrowRecordInfo model = new InfoneDeviceBorrowRecordInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Customer = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.CustomerContact = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.SerialNumber = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.DeviceModel = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.DevicePart = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.PartStatus = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.ProjectAbout = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.SaleMan = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.SendOrderCode = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.IsBack = reader.IsDBNull(12) ? false : reader.GetBoolean(12);
                        model.BackDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);
                        model.Register = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.Remark = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.FunType = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.RecordDate = reader.IsDBNull(17) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(17);
                        model.LastUpdatedDate = reader.IsDBNull(18) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(18);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<InfoneDeviceBorrowRecordInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,Customer,CustomerContact,SerialNumber,DeviceModel,DevicePart,PartStatus,ProjectAbout,SaleMan,SendOrderCode,IsBack,BackDate,Register,Remark,FunType,RecordDate,LastUpdatedDate
					   from DeviceBorrowRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<InfoneDeviceBorrowRecordInfo> list = new List<InfoneDeviceBorrowRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneDeviceBorrowRecordInfo model = new InfoneDeviceBorrowRecordInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Customer = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.CustomerContact = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.SerialNumber = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.DeviceModel = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.DevicePart = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.PartStatus = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.ProjectAbout = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.SaleMan = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.SendOrderCode = reader.IsDBNull(11) ? string.Empty : reader.GetString(11);
                        model.IsBack = reader.IsDBNull(12) ? false : reader.GetBoolean(12);
                        model.BackDate = reader.IsDBNull(13) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(13);
                        model.Register = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.Remark = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.FunType = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.RecordDate = reader.IsDBNull(17) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(17);
                        model.LastUpdatedDate = reader.IsDBNull(18) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(18);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<InfoneDeviceBorrowRecordInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,Customer,CustomerContact,SerialNumber,DeviceModel,DevicePart,PartStatus,ProjectAbout,SaleMan,SendOrderCode,IsBack,BackDate,Register,Remark,FunType,RecordDate,LastUpdatedDate
                        from DeviceBorrowRecord ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<InfoneDeviceBorrowRecordInfo> list = new List<InfoneDeviceBorrowRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneDeviceBorrowRecordInfo model = new InfoneDeviceBorrowRecordInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.Customer = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.CustomerContact = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.SerialNumber = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.DeviceModel = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.DevicePart = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.PartStatus = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.ProjectAbout = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.SaleMan = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.SendOrderCode = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.IsBack = reader.IsDBNull(11) ? false : reader.GetBoolean(11);
                        model.BackDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                        model.Register = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.Remark = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.FunType = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.RecordDate = reader.IsDBNull(16) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(16);
                        model.LastUpdatedDate = reader.IsDBNull(17) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(17);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<InfoneDeviceBorrowRecordInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,Customer,CustomerContact,SerialNumber,DeviceModel,DevicePart,PartStatus,ProjectAbout,SaleMan,SendOrderCode,IsBack,BackDate,Register,Remark,FunType,RecordDate,LastUpdatedDate 
			            from DeviceBorrowRecord
					    order by LastUpdatedDate desc ");

            IList<InfoneDeviceBorrowRecordInfo> list = new List<InfoneDeviceBorrowRecordInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        InfoneDeviceBorrowRecordInfo model = new InfoneDeviceBorrowRecordInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.Customer = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.CustomerContact = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.SerialNumber = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.DeviceModel = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.DevicePart = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.PartStatus = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.ProjectAbout = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.SaleMan = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.SendOrderCode = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                        model.IsBack = reader.IsDBNull(11) ? false : reader.GetBoolean(11);
                        model.BackDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                        model.Register = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.Remark = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.FunType = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.RecordDate = reader.IsDBNull(16) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(16);
                        model.LastUpdatedDate = reader.IsDBNull(17) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(17);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
