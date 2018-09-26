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
    public partial class Vehicle : IVehicle
    {
        #region IVehicle Member

        public int Insert(VehicleInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Vehicle (UserId,VehicleID,VehicleModel,Licence,LicPic,OffenceRecord,DriverID,DriverIDPicture,RewardRecord,Remark,Sort,IsDisable,LastUpdatedDate)
			            values
						(@UserId,@VehicleID,@VehicleModel,@Licence,@LicPic,@OffenceRecord,@DriverID,@DriverIDPicture,@RewardRecord,@Remark,@Sort,@IsDisable,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@VehicleID",SqlDbType.VarChar,50),
                                        new SqlParameter("@VehicleModel",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Licence",SqlDbType.VarChar,20),
                                        new SqlParameter("@LicPic",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OffenceRecord",SqlDbType.NVarChar,256),
                                        new SqlParameter("@DriverID",SqlDbType.VarChar,22),
                                        new SqlParameter("@DriverIDPicture",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@RewardRecord",SqlDbType.NVarChar,256),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@IsDisable",SqlDbType.Bit),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.VehicleID;
            parms[2].Value = model.VehicleModel;
            parms[3].Value = model.Licence;
            parms[4].Value = model.LicPic;
            parms[5].Value = model.OffenceRecord;
            parms[6].Value = model.DriverID;
            parms[7].Value = model.DriverIDPicture;
            parms[8].Value = model.RewardRecord;
            parms[9].Value = model.Remark;
            parms[10].Value = model.Sort;
            parms[11].Value = model.IsDisable;
            parms[12].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(VehicleInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Vehicle (Id,UserId,VehicleID,VehicleModel,Licence,LicPic,OffenceRecord,DriverID,DriverIDPicture,RewardRecord,Remark,Sort,IsDisable,LastUpdatedDate)
			            values
						(@Id,@UserId,@VehicleID,@VehicleModel,@Licence,@LicPic,@OffenceRecord,@DriverID,@DriverIDPicture,@RewardRecord,@Remark,@Sort,@IsDisable,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@VehicleID",SqlDbType.VarChar,50),
                                        new SqlParameter("@VehicleModel",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Licence",SqlDbType.VarChar,20),
                                        new SqlParameter("@LicPic",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OffenceRecord",SqlDbType.NVarChar,256),
                                        new SqlParameter("@DriverID",SqlDbType.VarChar,22),
                                        new SqlParameter("@DriverIDPicture",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@RewardRecord",SqlDbType.NVarChar,256),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@IsDisable",SqlDbType.Bit),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.VehicleID;
            parms[3].Value = model.VehicleModel;
            parms[4].Value = model.Licence;
            parms[5].Value = model.LicPic;
            parms[6].Value = model.OffenceRecord;
            parms[7].Value = model.DriverID;
            parms[8].Value = model.DriverIDPicture;
            parms[9].Value = model.RewardRecord;
            parms[10].Value = model.Remark;
            parms[11].Value = model.Sort;
            parms[12].Value = model.IsDisable;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(VehicleInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Vehicle set UserId = @UserId,VehicleID = @VehicleID,VehicleModel = @VehicleModel,Licence = @Licence,LicPic = @LicPic,OffenceRecord = @OffenceRecord,DriverID = @DriverID,DriverIDPicture = @DriverIDPicture,RewardRecord = @RewardRecord,Remark = @Remark,Sort = @Sort,IsDisable = @IsDisable,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@VehicleID",SqlDbType.VarChar,50),
                                    new SqlParameter("@VehicleModel",SqlDbType.NVarChar,100),
                                    new SqlParameter("@Licence",SqlDbType.VarChar,20),
                                    new SqlParameter("@LicPic",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@OffenceRecord",SqlDbType.NVarChar,256),
                                    new SqlParameter("@DriverID",SqlDbType.VarChar,22),
                                    new SqlParameter("@DriverIDPicture",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@RewardRecord",SqlDbType.NVarChar,256),
                                    new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                    new SqlParameter("@Sort",SqlDbType.Int),
                                    new SqlParameter("@IsDisable",SqlDbType.Bit),
                                    new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.VehicleID;
            parms[3].Value = model.VehicleModel;
            parms[4].Value = model.Licence;
            parms[5].Value = model.LicPic;
            parms[6].Value = model.OffenceRecord;
            parms[7].Value = model.DriverID;
            parms[8].Value = model.DriverIDPicture;
            parms[9].Value = model.RewardRecord;
            parms[10].Value = model.Remark;
            parms[11].Value = model.Sort;
            parms[12].Value = model.IsDisable;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Vehicle where Id = @Id ");
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
                sb.Append(@"delete from Vehicle where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public VehicleInfo GetModel(Guid id)
        {
            VehicleInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,VehicleID,VehicleModel,Licence,LicPic,OffenceRecord,DriverID,DriverIDPicture,RewardRecord,Remark,Sort,IsDisable,LastUpdatedDate 
			            from Vehicle
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
                        model = new VehicleInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.VehicleID = reader.GetString(2);
                        model.VehicleModel = reader.GetString(3);
                        model.Licence = reader.GetString(4);
                        model.LicPic = reader.GetGuid(5);
                        model.OffenceRecord = reader.GetString(6);
                        model.DriverID = reader.GetString(7);
                        model.DriverIDPicture = reader.GetGuid(8);
                        model.RewardRecord = reader.GetString(9);
                        model.Remark = reader.GetString(10);
                        model.Sort = reader.GetInt32(11);
                        model.IsDisable = reader.GetBoolean(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);
                    }
                }
            }

            return model;
        }

        public IList<VehicleInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Vehicle ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<VehicleInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          Id,UserId,VehicleID,VehicleModel,Licence,LicPic,OffenceRecord,DriverID,DriverIDPicture,RewardRecord,Remark,Sort,IsDisable,LastUpdatedDate
					  from Vehicle ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<VehicleInfo> list = new List<VehicleInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VehicleInfo model = new VehicleInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.VehicleID = reader.GetString(3);
                        model.VehicleModel = reader.GetString(4);
                        model.Licence = reader.GetString(5);
                        model.LicPic = reader.GetGuid(6);
                        model.OffenceRecord = reader.GetString(7);
                        model.DriverID = reader.GetString(8);
                        model.DriverIDPicture = reader.GetGuid(9);
                        model.RewardRecord = reader.GetString(10);
                        model.Remark = reader.GetString(11);
                        model.Sort = reader.GetInt32(12);
                        model.IsDisable = reader.GetBoolean(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<VehicleInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           Id,UserId,VehicleID,VehicleModel,Licence,LicPic,OffenceRecord,DriverID,DriverIDPicture,RewardRecord,Remark,Sort,IsDisable,LastUpdatedDate
					   from Vehicle ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<VehicleInfo> list = new List<VehicleInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VehicleInfo model = new VehicleInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.VehicleID = reader.GetString(3);
                        model.VehicleModel = reader.GetString(4);
                        model.Licence = reader.GetString(5);
                        model.LicPic = reader.GetGuid(6);
                        model.OffenceRecord = reader.GetString(7);
                        model.DriverID = reader.GetString(8);
                        model.DriverIDPicture = reader.GetGuid(9);
                        model.RewardRecord = reader.GetString(10);
                        model.Remark = reader.GetString(11);
                        model.Sort = reader.GetInt32(12);
                        model.IsDisable = reader.GetBoolean(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<VehicleInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,VehicleID,VehicleModel,Licence,LicPic,OffenceRecord,DriverID,DriverIDPicture,RewardRecord,Remark,Sort,IsDisable,LastUpdatedDate
                        from Vehicle ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<VehicleInfo> list = new List<VehicleInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VehicleInfo model = new VehicleInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.VehicleID = reader.GetString(2);
                        model.VehicleModel = reader.GetString(3);
                        model.Licence = reader.GetString(4);
                        model.LicPic = reader.GetGuid(5);
                        model.OffenceRecord = reader.GetString(6);
                        model.DriverID = reader.GetString(7);
                        model.DriverIDPicture = reader.GetGuid(8);
                        model.RewardRecord = reader.GetString(9);
                        model.Remark = reader.GetString(10);
                        model.Sort = reader.GetInt32(11);
                        model.IsDisable = reader.GetBoolean(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<VehicleInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,VehicleID,VehicleModel,Licence,LicPic,OffenceRecord,DriverID,DriverIDPicture,RewardRecord,Remark,Sort,IsDisable,LastUpdatedDate 
			            from Vehicle
					    order by Sort ");

            IList<VehicleInfo> list = new List<VehicleInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VehicleInfo model = new VehicleInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.VehicleID = reader.GetString(2);
                        model.VehicleModel = reader.GetString(3);
                        model.Licence = reader.GetString(4);
                        model.LicPic = reader.GetGuid(5);
                        model.OffenceRecord = reader.GetString(6);
                        model.DriverID = reader.GetString(7);
                        model.DriverIDPicture = reader.GetGuid(8);
                        model.RewardRecord = reader.GetString(9);
                        model.Remark = reader.GetString(10);
                        model.Sort = reader.GetInt32(11);
                        model.IsDisable = reader.GetBoolean(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
