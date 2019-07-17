using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class StockLocationProduct : IStockLocationProduct
    {
        #region IStockLocationProduct Member

        public string GetNameByProductId(Guid productId)
        {
            var cmdText = string.Format(@"select sl.Named
                                        from StockLocation sl 
                                        join StockLocationProduct slp on slp.StockLocationId = sl.Id
                                        join Zone z on z.Id = sl.ZoneId
                                        where z.ZoneCode = 'FinishedZ' and
                                        CHARINDEX('{0}', slp.ProductAttr) > 0 ", productId);

            var list = new List<string>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }
            }

            if (list.Count == 0) return "";

            return string.Join(",",list);
        }

        public StockLocationProductInfo GetModelByJoin(Guid Id)
        {
            var cmdText = @"select sl.Id,sl.Code,sl.Named,sl.Volume,slp.StockLocationId,slp.ProductAttr,slp.MaxVolume 
                            from StockLocation sl
                            left join StockLocationProduct slp on slp.StockLocationId = sl.Id
                            where sl.Id = @Id ";
            var parm = new SqlParameter("@Id", Id);
            StockLocationProductInfo model = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText,parm))
            {
                if (reader != null && reader.HasRows)
                {
                    if (reader.Read())
                    {
                        model = new StockLocationProductInfo();
                        model.StockLocationId = reader.GetGuid(0);
                        model.StockLocationCode = reader.GetString(1);
                        model.StockLocationName = reader.GetString(2);
                        model.Volume = reader.GetDouble(3);
                        model.IsHas = !reader.IsDBNull(4);
                        model.ProductAttr = reader.IsDBNull(5) ? "" : reader.GetString(5);
                        model.MaxVolume = reader.IsDBNull(6) ? 0 : reader.GetDouble(6);
                    }
                }
            }

            return model;
        }

        public IList<StockLocationProductInfo> GetListByUsable(Guid productId, double minVolume)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.AppendFormat(@"select sl.Id,sl.Code,sl.Named,isnull(slp.MaxVolume,sl.Volume) MaxVolume,slp.ProductAttr
                            from StockLocation sl
                            left join StockLocationProduct slp on slp.StockLocationId = sl.Id
                            join Zone z on z.Id = sl.ZoneId
                            where z.ZoneCode = 'FinishedZ' and isnull(slp.MaxVolume,sl.Volume) >= {0} ", minVolume);

            var list = new List<StockLocationProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new StockLocationProductInfo();
                        model.StockLocationId = reader.GetGuid(0);
                        model.StockLocationCode = reader.GetString(1);
                        model.StockLocationName = reader.GetString(2);
                        model.MaxVolume = reader.GetDouble(3);
                        if (!reader.IsDBNull(4))
                        {
                            var slpaList = JsonConvert.DeserializeObject<List<StockLocationProductInfo>>(reader.GetString(4));
                            var qpList = slpaList.Where(m => m.ProductId.Equals(productId)).OrderByDescending(m => m.LastUpdatedDate);
                            if (qpList != null && qpList.Count() > 0)
                            {
                                model.ProductId = productId;
                                model.LastUpdatedDate = qpList.First().LastUpdatedDate;
                            }
                        }
                        model.MaxQty = model.MaxVolume / minVolume;

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<StockLocationProductInfo> GetListForOrderSendProduct()
        {
            var cmdText = @"select sl.Id,sl.Code,sl.Named,slp.ProductAttr
                                from StockLocation sl 
                                join StockLocationProduct slp on slp.StockLocationId = sl.Id
                                join Zone z on z.Id = sl.ZoneId
                                where z.ZoneCode = 'FinishedZ'
                                and ProductAttr != '' ";

            var list = new List<StockLocationProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    var pDal = new Product();

                    while (reader.Read())
                    {
                        var paList = JsonConvert.DeserializeObject<List<StockLocationProductAttrInfo>>(reader.GetString(3));
                        if (paList.Count > 0)
                        {
                            var q = paList.Where(m => m.Qty > 0).OrderByDescending(m => m.LastUpdatedDate);
                            if (q != null && q.Count() > 0)
                            {
                                foreach (var item in q)
                                {
                                    var model = new StockLocationProductInfo();
                                    model.StockLocationId = reader.GetGuid(0);
                                    model.StockLocationCode = reader.GetString(1);
                                    model.StockLocationName = reader.GetString(2);
                                    model.ProductId = item.ProductId;
                                    model.MaxQty = item.Qty;

                                    list.Add(model);
                                }
                            }
                        }
                    }
                }
            }

            return list;
        }

        public IList<StockLocationProductInfo> GetListForOrderPickProduct(Guid productId)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.AppendFormat(@"select sl.Id,sl.Code,sl.Named,isnull(slp.MaxVolume,sl.Volume) MaxVolume,slp.ProductAttr
                        from StockLocationProduct slp 
                        join StockLocation sl on sl.Id = slp.StockLocationId
                        join Zone z on z.Id = sl.ZoneId
                        where z.ZoneCode = 'FinishedZ'
                        and CHARINDEX('{0}', slp.ProductAttr) > 0  ", productId);

            var list = new List<StockLocationProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.WmsDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var paList = JsonConvert.DeserializeObject<List<StockLocationProductAttrInfo>>(reader.GetString(4));
                        var item = paList.FirstOrDefault(m => m.ProductId.Equals(productId));
                        if(item != null && item.FreezeQty > 0)
                        {
                            var model = new StockLocationProductInfo();
                            model.StockLocationId = reader.GetGuid(0);
                            model.StockLocationCode = reader.GetString(1);
                            model.StockLocationName = reader.GetString(2);
                            model.MaxVolume = reader.GetDouble(3);
                            model.MaxQty = item.FreezeQty;
                            model.LastUpdatedDate = item.LastUpdatedDate;

                            list.Add(model);
                        }
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
