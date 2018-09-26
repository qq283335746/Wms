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
    public partial class Package
    {
        #region IPackage Member

        public PackageInfo GetModelByJoin(object Id)
        {
            PackageInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 pa.Id,pa.UserId,pa.CustomerId,pa.ProductId,pa.PackageCode,pa.TotalPiece,pa.TotalInsidePackage,pa.TotalBox,pa.TotalTray,pa.UnitXml,pa.Remark,pa.LastUpdatedDate 
			            ,c.Coded CustomerCode,c.Named CustomerName
                        ,p.ProductCode,p.ProductName
                        from Package pa
                        left join Customer c on c.Id = pa.CustomerId
                        left join Product p on p.Id = pa.ProductId
						where pa.Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new PackageInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.ProductId = reader.GetGuid(3);
                        model.PackageCode = reader.GetString(4);
                        model.TotalPiece = Math.Round(reader.GetDouble(5), 5);
                        model.TotalInsidePackage = Math.Round(reader.GetDouble(6), 5);
                        model.TotalBox = Math.Round(reader.GetDouble(7),5);
                        model.TotalTray = Math.Round(reader.GetDouble(8), 5);
                        model.UnitXml = reader.GetString(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        model.CustomerCode = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.CustomerName = reader.IsDBNull(13) ? "" : reader.GetString(13);

                        model.ProductCode = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.ProductName = reader.IsDBNull(15) ? "" : reader.GetString(15);
                    }
                }
            }

            return model;
        }

        public IList<PackageInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from Package pa 
                        left join Customer c on c.Id = pa.CustomerId
                        left join Product p on p.Id = pa.ProductId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PackageInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by pa.LastUpdatedDate desc) as RowNumber,
			          pa.Id,pa.UserId,pa.CustomerId,pa.ProductId,pa.PackageCode,pa.TotalPiece,pa.TotalInsidePackage,pa.TotalBox,pa.TotalTray,
                      pa.UnitXml,pa.Remark,pa.LastUpdatedDate
                      ,c.Coded CustomerCode,c.Named CustomerName
                      ,p.ProductCode,p.ProductName
					  from Package pa
                      left join Customer c on c.Id = pa.CustomerId
                      left join Product p on p.Id = pa.ProductId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PackageInfo> list = new List<PackageInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PackageInfo model = new PackageInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.ProductId = reader.GetGuid(4);
                        model.PackageCode = reader.GetString(5);
                        model.TotalPiece = reader.GetDouble(6);
                        model.TotalInsidePackage = reader.GetDouble(7);
                        model.TotalBox = reader.GetDouble(8);
                        model.TotalTray = reader.GetDouble(9);
                        model.UnitXml = reader.GetString(10);
                        model.Remark = reader.GetString(11);
                        model.LastUpdatedDate = reader.GetDateTime(12);

                        model.CustomerCode = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.CustomerName = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.ProductCode = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.ProductName = reader.IsDBNull(16) ? "" : reader.GetString(16);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
