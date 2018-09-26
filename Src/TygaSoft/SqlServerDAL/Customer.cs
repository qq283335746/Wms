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
    public partial class Customer
    {
        #region ICustomer Member

        public IList<CustomerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Customer c
                        left join FeatureUser fu on fu.FeatureId = c.Id
                        left join TygaSoftAspnetDb.dbo.SiteMulti sm on sm.Id = fu.FeatureId and fu.TypeName='Customer'
                        left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = fu.UserId and fu.TypeName='Customer'
                        ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<CustomerInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by c.LastUpdatedDate desc) as RowNumber,
			          c.Id,c.UserId,c.Coded CustomerCode,c.Named CustomerName,c.ShortName,c.ContactMan,c.Email,c.Phone,c.TelPhone,c.Fax,c.Postcode,c.Address,c.Remark,c.LastUpdatedDate
                      ,fu.UserId FUserId,sm.SiteLogo,u.UserName
					  from Customer c 
                      left join FeatureUser fu on fu.FeatureId = c.Id
                      left join TygaSoftAspnetDb.dbo.SiteMulti sm on sm.Id = fu.FeatureId and fu.TypeName='Customer'
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = fu.UserId and fu.TypeName='Customer'
                     ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<CustomerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new CustomerInfo();
                        model.Id = reader.GetGuid(1);
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

                        model.FUserId = reader.IsDBNull(15) ? Guid.Empty : reader.GetGuid(15);
                        model.SiteLogo = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.FUserName = reader.IsDBNull(17) ? "" : reader.GetString(17);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
