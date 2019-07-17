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
    public partial class Product
    {
        #region IProduct Member

        public string CreateCode(Guid categoryId)
        {
            var cmdText = @"select c.CategoryCode,(select count(1) from Product p where p.CategoryId = c.Id) TotalChild
                            from Category c
                            where c.Id = @Id ";
            var parm = new SqlParameter("@Id", categoryId);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0) + ".";
                        //return reader.GetString(0) + "." + (reader.GetInt32(1) + 1).ToString().PadLeft(3, '0');
                    }
                }
            }

            return string.Empty;
        }

        public bool IsExistCode(string productCode, Guid Id)
        {
            var cmdText = "";
            SqlParameter[] parms = new SqlParameter[1];
            if (Id.Equals(Guid.Empty))
            {
                cmdText = @"select 1 from [Product] where ProductCode = @ProductCode ";
                parms[0] = new SqlParameter("@ProductCode", SqlDbType.VarChar, 30);
                parms[0].Value = productCode;
            }
            else
            {
                cmdText = @"select 1 from [Product] where ProductCode = @ProductCode and Id <> @Id ";
                Array.Resize(ref parms, 2);
                parms[0] = new SqlParameter("@ProductCode", SqlDbType.VarChar, 30);
                parms[0].Value = productCode;
                parms[1] = new SqlParameter("@Id", Id);
            }

            object obj = SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, cmdText, parms);
            if (obj != null) return true;

            return false;
        }

        public IList<ProductInfo> GetListByCategory(int pageIndex, int pageSize, out int totalRecords, object categoryId, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.AppendFormat(@"select count(*) from Product p 
                        join
                        (
                            select c1.Id from Category c1 where CHARINDEX('{0}', c1.Step) > 0
                        )
                        c on c.Id = p.CategoryId
                      ", categoryId);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.AppendFormat(@"select * from(select row_number() over(order by p.Sort) as RowNumber,
			          p.Id,p.UserId,p.CategoryId,p.ProductCode,p.ProductName,p.FullName,p.Specs,p.Price,p.MaterialQuality,p.Weight,p.MaxStore
                      ,p.MinStore,p.OutPackVolume,p.OutPackWeight,p.InPackVolume,p.InPackWeight,p.OutPackQty,p.InPackQty,p.ShelfLife,p.Sort,p.Remark,p.IsDisable,p.LastUpdatedDate
                      from Product p
                        join
                        (
                          select c1.Id from Category c1 where CHARINDEX('{0}', c1.Step) > 0
                        )
                        c on c.Id = p.CategoryId
                      ", categoryId);

            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<ProductInfo>();

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
