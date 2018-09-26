using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class StockLocation
    {
        #region StockLocation Member

        public IList<StockLocationInfo> GetListInZoneIds(string zoneIds)
        {
            return dal.GetListInZoneIds(zoneIds);
        }

        public string GetStockLocationTextInIds(string Ids)
        {
            return dal.GetStockLocationTextInIds(Ids);
        }

        public StockLocationInfo GetModelForTemp()
        {
            return dal.GetModelForTemp();
        }

        public IList<ComboboxInfo> GetListByBest(Guid productId, double qty)
        {
            var list = new List<ComboboxInfo>();

            var pBll = new Product();
            var productModel = pBll.GetModel(productId);
            if (productModel == null)
            {
                return null;
                //throw new ArgumentException("产品ID为" + productId + "对应数据不存在或已被删除，请检查");
            }

            var slList = GetListByBest();

            var maxVolume = productModel.OutPackVolume * qty;
            var bestSlList = slList.Where(m => m.Volume >= maxVolume).Take(1);
            if (bestSlList == null || bestSlList.Count() == 0)
            {
                bestSlList = slList.OrderByDescending(m => m.Volume).OrderBy(m => m.Code);
                double totalVolume = 0;
                foreach (var item in bestSlList)
                {
                    if (totalVolume >= maxVolume) break;
                    totalVolume += item.Volume;
                    list.Add(new ComboboxInfo { Id = item.Id.ToString(), Text = item.Code });
                }
            }
            else
            {
                var firstItem = bestSlList.First();
                list.Add(new ComboboxInfo { Id = firstItem.Id.ToString(), Text = firstItem.Code });
                
            }

            return list;
        }

        public StockLocationInfo GetModelByJoin(object Id)
        {
            return dal.GetModelByJoin(Id);
        }

        public List<StockLocationInfo> GetListByBest()
        {
            return dal.GetListByBest();
        }

        public IList<StockLocationInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize,out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
