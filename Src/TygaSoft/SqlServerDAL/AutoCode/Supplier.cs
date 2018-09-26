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
    public partial class Supplier : ISupplier
    {
        #region ISupplier Member

        public int Insert(SupplierInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into Supplier (UserId,SupplierCode,SupplierName,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate)
			            values
						(@UserId,@SupplierCode,@SupplierName,@ShortName,@ContactMan,@Email,@Phone,@TelPhone,@Fax,@Postcode,@Address,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@SupplierCode",SqlDbType.VarChar,30),
new SqlParameter("@SupplierName",SqlDbType.NVarChar,50),
new SqlParameter("@ShortName",SqlDbType.NVarChar,20),
new SqlParameter("@ContactMan",SqlDbType.NVarChar,20),
new SqlParameter("@Email",SqlDbType.NVarChar,50),
new SqlParameter("@Phone",SqlDbType.VarChar,20),
new SqlParameter("@TelPhone",SqlDbType.VarChar,20),
new SqlParameter("@Fax",SqlDbType.VarChar,20),
new SqlParameter("@Postcode",SqlDbType.VarChar,10),
new SqlParameter("@Address",SqlDbType.NVarChar,50),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.SupplierCode;
            parms[2].Value = model.SupplierName;
            parms[3].Value = model.ShortName;
            parms[4].Value = model.ContactMan;
            parms[5].Value = model.Email;
            parms[6].Value = model.Phone;
            parms[7].Value = model.TelPhone;
            parms[8].Value = model.Fax;
            parms[9].Value = model.Postcode;
            parms[10].Value = model.Address;
            parms[11].Value = model.Remark;
            parms[12].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(SupplierInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update Supplier set UserId = @UserId,SupplierCode = @SupplierCode,SupplierName = @SupplierName,ShortName = @ShortName,ContactMan = @ContactMan,Email = @Email,Phone = @Phone,TelPhone = @TelPhone,Fax = @Fax,Postcode = @Postcode,Address = @Address,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@SupplierCode",SqlDbType.VarChar,30),
new SqlParameter("@SupplierName",SqlDbType.NVarChar,50),
new SqlParameter("@ShortName",SqlDbType.NVarChar,20),
new SqlParameter("@ContactMan",SqlDbType.NVarChar,20),
new SqlParameter("@Email",SqlDbType.NVarChar,50),
new SqlParameter("@Phone",SqlDbType.VarChar,20),
new SqlParameter("@TelPhone",SqlDbType.VarChar,20),
new SqlParameter("@Fax",SqlDbType.VarChar,20),
new SqlParameter("@Postcode",SqlDbType.VarChar,10),
new SqlParameter("@Address",SqlDbType.NVarChar,50),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.SupplierCode;
            parms[3].Value = model.SupplierName;
            parms[4].Value = model.ShortName;
            parms[5].Value = model.ContactMan;
            parms[6].Value = model.Email;
            parms[7].Value = model.Phone;
            parms[8].Value = model.TelPhone;
            parms[9].Value = model.Fax;
            parms[10].Value = model.Postcode;
            parms[11].Value = model.Address;
            parms[12].Value = model.Remark;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object Id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Supplier where Id = @Id");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parm);
        }

        public bool DeleteBatch(IList<object> list)
        {
            bool result = false;
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from Supplier where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.WmsDbConnString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int effect = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null);
                        tran.Commit();
                        if (effect > 0) result = true;
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }
            return result;
        }

        public SupplierInfo GetModel(object Id)
        {
            SupplierInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,SupplierCode,SupplierName,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate 
			            from Supplier
						where Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new SupplierInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.SupplierCode = reader.GetString(2);
                        model.SupplierName = reader.GetString(3);
                        model.ShortName = reader.GetString(4);
                        model.ContactMan = reader.GetString(5);
                        model.Email = reader.GetString(6);
                        model.Phone = reader.GetString(7);
                        model.TelPhone = reader.GetString(8);
                        model.Fax = reader.GetString(9);
                        model.Postcode = reader.GetString(10);
                        model.Address = reader.GetString(11);
                        model.Remark = reader.GetString(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);
                    }
                }
            }

            return model;
        }

        public IList<SupplierInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from Supplier ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<SupplierInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,SupplierCode,SupplierName,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate
					  from Supplier ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SupplierInfo> list = new List<SupplierInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SupplierInfo model = new SupplierInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.SupplierCode = reader.GetString(3);
                        model.SupplierName = reader.GetString(4);
                        model.ShortName = reader.GetString(5);
                        model.ContactMan = reader.GetString(6);
                        model.Email = reader.GetString(7);
                        model.Phone = reader.GetString(8);
                        model.TelPhone = reader.GetString(9);
                        model.Fax = reader.GetString(10);
                        model.Postcode = reader.GetString(11);
                        model.Address = reader.GetString(12);
                        model.Remark = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SupplierInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,SupplierCode,SupplierName,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate
					   from Supplier ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<SupplierInfo> list = new List<SupplierInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SupplierInfo model = new SupplierInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.SupplierCode = reader.GetString(3);
                        model.SupplierName = reader.GetString(4);
                        model.ShortName = reader.GetString(5);
                        model.ContactMan = reader.GetString(6);
                        model.Email = reader.GetString(7);
                        model.Phone = reader.GetString(8);
                        model.TelPhone = reader.GetString(9);
                        model.Fax = reader.GetString(10);
                        model.Postcode = reader.GetString(11);
                        model.Address = reader.GetString(12);
                        model.Remark = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SupplierInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,UserId,SupplierCode,SupplierName,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate
                        from Supplier ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<SupplierInfo> list = new List<SupplierInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SupplierInfo model = new SupplierInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.SupplierCode = reader.GetString(2);
                        model.SupplierName = reader.GetString(3);
                        model.ShortName = reader.GetString(4);
                        model.ContactMan = reader.GetString(5);
                        model.Email = reader.GetString(6);
                        model.Phone = reader.GetString(7);
                        model.TelPhone = reader.GetString(8);
                        model.Fax = reader.GetString(9);
                        model.Postcode = reader.GetString(10);
                        model.Address = reader.GetString(11);
                        model.Remark = reader.GetString(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<SupplierInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,UserId,SupplierCode,SupplierName,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate 
			            from Supplier
					    order by LastUpdatedDate desc ");

            IList<SupplierInfo> list = new List<SupplierInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SupplierInfo model = new SupplierInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.SupplierCode = reader.GetString(2);
                        model.SupplierName = reader.GetString(3);
                        model.ShortName = reader.GetString(4);
                        model.ContactMan = reader.GetString(5);
                        model.Email = reader.GetString(6);
                        model.Phone = reader.GetString(7);
                        model.TelPhone = reader.GetString(8);
                        model.Fax = reader.GetString(9);
                        model.Postcode = reader.GetString(10);
                        model.Address = reader.GetString(11);
                        model.Remark = reader.GetString(12);
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
