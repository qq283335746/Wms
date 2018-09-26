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
    public partial class OrderMap : IOrderMap
    {
        #region IOrderMap Member

        public int Insert(OrderMapInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into OrderMap (OrderCode,UserId,Lnglat,IP,ProvinceCity,Address,Platform,LastUpdatedDate)
			            values
						(@OrderCode,@UserId,@Lnglat,@IP,@ProvinceCity,@Address,@Platform,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Lnglat",SqlDbType.VarChar,100),
new SqlParameter("@IP",SqlDbType.VarChar,20),
new SqlParameter("@ProvinceCity",SqlDbType.NVarChar,50),
new SqlParameter("@Address",SqlDbType.NVarChar,100),
new SqlParameter("@Platform",SqlDbType.VarChar,10),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.OrderCode;
            parms[1].Value = model.UserId;
            parms[2].Value = model.Lnglat;
            parms[3].Value = model.IP;
            parms[4].Value = model.ProvinceCity;
            parms[5].Value = model.Address;
            parms[6].Value = model.Platform;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrderMapInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update OrderMap set UserId = @UserId,Lnglat = @Lnglat,IP = @IP,ProvinceCity = @ProvinceCity,Address = @Address,Platform = @Platform,LastUpdatedDate = @LastUpdatedDate 
			            where OrderCode = @OrderCode
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@OrderCode",SqlDbType.VarChar,20),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Lnglat",SqlDbType.VarChar,100),
new SqlParameter("@IP",SqlDbType.VarChar,20),
new SqlParameter("@ProvinceCity",SqlDbType.NVarChar,50),
new SqlParameter("@Address",SqlDbType.NVarChar,100),
new SqlParameter("@Platform",SqlDbType.VarChar,10),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.OrderCode;
            parms[1].Value = model.UserId;
            parms[2].Value = model.Lnglat;
            parms[3].Value = model.IP;
            parms[4].Value = model.ProvinceCity;
            parms[5].Value = model.Address;
            parms[6].Value = model.Platform;
            parms[7].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(string orderCode)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrderMap where OrderCode = @OrderCode ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderCode",SqlDbType.VarChar,20)
                                   };
            parms[0].Value = orderCode;

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
                sb.Append(@"delete from OrderMap where OrderCode = @OrderCode" + n + " ;");
                SqlParameter parm = new SqlParameter("@OrderCode" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public OrderMapInfo GetModel(string orderCode)
        {
            OrderMapInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 UserId,OrderCode,Lnglat,IP,ProvinceCity,Address,Platform,LastUpdatedDate 
			            from OrderMap
						where OrderCode = @OrderCode ");
            SqlParameter[] parms = {
                                     new SqlParameter("@OrderCode",SqlDbType.VarChar,20)
                                   };
            parms[0].Value = orderCode;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrderMapInfo();
                        model.UserId = reader.GetGuid(0);
                        model.OrderCode = reader.GetString(1);
                        model.Lnglat = reader.GetString(2);
                        model.IP = reader.GetString(3);
                        model.ProvinceCity = reader.GetString(4);
                        model.Address = reader.GetString(5);
                        model.Platform = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                    }
                }
            }

            return model;
        }

        public IList<OrderMapInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from OrderMap ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrderMapInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          UserId,OrderCode,Lnglat,IP,ProvinceCity,Address,Platform,LastUpdatedDate
					  from OrderMap ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderMapInfo> list = new List<OrderMapInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderMapInfo model = new OrderMapInfo();
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.Lnglat = reader.GetString(3);
                        model.IP = reader.GetString(4);
                        model.ProvinceCity = reader.GetString(5);
                        model.Address = reader.GetString(6);
                        model.Platform = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderMapInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           UserId,OrderCode,Lnglat,IP,ProvinceCity,Address,Platform,LastUpdatedDate
					   from OrderMap ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrderMapInfo> list = new List<OrderMapInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderMapInfo model = new OrderMapInfo();
                        model.UserId = reader.GetGuid(1);
                        model.OrderCode = reader.GetString(2);
                        model.Lnglat = reader.GetString(3);
                        model.IP = reader.GetString(4);
                        model.ProvinceCity = reader.GetString(5);
                        model.Address = reader.GetString(6);
                        model.Platform = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderMapInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select UserId,OrderCode,Lnglat,IP,ProvinceCity,Address,Platform,LastUpdatedDate
                        from OrderMap ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<OrderMapInfo> list = new List<OrderMapInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderMapInfo model = new OrderMapInfo();
                        model.UserId = reader.GetGuid(0);
                        model.OrderCode = reader.GetString(1);
                        model.Lnglat = reader.GetString(2);
                        model.IP = reader.GetString(3);
                        model.ProvinceCity = reader.GetString(4);
                        model.Address = reader.GetString(5);
                        model.Platform = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrderMapInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select UserId,OrderCode,Lnglat,IP,ProvinceCity,Address,Platform,LastUpdatedDate 
			            from OrderMap
					    order by LastUpdatedDate desc ");

            IList<OrderMapInfo> list = new List<OrderMapInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderMapInfo model = new OrderMapInfo();
                        model.UserId = reader.GetGuid(0);
                        model.OrderCode = reader.GetString(1);
                        model.Lnglat = reader.GetString(2);
                        model.IP = reader.GetString(3);
                        model.ProvinceCity = reader.GetString(4);
                        model.Address = reader.GetString(5);
                        model.Platform = reader.GetString(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
