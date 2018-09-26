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
    public partial class Product : IProduct
    {
        #region IProduct Member

        public int Insert(ProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Product (UserId,CategoryId,SupplierId,ProductCode,ProductName,FullName,Specs,Price,MaterialQuality,Weight,MaxStore,MinStore,OutPackVolume,OutPackWeight,InPackVolume,InPackWeight,OutPackQty,InPackQty,ShelfLife,Sort,Remark,IsDisable,LastUpdatedDate)
			            values
						(@UserId,@CategoryId,@SupplierId,@ProductCode,@ProductName,@FullName,@Specs,@Price,@MaterialQuality,@Weight,@MaxStore,@MinStore,@OutPackVolume,@OutPackWeight,@InPackVolume,@InPackWeight,@OutPackQty,@InPackQty,@ShelfLife,@Sort,@Remark,@IsDisable,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CategoryId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@SupplierId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductCode",SqlDbType.VarChar,30),
                                        new SqlParameter("@ProductName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@FullName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Specs",SqlDbType.NVarChar,256),
                                        new SqlParameter("@Price",SqlDbType.Decimal),
                                        new SqlParameter("@MaterialQuality",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Weight",SqlDbType.Float),
                                        new SqlParameter("@MaxStore",SqlDbType.Int),
                                        new SqlParameter("@MinStore",SqlDbType.Int),
                                        new SqlParameter("@OutPackVolume",SqlDbType.Float),
                                        new SqlParameter("@OutPackWeight",SqlDbType.Float),
                                        new SqlParameter("@InPackVolume",SqlDbType.Float),
                                        new SqlParameter("@InPackWeight",SqlDbType.Float),
                                        new SqlParameter("@OutPackQty",SqlDbType.Int),
                                        new SqlParameter("@InPackQty",SqlDbType.Int),
                                        new SqlParameter("@ShelfLife",SqlDbType.Int),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@IsDisable",SqlDbType.Bit),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.CategoryId;
            parms[2].Value = model.SupplierId;
            parms[3].Value = model.ProductCode;
            parms[4].Value = model.ProductName;
            parms[5].Value = model.FullName;
            parms[6].Value = model.Specs;
            parms[7].Value = model.Price;
            parms[8].Value = model.MaterialQuality;
            parms[9].Value = model.Weight;
            parms[10].Value = model.MaxStore;
            parms[11].Value = model.MinStore;
            parms[12].Value = model.OutPackVolume;
            parms[13].Value = model.OutPackWeight;
            parms[14].Value = model.InPackVolume;
            parms[15].Value = model.InPackWeight;
            parms[16].Value = model.OutPackQty;
            parms[17].Value = model.InPackQty;
            parms[18].Value = model.ShelfLife;
            parms[19].Value = model.Sort;
            parms[20].Value = model.Remark;
            parms[21].Value = model.IsDisable;
            parms[22].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(ProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Product (Id,UserId,CategoryId,SupplierId,ProductCode,ProductName,FullName,Specs,Price,MaterialQuality,Weight,MaxStore,MinStore,OutPackVolume,OutPackWeight,InPackVolume,InPackWeight,OutPackQty,InPackQty,ShelfLife,Sort,Remark,IsDisable,LastUpdatedDate)
			            values
						(@Id,@UserId,@CategoryId,@SupplierId,@ProductCode,@ProductName,@FullName,@Specs,@Price,@MaterialQuality,@Weight,@MaxStore,@MinStore,@OutPackVolume,@OutPackWeight,@InPackVolume,@InPackWeight,@OutPackQty,@InPackQty,@ShelfLife,@Sort,@Remark,@IsDisable,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CategoryId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@SupplierId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductCode",SqlDbType.VarChar,30),
                                        new SqlParameter("@ProductName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@FullName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Specs",SqlDbType.NVarChar,256),
                                        new SqlParameter("@Price",SqlDbType.Decimal),
                                        new SqlParameter("@MaterialQuality",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Weight",SqlDbType.Float),
                                        new SqlParameter("@MaxStore",SqlDbType.Int),
                                        new SqlParameter("@MinStore",SqlDbType.Int),
                                        new SqlParameter("@OutPackVolume",SqlDbType.Float),
                                        new SqlParameter("@OutPackWeight",SqlDbType.Float),
                                        new SqlParameter("@InPackVolume",SqlDbType.Float),
                                        new SqlParameter("@InPackWeight",SqlDbType.Float),
                                        new SqlParameter("@OutPackQty",SqlDbType.Int),
                                        new SqlParameter("@InPackQty",SqlDbType.Int),
                                        new SqlParameter("@ShelfLife",SqlDbType.Int),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@IsDisable",SqlDbType.Bit),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.CategoryId;
            parms[3].Value = model.SupplierId;
            parms[4].Value = model.ProductCode;
            parms[5].Value = model.ProductName;
            parms[6].Value = model.FullName;
            parms[7].Value = model.Specs;
            parms[8].Value = model.Price;
            parms[9].Value = model.MaterialQuality;
            parms[10].Value = model.Weight;
            parms[11].Value = model.MaxStore;
            parms[12].Value = model.MinStore;
            parms[13].Value = model.OutPackVolume;
            parms[14].Value = model.OutPackWeight;
            parms[15].Value = model.InPackVolume;
            parms[16].Value = model.InPackWeight;
            parms[17].Value = model.OutPackQty;
            parms[18].Value = model.InPackQty;
            parms[19].Value = model.ShelfLife;
            parms[20].Value = model.Sort;
            parms[21].Value = model.Remark;
            parms[22].Value = model.IsDisable;
            parms[23].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(ProductInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Product set UserId = @UserId,CategoryId = @CategoryId,SupplierId = @SupplierId,ProductCode = @ProductCode,ProductName = @ProductName,FullName = @FullName,Specs = @Specs,Price = @Price,MaterialQuality = @MaterialQuality,Weight = @Weight,MaxStore = @MaxStore,MinStore = @MinStore,OutPackVolume = @OutPackVolume,OutPackWeight = @OutPackWeight,InPackVolume = @InPackVolume,InPackWeight = @InPackWeight,OutPackQty = @OutPackQty,InPackQty = @InPackQty,ShelfLife = @ShelfLife,Sort = @Sort,Remark = @Remark,IsDisable = @IsDisable,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@CategoryId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@SupplierId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ProductCode",SqlDbType.VarChar,30),
                                        new SqlParameter("@ProductName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@FullName",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Specs",SqlDbType.NVarChar,256),
                                        new SqlParameter("@Price",SqlDbType.Decimal),
                                        new SqlParameter("@MaterialQuality",SqlDbType.NVarChar,100),
                                        new SqlParameter("@Weight",SqlDbType.Float),
                                        new SqlParameter("@MaxStore",SqlDbType.Int),
                                        new SqlParameter("@MinStore",SqlDbType.Int),
                                        new SqlParameter("@OutPackVolume",SqlDbType.Float),
                                        new SqlParameter("@OutPackWeight",SqlDbType.Float),
                                        new SqlParameter("@InPackVolume",SqlDbType.Float),
                                        new SqlParameter("@InPackWeight",SqlDbType.Float),
                                        new SqlParameter("@OutPackQty",SqlDbType.Int),
                                        new SqlParameter("@InPackQty",SqlDbType.Int),
                                        new SqlParameter("@ShelfLife",SqlDbType.Int),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                                        new SqlParameter("@IsDisable",SqlDbType.Bit),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UserId;
            parms[2].Value = model.CategoryId;
            parms[3].Value = model.SupplierId;
            parms[4].Value = model.ProductCode;
            parms[5].Value = model.ProductName;
            parms[6].Value = model.FullName;
            parms[7].Value = model.Specs;
            parms[8].Value = model.Price;
            parms[9].Value = model.MaterialQuality;
            parms[10].Value = model.Weight;
            parms[11].Value = model.MaxStore;
            parms[12].Value = model.MinStore;
            parms[13].Value = model.OutPackVolume;
            parms[14].Value = model.OutPackWeight;
            parms[15].Value = model.InPackVolume;
            parms[16].Value = model.InPackWeight;
            parms[17].Value = model.OutPackQty;
            parms[18].Value = model.InPackQty;
            parms[19].Value = model.ShelfLife;
            parms[20].Value = model.Sort;
            parms[21].Value = model.Remark;
            parms[22].Value = model.IsDisable;
            parms[23].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Product where Id = @Id ");
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
                sb.Append(@"delete from Product where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public ProductInfo GetModel(Guid id)
        {
            ProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UserId,CategoryId,SupplierId,ProductCode,ProductName,FullName,Specs,Price,MaterialQuality,Weight,MaxStore,MinStore,OutPackVolume,OutPackWeight,InPackVolume,InPackWeight,OutPackQty,InPackQty,ShelfLife,Sort,Remark,IsDisable,LastUpdatedDate 
			            from Product
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
                        model = new ProductInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CategoryId = reader.GetGuid(2);
                        model.SupplierId = reader.GetGuid(3);
                        model.ProductCode = reader.GetString(4);
                        model.ProductName = reader.GetString(5);
                        model.FullName = reader.GetString(6);
                        model.Specs = reader.GetString(7);
                        model.Price = reader.GetDecimal(8);
                        model.MaterialQuality = reader.GetString(9);
                        model.Weight = reader.GetDouble(10);
                        model.MaxStore = reader.GetInt32(11);
                        model.MinStore = reader.GetInt32(12);
                        model.OutPackVolume = reader.GetDouble(13);
                        model.OutPackWeight = reader.GetDouble(14);
                        model.InPackVolume = reader.GetDouble(15);
                        model.InPackWeight = reader.GetDouble(16);
                        model.OutPackQty = reader.GetInt32(17);
                        model.InPackQty = reader.GetInt32(18);
                        model.ShelfLife = reader.GetInt32(19);
                        model.Sort = reader.GetInt32(20);
                        model.Remark = reader.GetString(21);
                        model.IsDisable = reader.GetBoolean(22);
                        model.LastUpdatedDate = reader.GetDateTime(23);
                    }
                }
            }

            return model;
        }

        public IList<ProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Product ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          Id,UserId,CategoryId,SupplierId,ProductCode,ProductName,FullName,Specs,Price,MaterialQuality,Weight,MaxStore,MinStore,OutPackVolume,OutPackWeight,InPackVolume,InPackWeight,OutPackQty,InPackQty,ShelfLife,Sort,Remark,IsDisable,LastUpdatedDate
					  from Product ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ProductInfo> list = new List<ProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductInfo model = new ProductInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.CategoryId = reader.GetGuid(3);
                        model.SupplierId = reader.GetGuid(4);
                        model.ProductCode = reader.GetString(5);
                        model.ProductName = reader.GetString(6);
                        model.FullName = reader.GetString(7);
                        model.Specs = reader.GetString(8);
                        model.Price = reader.GetDecimal(9);
                        model.MaterialQuality = reader.GetString(10);
                        model.Weight = reader.GetDouble(11);
                        model.MaxStore = reader.GetInt32(12);
                        model.MinStore = reader.GetInt32(13);
                        model.OutPackVolume = reader.GetDouble(14);
                        model.OutPackWeight = reader.GetDouble(15);
                        model.InPackVolume = reader.GetDouble(16);
                        model.InPackWeight = reader.GetDouble(17);
                        model.OutPackQty = reader.GetInt32(18);
                        model.InPackQty = reader.GetInt32(19);
                        model.ShelfLife = reader.GetInt32(20);
                        model.Sort = reader.GetInt32(21);
                        model.Remark = reader.GetString(22);
                        model.IsDisable = reader.GetBoolean(23);
                        model.LastUpdatedDate = reader.GetDateTime(24);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           Id,UserId,CategoryId,SupplierId,ProductCode,ProductName,FullName,Specs,Price,MaterialQuality,Weight,MaxStore,MinStore,OutPackVolume,OutPackWeight,InPackVolume,InPackWeight,OutPackQty,InPackQty,ShelfLife,Sort,Remark,IsDisable,LastUpdatedDate
					   from Product ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ProductInfo> list = new List<ProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductInfo model = new ProductInfo();
                        model.Id = reader.GetGuid(1);
                        model.UserId = reader.GetGuid(2);
                        model.CategoryId = reader.GetGuid(3);
                        model.SupplierId = reader.GetGuid(4);
                        model.ProductCode = reader.GetString(5);
                        model.ProductName = reader.GetString(6);
                        model.FullName = reader.GetString(7);
                        model.Specs = reader.GetString(8);
                        model.Price = reader.GetDecimal(9);
                        model.MaterialQuality = reader.GetString(10);
                        model.Weight = reader.GetDouble(11);
                        model.MaxStore = reader.GetInt32(12);
                        model.MinStore = reader.GetInt32(13);
                        model.OutPackVolume = reader.GetDouble(14);
                        model.OutPackWeight = reader.GetDouble(15);
                        model.InPackVolume = reader.GetDouble(16);
                        model.InPackWeight = reader.GetDouble(17);
                        model.OutPackQty = reader.GetInt32(18);
                        model.InPackQty = reader.GetInt32(19);
                        model.ShelfLife = reader.GetInt32(20);
                        model.Sort = reader.GetInt32(21);
                        model.Remark = reader.GetString(22);
                        model.IsDisable = reader.GetBoolean(23);
                        model.LastUpdatedDate = reader.GetDateTime(24);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,UserId,CategoryId,SupplierId,ProductCode,ProductName,FullName,Specs,Price,MaterialQuality,Weight,MaxStore,MinStore,OutPackVolume,OutPackWeight,InPackVolume,InPackWeight,OutPackQty,InPackQty,ShelfLife,Sort,Remark,IsDisable,LastUpdatedDate
                        from Product ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<ProductInfo> list = new List<ProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductInfo model = new ProductInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CategoryId = reader.GetGuid(2);
                        model.SupplierId = reader.GetGuid(3);
                        model.ProductCode = reader.GetString(4);
                        model.ProductName = reader.GetString(5);
                        model.FullName = reader.GetString(6);
                        model.Specs = reader.GetString(7);
                        model.Price = reader.GetDecimal(8);
                        model.MaterialQuality = reader.GetString(9);
                        model.Weight = reader.GetDouble(10);
                        model.MaxStore = reader.GetInt32(11);
                        model.MinStore = reader.GetInt32(12);
                        model.OutPackVolume = reader.GetDouble(13);
                        model.OutPackWeight = reader.GetDouble(14);
                        model.InPackVolume = reader.GetDouble(15);
                        model.InPackWeight = reader.GetDouble(16);
                        model.OutPackQty = reader.GetInt32(17);
                        model.InPackQty = reader.GetInt32(18);
                        model.ShelfLife = reader.GetInt32(19);
                        model.Sort = reader.GetInt32(20);
                        model.Remark = reader.GetString(21);
                        model.IsDisable = reader.GetBoolean(22);
                        model.LastUpdatedDate = reader.GetDateTime(23);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ProductInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,UserId,CategoryId,SupplierId,ProductCode,ProductName,FullName,Specs,Price,MaterialQuality,Weight,MaxStore,MinStore,OutPackVolume,OutPackWeight,InPackVolume,InPackWeight,OutPackQty,InPackQty,ShelfLife,Sort,Remark,IsDisable,LastUpdatedDate 
			            from Product
					    order by Sort ");

            IList<ProductInfo> list = new List<ProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductInfo model = new ProductInfo();
                        model.Id = reader.GetGuid(0);
                        model.UserId = reader.GetGuid(1);
                        model.CategoryId = reader.GetGuid(2);
                        model.SupplierId = reader.GetGuid(3);
                        model.ProductCode = reader.GetString(4);
                        model.ProductName = reader.GetString(5);
                        model.FullName = reader.GetString(6);
                        model.Specs = reader.GetString(7);
                        model.Price = reader.GetDecimal(8);
                        model.MaterialQuality = reader.GetString(9);
                        model.Weight = reader.GetDouble(10);
                        model.MaxStore = reader.GetInt32(11);
                        model.MinStore = reader.GetInt32(12);
                        model.OutPackVolume = reader.GetDouble(13);
                        model.OutPackWeight = reader.GetDouble(14);
                        model.InPackVolume = reader.GetDouble(15);
                        model.InPackWeight = reader.GetDouble(16);
                        model.OutPackQty = reader.GetInt32(17);
                        model.InPackQty = reader.GetInt32(18);
                        model.ShelfLife = reader.GetInt32(19);
                        model.Sort = reader.GetInt32(20);
                        model.Remark = reader.GetString(21);
                        model.IsDisable = reader.GetBoolean(22);
                        model.LastUpdatedDate = reader.GetDateTime(23);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
