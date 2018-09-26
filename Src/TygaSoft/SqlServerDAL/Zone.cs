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
    public partial class Zone
    {
        #region IZone Member

        public IList<ZoneInfo> GetListInStockLocation(string slIds)
        {
            var list = new List<ZoneInfo>();

            var sqlIn = new StringBuilder(1000);
            var Ids = slIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in Ids)
            {
                sqlIn.AppendFormat("'{0}',", item);
            }

            var cmdText = string.Format(@"select z.Id,z.ZoneCode,z.ZoneName from 
                            (select sl.ZoneId from StockLocation sl where sl.Id in({0})group by sl.ZoneId) objT
                            join Zone z on z.Id = ZoneId ",sqlIn.ToString().Trim(','));
            
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ZoneInfo model = new ZoneInfo();
                        model.Id = reader.GetGuid(0);
                        model.ZoneCode = reader.GetString(1);
                        model.ZoneName = reader.GetString(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
