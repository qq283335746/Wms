using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TygaSoft.SysException;
using TygaSoft.SysHelper;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.WcfModel;
using TygaSoft.DBUtility;
using TygaSoft.WebHelper;

namespace TygaSoft.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PdaService : IPda
    {
        #region MES

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveMesOrder(MesOrderModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var bll = new MesOrder();
                var effect = 0;
                var oldModel = bll.GetModel(model.OBarcode, model.PBarcode, model.PdBarcode, model.PtBarcode);
                if (oldModel != null)
                {
                    oldModel.Qty += model.Qty;
                    effect = bll.Update(oldModel);
                }
                else
                {
                    var currTime = DateTime.Now;
                    var modelInfo = new MesOrderInfo(Guid.Empty, WebCommon.GetUserId(), model.OBarcode, model.PBarcode, model.PdBarcode, model.PtBarcode, model.Qty, currTime, currTime, 0, "", currTime);
                    effect = bll.Insert(modelInfo);
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region RFID

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveRFIDQueue(RFIDModel model)
        {
            try
            {
                if(model == null) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                new ScanQueueAsyn().Insert(new RunQueueInfo("SaveRFID", JsonConvert.SerializeObject(model), "RFID", 0));

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 条码扫描上传服务器处理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveBarcodeScanQueue(BarcodeScanQueueModel model)
        {
            try
            {
                var list = new List<ComboboxInfo>();
                var bsaBll = new BarcodeScanAsyn();

                var sItemBody = HttpUtility.UrlDecode(model.ItemBody);

                new CustomException(sItemBody);

                if (model.From == EnumData.EnumStep.上架.ToString())
                {
                    List<ShelfMissionProductQueueInfo> smpList = JsonConvert.DeserializeObject<List<ShelfMissionProductQueueInfo>>(sItemBody);
                    if (smpList != null && smpList.Count > 0)
                    {
                        foreach (var item in smpList)
                        {
                            bsaBll.Insert(new BarcodeTypeInfo(model.From, JsonConvert.SerializeObject(item)));
                            list.Add(new ComboboxInfo { Id = item.Id.ToString(), Text = "1" });
                        }
                    }
                }

                return ResResult.Response(true, "", JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 物流公司查单信息

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetLogisticsDistributionByOrdercode(string OrderCode)
        {
            try
            {
                var bll = new LogisticsDistribution();
                var modelInfo = bll.GetModel(OrderCode);
                return ResResult.Response(true, "", JsonConvert.SerializeObject(modelInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveLogisticsDistribution(PdaLogisticsDistributionModel model)
        {
            try
            {
                new CustomException("SaveLogisticsDistribution--"+JsonConvert.SerializeObject(model));

                if (string.IsNullOrWhiteSpace(model.OrderCode) || string.IsNullOrWhiteSpace(model.Status)) throw new ArgumentException(MC.Request_Params_InvalidError);
                var bll = new LogisticsDistribution();
                var modelInfo = bll.GetModel(model.OrderCode);
                if(modelInfo == null) throw new ArgumentException(MC.Data_NotExist);
                modelInfo.Status = model.Status;
                if(bll.Update(modelInfo) < 1) throw new ArgumentException(MC.M_Save_Error);
                var ip = HttpClientHelper.GetClientIp(HttpContext.Current);
                var om = new OrderMap();
                var oldInfo = om.GetModel(model.OrderCode);
                if (oldInfo != null)
                {
                    var omInfo = new OrderMapInfo(oldInfo.UserId, oldInfo.OrderCode, model.Lnglat, ip, "", "", model.Platform, DateTime.Now);
                    om.Update(omInfo);
                }
                else
                {
                    var omInfo = new OrderMapInfo(Guid.Empty, model.OrderCode, model.Lnglat, ip, "", "", model.Platform, DateTime.Now);
                    om.Insert(omInfo);
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 收货

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrderReceiptRecord(PdaOrderReceiptRecordModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.OrderNum))
                {
                    return ResResult.Response(false, "收货单号不能为空字符串", null);
                }

                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                Guid orderId = Guid.Empty;
                Guid productId = Guid.Empty;
                Guid packageId = Guid.Empty;
                Guid stockLocationId = Guid.Empty;
                if (model.OrderId != null && !string.IsNullOrWhiteSpace(model.OrderId.ToString())) Guid.TryParse(model.OrderId.ToString(), out orderId);
                if (model.ProductId != null && !string.IsNullOrWhiteSpace(model.ProductId.ToString())) Guid.TryParse(model.ProductId.ToString(), out productId);
                if (model.PackageId != null && !string.IsNullOrWhiteSpace(model.PackageId.ToString())) Guid.TryParse(model.PackageId.ToString(), out packageId);
                if (model.StockLocationId != null && !string.IsNullOrWhiteSpace(model.StockLocationId.ToString())) Guid.TryParse(model.StockLocationId.ToString(), out stockLocationId);

                if (orderId.Equals(Guid.Empty)) return ResResult.Response(false, "参数值不正确，请正确操作", null);
                if (productId.Equals(Guid.Empty)) return ResResult.Response(false, "参数值不正确，请正确操作", null);
                if (packageId.Equals(Guid.Empty)) return ResResult.Response(false, "参数值不正确，请正确操作", null);
                if (stockLocationId.Equals(Guid.Empty)) return ResResult.Response(false, "参数值不正确，请正确操作", null);

                var currentTime = DateTime.Now;

                var modelInfo = new OrderReceiptRecordInfo();
                modelInfo.Id = Id;
                modelInfo.UserId = Guid.Empty;
                modelInfo.OrderId = orderId;
                modelInfo.ProductId = productId;
                modelInfo.PackageId = packageId;
                modelInfo.StockLocationId = stockLocationId;
                modelInfo.Unit = model.Unit;
                modelInfo.Qty = model.Qty;
                modelInfo.LPN = model.LPN;
                modelInfo.LastUpdatedDate = currentTime;

                var bll = new OrderReceiptRecord();
                int effect = -1;

                using (TransactionScope scope = new TransactionScope())
                {
                    if (Id.Equals(Guid.Empty))
                    {
                        bll.Insert(modelInfo);
                        bll.DoOrderReceipt(modelInfo);
                    }
                    else
                    {
                        bll.Update(modelInfo);
                        bll.DoOrderReceipt(modelInfo);
                    }

                    scope.Complete();
                    effect = 1;
                }

                if (effect < 1) return ResResult.Response(false, "调用失败，数据库操作异常", null);

                return ResResult.Response(true, "调用成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderIdByReceipt(string orderNum)
        {
            if (string.IsNullOrWhiteSpace(orderNum))
            {
                return ResResult.Response(false, "参数值不正确", "");
            }

            try
            {
                var bll = new OrderReceipt();
                return ResResult.Response(true, "调用成功", bll.GetOrderId(orderNum));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetSkuModel(object orderId, string productCode)
        {
            Guid gId = Guid.Empty;
            if (orderId != null) Guid.TryParse(orderId.ToString(), out gId);
            if (gId.Equals(Guid.Empty))
            {
                return ResResult.Response(false, "参数值不正确", "");
            }
            if (string.IsNullOrWhiteSpace(productCode))
            {
                return ResResult.Response(false, "参数值不正确", "");
            }
            var orpBll = new OrderReceiptProduct();
            var model = orpBll.GetModelByProductcode(gId, productCode);
            if (model == null) return ResResult.Response(false, "数据不存在或已被删除", "");

            var stlBll = new StockLocation();
            var stlModel = stlBll.GetList(1, 1, "", null).FirstOrDefault();
            if (stlModel == null) return ResResult.Response(false, "未找到任何库位", "");

            var skuModel = new PdaSkuModel();
            skuModel.ExpectedAmount = model.ExpectedQty;
            skuModel.ReceiptAmount = model.ReceiptQty;
            skuModel.ProductId = model.ProductId;
            skuModel.PackageId = model.PackageId;
            skuModel.Unit = model.Unit;
            skuModel.StockLocationId = stlModel.Id;
            skuModel.StockLocationCode = stlModel.Code;
            skuModel.StockLocationName = stlModel.Named;

            return ResResult.Response(true, "调用成功", JsonConvert.SerializeObject(skuModel));
        }

        #endregion

        #region 上架

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetShelfMissionList(PdaShelfMissionModel model)
        {
            try
            {
                var bll = new ShelfMission();
                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;

                if (!string.IsNullOrWhiteSpace(model.OrderCode))
                {
                    sqlWhere = new StringBuilder(300);
                    sqlWhere.AppendFormat("and OrderNum like @OrderNum ");
                    var parm = new SqlParameter("@OrderNum", SqlDbType.VarChar, 20);
                    parm.Value = "%" + model.OrderCode + "%";
                    parms = new ParamsHelper();
                    parms.Add(parm);
                }

                var list = bll.GetList(model.PageIndex, model.PageSize, sqlWhere == null ? "" : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetShelfMissionProductList(PdaShelfMissionProductModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new ShelfMissionProduct();

                var sqlWhere = "and ShelfMissionId = @ShelfMissionId ";
                var parm = new SqlParameter("@ShelfMissionId", model.ShelfMissionId);
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere, parm);

                return ResResult.Response(true, "", JsonConvert.SerializeObject(list));

                //throw new ArgumentException("该方法未实现");

                //if (model.PageIndex < 1) model.PageIndex = 1;
                //if (model.PageSize < 1) model.PageSize = 10;
                //var shelfMissionId = Guid.Parse(model.ShelfMissionId.ToString());

                //var list = new List<PdaShelfMissionProductInfo>();

                //var spBll = new StockProduct();
                //var slBll = new StockLocation();
                //var bll = new ShelfMissionProduct();

                //var scanningList = spBll.GetProductsByStep(1, 100000, EnumData.EnumStep.收货.ToString());
                //if (scanningList != null && scanningList.Count > 0)
                //{
                //    foreach (var item in scanningList)
                //    {
                //        var stockLocationList = new List<PdaStockLocationProductInfo>();
                //        var bestStockLocation = slBll.GetListByBest(item.ProductId, item.Qty);
                //        if (bestStockLocation != null && bestStockLocation.Count > 0)
                //        {
                //            foreach (var slItem in bestStockLocation)
                //            {
                //                stockLocationList.Add(new PdaStockLocationProductInfo(slItem.Text, 0));
                //            }
                //        }

                //        list.Add(new PdaShelfMissionProductInfo(item.ProductId, item.ProductCode, item.ProductName, item.Qty, (int)EnumData.EnumStep.收货, stockLocationList));
                //    }
                //}

                //var scannedList = bll.GetListByScanned(shelfMissionId);
                //if (scannedList != null && scannedList.Count > 0)
                //{
                //    foreach (var item in scannedList)
                //    {
                //        var stockLocationList = new List<PdaStockLocationProductInfo>();
                //        var bestStockLocation = slBll.GetListByBest(item.ProductId, item.Qty);
                //        if (bestStockLocation != null && bestStockLocation.Count > 0)
                //        {
                //            foreach (var slItem in bestStockLocation)
                //            {
                //                stockLocationList.Add(new PdaStockLocationProductInfo(slItem.Text, 0));
                //            }
                //        }

                //        list.Add(new PdaShelfMissionProductInfo(item.ProductId, item.ProductCode, item.ProductName, item.Qty, (int)EnumData.EnumStep.上架, stockLocationList));
                //    }
                //}

                //var data = list.Skip(((model.PageIndex - 1)*model.PageSize)).Take(model.PageSize);

                //return ResResult.Response(true, "", JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveShelfMissionProduct(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);

                var bll = new ShelfMissionProduct();
                bll.DoShelfMissionProduct(itemAppend);

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 发货

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderSendList(PdaOrderSendModel model)
        {
            try
            {
                var bll = new OrderSend();
                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;

                var list = bll.GetList(model.PageIndex, model.PageSize, sqlWhere == null ? "" : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 拣货

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderPickedList(OrderSendModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new OrderPicked();

                var sb = new StringBuilder(300);
                var parms = new ParamsHelper();
                if (!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    sb.Append("and (op.OrderCode like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                    parm.Value = model.Keyword;
                    parms.Add(parm);
                }
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sb.ToString(), parms.ToArray());

                return ResResult.Response(true, "", JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderPickProductList(PdaOrderPickProductModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                var bll = new OrderPickProduct();

                var sqlWhere = "and op.Id = @Id ";
                var parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(model.Id.ToString());
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, sqlWhere, parm);

                return ResResult.Response(true, "", JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrderPickProduct(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var bll = new OrderPickProduct();
                bll.DoOrderPickProduct(itemAppend);

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 盘点

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianList(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                var bll = new Pandian();

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, "", null);

                return ResResult.Response(true, "", JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianProductList(ListModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                var bll = new PandianProduct();
                int[] totals = null;

                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;
                if (model.ParentId != null)
                {
                    var pandianId = Guid.Parse(model.ParentId.ToString());
                    parms = new ParamsHelper();
                    sqlWhere = new StringBuilder(100);
                    sqlWhere.AppendFormat("and pdp.PandianId = @PandianId ");
                    var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
                    parm.Value = pandianId;
                    parms.Add(parm);

                    totals = bll.GetTotal(pandianId);
                }

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", "{\"rows\":" + JsonConvert.SerializeObject(list) + ",\"footer\":{\"TotalPan\":" + totals[0] + ",\"TotalYpan\":" + totals[1] + ",\"TotalNotPan\":" + totals[2] + "}}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePandianProduct(PandianProductFmModel model)
        {
            try
            {
                var bll = new PandianProduct();
                int effect = -1;

                var oldInfo = bll.GetModel(model.PandianId, model.ProductId, model.CustomerId);
                if (oldInfo == null) throw new ArgumentException(MC.Data_NotExist);

                oldInfo.UserId = WebCommon.GetUserId();
                oldInfo.Qty = model.Qty;
                oldInfo.FailQty = oldInfo.StayQty - model.Qty;
                oldInfo.UpdatedZones = model.Zones;
                oldInfo.UpdatedStockLocations = HttpUtility.UrlDecode(model.StockLocations);
                if (oldInfo.Qty > 0)
                {
                    if (oldInfo.FailQty == 0) oldInfo.Status = EnumData.EnumOrderStatus.已完成.ToString();
                    else oldInfo.Status = EnumData.EnumOrderStatus.待完成.ToString();
                }
                oldInfo.LastUpdatedDate = DateTime.Now;
                effect = bll.Update(oldInfo);
                effect += new Pandian().UpdateStatus(oldInfo.PandianId, EnumData.EnumOrderStatus.待完成.ToString());

                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 库位货品

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetStockLocationProductList(StockLocationProductModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;

                if (model.KeyName == "OrderSendProduct" || model.KeyName == "OrderPickProduct")
                {
                    var spBll = new StockProduct();
                    if (model.KeyName == "OrderSendProduct")
                    {
                        var ospList = spBll.GetSelectProductListByStepName(model.PageIndex, model.PageSize, EnumData.EnumStep.发货.ToString(), null, null, model.Qty);
                        return ResResult.Response(true, "", JsonConvert.SerializeObject(ospList));
                    }
                    else if (model.KeyName == "OrderPickProduct")
                    {
                        var oppslList = spBll.GetSelectProductListByStepName(model.PageIndex, model.PageSize, EnumData.EnumStep.拣货.ToString(), model.ProductId, model.CustomerId, model.Qty);
                        return ResResult.Response(true, "", JsonConvert.SerializeObject(oppslList));
                    }
                }

                var slpBll = new StockLocationProduct();
                switch (model.KeyName)
                {
                    case "ShelfMissionProduct":
                        var smpList = slpBll.GetListForShelfMissionProduct(Guid.Parse(model.ProductId.ToString()), model.Qty);
                        return ResResult.Response(true, "", JsonConvert.SerializeObject(smpList));
                    default:
                        throw new ArgumentException(MC.GetString(MC.Params_SwitchNameNotExist, model.KeyName));
                }
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 库存

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetStockProductList(StockProductModel model)
        {
            try
            {
                ParamsHelper parms = new ParamsHelper();
                StringBuilder sqlWhere = null;

                var startDate = DateTime.MinValue;
                var endDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(model.StartDate)) DateTime.TryParse(model.StartDate, out startDate);
                if (!string.IsNullOrWhiteSpace(model.EndDate)) DateTime.TryParse(model.EndDate, out endDate);

                #region 查询条件

                if (!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                    sqlWhere.Append("and (p.ProductCode like @Keyword or p.ProductName like @Keyword or c.Coded like @Keyword or c.Named like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                    parm.Value = "%" + model.Keyword + "%";
                    parms.Add(parm);
                }

                if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                    sqlWhere.AppendFormat("and sp.LastUpdatedDate between @StartDate and @EndDate ");
                    var parm = new SqlParameter("@StartDate", startDate);
                    parms.Add(parm);
                    parm = new SqlParameter("@EndDate", DateTime.Parse(string.Format("{0} 23:59:59", model.EndDate)));
                    parms.Add(parm);
                }
                else
                {
                    if (startDate != DateTime.MinValue)
                    {
                        if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                        sqlWhere.AppendFormat("and sp.LastUpdatedDate >= @StartDate ");
                        var parm = new SqlParameter("@StartDate", startDate);
                        parms.Add(parm);
                    }
                    else if (endDate != DateTime.MinValue)
                    {
                        if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                        sqlWhere.AppendFormat("and sp.LastUpdatedDate <= @EndDate ");
                        var parm = new SqlParameter("@EndDate", DateTime.Parse(string.Format("{0} 23:59:59", model.EndDate)));
                        parms.Add(parm);
                    }
                }

                #endregion

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new StockProduct();
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? "" : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, null, JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 包装

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public IList<ComboboxInfo> GetCbbPackage()
        {
            try
            {
                int totalRecord = 0;
                var bll = new Package();

                var list = bll.GetListByJoin(1, 100, out totalRecord, "", null);
                var cbbList = new List<ComboboxInfo>();
                foreach (var model in list)
                {
                    cbbList.Add(new ComboboxInfo { Id = model.Id.ToString(), Text = model.PackageCode });
                }

                return cbbList;
            }
            catch
            {
                return new List<ComboboxInfo>();
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public IList<ComboboxInfo> GetCbbUnit()
        {
            try
            {
                var list = EnumHelper.GetList(typeof(EnumData.EnumUnitLevel));
                var cbbList = new List<ComboboxInfo>();
                foreach (var model in list)
                {
                    cbbList.Add(new ComboboxInfo { Id = model.Key, Text = model.Value });
                }

                return cbbList;
            }
            catch
            {
                return new List<ComboboxInfo>();
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPackageList(DlgPackageModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new Package();

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, "", null);

                return ResResult.Response(true, "调用成功", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

    }
}
