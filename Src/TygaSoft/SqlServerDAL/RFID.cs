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
    public partial class RFID
    {
        #region IRFID Member

        public IList<RFIDInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(500);
            sb.Append(@"select count(*) from RFID r 
                        left join Product p on p.ProductCode = r.EPC
                       ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<RFIDInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by r.LastUpdatedDate desc) as RowNumber,
			          r.TID,r.EPC,r.LastUpdatedDate,p.ProductName,p.FullName,p.Specs
					  from RFID r 
                      left join Product p on p.ProductCode = r.EPC
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<RFIDInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new RFIDInfo();
                        model.TID = reader.GetString(1);
                        model.EPC = reader.GetString(2);
                        model.LastUpdatedDate = reader.GetDateTime(3);
                        model.ProductName = reader.IsDBNull(4) ? "" : reader.GetString(4);
                        model.FullName = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        model.Specs = reader.IsDBNull(6) ? "" : reader.GetString(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
