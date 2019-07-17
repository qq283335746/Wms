using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.BLL
{
    public partial class StockLocationProduct
    {
        #region StockLocationProduct Member

        public void DoProduct(Guid slId, Guid productId, bool isIncrease, double qty, double freezeQty)
        {
            var slpInfo = dal.GetModel(slId);
            if (!isIncrease)
            {
                #region 对库位货物“减”处理

                if (slpInfo != null)
                {
                    var slpaList = JsonConvert.DeserializeObject<List<StockLocationProductAttrInfo>>(slpInfo.ProductAttr);
                    var slpaInfo = slpaList.FirstOrDefault(m => m.ProductId.Equals(productId));
                    if (slpaInfo != null)
                    {
                        if (qty > 0)
                        {
                            slpaInfo.Qty -= qty;
                            if (slpaInfo.Qty < 0) throw new ArgumentException(MC.GetString(MC.Request_InvalidQty, qty.ToString()));
                        }
                        if (freezeQty > 0)
                        {
                            slpaInfo.FreezeQty -= freezeQty;
                            if (slpaInfo.FreezeQty < 0) throw new ArgumentException(MC.GetString(MC.Request_InvalidQty, freezeQty.ToString()));
                        }
                        if (slpaInfo.Qty <= 0 && slpaInfo.FreezeQty <= 0) slpaList.Remove(slpaInfo);

                        slpInfo.ProductAttr = JsonConvert.SerializeObject(slpaList);
                        dal.Update(slpInfo);
                    }
                }

                #endregion
            }
            else
            {
                #region 对库位货物“增”处理

                if (slpInfo != null)
                {
                    #region 已存在相应数据，则执行修改操作

                    var slpaList = JsonConvert.DeserializeObject<List<StockLocationProductAttrInfo>>(slpInfo.ProductAttr);
                    var slpaInfo = slpaList.FirstOrDefault(m => m.ProductId.Equals(productId));
                    if (slpaInfo != null)
                    {
                        if (qty > 0)
                        {
                            slpaInfo.Qty += qty;
                        }
                        if (freezeQty > 0)
                        {
                            slpaInfo.FreezeQty += freezeQty;
                        }
                    }
                    else
                    {
                        slpaList.Add(new StockLocationProductAttrInfo(productId, qty, freezeQty, DateTime.Now));
                    }
                    slpInfo.ProductAttr = JsonConvert.SerializeObject(slpaList);
                    dal.Update(slpInfo);

                    #endregion
                }
                else
                {
                    #region 否则执行新增操作

                    var slpaList = new List<StockLocationProductAttrInfo>();
                    slpaList.Add(new StockLocationProductAttrInfo(productId, qty > 0 ? qty : 0, freezeQty > 0 ? freezeQty : 0, DateTime.Now));

                    var slBll = new StockLocation();
                    var slInfo = slBll.GetModel(slId);
                    var maxVolume = slInfo.Volume;
                    if (qty > 0) maxVolume -= qty;
                    slpInfo = new StockLocationProductInfo(slId, JsonConvert.SerializeObject(slpaList), maxVolume);

                    dal.Insert(slpInfo);

                    #endregion
                }

                #endregion
            }
        }

        public string GetNameByProductId(Guid productId)
        {
            return dal.GetNameByProductId(productId);
        }

        public StockLocationProductInfo GetModelByJoin(Guid Id)
        {
            return dal.GetModelByJoin(Id);
        }

        public IList<StockLocationProductInfo> GetListForShelfMissionProduct(Guid productId, double qty)
        {
            var list = new List<StockLocationProductInfo>();

            var pBll = new Product();
            var productInfo = pBll.GetModel(productId);
            if (productInfo == null)
            {
                return null;
            }

            var minVolume = productInfo.OutPackVolume <= 0 ? 1 : productInfo.OutPackVolume;
            var maxVolume = minVolume * qty;
            var slList = GetListByUsable(productId, minVolume);
            var qBest = slList.Where(m => m.ProductId.Equals(productId)).OrderByDescending(m => m.LastUpdatedDate);
            double totalVolume = 0;

            if (qBest != null && qBest.Count() > 0)
            {
                foreach (var item in qBest)
                {
                    if (totalVolume < maxVolume)
                    {
                        totalVolume += item.MaxVolume;
                        item.IsBest = true;
                    }

                    list.Add(item);
                    slList.Remove(item);
                }

            }

            var qList = slList.OrderByDescending(m => m.MaxVolume);
            foreach (var item in qList)
            {
                if (totalVolume < maxVolume)
                {
                    totalVolume += item.MaxVolume;
                    item.IsBest = true;
                }

                list.Add(item);
            }

            return list;
        }

        public IList<StockLocationProductInfo> GetListByBest(Guid productId, double qty)
        {
            var list = new List<StockLocationProductInfo>();

            var pBll = new Product();
            var productInfo = pBll.GetModel(productId);
            if (productInfo == null)
            {
                return null;
            }

            var minVolume = productInfo.OutPackVolume <= 0 ? 1 : productInfo.OutPackVolume;
            var maxVolume = minVolume * qty;

            var slList = GetListByUsable(productId, minVolume);
            var bestSlList = slList.Where(m => m.MaxVolume >= maxVolume);
            if (bestSlList == null || bestSlList.Count() == 0)
            {
                bestSlList = slList.OrderByDescending(m => m.MaxVolume).OrderBy(m => m.StockLocationCode);
                double totalVolume = 0;
                foreach (var item in bestSlList)
                {
                    if (totalVolume >= maxVolume) break;
                    totalVolume += item.MaxVolume;
                    list.Add(new StockLocationProductInfo { StockLocationId = item.StockLocationId, StockLocationCode = item.StockLocationCode, StockLocationName = item.StockLocationName, MaxVolume = item.MaxVolume });
                }
            }
            else
            {
                var firstItem = bestSlList.First();
                list.Add(new StockLocationProductInfo { StockLocationId = firstItem.StockLocationId, StockLocationCode = firstItem.StockLocationCode, StockLocationName = firstItem.StockLocationName, MaxQty = firstItem.MaxQty, MaxVolume = firstItem.MaxVolume });
            }

            return list;
        }

        public IList<StockLocationProductInfo> GetListByOther(Guid productId, double qty)
        {
            var bestList = new List<StockLocationProductInfo>();

            var pBll = new Product();
            var productInfo = pBll.GetModel(productId);
            if (productInfo == null)
            {
                return null;
            }

            var minVolume = productInfo.OutPackVolume <= 0 ? 1 : productInfo.OutPackVolume;
            var maxVolume = minVolume * qty;

            var slList = GetListByUsable(productId, minVolume);
            var bestSlList = slList.Where(m => m.MaxVolume >= maxVolume);
            if (bestSlList == null || bestSlList.Count() == 0)
            {
                bestSlList = slList.OrderByDescending(m => m.MaxVolume).OrderBy(m => m.StockLocationCode);
                double totalVolume = 0;
                foreach (var item in bestSlList)
                {
                    if (totalVolume >= maxVolume) break;
                    totalVolume += item.MaxVolume;
                    bestList.Add(new StockLocationProductInfo { StockLocationId = item.StockLocationId, StockLocationCode = item.StockLocationCode, StockLocationName = item.StockLocationName, MaxVolume = item.MaxVolume });
                }
            }
            else
            {
                var firstItem = bestSlList.First();
                bestList.Add(new StockLocationProductInfo { StockLocationId = firstItem.StockLocationId, StockLocationCode = firstItem.StockLocationCode, StockLocationName = firstItem.StockLocationName, MaxQty = firstItem.MaxQty, MaxVolume = firstItem.MaxVolume });
            }
            foreach (var bestItem in bestList)
            {
                slList.Remove(slList.First(m => m.StockLocationId == bestItem.StockLocationId));
            }

            return slList;

            #region 已注释

            //var pBll = new Product();
            //var productInfo = pBll.GetModel(productId);
            //if (productInfo == null)
            //{
            //    return null;
            //}
            //var minVolume = productInfo.OutPackVolume <= 0 ? 1 : productInfo.OutPackVolume;
            //var usableList = GetListByUsable(minVolume);
            //var bestList = GetListByBest(productId, qty);
            //var list = new List<StockLocationProductInfo>();

            //foreach (var item in usableList)
            //{
            //    if (!bestList.Any(m => m.StockLocationId == item.StockLocationId))
            //    {
            //        list.Add(item);
            //    }
            //}

            //return list;

            #endregion
        }

        public IList<StockLocationProductInfo> GetListByUsable(Guid productId, double minVolume)
        {
            return dal.GetListByUsable(productId, minVolume);
        }

        public IList<StockLocationProductInfo> GetListForOrderSendProduct()
        {
            var list = dal.GetListForOrderSendProduct();
            if (list.Count > 0)
            {
                var pBll = new Product();
                var pList = pBll.GetListInIds(string.Join(",", list.Select(m => m.ProductId)));
                foreach (var item in list)
                {
                    var productInfo = pList.FirstOrDefault(m => m.Id.Equals(item.ProductId));
                    item.ProductCode = productInfo.ProductCode;
                    item.ProductName = productInfo.ProductName;
                }
            }

            return list;
        }

        public IList<StockLocationProductInfo> GetListForOrderPickProduct(Guid productId, double qty)
        {
            var list = new List<StockLocationProductInfo>();
            var oppList = dal.GetListForOrderPickProduct(productId).OrderByDescending(m => m.LastUpdatedDate);

            var totalQty = 0d;
            foreach (var item in oppList)
            {
                if (totalQty < qty)
                {
                    totalQty += item.MaxQty;
                    item.IsBest = true;
                }
                list.Add(item);
            }

            return list;
        }

        #endregion
    }
}
