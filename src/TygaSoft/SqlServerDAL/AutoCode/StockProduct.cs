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
    public partial class StockProduct : IStockProduct
    {
        #region IStockProduct Member

        public int Insert(StockProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into StockProduct (ProductId,CustomerId,Qty,UnQty,FreezeQty,StepCode,LastStepName,Status,StockLocations,WarnMsg,LastUpdatedDate)
			            values
						(@ProductId,@CustomerId,@Qty,@UnQty,@FreezeQty,@StepCode,@LastStepName,@Status,@StockLocations,@WarnMsg,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@UnQty",SqlDbType.Float),
                                        new SqlParameter("@FreezeQty",SqlDbType.Float),
                                        new SqlParameter("@StepCode",SqlDbType.VarChar,50),
                                        new SqlParameter("@LastStepName",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@WarnMsg",SqlDbType.NVarChar,256),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.ProductId;
            parms[1].Value = model.CustomerId;
            parms[2].Value = model.Qty;
            parms[3].Value = model.UnQty;
            parms[4].Value = model.FreezeQty;
            parms[5].Value = model.StepCode;
            parms[6].Value = model.LastStepName;
            parms[7].Value = model.Status;
            parms[8].Value = model.StockLocations;
            parms[9].Value = model.WarnMsg;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(StockProductInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update StockProduct set Qty = @Qty,UnQty = @UnQty,FreezeQty = @FreezeQty,StepCode = @StepCode,LastStepName = @LastStepName,Status = @Status,StockLocations = @StockLocations,WarnMsg = @WarnMsg,LastUpdatedDate = @LastUpdatedDate 
			            where ProductId = @ProductId and CustomerId = @CustomerId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Qty",SqlDbType.Float),
                                        new SqlParameter("@UnQty",SqlDbType.Float),
                                        new SqlParameter("@FreezeQty",SqlDbType.Float),
                                        new SqlParameter("@StepCode",SqlDbType.VarChar,50),
                                        new SqlParameter("@LastStepName",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@StockLocations",SqlDbType.VarChar),
                                        new SqlParameter("@WarnMsg",SqlDbType.NVarChar,256),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.ProductId;
            parms[1].Value = model.CustomerId;
            parms[2].Value = model.Qty;
            parms[3].Value = model.UnQty;
            parms[4].Value = model.FreezeQty;
            parms[5].Value = model.StepCode;
            parms[6].Value = model.LastStepName;
            parms[7].Value = model.Status;
            parms[8].Value = model.StockLocations;
            parms[9].Value = model.WarnMsg;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid productId, Guid customerId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from StockProduct where ProductId = @ProductId and CustomerId = @CustomerId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = productId;
            parms[1].Value = customerId;

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
                sb.Append(@"delete from StockProduct where ProductId = @ProductId" + n + " ;");
                SqlParameter parm = new SqlParameter("@ProductId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public StockProductInfo GetModel(Guid productId, Guid customerId)
        {
            StockProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 ProductId,CustomerId,Qty,UnQty,FreezeQty,StepCode,LastStepName,Status,StockLocations,WarnMsg,LastUpdatedDate 
			            from StockProduct
						where ProductId = @ProductId and CustomerId = @CustomerId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@CustomerId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = productId;
            parms[1].Value = customerId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new StockProductInfo();
                        model.ProductId = reader.GetGuid(0);
                        model.CustomerId = reader.GetGuid(1);
                        model.Qty = reader.GetDouble(2);
                        model.UnQty = reader.GetDouble(3);
                        model.FreezeQty = reader.GetDouble(4);
                        model.StepCode = reader.GetString(5);
                        model.LastStepName = reader.GetString(6);
                        model.Status = reader.GetString(7);
                        model.StockLocations = reader.GetString(8);
                        model.WarnMsg = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);
                    }
                }
            }

            return model;
        }

        public IList<StockProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from StockProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StockProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          ProductId,CustomerId,Qty,UnQty,FreezeQty,StepCode,LastStepName,Status,StockLocations,WarnMsg,LastUpdatedDate
					  from StockProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StockProductInfo> list = new List<StockProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockProductInfo model = new StockProductInfo();
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.Qty = reader.GetDouble(3);
                        model.UnQty = reader.GetDouble(4);
                        model.FreezeQty = reader.GetDouble(5);
                        model.StepCode = reader.GetString(6);
                        model.LastStepName = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.StockLocations = reader.GetString(9);
                        model.WarnMsg = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			           ProductId,CustomerId,Qty,UnQty,FreezeQty,StepCode,LastStepName,Status,StockLocations,WarnMsg,LastUpdatedDate
					   from StockProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StockProductInfo> list = new List<StockProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockProductInfo model = new StockProductInfo();
                        model.ProductId = reader.GetGuid(1);
                        model.CustomerId = reader.GetGuid(2);
                        model.Qty = reader.GetDouble(3);
                        model.UnQty = reader.GetDouble(4);
                        model.FreezeQty = reader.GetDouble(5);
                        model.StepCode = reader.GetString(6);
                        model.LastStepName = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.StockLocations = reader.GetString(9);
                        model.WarnMsg = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select ProductId,CustomerId,Qty,UnQty,FreezeQty,StepCode,LastStepName,Status,StockLocations,WarnMsg,LastUpdatedDate
                        from StockProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate ");

            IList<StockProductInfo> list = new List<StockProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockProductInfo model = new StockProductInfo();
                        model.ProductId = reader.GetGuid(0);
                        model.CustomerId = reader.GetGuid(1);
                        model.Qty = reader.GetDouble(2);
                        model.UnQty = reader.GetDouble(3);
                        model.FreezeQty = reader.GetDouble(4);
                        model.StepCode = reader.GetString(5);
                        model.LastStepName = reader.GetString(6);
                        model.Status = reader.GetString(7);
                        model.StockLocations = reader.GetString(8);
                        model.WarnMsg = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockProductInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select ProductId,CustomerId,Qty,UnQty,FreezeQty,StepCode,LastStepName,Status,StockLocations,WarnMsg,LastUpdatedDate 
			            from StockProduct
					    order by LastUpdatedDate ");

            IList<StockProductInfo> list = new List<StockProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockProductInfo model = new StockProductInfo();
                        model.ProductId = reader.GetGuid(0);
                        model.CustomerId = reader.GetGuid(1);
                        model.Qty = reader.GetDouble(2);
                        model.UnQty = reader.GetDouble(3);
                        model.FreezeQty = reader.GetDouble(4);
                        model.StepCode = reader.GetString(5);
                        model.LastStepName = reader.GetString(6);
                        model.Status = reader.GetString(7);
                        model.StockLocations = reader.GetString(8);
                        model.WarnMsg = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
