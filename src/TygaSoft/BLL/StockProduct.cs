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
    public partial class StockProduct
    {
        #region StockProduct Member

        public IList<PandianProductInfo> GetPandianProductList(Guid pandianId, Guid userId, DateTime stockStartDate, DateTime stockEndDate, string customer, string zones, string stockLocations)
        {
            var list = new List<PandianProductInfo>();
            var slBll = new StockLocation();
            var slTempInfo = slBll.GetModelForTemp();
            var zBll = new Zone();

            var hasStartDate = (stockStartDate != DateTime.Parse("1754-01-01") && stockStartDate != DateTime.MinValue);
            var hasEndDate = (stockEndDate != DateTime.Parse("1754-01-01") && stockEndDate != DateTime.MinValue);

            string[] customers = null;
            if (!string.IsNullOrWhiteSpace(customer)) customers = customer.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if ((customers != null && customers.Length > 0) && (!hasStartDate && !hasEndDate && string.IsNullOrWhiteSpace(zones) && string.IsNullOrWhiteSpace(stockLocations)))
            {
                #region 获取只客户为条件的库存货品

                StringBuilder sqlWhere = null;
                SqlParameter parm = null;

                if (customers.Length == 1)
                {
                    sqlWhere = new StringBuilder(@"and sp.CustomerId = @CustomerId ");
                    parm = new SqlParameter("@CustomerId", SqlDbType.UniqueIdentifier);
                    parm.Value = Guid.Parse(customers[0]);
                }
                else
                {
                    sqlWhere = new StringBuilder(1200);
                    var sqlIn = new StringBuilder(1000);
                    foreach (var item in customers)
                    {
                        sqlIn.AppendFormat("'{0}',", item);
                    }
                    sqlWhere.AppendFormat("and sp.CustomerId in ({0})", sqlIn.ToString().Trim(','));
                }

                var spList = dal.GetListByJoin(sqlWhere.ToString(), parm);
                if (spList != null && spList.Count > 0)
                {
                    var zList = zBll.GetList();
                    var slList = slBll.GetList();
                    foreach (var item in spList)
                    {
                        var qty = 0d;
                        var sZones = new List<string>();

                        var pslList = JsonConvert.DeserializeObject<List<ProductStockLocationAttrInfo>>(item.StockLocations);
                        if (pslList != null && pslList.Count > 0)
                        {
                            pslList.RemoveAll(m => m.StockLocationId.Equals(slTempInfo.Id));
                            if (pslList.Count > 0)
                            {
                                qty = pslList.Sum(m => (m.Qty + m.FreezeQty));
                                var minSlList = new List<MinStockLocationInfo>();
                                foreach (var pslInfo in pslList)
                                {
                                    var slInfo = slList.FirstOrDefault(m => m.Id.Equals(pslInfo.StockLocationId));
                                    if (slInfo != null)
                                    {
                                        var zInfo = zList.FirstOrDefault(m => m.Id.Equals(slInfo.ZoneId));
                                        if (zInfo != null)
                                        {
                                            if (!sZones.Contains(zInfo.ZoneCode)) sZones.Add(zInfo.ZoneCode);
                                        }
                                        minSlList.Add(new MinStockLocationInfo(slInfo.Id, slInfo.Code, slInfo.Named, (pslInfo.Qty + pslInfo.FreezeQty)));
                                    }
                                }
                                qty = minSlList.Sum(m => m.Qty);

                                list.Add(new PandianProductInfo(pandianId, userId, item.ProductId, item.CustomerId, string.Join(",", sZones), JsonConvert.SerializeObject(minSlList), qty, "", "", 0, 0, EnumData.EnumOrderStatus.新建.ToString(), "", DateTime.Now));
                            }
                        }
                    }
                }

                #endregion
            }
            else
            {
                #region 获取可能包含入库时间、库区、库位为条件的库存货品

                var spList = dal.GetList();
                if (spList != null && spList.Count > 0)
                {
                    string[] slItems = null;
                    if (!string.IsNullOrWhiteSpace(stockLocations))
                    {
                        slItems = stockLocations.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    IList<StockLocationInfo> zslList = null;
                    if (!string.IsNullOrWhiteSpace(zones) && (slItems == null || slItems.Length == 0))
                    {
                        zslList = slBll.GetListInZoneIds(zones);
                    }

                    var zList = zBll.GetList();
                    var slList = slBll.GetList();

                    foreach (var item in spList)
                    {
                        var pslList = JsonConvert.DeserializeObject<List<ProductStockLocationAttrInfo>>(item.StockLocations);
                        if (pslList == null || pslList.Count == 0) continue;
                        pslList.RemoveAll(m => m.StockLocationId.Equals(slTempInfo.Id));
                        if (pslList.Count == 0) continue;

                        if (customers != null && customers.Length > 0 && pslList.Count > 0)
                        {
                            if (!customers.Contains(item.CustomerId.ToString())) continue;
                        }

                        if (hasStartDate)
                        {
                            pslList.RemoveAll(m => m.LastUpdatedDate < stockStartDate);
                        }
                        if (hasEndDate && pslList.Count > 0)
                        {
                            pslList.RemoveAll(m => m.LastUpdatedDate > stockStartDate);
                        }
                        if (slItems != null && slItems.Length > 0 && pslList.Count > 0)
                        {
                            pslList.RemoveAll(m => !slItems.Contains(m.StockLocationId.ToString()));
                        }
                        if (!string.IsNullOrWhiteSpace(zones) && (slItems != null && slItems.Length == 0) && pslList.Count > 0)
                        {
                            pslList.RemoveAll(m => !slList.Any(mm => mm.Id.Equals(m.StockLocationId)));
                        }
                        if (pslList.Count == 0) continue;

                        var sZones = new List<string>();
                        var minSlList = new List<MinStockLocationInfo>();
                        foreach (var pslInfo in pslList)
                        {
                            var slInfo = slList.FirstOrDefault(m => m.Id.Equals(pslInfo.StockLocationId));
                            if (slInfo != null)
                            {
                                var zInfo = zList.FirstOrDefault(m => m.Id.Equals(slInfo.ZoneId));
                                if (zInfo != null)
                                {
                                    if (!sZones.Contains(zInfo.ZoneCode)) sZones.Add(zInfo.ZoneCode);
                                }
                                minSlList.Add(new MinStockLocationInfo(slInfo.Id, slInfo.Code, slInfo.Named, (pslInfo.Qty + pslInfo.FreezeQty)));
                            }
                        }
                        var qty = minSlList.Sum(m => m.Qty);

                        list.Add(new PandianProductInfo(pandianId, userId, item.ProductId, item.CustomerId, string.Join(",", sZones), JsonConvert.SerializeObject(minSlList), qty, "", "", 0, 0, EnumData.EnumOrderStatus.新建.ToString(), "", DateTime.Now));
                    }
                }

                #endregion
            }

            return list;
        }

        public int UpdateWarnMsg(Guid productId, Guid customerId, string warnMsg)
        {
            return dal.UpdateWarnMsg(productId, customerId, warnMsg);
        }

        public void DoProduct(Guid productId, Guid customerId, int stepCode, Dictionary<Guid, float> dicSl)
        {
            var slpBll = new StockLocationProduct();
            foreach (KeyValuePair<Guid,float> kvp in dicSl)
            {
                DoProduct(productId, customerId, kvp.Key, stepCode, kvp.Value, 0, 0);
                slpBll.DoProduct(kvp.Key, productId, true, kvp.Value, 0);
            }
        }

        public void DoProduct(Guid productId, Guid customerId, int stepCode, bool isIncrease, Dictionary<Guid, float> dicSl)
        {
            var spBll = new StockProduct();
            var spInfo = spBll.GetModel(productId, customerId);
            if (spInfo == null) throw new ArgumentException(MC.M_StockProductInvalidError);
            var pslList = JsonConvert.DeserializeObject<List<ProductStockLocationAttrInfo>>(spInfo.StockLocations);
            var slpBll = new StockLocationProduct();

            if ((int)EnumData.EnumStep.拣货 == stepCode)
            {
                if (!isIncrease)
                {
                    foreach (KeyValuePair<Guid, float> kvp in dicSl)
                    {
                        var pslItem = pslList.FirstOrDefault(m => m.StockLocationId.Equals(kvp.Key));
                        if (pslItem == null) throw new ArgumentException(MC.M_StockProductInvalidError);
                        pslItem.FreezeQty -= kvp.Value;
                        if (pslItem.FreezeQty < 0) pslItem.FreezeQty = 0;
                        if (pslItem.Qty == 0 && pslItem.FreezeQty == 0) pslList.Remove(pslItem);
                        spInfo.FreezeQty -= kvp.Value;

                        var slpInfo = slpBll.GetModel(kvp.Key);
                        var slpList = JsonConvert.DeserializeObject<List<StockLocationProductAttrInfo>>(slpInfo.ProductAttr);
                        var slpItem = slpList.FirstOrDefault(m => m.ProductId.Equals(productId));
                        if (slpItem == null) throw new ArgumentException(MC.M_StockProductInvalidError);
                        slpItem.FreezeQty -= kvp.Value;
                        if (slpItem.FreezeQty < 0) slpItem.FreezeQty = 0;
                        slpItem.Qty -= kvp.Value;
                        if (slpItem.Qty < 0) slpItem.Qty = 0;
                        if (slpItem.Qty == 0 && slpItem.FreezeQty == 0) slpList.Remove(slpItem);
                        slpInfo.MaxVolume += kvp.Value;
                        slpInfo.ProductAttr = JsonConvert.SerializeObject(slpList);
                        slpBll.Update(slpInfo);
                    }
                }
            }

            spInfo.StepCode = Common.GetStepCode(spInfo.StepCode, stepCode.ToString(), false);
            spInfo.LastStepName = Enum.GetName(typeof(EnumData.EnumStep), stepCode);
            spInfo.StockLocations = JsonConvert.SerializeObject(pslList);
            spBll.Update(spInfo);
        }

        public void DoProduct(Guid productId, Guid customerId, int stepCode, bool isIncrease, double qty, double unQty, double freezeQty)
        {
            var spBll = new StockProduct();
            var slpBll = new StockLocationProduct();
            var spInfo = spBll.GetModel(productId, customerId);
            if (spInfo == null) return;

            var pslList = JsonConvert.DeserializeObject<List<ProductStockLocationAttrInfo>>(spInfo.StockLocations);

            if ((int)EnumData.EnumStep.发货 == stepCode)
            {
                if (!isIncrease)
                {
                    var qpslInfo = pslList.FirstOrDefault(m => m.FreezeQty >= freezeQty);
                    if (qpslInfo == null) throw new ArgumentException(MC.M_QtyInvalidError);
                    qpslInfo.FreezeQty -= freezeQty;
                    spInfo.FreezeQty -= freezeQty;
                    qpslInfo.Qty += freezeQty;
                    spInfo.Qty += freezeQty;
                    if (qpslInfo.Qty == 0 && qpslInfo.FreezeQty == 0) pslList.Remove(qpslInfo);

                    var slpInfo = slpBll.GetModel(qpslInfo.StockLocationId);
                    var slpList = JsonConvert.DeserializeObject<List<StockLocationProductAttrInfo>>(slpInfo.ProductAttr);
                    var slpItem = slpList.First(m => m.ProductId.Equals(productId));
                    slpItem.FreezeQty -= freezeQty;
                    if (slpItem.FreezeQty < 0) slpItem.FreezeQty = 0;
                    if (slpItem.Qty == 0 && slpItem.FreezeQty == 0) slpList.Remove(slpItem);
                    slpInfo.ProductAttr = JsonConvert.SerializeObject(slpList);
                    slpInfo.MaxVolume -= freezeQty;
                    slpBll.Update(slpInfo);
                }
            }
            spInfo.StockLocations = JsonConvert.SerializeObject(pslList);
            spBll.Update(spInfo);
        }

        public void DoProduct(Guid productId, Guid customerId, Guid slId, bool isIncrease, double qty, double unQty, double freezeQty, int stepCode, string status)
        {
            var spInfo = dal.GetModel(productId, customerId);
            var sStepCode = Common.GetStepCode(spInfo == null ? null : spInfo.StepCode, stepCode.ToString(), false);
            var sLastStepName = Enum.GetName(typeof(EnumData.EnumStep), stepCode);
            var currQty = 0d;
            var currFreezeQty = 0d;

            if (!isIncrease)
            {
                #region 对库存货物“减”处理

                if (spInfo != null)
                {
                    if (unQty > 0)
                    {
                        currQty += unQty;
                        spInfo.UnQty -= unQty;
                        if (spInfo.UnQty < 0) throw new ArgumentException(MC.M_QtyInvalidError);
                    }
                    var pslaList = JsonConvert.DeserializeObject<List<ProductStockLocationAttrInfo>>(spInfo.StockLocations);
                    var pslaInfo = pslaList.FirstOrDefault(m => m.StockLocationId.Equals(slId));
                    if (pslaInfo != null)
                    {
                        pslaInfo.Qty -= currQty;
                        pslaInfo.FreezeQty -= currFreezeQty;
                    }
                    spInfo.StockLocations = JsonConvert.SerializeObject(pslaList);

                    if (spInfo.StepCode == ((int)EnumData.EnumStep.收货).ToString() && spInfo.Qty == 0 && spInfo.UnQty == 0 && spInfo.FreezeQty == 0)
                    {
                        dal.Delete(productId, customerId);
                    }
                    else
                    {
                        dal.Update(spInfo);
                    }
                }

                #endregion
            }
            else
            {
                #region 对库存货物“增”处理

                if (spInfo != null)
                {
                    #region 已存在相应数据，则执行修改操作

                    if (unQty > 0)
                    {
                        spInfo.UnQty += unQty;
                        currQty += unQty;
                    }
                    var pslaList = JsonConvert.DeserializeObject<List<ProductStockLocationAttrInfo>>(spInfo.StockLocations);
                    var pslaInfo = pslaList.FirstOrDefault(m => m.StockLocationId.Equals(slId));
                    if (pslaInfo != null)
                    {
                        pslaInfo.Qty += unQty;
                    }
                    else
                    {
                        var slBll = new StockLocation();
                        var slInfo = slBll.GetModel(slId);
                        if (slInfo == null) throw new ArgumentException(MC.GetString(MC.Params_Data_NotExist, slId.ToString()));
                        pslaList.Add(new ProductStockLocationAttrInfo(slId, slInfo.Code,slInfo.Named, currQty, currFreezeQty, DateTime.Now));
                    }
                    spInfo.StockLocations = JsonConvert.SerializeObject(pslaList);

                    dal.Update(spInfo);

                    #endregion
                }
                else
                {
                    #region 否则执行新增操作

                    if (unQty > 0)
                    {
                        currQty += unQty;
                    }
                    var slBll = new StockLocation();
                    var slInfo = slBll.GetModel(slId);
                    if (slInfo == null) throw new ArgumentException(MC.GetString(MC.Params_Data_NotExist, slId.ToString()));

                    var pslaList = new List<ProductStockLocationAttrInfo>();
                    pslaList.Add(new ProductStockLocationAttrInfo(slId, slInfo.Code, slInfo.Named, currQty, currFreezeQty, DateTime.Now));

                    spInfo = new StockProductInfo(productId, customerId, qty, unQty, freezeQty, sStepCode, sLastStepName, status, JsonConvert.SerializeObject(pslaList), "", DateTime.Now);
                    dal.Insert(spInfo);

                    #endregion
                }

                #endregion
            }
        }

        public void DoProduct(Guid productId, Guid customerId, Guid slId, int stepCode, double qty, double unQty, double freezeQty)
        {
            var spBll = new StockProduct();
            var spInfo = spBll.GetModel(productId, customerId);
            if (spInfo == null) throw new ArgumentException(MC.M_StockProductInvalidError);
            var pslaList = JsonConvert.DeserializeObject<List<ProductStockLocationAttrInfo>>(spInfo.StockLocations);
            var currTime = DateTime.Now;

            if (stepCode == (int)EnumData.EnumStep.发货)
            {
                #region 发货

                var pslItem = pslaList.FirstOrDefault(m => m.StockLocationId.Equals(slId));
                if (pslItem == null) throw new ArgumentException(MC.GetString(MC.Params_Data_NotExist, "库位ID为“" + slId + "”"));
                pslItem.FreezeQty += freezeQty;
                pslItem.Qty -= freezeQty;
                if (pslItem.Qty == 0 && pslItem.FreezeQty == 0) pslaList.Remove(pslItem);
                spInfo.Qty -= freezeQty;
                spInfo.FreezeQty += freezeQty;

                #endregion
            }
            else if (stepCode == (int)EnumData.EnumStep.上架)
            {
                #region 上架

                var pslItem = pslaList.FirstOrDefault(m => m.StockLocationId.Equals(slId));
                if (pslItem == null)
                {
                    var slBll = new StockLocation();
                    var slInfo = slBll.GetModel(slId);
                    if (slInfo == null) throw new ArgumentException(MC.GetString(MC.Params_Data_NotExist, "库位ID为“" + slId + "”"));
                    pslaList.Add(new ProductStockLocationAttrInfo(slId, slInfo.Code, slInfo.Named, qty, 0, currTime));
                }
                else
                {
                    pslItem.Qty += qty;
                    if (pslItem.Qty == 0 && pslItem.FreezeQty == 0) pslaList.Remove(pslItem);
                }
                spInfo.Qty += qty;
                spInfo.UnQty -= qty;
                if (spInfo.UnQty < 0) throw new ArgumentException(MC.GetString(MC.Request_InvalidQty, qty.ToString()));

                #endregion
            }
            spInfo.StockLocations = JsonConvert.SerializeObject(pslaList);
            spBll.Update(spInfo);
        }

        public IEnumerable<StockProductInfo> GetSelectProductListByStepName(int pageIndex, int pageSize, string stepName, object productId, object customerId, double qty)
        {
            var sqlWhere = new StringBuilder(100);
            if (EnumData.EnumStep.发货.ToString() == stepName)
            {
                sqlWhere.Append("and sp.Qty > 0 ");
            }
            else if (EnumData.EnumStep.拣货.ToString() == stepName)
            {
                sqlWhere.AppendFormat("and sp.FreezeQty > 0 and ProductId = '{0}' and CustomerId = '{1}' ", productId, customerId);
            }

            var spList = new List<StockProductInfo>();
            var list = dal.GetListByJoin(pageIndex, pageSize, sqlWhere.ToString(), null);
            if (list != null && list.Count > 0)
            {
                var slBll = new StockLocation();
                var slTemp = slBll.GetModelForTemp();
                var slList = slBll.GetList();
                foreach (var item in list)
                {
                    var pslaList = JsonConvert.DeserializeObject<List<ProductStockLocationAttrInfo>>(item.StockLocations);
                    if (EnumData.EnumStep.发货.ToString() == stepName || EnumData.EnumStep.拣货.ToString() == stepName)
                    {
                        pslaList.RemoveAll(m => m.StockLocationId.Equals(slTemp.Id));
                    }
                    foreach (var pslaItem in pslaList)
                    {
                        if (EnumData.EnumStep.发货.ToString() == stepName)
                        {
                            if (pslaItem.Qty <= 0) continue;
                        }
                        else if (EnumData.EnumStep.拣货.ToString() == stepName)
                        {
                            if (pslaItem.FreezeQty <= 0) continue;
                        }
                        var slInfo = slList.FirstOrDefault(m => m.Id.Equals(pslaItem.StockLocationId));
                        if (slInfo == null) throw new ArgumentException(string.Format("{0}对应的库位数据不存在或已被删除", pslaItem.StockLocationId));
                        var spInfo = new StockProductInfo(item.ProductId, item.CustomerId, item.Qty, item.UnQty, item.FreezeQty, item.StepCode, item.LastStepName, item.Status, item.StockLocations, "", item.LastUpdatedDate);
                        spInfo.StockLocations = "";
                        spInfo.StockLocationId = slInfo.Id;
                        spInfo.StockLocationCode = slInfo.Code;
                        spInfo.StockLocationName = slInfo.Named;
                        spInfo.LastUpdatedDate = pslaItem.LastUpdatedDate;
                        spInfo.Qty = 0;
                        spInfo.ProductCode = item.ProductCode;
                        spInfo.ProductName = item.ProductName;
                        spInfo.CustomerCode = item.CustomerCode;
                        spInfo.CustomerName = item.CustomerName;

                        if (EnumData.EnumStep.拣货.ToString() == stepName) spInfo.MaxQty = pslaItem.FreezeQty;
                        else spInfo.MaxQty = pslaItem.Qty;

                        spList.Add(spInfo);
                    }
                }
                var q = spList.OrderBy(m => m.LastUpdatedDate);
                var totalQty = 0d;
                foreach (var item in q)
                {
                    if (totalQty >= qty) break;
                    item.IsBest = true;
                    totalQty += item.MaxQty;
                }
            }

            return spList;
        }

        public IList<StockProductInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<StockProductInfo> GetListByJoin(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public DataSet GetExportData()
        {
            return dal.GetExportData();
        }

        #endregion
    }
}
