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
    public partial class StockLocation : IStockLocation
    {
        #region IStockLocation Member

        public int Insert(StockLocationInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into StockLocation (UserId,ZoneId,Code,Named,Width,Wide,High,Volume,Cubage,Row,Layer,Col,Passway,Xc,Yc,Zc,Orientation,StockLocationType,StackLimit,GroundTrayQty,StockLocationDeal,CarryWeight,UseStatus,Remark,LastUpdatedDate)
			            values
						(@UserId,@ZoneId,@Code,@Named,@Width,@Wide,@High,@Volume,@Cubage,@Row,@Layer,@Col,@Passway,@Xc,@Yc,@Zc,@Orientation,@StockLocationType,@StackLimit,@GroundTrayQty,@StockLocationDeal,@CarryWeight,@UseStatus,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ZoneId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Code",SqlDbType.VarChar,30),
                                        new SqlParameter("@Named",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Width",SqlDbType.Float),
                                        new SqlParameter("@Wide",SqlDbType.Float),
                                        new SqlParameter("@High",SqlDbType.Float),
                                        new SqlParameter("@Volume",SqlDbType.Float),
                                        new SqlParameter("@Cubage",SqlDbType.Float),
                                        new SqlParameter("@Row",SqlDbType.Float),
                                        new SqlParameter("@Layer",SqlDbType.Float),
                                        new SqlParameter("@Col",SqlDbType.Float),
                                        new SqlParameter("@Passway",SqlDbType.Float),
                                        new SqlParameter("@Xc",SqlDbType.Float),
                                        new SqlParameter("@Yc",SqlDbType.Float),
                                        new SqlParameter("@Zc",SqlDbType.Float),
                                        new SqlParameter("@Orientation",SqlDbType.Float),
                                        new SqlParameter("@StockLocationType",SqlDbType.NVarChar,20),
                                        new SqlParameter("@StackLimit",SqlDbType.Float),
                                        new SqlParameter("@GroundTrayQty",SqlDbType.Float),
                                        new SqlParameter("@StockLocationDeal",SqlDbType.NVarChar,20),
                                        new SqlParameter("@CarryWeight",SqlDbType.Float),
                                        new SqlParameter("@UseStatus",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.ZoneId;
            parms[2].Value = model.Code;
            parms[3].Value = model.Named;
            parms[4].Value = model.Width;
            parms[5].Value = model.Wide;
            parms[6].Value = model.High;
            parms[7].Value = model.Volume;
            parms[8].Value = model.Cubage;
            parms[9].Value = model.Row;
            parms[10].Value = model.Layer;
            parms[11].Value = model.Col;
            parms[12].Value = model.Passway;
            parms[13].Value = model.Xc;
            parms[14].Value = model.Yc;
            parms[15].Value = model.Zc;
            parms[16].Value = model.Orientation;
            parms[17].Value = model.StockLocationType;
            parms[18].Value = model.StackLimit;
            parms[19].Value = model.GroundTrayQty;
            parms[20].Value = model.StockLocationDeal;
            parms[21].Value = model.CarryWeight;
            parms[22].Value = model.UseStatus;
            parms[23].Value = model.Remark;
            parms[24].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(StockLocationInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into StockLocation (Id,UserId,ZoneId,Code,Named,Width,Wide,High,Volume,Cubage,Row,Layer,Col,Passway,Xc,Yc,Zc,Orientation,StockLocationType,StackLimit,GroundTrayQty,StockLocationDeal,CarryWeight,UseStatus,Remark,LastUpdatedDate)
			            values
						(@Id,@UserId,@ZoneId,@Code,@Named,@Width,@Wide,@High,@Volume,@Cubage,@Row,@Layer,@Col,@Passway,@Xc,@Yc,@Zc,@Orientation,@StockLocationType,@StackLimit,@GroundTrayQty,@StockLocationDeal,@CarryWeight,@UseStatus,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ZoneId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Code",SqlDbType.VarChar,30),
                                        new SqlParameter("@Named",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Width",SqlDbType.Float),
                                        new SqlParameter("@Wide",SqlDbType.Float),
                                        new SqlParameter("@High",SqlDbType.Float),
                                        new SqlParameter("@Volume",SqlDbType.Float),
                                        new SqlParameter("@Cubage",SqlDbType.Float),
                                        new SqlParameter("@Row",SqlDbType.Float),
                                        new SqlParameter("@Layer",SqlDbType.Float),
                                        new SqlParameter("@Col",SqlDbType.Float),
                                        new SqlParameter("@Passway",SqlDbType.Float),
                                        new SqlParameter("@Xc",SqlDbType.Float),
                                        new SqlParameter("@Yc",SqlDbType.Float),
                                        new SqlParameter("@Zc",SqlDbType.Float),
                                        new SqlParameter("@Orientation",SqlDbType.Float),
                                        new SqlParameter("@StockLocationType",SqlDbType.NVarChar,20),
                                        new SqlParameter("@StackLimit",SqlDbType.Float),
                                        new SqlParameter("@GroundTrayQty",SqlDbType.Float),
                                        new SqlParameter("@StockLocationDeal",SqlDbType.NVarChar,20),
                                        new SqlParameter("@CarryWeight",SqlDbType.Float),
                                        new SqlParameter("@UseStatus",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ZoneId;
            parms[3].Value = model.Code;
            parms[4].Value = model.Named;
            parms[5].Value = model.Width;
            parms[6].Value = model.Wide;
            parms[7].Value = model.High;
            parms[8].Value = model.Volume;
            parms[9].Value = model.Cubage;
            parms[10].Value = model.Row;
            parms[11].Value = model.Layer;
            parms[12].Value = model.Col;
            parms[13].Value = model.Passway;
            parms[14].Value = model.Xc;
            parms[15].Value = model.Yc;
            parms[16].Value = model.Zc;
            parms[17].Value = model.Orientation;
            parms[18].Value = model.StockLocationType;
            parms[19].Value = model.StackLimit;
            parms[20].Value = model.GroundTrayQty;
            parms[21].Value = model.StockLocationDeal;
            parms[22].Value = model.CarryWeight;
            parms[23].Value = model.UseStatus;
            parms[24].Value = model.Remark;
            parms[25].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(StockLocationInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update StockLocation set UserId = @UserId,ZoneId = @ZoneId,Code = @Code,Named = @Named,Width = @Width,Wide = @Wide,High = @High,Volume = @Volume,Cubage = @Cubage,Row = @Row,Layer = @Layer,Col = @Col,Passway = @Passway,Xc = @Xc,Yc = @Yc,Zc = @Zc,Orientation = @Orientation,StockLocationType = @StockLocationType,StackLimit = @StackLimit,GroundTrayQty = @GroundTrayQty,StockLocationDeal = @StockLocationDeal,CarryWeight = @CarryWeight,UseStatus = @UseStatus,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ZoneId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@Code",SqlDbType.VarChar,30),
                                    new SqlParameter("@Named",SqlDbType.NVarChar,50),
                                    new SqlParameter("@Width",SqlDbType.Float),
                                    new SqlParameter("@Wide",SqlDbType.Float),
                                    new SqlParameter("@High",SqlDbType.Float),
                                    new SqlParameter("@Volume",SqlDbType.Float),
                                    new SqlParameter("@Cubage",SqlDbType.Float),
                                    new SqlParameter("@Row",SqlDbType.Float),
                                    new SqlParameter("@Layer",SqlDbType.Float),
                                    new SqlParameter("@Col",SqlDbType.Float),
                                    new SqlParameter("@Passway",SqlDbType.Float),
                                    new SqlParameter("@Xc",SqlDbType.Float),
                                    new SqlParameter("@Yc",SqlDbType.Float),
                                    new SqlParameter("@Zc",SqlDbType.Float),
                                    new SqlParameter("@Orientation",SqlDbType.Float),
                                    new SqlParameter("@StockLocationType",SqlDbType.NVarChar,20),
                                    new SqlParameter("@StackLimit",SqlDbType.Float),
                                    new SqlParameter("@GroundTrayQty",SqlDbType.Float),
                                    new SqlParameter("@StockLocationDeal",SqlDbType.NVarChar,20),
                                    new SqlParameter("@CarryWeight",SqlDbType.Float),
                                    new SqlParameter("@UseStatus",SqlDbType.NVarChar,20),
                                    new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                    new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ZoneId;
            parms[3].Value = model.Code;
            parms[4].Value = model.Named;
            parms[5].Value = model.Width;
            parms[6].Value = model.Wide;
            parms[7].Value = model.High;
            parms[8].Value = model.Volume;
            parms[9].Value = model.Cubage;
            parms[10].Value = model.Row;
            parms[11].Value = model.Layer;
            parms[12].Value = model.Col;
            parms[13].Value = model.Passway;
            parms[14].Value = model.Xc;
            parms[15].Value = model.Yc;
            parms[16].Value = model.Zc;
            parms[17].Value = model.Orientation;
            parms[18].Value = model.StockLocationType;
            parms[19].Value = model.StackLimit;
            parms[20].Value = model.GroundTrayQty;
            parms[21].Value = model.StockLocationDeal;
            parms[22].Value = model.CarryWeight;
            parms[23].Value = model.UseStatus;
            parms[24].Value = model.Remark;
            parms[25].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from StockLocation where Id = @Id ");
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
                sb.Append(@"delete from StockLocation where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public StockLocationInfo GetModel(Guid id)
        {
            StockLocationInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,ZoneId,Code,Named,Width,Wide,High,Volume,Cubage,Row,Layer,Col,Passway,Xc,Yc,Zc,Orientation,StockLocationType,StackLimit,GroundTrayQty,StockLocationDeal,CarryWeight,UseStatus,Remark,LastUpdatedDate 
			            from StockLocation
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
                    }
                }
            }

            return model;
        }

        public IList<StockLocationInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from StockLocation ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StockLocationInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UserId,ZoneId,Code,Named,Width,Wide,High,Volume,Cubage,Row,Layer,Col,Passway,Xc,Yc,Zc,Orientation,StockLocationType,StackLimit,GroundTrayQty,StockLocationDeal,CarryWeight,UseStatus,Remark,LastUpdatedDate
					  from StockLocation ");
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockLocationInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UserId,ZoneId,Code,Named,Width,Wide,High,Volume,Cubage,Row,Layer,Col,Passway,Xc,Yc,Zc,Orientation,StockLocationType,StackLimit,GroundTrayQty,StockLocationDeal,CarryWeight,UseStatus,Remark,LastUpdatedDate
					   from StockLocation ");
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

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockLocationInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,ZoneId,Code,Named,Width,Wide,High,Volume,Cubage,Row,Layer,Col,Passway,Xc,Yc,Zc,Orientation,StockLocationType,StackLimit,GroundTrayQty,StockLocationDeal,CarryWeight,UseStatus,Remark,LastUpdatedDate
                        from StockLocation ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<StockLocationInfo> list = new List<StockLocationInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
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

        public IList<StockLocationInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,ZoneId,Code,Named,Width,Wide,High,Volume,Cubage,Row,Layer,Col,Passway,Xc,Yc,Zc,Orientation,StockLocationType,StackLimit,GroundTrayQty,StockLocationDeal,CarryWeight,UseStatus,Remark,LastUpdatedDate 
			            from StockLocation
					    order by LastUpdatedDate desc ");

            IList<StockLocationInfo> list = new List<StockLocationInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
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

        #endregion
    }
}
