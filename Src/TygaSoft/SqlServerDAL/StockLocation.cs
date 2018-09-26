using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class StockLocation
    {
        #region IStockLocation Member

        public IList<StockLocationInfo> GetListInZoneIds(string zoneIds)
        {
            var list = new List<StockLocationInfo>();

            var sqlIn = new StringBuilder(1000);
            var Ids = zoneIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in Ids)
            {
                sqlIn.AppendFormat("'{0}',", item);
            }
            var cmdText = string.Format(@"select sl.Id,sl.Code,sl.Named from 
                                        (select z.Id ZoneId from Zone z where 
                                         z.Id in({0}) group by z.Id
                                        ) objT
                                        join StockLocation sl on sl.ZoneId = objT.ZoneId ", sqlIn.ToString().Trim(','));

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new StockLocationInfo();
                        model.Id = reader.GetGuid(0);
                        model.ZoneCode = reader.GetString(1);
                        model.ZoneName = reader.GetString(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public string GetStockLocationTextInIds(string Ids)
        {
            var items = Ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder(1000);
            foreach (var item in items)
            {
                sb.AppendFormat("'{0}',", item);
            }

            var cmdText = "select Id,Code,Named from StockLocation where Id in (" + sb.ToString().Trim(',') + ") ";
            var sAppend = new StringBuilder(1000);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sAppend.AppendFormat("{0},", reader.GetString(1));
                    }
                }
            }

            return sAppend.ToString().Trim(',');
        }

        public StockLocationInfo GetModelForTemp()
        {
            StockLocationInfo model = null;
            var cmdText = @"select s.Id,s.ZoneId,s.Code,s.Named 
                            from StockLocation s
                            join Zone z on z.Id = s.ZoneId
                            where z.ZoneName = '暂存区'";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, null))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new StockLocationInfo();
                        model.Id = reader.GetGuid(0);
                        model.ZoneId = reader.GetGuid(1);
                        model.Code = reader.GetString(2);
                        model.Named = reader.GetString(3);
                    }
                }
            }

            if (model == null) throw new ArgumentException("未找到暂存区的任何库位");

            return model;
        }

        public StockLocationInfo GetModelByJoin(object Id)
        {
            StockLocationInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 sl.Id,sl.UserId,sl.ZoneId,sl.Code,sl.Named,sl.Width,sl.Wide,sl.High,sl.Volume,sl.Cubage,sl.Row,sl.Layer,sl.Col,sl.Passway,sl.Xc,
                        sl.Yc,sl.Zc,sl.Orientation,sl.StockLocationType,sl.StackLimit,sl.GroundTrayQty,sl.StockLocationDeal,sl.CarryWeight,sl.UseStatus,sl.Remark,
                        sl.LastUpdatedDate 
			            ,slc.RouteSeq,slc.IsMixPlace,slc.IsBatchNum,slc.IsLoseId,slc.Status,slc.Warehouse,slc.LevelNum,slc.CtrType,slc.ABC,slc.InsertTaskSeq,
                        slc.PickArea,slc.PickMethod,slc.InventoryGroupId 
                        ,z.ZoneCode,z.ZoneName
                        from StockLocation sl
                        left join StockLocationCtr slc on slc.StockLocationId = sl.Id
                        left join Zone z on z.Id = sl.ZoneId
						where sl.Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new StockLocationInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ZoneId = reader.GetGuid(2);
                        model.Code = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.Width = reader.GetDouble(5);
                        model.Wide = reader.GetDouble(6);
                        model.High = reader.GetDouble(7);
                        model.Volume = reader.GetDouble(8);
                        model.Cubage = reader.GetDouble(9);
                        model.Row = reader.GetDouble(10);
                        model.Layer = reader.GetDouble(11);
                        model.Col = reader.GetDouble(12);
                        model.Passway = reader.GetDouble(13);
                        model.Xc = reader.GetDouble(14);
                        model.Yc = reader.GetDouble(15);
                        model.Zc = reader.GetDouble(16);
                        model.Orientation = reader.GetDouble(17);
                        model.StockLocationType = reader.GetString(18);
                        model.StackLimit = reader.GetDouble(19);
                        model.GroundTrayQty = reader.GetDouble(20);
                        model.StockLocationDeal = reader.GetString(21);
                        model.CarryWeight = reader.GetDouble(22);
                        model.UseStatus = reader.GetString(23);
                        model.Remark = reader.GetString(24);
                        model.LastUpdatedDate = reader.GetDateTime(25);

                        model.RouteSeq = reader.GetString(26);
                        model.IsMixPlace = reader.GetBoolean(27);
                        model.IsBatchNum = reader.GetBoolean(28);
                        model.IsLoseId = reader.GetBoolean(29);
                        model.Status = reader.GetString(30);
                        model.Warehouse = reader.GetString(31);
                        model.LevelNum = reader.GetDouble(32);
                        model.CtrType = reader.GetString(33);
                        model.ABC = reader.GetString(34);
                        model.InsertTaskSeq = reader.GetDouble(35);
                        model.PickArea = reader.GetString(36);
                        model.PickMethod = reader.GetString(37);
                        model.InventoryGroupId = reader.GetDouble(38);

                        model.ZoneCode = reader.IsDBNull(39) ? "" : reader.GetString(39);
                        model.ZoneName = reader.IsDBNull(40) ? "" : reader.GetString(40);
                    }
                }
            }

            return model;
        }

        public List<StockLocationInfo> GetListByBest()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select sl.Id,sl.UserId,sl.ZoneId,sl.Code,sl.Named,sl.Width,sl.Wide,sl.High,sl.Volume,sl.Cubage,sl.Row,sl.Layer,
                        sl.Col,sl.Passway,sl.Xc,sl.Yc,sl.Zc,sl.Orientation,sl.StockLocationType,sl.StackLimit,sl.GroundTrayQty,
                        sl.StockLocationDeal,sl.CarryWeight,sl.UseStatus,sl.Remark,sl.LastUpdatedDate
                        ,z.ZoneCode,z.ZoneName
                        from StockLocation sl
                        left join Zone z on z.Id = sl.ZoneId
                        where z.ZoneCode <> 'BufferZ' ");

            var list = new List<StockLocationInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), null))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockLocationInfo model = new StockLocationInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ZoneId = reader.GetGuid(2);
                        model.Code = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.Width = reader.GetDouble(5);
                        model.Wide = reader.GetDouble(6);
                        model.High = reader.GetDouble(7);
                        model.Volume = reader.GetDouble(8);
                        model.Cubage = reader.GetDouble(9);
                        model.Row = reader.GetDouble(10);
                        model.Layer = reader.GetDouble(11);
                        model.Col = reader.GetDouble(12);
                        model.Passway = reader.GetDouble(13);
                        model.Xc = reader.GetDouble(14);
                        model.Yc = reader.GetDouble(15);
                        model.Zc = reader.GetDouble(16);
                        model.Orientation = reader.GetDouble(17);
                        model.StockLocationType = reader.GetString(18);
                        model.StackLimit = reader.GetDouble(19);
                        model.GroundTrayQty = reader.GetDouble(20);
                        model.StockLocationDeal = reader.GetString(21);
                        model.CarryWeight = reader.GetDouble(22);
                        model.UseStatus = reader.GetString(23);
                        model.Remark = reader.GetString(24);
                        model.LastUpdatedDate = reader.GetDateTime(25);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockLocationInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from StockLocation sl
                        left join StockLocationCtr slc on slc.StockLocationId = sl.Id
                        left join Zone z on z.Id = sl.ZoneId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StockLocationInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by sl.LastUpdatedDate desc) as RowNumber,
			          sl.Id,sl.UserId,sl.ZoneId,sl.Code,sl.Named,sl.Width,sl.Wide,sl.High,sl.Volume,sl.Cubage,sl.Row,sl.Layer,sl.Col,sl.Passway,sl.Xc,sl.Yc,sl.Zc,
                      sl.Orientation,sl.StockLocationType,sl.StackLimit,sl.GroundTrayQty,sl.StockLocationDeal,sl.CarryWeight,sl.UseStatus,sl.Remark,sl.LastUpdatedDate
                      ,slc.RouteSeq,slc.IsMixPlace,slc.IsBatchNum,slc.IsLoseId,slc.Status,slc.Warehouse,slc.LevelNum,slc.CtrType,slc.ABC,slc.InsertTaskSeq,
                      slc.PickArea,slc.PickMethod,slc.InventoryGroupId 
                      ,z.ZoneCode,z.ZoneName
                      from StockLocation sl
                      left join StockLocationCtr slc on slc.StockLocationId = sl.Id
                      left join Zone z on z.Id = sl.ZoneId
                     ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StockLocationInfo> list = new List<StockLocationInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockLocationInfo model = new StockLocationInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ZoneId = reader.GetGuid(3);
                        model.Code = reader.GetString(4);
                        model.Named = reader.GetString(5);
                        model.Width = reader.GetDouble(6);
                        model.Wide = reader.GetDouble(7);
                        model.High = reader.GetDouble(8);
                        model.Volume = reader.GetDouble(9);
                        model.Cubage = reader.GetDouble(10);
                        model.Row = reader.GetDouble(11);
                        model.Layer = reader.GetDouble(12);
                        model.Col = reader.GetDouble(13);
                        model.Passway = reader.GetDouble(14);
                        model.Xc = reader.GetDouble(15);
                        model.Yc = reader.GetDouble(16);
                        model.Zc = reader.GetDouble(17);
                        model.Orientation = reader.GetDouble(18);
                        model.StockLocationType = reader.GetString(19);
                        model.StackLimit = reader.GetDouble(20);
                        model.GroundTrayQty = reader.GetDouble(21);
                        model.StockLocationDeal = reader.GetString(22);
                        model.CarryWeight = reader.GetDouble(23);
                        model.UseStatus = reader.GetString(24);
                        model.Remark = reader.GetString(25);
                        model.LastUpdatedDate = reader.GetDateTime(26);

                        model.RouteSeq = reader.GetString(27);
                        model.IsMixPlace = reader.GetBoolean(28);
                        model.IsBatchNum = reader.GetBoolean(29);
                        model.IsLoseId = reader.GetBoolean(30);
                        model.Status = reader.GetString(31);
                        model.Warehouse = reader.GetString(32);
                        model.LevelNum = reader.GetDouble(33);
                        model.CtrType = reader.GetString(34);
                        model.ABC = reader.GetString(35);
                        model.InsertTaskSeq = reader.GetDouble(36);
                        model.PickArea = reader.GetString(37);
                        model.PickMethod = reader.GetString(38);
                        model.InventoryGroupId = reader.GetDouble(39);

                        model.ZoneCode = reader.IsDBNull(40) ? "" : reader.GetString(40);
                        model.ZoneName = reader.IsDBNull(41) ? "" : reader.GetString(41);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
