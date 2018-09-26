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
    public partial class Vehicle
    {
        #region IVehicle Member

        public IList<VehicleInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Vehicle v 
                        left join SitePicture sp1 on sp1.Id = v.LicPic
                        left join SitePicture sp2 on sp2.Id = v.DriverIDPicture
                        ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<VehicleInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by v.Sort) as RowNumber,
			          v.Id,v.UserId,v.VehicleID,v.VehicleModel,v.Licence,v.LicPic,v.OffenceRecord,v.DriverID,v.DriverIDPicture,v.RewardRecord,v.Remark,v.Sort,v.IsDisable,v.LastUpdatedDate
					  ,sp1.FileName,sp1.FileDirectory,sp2.FileName FileName2,sp2.FileDirectory FileDirectory2
                      from Vehicle v 
                      left join SitePicture sp1 on sp1.Id = v.LicPic
                      left join SitePicture sp2 on sp2.Id = v.DriverIDPicture
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<VehicleInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VehicleInfo model = new VehicleInfo();
                        model.Id = reader.GetGuid(1);
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

                        model.LicPicUrl = reader.IsDBNull(15) ? "/wms/Images/nopic.gif" : string.Format("/Wms{0}{1}", reader.GetString(16), reader.GetString(15));
                        model.DriverIDPictureUrl = reader.IsDBNull(17) ? "/wms/Images/nopic.gif" : string.Format("/Wms{0}{1}", reader.GetString(18), reader.GetString(17));

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
