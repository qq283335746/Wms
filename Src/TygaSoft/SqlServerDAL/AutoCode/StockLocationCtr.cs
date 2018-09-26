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
    public partial class StockLocationCtr : IStockLocationCtr
    {
        #region IStockLocationCtr Member

        public int Insert(StockLocationCtrInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into StockLocationCtr (StockLocationId,RouteSeq,IsMixPlace,IsBatchNum,IsLoseId,Status,Warehouse,LevelNum,CtrType,ABC,InsertTaskSeq,PickArea,PickMethod,InventoryGroupId)
			            values
						(@StockLocationId,@RouteSeq,@IsMixPlace,@IsBatchNum,@IsLoseId,@Status,@Warehouse,@LevelNum,@CtrType,@ABC,@InsertTaskSeq,@PickArea,@PickMethod,@InventoryGroupId)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@RouteSeq",SqlDbType.NVarChar,256),
                                        new SqlParameter("@IsMixPlace",SqlDbType.Bit),
                                        new SqlParameter("@IsBatchNum",SqlDbType.Bit),
                                        new SqlParameter("@IsLoseId",SqlDbType.Bit),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Warehouse",SqlDbType.NVarChar,50),
                                        new SqlParameter("@LevelNum",SqlDbType.Float),
                                        new SqlParameter("@CtrType",SqlDbType.NVarChar,20),
                                        new SqlParameter("@ABC",SqlDbType.Char,1),
                                        new SqlParameter("@InsertTaskSeq",SqlDbType.Float),
                                        new SqlParameter("@PickArea",SqlDbType.NVarChar,20),
                                        new SqlParameter("@PickMethod",SqlDbType.NVarChar,50),
                                        new SqlParameter("@InventoryGroupId",SqlDbType.Float)
                                   };
            parms[0].Value = model.StockLocationId;
            parms[1].Value = model.RouteSeq;
            parms[2].Value = model.IsMixPlace;
            parms[3].Value = model.IsBatchNum;
            parms[4].Value = model.IsLoseId;
            parms[5].Value = model.Status;
            parms[6].Value = model.Warehouse;
            parms[7].Value = model.LevelNum;
            parms[8].Value = model.CtrType;
            parms[9].Value = model.ABC;
            parms[10].Value = model.InsertTaskSeq;
            parms[11].Value = model.PickArea;
            parms[12].Value = model.PickMethod;
            parms[13].Value = model.InventoryGroupId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(StockLocationCtrInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update StockLocationCtr set RouteSeq = @RouteSeq,IsMixPlace = @IsMixPlace,IsBatchNum = @IsBatchNum,IsLoseId = @IsLoseId,Status = @Status,Warehouse = @Warehouse,LevelNum = @LevelNum,CtrType = @CtrType,ABC = @ABC,InsertTaskSeq = @InsertTaskSeq,PickArea = @PickArea,PickMethod = @PickMethod,InventoryGroupId = @InventoryGroupId 
			            where StockLocationId = @StockLocationId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@RouteSeq",SqlDbType.NVarChar,256),
                                        new SqlParameter("@IsMixPlace",SqlDbType.Bit),
                                        new SqlParameter("@IsBatchNum",SqlDbType.Bit),
                                        new SqlParameter("@IsLoseId",SqlDbType.Bit),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Warehouse",SqlDbType.NVarChar,50),
                                        new SqlParameter("@LevelNum",SqlDbType.Float),
                                        new SqlParameter("@CtrType",SqlDbType.NVarChar,20),
                                        new SqlParameter("@ABC",SqlDbType.Char,1),
                                        new SqlParameter("@InsertTaskSeq",SqlDbType.Float),
                                        new SqlParameter("@PickArea",SqlDbType.NVarChar,20),
                                        new SqlParameter("@PickMethod",SqlDbType.NVarChar,50),
                                        new SqlParameter("@InventoryGroupId",SqlDbType.Float)
                                   };
            parms[0].Value = model.StockLocationId;
            parms[1].Value = model.RouteSeq;
            parms[2].Value = model.IsMixPlace;
            parms[3].Value = model.IsBatchNum;
            parms[4].Value = model.IsLoseId;
            parms[5].Value = model.Status;
            parms[6].Value = model.Warehouse;
            parms[7].Value = model.LevelNum;
            parms[8].Value = model.CtrType;
            parms[9].Value = model.ABC;
            parms[10].Value = model.InsertTaskSeq;
            parms[11].Value = model.PickArea;
            parms[12].Value = model.PickMethod;
            parms[13].Value = model.InventoryGroupId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid stockLocationId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from StockLocationCtr where StockLocationId = @StockLocationId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = stockLocationId;

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
                sb.Append(@"delete from StockLocationCtr where StockLocationId = @StockLocationId" + n + " ;");
                SqlParameter parm = new SqlParameter("@StockLocationId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public StockLocationCtrInfo GetModel(Guid stockLocationId)
        {
            StockLocationCtrInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 StockLocationId,RouteSeq,IsMixPlace,IsBatchNum,IsLoseId,Status,Warehouse,LevelNum,CtrType,ABC,InsertTaskSeq,PickArea,PickMethod,InventoryGroupId 
			            from StockLocationCtr
						where StockLocationId = @StockLocationId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = stockLocationId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new StockLocationCtrInfo();
                        model.StockLocationId = reader.GetGuid(0);
                        model.RouteSeq = reader.GetString(1);
                        model.IsMixPlace = reader.GetBoolean(2);
                        model.IsBatchNum = reader.GetBoolean(3);
                        model.IsLoseId = reader.GetBoolean(4);
                        model.Status = reader.GetString(5);
                        model.Warehouse = reader.GetString(6);
                        model.LevelNum = reader.GetDouble(7);
                        model.CtrType = reader.GetString(8);
                        model.ABC = reader.GetString(9);
                        model.InsertTaskSeq = reader.GetDouble(10);
                        model.PickArea = reader.GetString(11);
                        model.PickMethod = reader.GetString(12);
                        model.InventoryGroupId = reader.GetDouble(13);
                    }
                }
            }

            return model;
        }

        public IList<StockLocationCtrInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from StockLocationCtr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StockLocationCtrInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          StockLocationId,RouteSeq,IsMixPlace,IsBatchNum,IsLoseId,Status,Warehouse,LevelNum,CtrType,ABC,InsertTaskSeq,PickArea,PickMethod,InventoryGroupId
					  from StockLocationCtr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StockLocationCtrInfo> list = new List<StockLocationCtrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockLocationCtrInfo model = new StockLocationCtrInfo();
                        model.StockLocationId = reader.GetGuid(1);
                        model.RouteSeq = reader.GetString(2);
                        model.IsMixPlace = reader.GetBoolean(3);
                        model.IsBatchNum = reader.GetBoolean(4);
                        model.IsLoseId = reader.GetBoolean(5);
                        model.Status = reader.GetString(6);
                        model.Warehouse = reader.GetString(7);
                        model.LevelNum = reader.GetDouble(8);
                        model.CtrType = reader.GetString(9);
                        model.ABC = reader.GetString(10);
                        model.InsertTaskSeq = reader.GetDouble(11);
                        model.PickArea = reader.GetString(12);
                        model.PickMethod = reader.GetString(13);
                        model.InventoryGroupId = reader.GetDouble(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockLocationCtrInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           StockLocationId,RouteSeq,IsMixPlace,IsBatchNum,IsLoseId,Status,Warehouse,LevelNum,CtrType,ABC,InsertTaskSeq,PickArea,PickMethod,InventoryGroupId
					   from StockLocationCtr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StockLocationCtrInfo> list = new List<StockLocationCtrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockLocationCtrInfo model = new StockLocationCtrInfo();
                        model.StockLocationId = reader.GetGuid(1);
                        model.RouteSeq = reader.GetString(2);
                        model.IsMixPlace = reader.GetBoolean(3);
                        model.IsBatchNum = reader.GetBoolean(4);
                        model.IsLoseId = reader.GetBoolean(5);
                        model.Status = reader.GetString(6);
                        model.Warehouse = reader.GetString(7);
                        model.LevelNum = reader.GetDouble(8);
                        model.CtrType = reader.GetString(9);
                        model.ABC = reader.GetString(10);
                        model.InsertTaskSeq = reader.GetDouble(11);
                        model.PickArea = reader.GetString(12);
                        model.PickMethod = reader.GetString(13);
                        model.InventoryGroupId = reader.GetDouble(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockLocationCtrInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select StockLocationId,RouteSeq,IsMixPlace,IsBatchNum,IsLoseId,Status,Warehouse,LevelNum,CtrType,ABC,InsertTaskSeq,PickArea,PickMethod,InventoryGroupId
                        from StockLocationCtr ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<StockLocationCtrInfo> list = new List<StockLocationCtrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockLocationCtrInfo model = new StockLocationCtrInfo();
                        model.StockLocationId = reader.GetGuid(0);
                        model.RouteSeq = reader.GetString(1);
                        model.IsMixPlace = reader.GetBoolean(2);
                        model.IsBatchNum = reader.GetBoolean(3);
                        model.IsLoseId = reader.GetBoolean(4);
                        model.Status = reader.GetString(5);
                        model.Warehouse = reader.GetString(6);
                        model.LevelNum = reader.GetDouble(7);
                        model.CtrType = reader.GetString(8);
                        model.ABC = reader.GetString(9);
                        model.InsertTaskSeq = reader.GetDouble(10);
                        model.PickArea = reader.GetString(11);
                        model.PickMethod = reader.GetString(12);
                        model.InventoryGroupId = reader.GetDouble(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockLocationCtrInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select StockLocationId,RouteSeq,IsMixPlace,IsBatchNum,IsLoseId,Status,Warehouse,LevelNum,CtrType,ABC,InsertTaskSeq,PickArea,PickMethod,InventoryGroupId 
			            from StockLocationCtr
					    order by LastUpdatedDate desc ");

            IList<StockLocationCtrInfo> list = new List<StockLocationCtrInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockLocationCtrInfo model = new StockLocationCtrInfo();
                        model.StockLocationId = reader.GetGuid(0);
                        model.RouteSeq = reader.GetString(1);
                        model.IsMixPlace = reader.GetBoolean(2);
                        model.IsBatchNum = reader.GetBoolean(3);
                        model.IsLoseId = reader.GetBoolean(4);
                        model.Status = reader.GetString(5);
                        model.Warehouse = reader.GetString(6);
                        model.LevelNum = reader.GetDouble(7);
                        model.CtrType = reader.GetString(8);
                        model.ABC = reader.GetString(9);
                        model.InsertTaskSeq = reader.GetDouble(10);
                        model.PickArea = reader.GetString(11);
                        model.PickMethod = reader.GetString(12);
                        model.InventoryGroupId = reader.GetDouble(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
