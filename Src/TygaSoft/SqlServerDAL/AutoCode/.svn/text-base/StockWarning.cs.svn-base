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
    public partial class StockWarning : IStockWarning
    {
        #region IStockWarning Member

        public int Insert(StockWarningInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into StockWarning (UserId,ZoneId,StockLocationId,Coded,ZoneProperty,StockLocationProperty,StockAmount,OverdueDay,MinQty,MaxQty,Remark,Sort,IsDisable,LastUpdatedDate)
			            values
						(@UserId,@ZoneId,@StockLocationId,@Coded,@ZoneProperty,@StockLocationProperty,@StockAmount,@OverdueDay,@MinQty,@MaxQty,@Remark,@Sort,@IsDisable,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ZoneId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Coded",SqlDbType.VarChar,36),
                                        new SqlParameter("@ZoneProperty",SqlDbType.NVarChar,20),
                                        new SqlParameter("@StockLocationProperty",SqlDbType.NVarChar,20),
                                        new SqlParameter("@StockAmount",SqlDbType.Decimal),
                                        new SqlParameter("@OverdueDay",SqlDbType.Int),
                                        new SqlParameter("@MinQty",SqlDbType.Float),
                                        new SqlParameter("@MaxQty",SqlDbType.Float),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@IsDisable",SqlDbType.Bit),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.ZoneId;
            parms[2].Value = model.StockLocationId;
            parms[3].Value = model.Coded;
            parms[4].Value = model.ZoneProperty;
            parms[5].Value = model.StockLocationProperty;
            parms[6].Value = model.StockAmount;
            parms[7].Value = model.OverdueDay;
            parms[8].Value = model.MinQty;
            parms[9].Value = model.MaxQty;
            parms[10].Value = model.Remark;
            parms[11].Value = model.Sort;
            parms[12].Value = model.IsDisable;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(StockWarningInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into StockWarning (Id,UserId,ZoneId,StockLocationId,Coded,ZoneProperty,StockLocationProperty,StockAmount,OverdueDay,MinQty,MaxQty,Remark,Sort,IsDisable,LastUpdatedDate)
			            values
						(@Id,@UserId,@ZoneId,@StockLocationId,@Coded,@ZoneProperty,@StockLocationProperty,@StockAmount,@OverdueDay,@MinQty,@MaxQty,@Remark,@Sort,@IsDisable,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ZoneId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Coded",SqlDbType.VarChar,36),
                                        new SqlParameter("@ZoneProperty",SqlDbType.NVarChar,20),
                                        new SqlParameter("@StockLocationProperty",SqlDbType.NVarChar,20),
                                        new SqlParameter("@StockAmount",SqlDbType.Decimal),
                                        new SqlParameter("@OverdueDay",SqlDbType.Int),
                                        new SqlParameter("@MinQty",SqlDbType.Float),
                                        new SqlParameter("@MaxQty",SqlDbType.Float),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@IsDisable",SqlDbType.Bit),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ZoneId;
            parms[3].Value = model.StockLocationId;
            parms[4].Value = model.Coded;
            parms[5].Value = model.ZoneProperty;
            parms[6].Value = model.StockLocationProperty;
            parms[7].Value = model.StockAmount;
            parms[8].Value = model.OverdueDay;
            parms[9].Value = model.MinQty;
            parms[10].Value = model.MaxQty;
            parms[11].Value = model.Remark;
            parms[12].Value = model.Sort;
            parms[13].Value = model.IsDisable;
            parms[14].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(StockWarningInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update StockWarning set UserId = @UserId,ZoneId = @ZoneId,StockLocationId = @StockLocationId,Coded = @Coded,ZoneProperty = @ZoneProperty,StockLocationProperty = @StockLocationProperty,StockAmount = @StockAmount,OverdueDay = @OverdueDay,MinQty = @MinQty,MaxQty = @MaxQty,Remark = @Remark,Sort = @Sort,IsDisable = @IsDisable,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ZoneId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Coded",SqlDbType.VarChar,36),
                                        new SqlParameter("@ZoneProperty",SqlDbType.NVarChar,20),
                                        new SqlParameter("@StockLocationProperty",SqlDbType.NVarChar,20),
                                        new SqlParameter("@StockAmount",SqlDbType.Decimal),
                                        new SqlParameter("@OverdueDay",SqlDbType.Int),
                                        new SqlParameter("@MinQty",SqlDbType.Float),
                                        new SqlParameter("@MaxQty",SqlDbType.Float),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@IsDisable",SqlDbType.Bit),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.ZoneId;
            parms[3].Value = model.StockLocationId;
            parms[4].Value = model.Coded;
            parms[5].Value = model.ZoneProperty;
            parms[6].Value = model.StockLocationProperty;
            parms[7].Value = model.StockAmount;
            parms[8].Value = model.OverdueDay;
            parms[9].Value = model.MinQty;
            parms[10].Value = model.MaxQty;
            parms[11].Value = model.Remark;
            parms[12].Value = model.Sort;
            parms[13].Value = model.IsDisable;
            parms[14].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from StockWarning where Id = @Id ");
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
                sb.Append(@"delete from StockWarning where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public StockWarningInfo GetModel(Guid id)
        {
            StockWarningInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,ZoneId,StockLocationId,Coded,ZoneProperty,StockLocationProperty,StockAmount,OverdueDay,MinQty,MaxQty,Remark,Sort,IsDisable,LastUpdatedDate 
			            from StockWarning
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
                        model = new StockWarningInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ZoneId = reader.GetGuid(2);
                        model.StockLocationId = reader.GetGuid(3);
                        model.Coded = reader.GetString(4);
                        model.ZoneProperty = reader.GetString(5);
                        model.StockLocationProperty = reader.GetString(6);
                        model.StockAmount = reader.GetDecimal(7);
                        model.OverdueDay = reader.GetInt32(8);
                        model.MinQty = reader.GetDouble(9);
                        model.MaxQty = reader.GetDouble(10);
                        model.Remark = reader.GetString(11);
                        model.Sort = reader.GetInt32(12);
                        model.IsDisable = reader.GetBoolean(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);
                    }
                }
            }

            return model;
        }

        public IList<StockWarningInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from StockWarning ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StockWarningInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          Id,UserId,ZoneId,StockLocationId,Coded,ZoneProperty,StockLocationProperty,StockAmount,OverdueDay,MinQty,MaxQty,Remark,Sort,IsDisable,LastUpdatedDate
					  from StockWarning ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StockWarningInfo> list = new List<StockWarningInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockWarningInfo model = new StockWarningInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ZoneId = reader.GetGuid(3);
                        model.StockLocationId = reader.GetGuid(4);
                        model.Coded = reader.GetString(5);
                        model.ZoneProperty = reader.GetString(6);
                        model.StockLocationProperty = reader.GetString(7);
                        model.StockAmount = reader.GetDecimal(8);
                        model.OverdueDay = reader.GetInt32(9);
                        model.MinQty = reader.GetDouble(10);
                        model.MaxQty = reader.GetDouble(11);
                        model.Remark = reader.GetString(12);
                        model.Sort = reader.GetInt32(13);
                        model.IsDisable = reader.GetBoolean(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockWarningInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           Id,UserId,ZoneId,StockLocationId,Coded,ZoneProperty,StockLocationProperty,StockAmount,OverdueDay,MinQty,MaxQty,Remark,Sort,IsDisable,LastUpdatedDate
					   from StockWarning ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StockWarningInfo> list = new List<StockWarningInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockWarningInfo model = new StockWarningInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.ZoneId = reader.GetGuid(3);
                        model.StockLocationId = reader.GetGuid(4);
                        model.Coded = reader.GetString(5);
                        model.ZoneProperty = reader.GetString(6);
                        model.StockLocationProperty = reader.GetString(7);
                        model.StockAmount = reader.GetDecimal(8);
                        model.OverdueDay = reader.GetInt32(9);
                        model.MinQty = reader.GetDouble(10);
                        model.MaxQty = reader.GetDouble(11);
                        model.Remark = reader.GetString(12);
                        model.Sort = reader.GetInt32(13);
                        model.IsDisable = reader.GetBoolean(14);
                        model.LastUpdatedDate = reader.GetDateTime(15);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockWarningInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,ZoneId,StockLocationId,Coded,ZoneProperty,StockLocationProperty,StockAmount,OverdueDay,MinQty,MaxQty,Remark,Sort,IsDisable,LastUpdatedDate
                        from StockWarning ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<StockWarningInfo> list = new List<StockWarningInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockWarningInfo model = new StockWarningInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ZoneId = reader.GetGuid(2);
                        model.StockLocationId = reader.GetGuid(3);
                        model.Coded = reader.GetString(4);
                        model.ZoneProperty = reader.GetString(5);
                        model.StockLocationProperty = reader.GetString(6);
                        model.StockAmount = reader.GetDecimal(7);
                        model.OverdueDay = reader.GetInt32(8);
                        model.MinQty = reader.GetDouble(9);
                        model.MaxQty = reader.GetDouble(10);
                        model.Remark = reader.GetString(11);
                        model.Sort = reader.GetInt32(12);
                        model.IsDisable = reader.GetBoolean(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockWarningInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,ZoneId,StockLocationId,Coded,ZoneProperty,StockLocationProperty,StockAmount,OverdueDay,MinQty,MaxQty,Remark,Sort,IsDisable,LastUpdatedDate 
			            from StockWarning
					    order by Sort ");

            IList<StockWarningInfo> list = new List<StockWarningInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockWarningInfo model = new StockWarningInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.ZoneId = reader.GetGuid(2);
                        model.StockLocationId = reader.GetGuid(3);
                        model.Coded = reader.GetString(4);
                        model.ZoneProperty = reader.GetString(5);
                        model.StockLocationProperty = reader.GetString(6);
                        model.StockAmount = reader.GetDecimal(7);
                        model.OverdueDay = reader.GetInt32(8);
                        model.MinQty = reader.GetDouble(9);
                        model.MaxQty = reader.GetDouble(10);
                        model.Remark = reader.GetString(11);
                        model.Sort = reader.GetInt32(12);
                        model.IsDisable = reader.GetBoolean(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
