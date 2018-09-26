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
    public partial class MesOrder : IMesOrder
    {
        #region IMesOrder Member

        public int Insert(MesOrderInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into MesOrder (UserId,OBarcode,PBarcode,PdBarcode,PtBarcode,Qty,StartDate,EndDate,Sort,Remark,LastUpdatedDate)
			            values
						(@UserId,@OBarcode,@PBarcode,@PdBarcode,@PtBarcode,@Qty,@StartDate,@EndDate,@Sort,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OBarcode",SqlDbType.VarChar,36),
new SqlParameter("@PBarcode",SqlDbType.VarChar,36),
new SqlParameter("@PdBarcode",SqlDbType.VarChar,36),
new SqlParameter("@PtBarcode",SqlDbType.VarChar,36),
new SqlParameter("@Qty",SqlDbType.Float),
new SqlParameter("@StartDate",SqlDbType.DateTime),
new SqlParameter("@EndDate",SqlDbType.DateTime),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.OBarcode;
            parms[2].Value = model.PBarcode;
            parms[3].Value = model.PdBarcode;
            parms[4].Value = model.PtBarcode;
            parms[5].Value = model.Qty;
            parms[6].Value = model.StartDate;
            parms[7].Value = model.EndDate;
            parms[8].Value = model.Sort;
            parms[9].Value = model.Remark;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(MesOrderInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into MesOrder (Id,UserId,OBarcode,PBarcode,PdBarcode,PtBarcode,Qty,StartDate,EndDate,Sort,Remark,LastUpdatedDate)
			            values
						(@Id,@UserId,@OBarcode,@PBarcode,@PdBarcode,@PtBarcode,@Qty,@StartDate,@EndDate,@Sort,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OBarcode",SqlDbType.VarChar,36),
new SqlParameter("@PBarcode",SqlDbType.VarChar,36),
new SqlParameter("@PdBarcode",SqlDbType.VarChar,36),
new SqlParameter("@PtBarcode",SqlDbType.VarChar,36),
new SqlParameter("@Qty",SqlDbType.Float),
new SqlParameter("@StartDate",SqlDbType.DateTime),
new SqlParameter("@EndDate",SqlDbType.DateTime),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OBarcode;
            parms[3].Value = model.PBarcode;
            parms[4].Value = model.PdBarcode;
            parms[5].Value = model.PtBarcode;
            parms[6].Value = model.Qty;
            parms[7].Value = model.StartDate;
            parms[8].Value = model.EndDate;
            parms[9].Value = model.Sort;
            parms[10].Value = model.Remark;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(MesOrderInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update MesOrder set UserId = @UserId,OBarcode = @OBarcode,PBarcode = @PBarcode,PdBarcode = @PdBarcode,PtBarcode = @PtBarcode,Qty = @Qty,StartDate = @StartDate,EndDate = @EndDate,Sort = @Sort,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OBarcode",SqlDbType.VarChar,36),
new SqlParameter("@PBarcode",SqlDbType.VarChar,36),
new SqlParameter("@PdBarcode",SqlDbType.VarChar,36),
new SqlParameter("@PtBarcode",SqlDbType.VarChar,36),
new SqlParameter("@Qty",SqlDbType.Float),
new SqlParameter("@StartDate",SqlDbType.DateTime),
new SqlParameter("@EndDate",SqlDbType.DateTime),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OBarcode;
            parms[3].Value = model.PBarcode;
            parms[4].Value = model.PdBarcode;
            parms[5].Value = model.PtBarcode;
            parms[6].Value = model.Qty;
            parms[7].Value = model.StartDate;
            parms[8].Value = model.EndDate;
            parms[9].Value = model.Sort;
            parms[10].Value = model.Remark;
            parms[11].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from MesOrder where Id = @Id ");
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
                sb.Append(@"delete from MesOrder where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public MesOrderInfo GetModel(Guid id)
        {
            MesOrderInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,OBarcode,PBarcode,PdBarcode,PtBarcode,Qty,StartDate,EndDate,Sort,Remark,LastUpdatedDate 
			            from MesOrder
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
                        model = new MesOrderInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OBarcode = reader.GetString(2);
                        model.PBarcode = reader.GetString(3);
                        model.PdBarcode = reader.GetString(4);
                        model.PtBarcode = reader.GetString(5);
                        model.Qty = reader.GetDouble(6);
                        model.StartDate = reader.GetDateTime(7);
                        model.EndDate = reader.GetDateTime(8);
                        model.Sort = reader.GetInt32(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);
                    }
                }
            }

            return model;
        }

        public IList<MesOrderInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from MesOrder ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<MesOrderInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          Id,UserId,OBarcode,PBarcode,PdBarcode,PtBarcode,Qty,StartDate,EndDate,Sort,Remark,LastUpdatedDate
					  from MesOrder ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<MesOrderInfo> list = new List<MesOrderInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MesOrderInfo model = new MesOrderInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OBarcode = reader.GetString(3);
                        model.PBarcode = reader.GetString(4);
                        model.PdBarcode = reader.GetString(5);
                        model.PtBarcode = reader.GetString(6);
                        model.Qty = reader.GetDouble(7);
                        model.StartDate = reader.GetDateTime(8);
                        model.EndDate = reader.GetDateTime(9);
                        model.Sort = reader.GetInt32(10);
                        model.Remark = reader.GetString(11);
                        model.LastUpdatedDate = reader.GetDateTime(12);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<MesOrderInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           Id,UserId,OBarcode,PBarcode,PdBarcode,PtBarcode,Qty,StartDate,EndDate,Sort,Remark,LastUpdatedDate
					   from MesOrder ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<MesOrderInfo> list = new List<MesOrderInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MesOrderInfo model = new MesOrderInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.OBarcode = reader.GetString(3);
                        model.PBarcode = reader.GetString(4);
                        model.PdBarcode = reader.GetString(5);
                        model.PtBarcode = reader.GetString(6);
                        model.Qty = reader.GetDouble(7);
                        model.StartDate = reader.GetDateTime(8);
                        model.EndDate = reader.GetDateTime(9);
                        model.Sort = reader.GetInt32(10);
                        model.Remark = reader.GetString(11);
                        model.LastUpdatedDate = reader.GetDateTime(12);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<MesOrderInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,OBarcode,PBarcode,PdBarcode,PtBarcode,Qty,StartDate,EndDate,Sort,Remark,LastUpdatedDate
                        from MesOrder ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<MesOrderInfo> list = new List<MesOrderInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MesOrderInfo model = new MesOrderInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OBarcode = reader.GetString(2);
                        model.PBarcode = reader.GetString(3);
                        model.PdBarcode = reader.GetString(4);
                        model.PtBarcode = reader.GetString(5);
                        model.Qty = reader.GetDouble(6);
                        model.StartDate = reader.GetDateTime(7);
                        model.EndDate = reader.GetDateTime(8);
                        model.Sort = reader.GetInt32(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<MesOrderInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,OBarcode,PBarcode,PdBarcode,PtBarcode,Qty,StartDate,EndDate,Sort,Remark,LastUpdatedDate 
			            from MesOrder
					    order by Sort ");

            IList<MesOrderInfo> list = new List<MesOrderInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MesOrderInfo model = new MesOrderInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.OBarcode = reader.GetString(2);
                        model.PBarcode = reader.GetString(3);
                        model.PdBarcode = reader.GetString(4);
                        model.PtBarcode = reader.GetString(5);
                        model.Qty = reader.GetDouble(6);
                        model.StartDate = reader.GetDateTime(7);
                        model.EndDate = reader.GetDateTime(8);
                        model.Sort = reader.GetInt32(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
