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
    public partial class PandianProduct : IPandianProduct
    {
        #region IPandianProduct Member

        public int Insert(PandianProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into PandianProduct (PandianId,ProductId,CustomerId,UserId,Zones,StockLocations,StayQty,UpdatedZones,UpdatedStockLocations,Qty,FailQty,Status,Remark,LastUpdatedDate)
			            values
						(@PandianId,@ProductId,@CustomerId,@UserId,@Zones,@StockLocations,@StayQty,@UpdatedZones,@UpdatedStockLocations,@Qty,@FailQty,@Status,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Zones",SqlDbType.VarChar,1000),
                                        new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@StayQty",SqlDbType.Float),
                                        new SqlParameter("@UpdatedZones",SqlDbType.VarChar,1000),
                                        new SqlParameter("@UpdatedStockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@FailQty",SqlDbType.Float),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,300),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.PandianId;
            parms[1].Value = model.ProductId;
            parms[2].Value = model.CustomerId;
            parms[3].Value = model.UserId;
            parms[4].Value = model.Zones;
            parms[5].Value = model.StockLocations;
            parms[6].Value = model.StayQty;
            parms[7].Value = model.UpdatedZones;
            parms[8].Value = model.UpdatedStockLocations;
            parms[9].Value = model.Qty;
            parms[10].Value = model.FailQty;
            parms[11].Value = model.Status;
            parms[12].Value = model.Remark;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(PandianProductInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update PandianProduct set UserId = @UserId,Zones = @Zones,StockLocations = @StockLocations,StayQty = @StayQty,UpdatedZones = @UpdatedZones,UpdatedStockLocations = @UpdatedStockLocations,Qty = @Qty,FailQty = @FailQty,Status = @Status,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where PandianId = @PandianId and ProductId = @ProductId and CustomerId = @CustomerId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Zones",SqlDbType.VarChar,1000),
                                        new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@StayQty",SqlDbType.Float),
                                        new SqlParameter("@UpdatedZones",SqlDbType.VarChar,1000),
                                        new SqlParameter("@UpdatedStockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@FailQty",SqlDbType.Float),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,300),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.PandianId;
            parms[1].Value = model.ProductId;
            parms[2].Value = model.CustomerId;
            parms[3].Value = model.UserId;
            parms[4].Value = model.Zones;
            parms[5].Value = model.StockLocations;
            parms[6].Value = model.StayQty;
            parms[7].Value = model.UpdatedZones;
            parms[8].Value = model.UpdatedStockLocations;
            parms[9].Value = model.Qty;
            parms[10].Value = model.FailQty;
            parms[11].Value = model.Status;
            parms[12].Value = model.Remark;
            parms[13].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid pandianId, Guid productId, Guid customerId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from PandianProduct where PandianId = @PandianId and ProductId = @ProductId and CustomerId = @CustomerId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = pandianId;
            parms[1].Value = productId;
            parms[2].Value = customerId;

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
                sb.Append(@"delete from PandianProduct where PandianId = @PandianId" + n + " ;");
                SqlParameter parm = new SqlParameter("@PandianId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public PandianProductInfo GetModel(Guid pandianId, Guid productId, Guid customerId)
        {
            PandianProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 PandianId,ProductId,CustomerId,UserId,Zones,StockLocations,StayQty,UpdatedZones,UpdatedStockLocations,Qty,FailQty,Status,Remark,LastUpdatedDate 
			            from PandianProduct
						where PandianId = @PandianId and ProductId = @ProductId and CustomerId = @CustomerId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = pandianId;
            parms[1].Value = productId;
            parms[2].Value = customerId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new PandianProductInfo();
                        model.PandianId = reader.GetGuid(0);
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.UserId = reader.GetGuid(3);
                        model.Zones = reader.GetString(4);
                        model.StockLocations = reader.GetString(5);
                        model.StayQty = reader.GetDouble(6);
                        model.UpdatedZones = reader.GetString(7);
                        model.UpdatedStockLocations = reader.GetString(8);
                        model.Qty = reader.GetDouble(9);
                        model.FailQty = reader.GetDouble(10);
                        model.Status = reader.GetString(11);
                        model.Remark = reader.GetString(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);
                    }
                }
            }

            return model;
        }

        public IList<PandianProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from PandianProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          PandianId,ProductId,CustomerId,UserId,Zones,StockLocations,StayQty,UpdatedZones,UpdatedStockLocations,Qty,FailQty,Status,Remark,LastUpdatedDate
					  from PandianProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianProductInfo> list = new List<PandianProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianProductInfo model = new PandianProductInfo();
                        model.PandianId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.UserId = reader.GetGuid(4);
                        model.Zones = reader.GetString(5);
                        model.StockLocations = reader.GetString(6);
                        model.StayQty = reader.GetDouble(7);
                        model.UpdatedZones = reader.GetString(8);
                        model.UpdatedStockLocations = reader.GetString(9);
                        model.Qty = reader.GetDouble(10);
                        model.FailQty = reader.GetDouble(11);
                        model.Status = reader.GetString(12);
                        model.Remark = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           PandianId,ProductId,CustomerId,UserId,Zones,StockLocations,StayQty,UpdatedZones,UpdatedStockLocations,Qty,FailQty,Status,Remark,LastUpdatedDate
					   from PandianProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianProductInfo> list = new List<PandianProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianProductInfo model = new PandianProductInfo();
                        model.PandianId = reader.GetGuid(1);
                        model.ProductId = reader.GetGuid(2);
                        model.CustomerId = reader.GetGuid(3);
                        model.UserId = reader.GetGuid(4);
                        model.Zones = reader.GetString(5);
                        model.StockLocations = reader.GetString(6);
                        model.StayQty = reader.GetDouble(7);
                        model.UpdatedZones = reader.GetString(8);
                        model.UpdatedStockLocations = reader.GetString(9);
                        model.Qty = reader.GetDouble(10);
                        model.FailQty = reader.GetDouble(11);
                        model.Status = reader.GetString(12);
                        model.Remark = reader.GetString(13);
                        model.LastUpdatedDate = reader.GetDateTime(14);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select PandianId,ProductId,CustomerId,UserId,Zones,StockLocations,StayQty,UpdatedZones,UpdatedStockLocations,Qty,FailQty,Status,Remark,LastUpdatedDate
                        from PandianProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<PandianProductInfo> list = new List<PandianProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianProductInfo model = new PandianProductInfo();
                        model.PandianId = reader.GetGuid(0);
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.UserId = reader.GetGuid(3);
                        model.Zones = reader.GetString(4);
                        model.StockLocations = reader.GetString(5);
                        model.StayQty = reader.GetDouble(6);
                        model.UpdatedZones = reader.GetString(7);
                        model.UpdatedStockLocations = reader.GetString(8);
                        model.Qty = reader.GetDouble(9);
                        model.FailQty = reader.GetDouble(10);
                        model.Status = reader.GetString(11);
                        model.Remark = reader.GetString(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianProductInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select PandianId,ProductId,CustomerId,UserId,Zones,StockLocations,StayQty,UpdatedZones,UpdatedStockLocations,Qty,FailQty,Status,Remark,LastUpdatedDate 
			            from PandianProduct
					    order by LastUpdatedDate desc ");

            IList<PandianProductInfo> list = new List<PandianProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianProductInfo model = new PandianProductInfo();
                        model.PandianId = reader.GetGuid(0);
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.UserId = reader.GetGuid(3);
                        model.Zones = reader.GetString(4);
                        model.StockLocations = reader.GetString(5);
                        model.StayQty = reader.GetDouble(6);
                        model.UpdatedZones = reader.GetString(7);
                        model.UpdatedStockLocations = reader.GetString(8);
                        model.Qty = reader.GetDouble(9);
                        model.FailQty = reader.GetDouble(10);
                        model.Status = reader.GetString(11);
                        model.Remark = reader.GetString(12);
                        model.LastUpdatedDate = reader.GetDateTime(13);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
