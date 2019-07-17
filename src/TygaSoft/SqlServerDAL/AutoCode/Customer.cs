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
    public partial class Customer : ICustomer
    {
        #region ICustomer Member

        public int Insert(CustomerInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Customer (UserId,Coded,Named,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate)
			            values
						(@UserId,@Coded,@Named,@ShortName,@ContactMan,@Email,@Phone,@TelPhone,@Fax,@Postcode,@Address,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,30),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
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
            parms[1].Value = model.Coded;
            parms[2].Value = model.Named;
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

        public int InsertByOutput(CustomerInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Customer (Id,UserId,Coded,Named,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate)
			            values
						(@Id,@UserId,@Coded,@Named,@ShortName,@ContactMan,@Email,@Phone,@TelPhone,@Fax,@Postcode,@Address,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,30),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
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
            parms[2].Value = model.Coded;
            parms[3].Value = model.Named;
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

        public int Update(CustomerInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Customer set UserId = @UserId,Coded = @Coded,Named = @Named,ShortName = @ShortName,ContactMan = @ContactMan,Email = @Email,Phone = @Phone,TelPhone = @TelPhone,Fax = @Fax,Postcode = @Postcode,Address = @Address,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,30),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
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
            parms[2].Value = model.Coded;
            parms[3].Value = model.Named;
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

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Customer where Id = @Id ");
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
                sb.Append(@"delete from Customer where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public CustomerInfo GetModel(Guid id)
        {
            CustomerInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,Coded,Named,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate 
			            from Customer
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
                        model = new CustomerInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
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

        public IList<CustomerInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Customer ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<CustomerInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,Coded,Named,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate
					  from Customer ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<CustomerInfo> list = new List<CustomerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CustomerInfo model = new CustomerInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
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

        public IList<CustomerInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,Coded,Named,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate
					   from Customer ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<CustomerInfo> list = new List<CustomerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CustomerInfo model = new CustomerInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
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

        public IList<CustomerInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,Coded,Named,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate
                        from Customer ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<CustomerInfo> list = new List<CustomerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CustomerInfo model = new CustomerInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
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

        public IList<CustomerInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,Coded,Named,ShortName,ContactMan,Email,Phone,TelPhone,Fax,Postcode,Address,Remark,LastUpdatedDate 
			            from Customer
					    order by LastUpdatedDate desc ");

            IList<CustomerInfo> list = new List<CustomerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CustomerInfo model = new CustomerInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
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
