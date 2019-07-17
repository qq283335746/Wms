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
    public partial class InfoneCustomer
    {
        #region IInfoneCustomer Member
        
        public IList<InfoneCustomerInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Customer c ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<InfoneCustomerInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by c.LastUpdatedDate desc) as RowNumber,
			          c.Id,c.UserId,c.Coded,c.Named,c.ShortName,c.InCompany,c.ContactMan,c.ContactPhone,c.TelPhone,c.Fax,c.PostCode,c.Address,c.CompanyAbout,c.RecordDate,c.LastUpdatedDate
					  from Customer c ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<InfoneCustomerInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.InfoneDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new InfoneCustomerInfo();
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.ShortName = reader.GetString(5);
                        model.InCompany = reader.GetString(6);
                        model.ContactMan = reader.GetString(7);
                        model.ContactPhone = reader.GetString(8);
                        model.TelPhone = reader.GetString(9);
                        model.Fax = reader.GetString(10);
                        model.PostCode = reader.GetString(11);
                        model.Address = reader.GetString(12);
                        model.CompanyAbout = reader.GetString(13);
                        model.RecordDate = reader.GetDateTime(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);
                        model.SRecordDate = model.RecordDate.ToString("yyyy-MM-dd");

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
