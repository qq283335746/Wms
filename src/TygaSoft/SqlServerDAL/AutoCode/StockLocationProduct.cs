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
    public partial class StockLocationProduct : IStockLocationProduct
    {
        #region IStockLocationProduct Member

        public int Insert(StockLocationProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into StockLocationProduct (StockLocationId,ProductAttr,MaxVolume)
			            values
						(@StockLocationId,@ProductAttr,@MaxVolume)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductAttr",SqlDbType.VarChar),
                                        new SqlParameter("@MaxVolume",SqlDbType.Float)
                                   };
            parms[0].Value = model.StockLocationId;
            parms[1].Value = model.ProductAttr;
            parms[2].Value = model.MaxVolume;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(StockLocationProductInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update StockLocationProduct set ProductAttr = @ProductAttr,MaxVolume = @MaxVolume 
			            where StockLocationId = @StockLocationId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@StockLocationId",SqlDbType.UniqueIdentifier),
                                    new SqlParameter("@ProductAttr",SqlDbType.VarChar),
                                    new SqlParameter("@MaxVolume",SqlDbType.Float)
                                   };
            parms[0].Value = model.StockLocationId;
            parms[1].Value = model.ProductAttr;
            parms[2].Value = model.MaxVolume;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid stockLocationId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from StockLocationProduct where StockLocationId = @StockLocationId ");
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
                sb.Append(@"delete from StockLocationProduct where StockLocationId = @StockLocationId" + n + " ;");
                SqlParameter parm = new SqlParameter("@StockLocationId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public StockLocationProductInfo GetModel(Guid stockLocationId)
        {
            StockLocationProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 StockLocationId,ProductAttr,MaxVolume 
			            from StockLocationProduct
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
                        model = new StockLocationProductInfo();
                        model.StockLocationId = reader.GetGuid(0);
                        model.ProductAttr = reader.GetString(1);
                        model.MaxVolume = reader.GetDouble(2);
                    }
                }
            }

            return model;
        }

        public IList<StockLocationProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from StockLocationProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<StockLocationProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			          StockLocationId,ProductAttr,MaxVolume
					  from StockLocationProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StockLocationProductInfo> list = new List<StockLocationProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockLocationProductInfo model = new StockLocationProductInfo();
                        model.StockLocationId = reader.GetGuid(1);
                        model.ProductAttr = reader.GetString(2);
                        model.MaxVolume = reader.GetDouble(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockLocationProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate) as RowNumber,
			           StockLocationId,ProductAttr,MaxVolume
					   from StockLocationProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<StockLocationProductInfo> list = new List<StockLocationProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockLocationProductInfo model = new StockLocationProductInfo();
                        model.StockLocationId = reader.GetGuid(1);
                        model.ProductAttr = reader.GetString(2);
                        model.MaxVolume = reader.GetDouble(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockLocationProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select StockLocationId,ProductAttr,MaxVolume
                        from StockLocationProduct ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate ");

            IList<StockLocationProductInfo> list = new List<StockLocationProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockLocationProductInfo model = new StockLocationProductInfo();
                        model.StockLocationId = reader.GetGuid(0);
                        model.ProductAttr = reader.GetString(1);
                        model.MaxVolume = reader.GetDouble(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockLocationProductInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select StockLocationId,ProductAttr,MaxVolume 
			            from StockLocationProduct
					    order by LastUpdatedDate ");

            IList<StockLocationProductInfo> list = new List<StockLocationProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StockLocationProductInfo model = new StockLocationProductInfo();
                        model.StockLocationId = reader.GetGuid(0);
                        model.ProductAttr = reader.GetString(1);
                        model.MaxVolume = reader.GetDouble(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
