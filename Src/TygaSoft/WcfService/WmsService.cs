using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TygaSoft.CustomProvider;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;
using TygaSoft.DBUtility;
using TygaSoft.Model;
using TygaSoft.WcfModel;
using TygaSoft.BLL;

namespace TygaSoft.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WmsService : IWms
    {
        #region 通知

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetWechatUserList()
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);
                var res = HttpHelper.DoGet("https://wx.prefercode.cc/api/v2/user");
                JObject o = JObject.Parse(res);
                return ResResult.Response(true, "", o["result"].ToString());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SendStockProductNotice(StockProductNoticeModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);
                var content = new Dictionary<string,object>();
                content.Add("to", model.OpenId);
                content.Add("time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                content.Add("procode", model.Coded);
                content.Add("proname", model.Named);
                content.Add("customer", model.CustomerName);
                content.Add("quantity", model.Qty);

                var res = HttpHelper.DoPost("https://wx.prefercode.cc/api/v2/message",JsonConvert.SerializeObject(content),"", "application/json");
                return ResResult.Response(true, "", res);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 打印、条码

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetBarcodeTemplateInfo(BarcodeTemplateFmModel model)
        {
            try
            {
                var bh = new BarcodeHelper();
                var data = bh.GetBarcodeTemplateInfo();

                return ResResult.Response(true, "", JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetBarcodeBrowser(BarcodeTemplateFmModel model)
        {
            try
            {
                var barcodeInfo = new BarcodeInfo(model.Barcode, model.BarcodeFormat, model.Width, model.Height, model.Margin, "");
                return ResResult.Response(true, "", new BarcodeHelper().CreateBarcodeBrowser(barcodeInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetBarcodeTemplateList(ListModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;

                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;

                if (!string.IsNullOrWhiteSpace(model.TypeName))
                {
                    sqlWhere = new StringBuilder(100);
                    sqlWhere.Append("and TypeName = @TypeName ");
                    var parm = new SqlParameter("@TypeName", SqlDbType.NVarChar, 20);
                    parm.Value = model.TypeName;
                    parms = new ParamsHelper();
                    parms.Add(parm);
                }

                var bll = new BarcodeTemplate();
                var list = bll.GetList(model.PageIndex, model.PageSize, sqlWhere == null ? "" : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + list.Count + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveBarcodeTemplate(BarcodeTemplateFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                if (Id.Equals(Guid.Empty)) model.Id = Guid.NewGuid();
                var userId = WebCommon.GetUserId();

                BarcodeTemplateInfo modelInfo = null;

                if (model.TypeName == "Barcode")
                {
                    var barcodeInfo = new BarcodeInfo(model.Barcode, model.BarcodeFormat, model.Width, model.Height, model.Margin, "");
                    barcodeInfo.ImageUrl = new BarcodeHelper().CreateBarcode(barcodeInfo, model.Id.ToString(), true);
                    modelInfo = new BarcodeTemplateInfo(Guid.Parse(model.Id.ToString()), userId, model.Title, JsonConvert.SerializeObject(barcodeInfo), model.IsDefault, model.TypeName, DateTime.Now);
                }
                else
                {
                    modelInfo = new BarcodeTemplateInfo(Guid.Parse(model.Id.ToString()), userId, model.Title, HttpUtility.UrlDecode(model.JContent), model.IsDefault, model.TypeName, DateTime.Now);
                }

                var bll = new BarcodeTemplate();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteBarcodeTemplate(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var bll = new BarcodeTemplate();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveSetDefault(BarcodeTemplateFmModel model)
        {
            try
            {
                if (model.Id.Equals(Guid.Empty)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                var bll = new BarcodeTemplate();
                var effect = bll.SetDefault(Guid.Parse(model.Id.ToString()), model.IsDefault, model.TypeName);
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 预收货单、收货单

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCbgOrderReceipt(OrderReceiptModel model)
        {
            try
            {
                var bll = new OrderReceipt();
                int totalRecord = 0;

                var list = bll.GetCbgOrderReceipt(model.PageIndex, model.PageSize, out totalRecord, model.OrderType);

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetOrderReceiptInfo(Guid Id)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                var bll = new OrderReceipt();
                var modelInfo = bll.GetModelByJoin(Id);
                return ResResult.Response(true, "", JsonConvert.SerializeObject(modelInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderReceiptList(OrderReceiptModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new OrderReceipt();

                var sb = new StringBuilder(300);
                var parms = new ParamsHelper();
                sb.Append("and OrderType = " + model.OrderType + "");
                if (!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    sb.Append("and (orb.OrderCode like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                    parm.Value = model.Keyword;
                    parms.Add(parm);
                }
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sb.ToString(), parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrderReceipt(OrderReceiptFmModel model)
        {
            try
            {
                #region 请求参数处理

                if (!string.IsNullOrWhiteSpace(model.CustomAttr))
                {
                    model.CustomAttr = HttpUtility.UrlDecode(model.CustomAttr);
                }

                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                Guid customerId = Guid.Empty;
                Guid supplierId = Guid.Empty;
                if (model.CustomerId != null && !string.IsNullOrWhiteSpace(model.CustomerId.ToString())) Guid.TryParse(model.CustomerId.ToString(), out customerId);
                if (model.SupplierId != null && !string.IsNullOrWhiteSpace(model.SupplierId.ToString())) Guid.TryParse(model.SupplierId.ToString(), out supplierId);
                var currentTime = DateTime.Now;
                var userId = WebCommon.GetUserId();

                var recordDate = string.IsNullOrWhiteSpace(model.RecordDate) ? currentTime : DateTime.Parse(model.RecordDate);
                var settlementDate = string.IsNullOrWhiteSpace(model.SettlementDate) ? currentTime : DateTime.Parse(model.SettlementDate);

                var modelInfo = new OrderReceiptInfo(Id, userId, customerId, supplierId, model.OrderCode, model.OrderType, model.PreOrderCode, model.PurchaseOrderCode, model.TypeName, settlementDate, recordDate, false, (byte)EnumData.EnumOrderStatus.新建, model.Sort, model.Remark, currentTime);

                var oraModel = new OrderReceiptAttrInfo();
                oraModel.OrderId = Id;
                var dateTime = DateTime.MinValue;
                DateTime.TryParse(model.LastTakeDate, out dateTime);
                if (dateTime == DateTime.MinValue) dateTime = currentTime;
                oraModel.LastTakeDate = dateTime;

                DateTime.TryParse(model.ExpectTakeDate, out dateTime);
                if (dateTime == DateTime.MinValue) dateTime = currentTime;
                oraModel.ExpectTakeDate = dateTime;

                DateTime.TryParse(model.SendDate, out dateTime);
                if (dateTime == DateTime.MinValue) dateTime = currentTime;
                oraModel.SendDate = dateTime;

                DateTime.TryParse(model.PlanSendDate, out dateTime);
                if (dateTime == DateTime.MinValue) dateTime = currentTime;
                oraModel.PlanSendDate = dateTime;

                oraModel.RMA = model.RMA;
                oraModel.ExpectVolume = model.ExpectVolume;
                oraModel.GW = model.GW;
                oraModel.CustomAttr = model.CustomAttr;

                #endregion

                var bll = new OrderReceipt();
                var oraBll = new OrderReceiptAttr();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    var rcBll = new RandomCode();
                    string prefix = "";
                    switch (model.OrderType)
                    {
                        case 1:
                            prefix = ((int)EnumData.EnumOrderPrefix.预收货).ToString();
                            break;
                        case 2:
                            prefix = ((int)EnumData.EnumOrderPrefix.收货).ToString();
                            break;
                        default:
                            break;
                    }

                    var orderCode = rcBll.GetOrderCode(prefix);
                    modelInfo.OrderCode = orderCode;

                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                    modelInfo.Id = Guid.NewGuid();
                    effect = bll.InsertByOutput(modelInfo);

                    oraModel.OrderId = modelInfo.Id;
                    effect += oraBll.InsertByOutput(oraModel);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                    effect = bll.Update(modelInfo);
                    effect += oraBll.Update(oraModel);
                }

                #region 关联预收货单明细

                if (!string.IsNullOrWhiteSpace(model.PreOrderCode) && model.OrderType == (int)EnumData.EnumStep.收货)
                {
                    var orpBll = new OrderReceiptProduct();
                    var orpaBll = new OrderReceiptProductAttr();
                    var orpqBll = new OrderReceiptProductQuality();

                    var orpList = orpBll.GetListByOrderId(modelInfo.Id);
                    var preOrpList = orpBll.GetListJoinByOrderCode(model.PreOrderCode);

                    if (preOrpList.Count > 0 && orpList.Count > 0)
                    {
                        foreach (var item in orpList)
                        {
                            var oldItem = preOrpList.FirstOrDefault(m => m.ProductId.Equals(item.ProductId));
                            if (oldItem != null) preOrpList.Remove(oldItem);
                        }
                    }

                    if (preOrpList.Count > 0)
                    {
                        foreach (var item in preOrpList)
                        {
                            var orpId = Guid.NewGuid();
                            var orpInfo = new OrderReceiptProductInfo(orpId, userId, modelInfo.Id, item.ProductId, item.PackageId, item.Unit, item.ExpectedQty, item.ReceiptQty, item.RecordDate, model.PreOrderCode, item.PurchaseOrderCode, item.Status, item.Sort, item.Remark, item.LastUpdatedDate);
                            var orpaInfo = new OrderReceiptProductAttrInfo(orpId, item.PackageName, item.SupplierName, item.ProduceDate, item.QualityStatus, item.PurchaseOrderCode);
                            var orpqInfo = new OrderReceiptProductQualityInfo(orpId, item.CheckQuantity, item.RejectQuantity, item.QCStatus, item.IsQCNeed);

                            orpBll.InsertByOutput(orpInfo);
                            orpaBll.Insert(orpaInfo);
                            orpqBll.Insert(orpqInfo);
                        }
                    }

                    bll.SetNext(Guid.Empty, model.PreOrderCode, true);
                }

                #endregion

                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, MC.M_Save_Ok, modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteOrderReceipt(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var bll = new OrderReceipt();
                var list = new List<object>();
                foreach (var item in items)
                {
                    if (bll.IsExistProduct(item)) throw new ArgumentException(MC.M_OrderExistProductError);
                    list.Add(item);
                }

                if (!bll.DeleteBatch(list)) return ResResult.Response(false, MC.M_Save_Error, null);
                return ResResult.Response(true, MC.M_Save_Ok, null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderReceiptProductList(OrderReceiptProductModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new OrderReceiptProduct();
                IList<OrderReceiptProductInfo> list = null;
                if (model.OrderId != null)
                {
                    list = bll.GetListByOrder(model.PageIndex, model.PageSize, out totalRecord, model.OrderId);
                    var slBll = new StockLocation();

                    var taskList = new List<Task>();
                    foreach (var item in list)
                    {
                        var task = Task.Factory.StartNew(() =>
                        {
                            var bestArr = slBll.GetListByBest(item.ProductId, item.ExpectedQty);
                            if (bestArr != null && bestArr.Count > 0)
                            {
                                foreach (var subItem in bestArr)
                                {
                                    item.StockLocationAppendId += subItem.Id + "|";
                                    item.StockLocationAppendName += subItem.Text + "|";
                                }
                                item.StockLocationAppendId = item.StockLocationAppendId.Trim('|');
                                item.StockLocationAppendName = item.StockLocationAppendName.Trim('|');
                                item.StockLocationJson = JsonConvert.SerializeObject(slBll.GetListByBest(item.ProductId, item.ExpectedQty));
                            }
                        });
                        taskList.Add(task);
                    }
                    Task.WaitAll(taskList.ToArray());
                }
                else
                {
                    var sb = new StringBuilder(300);
                    sb.AppendFormat("and o.OrderType = {0} and ReceiptQty > 0 ", (int)EnumData.EnumStep.收货);
                    var parms = new ParamsHelper();
                    if (!string.IsNullOrWhiteSpace(model.Keyword))
                    {
                        sb.Append("and (o.OrderCode like @Keyword or orp.PreOrderCode like @Keyword or p.ProductCode like @Keyword or p.ProductName like @Keyword) ");
                        var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                        parm.Value = model.Keyword;
                        parms.Add(parm);
                    }
                    list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sb.ToString(), parms == null ? null : parms.ToArray());
                }

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrderReceiptProduct(OrderReceiptProductFmModel model)
        {
            try
            {
                if (model.PreOrderCode == null) model.PreOrderCode = "";
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                Guid orderId = Guid.Empty;
                if (model.OrderId != null) Guid.TryParse(model.OrderId.ToString(), out orderId);
                if (orderId.Equals(Guid.Empty)) return ResResult.Response(false, MC.M_Order_NotExist, null);

                Guid productId = Guid.Empty;
                Guid packageId = Guid.Empty;
                if (model.ProductId != null && !string.IsNullOrWhiteSpace(model.ProductId.ToString())) Guid.TryParse(model.ProductId.ToString(), out productId);
                if (model.PackageId != null && !string.IsNullOrWhiteSpace(model.PackageId.ToString())) Guid.TryParse(model.PackageId.ToString(), out packageId);
                var currentTime = DateTime.Now;
                var userId = WebCommon.GetUserId();

                var pBll = new Product();
                var productInfo = pBll.GetModel(productId);
                if (productInfo == null) return ResResult.Response(false, MC.GetString(MC.Request_NotExist, "“" + productId + "”对应的货物"), null);
                var minVolume = productInfo.OutPackVolume == 0 ? 1 : productInfo.OutPackVolume;

                var oBll = new OrderReceipt();
                var orderInfo = oBll.GetModel(orderId);
                if (orderInfo == null) return ResResult.Response(false, MC.GetString(MC.Params_OrderProductPassError, productInfo.ProductCode, model.ReceiptQty.ToString()), null);

                var orpBll = new OrderReceiptProduct();
                var orpaBll = new OrderReceiptProductAttr();
                var orpqBll = new OrderReceiptProductQuality();

                var modelInfo = new OrderReceiptProductInfo(Id, userId, orderId, productId, packageId, model.Unit, model.ExpectedQty, model.ReceiptQty, model.RecordDate, model.PreOrderCode, model.PurchaseOrderCode, model.Status, model.Sort, model.Remark, currentTime);
                OrderReceiptProductInfo oldOrpInfo = null;
                if (!Id.Equals(Guid.Empty))
                {
                    oldOrpInfo = orpBll.GetModel(orderId, productId);
                    modelInfo.PreOrderCode = oldOrpInfo.PreOrderCode;
                    modelInfo.RecordDate = oldOrpInfo.RecordDate;
                }
                var orpaModel = new OrderReceiptProductAttrInfo(Guid.Empty, model.PackageName, model.SupplierName, model.ProduceDate, model.QualityStatus, model.ProductAttrPurchaseOrderCode);
                var orpqModel = new OrderReceiptProductQualityInfo(Guid.Empty, model.CheckQuantity, model.RejectQuantity, model.QCStatus, model.IsQCNeed);

                var stepCode = (int)EnumData.EnumStep.收货;

                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                    modelInfo.RecordDate = currentTime;
                    modelInfo.Id = Guid.NewGuid();
                    effect = orpBll.InsertByOutput(modelInfo);

                    orpaModel.OrderProductId = modelInfo.Id;
                    orpqModel.OrderProductId = modelInfo.Id;
                    effect += orpaBll.Insert(orpaModel);
                    effect += orpqBll.Insert(orpqModel);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                    effect = orpBll.Update(modelInfo);
                    orpaModel.OrderProductId = modelInfo.Id;
                    orpqModel.OrderProductId = modelInfo.Id;
                    effect += orpaBll.Update(orpaModel);
                    effect += orpqBll.Update(orpqModel);
                }

                if (orderInfo.OrderType == (int)EnumData.EnumStep.收货)
                {
                    #region 库存库位操作

                    var slBll = new StockLocation();
                    var tempSLInfo = slBll.GetModelForTemp();
                    if (tempSLInfo == null) throw new ArgumentException(MC.GetString(MC.Params_Data_NotExist, "暂存区库位"));

                    var increaseQty = model.ReceiptQty;
                    if (!Id.Equals(Guid.Empty))
                    {
                        increaseQty = model.ReceiptQty - oldOrpInfo.ReceiptQty;
                    }
                    var spBll = new StockProduct();
                    spBll.DoProduct(productId, orderInfo.CustomerId, tempSLInfo.Id, increaseQty >= 0, 0, increaseQty >= 0 ? increaseQty : -increaseQty, 0, stepCode, EnumData.EnumStatus.正常.ToString());

                    var slpBll = new StockLocationProduct();
                    slpBll.DoProduct(tempSLInfo.Id, productId, increaseQty >= 0, increaseQty >= 0 ? increaseQty : -increaseQty, 0);

                    #endregion
                }

                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteOrderReceiptProduct(Guid orderId, string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var oBll = new OrderReceipt();
                var orderInfo = oBll.GetModel(orderId);
                var isReceiptOrder = orderInfo.OrderType == (int)EnumData.EnumStep.收货;

                var orpBll = new OrderReceiptProduct();
                StockProduct spBll = null;
                StockLocation slBll = null;
                StockLocationProduct slpBll = null;
                StockLocationInfo slInfo = null;
                if (isReceiptOrder)
                {
                    spBll = new StockProduct();
                    slBll = new StockLocation();
                    slpBll = new StockLocationProduct();
                    slInfo = slBll.GetModelForTemp();
                }

                foreach (var item in items)
                {
                    var Id = Guid.Parse(item);
                    if (!isReceiptOrder)
                    {
                        orpBll.Delete(Id);
                    }
                    else
                    {
                        var orpInfo = orpBll.GetModel(Id);
                        if (orpInfo.ReceiptQty > 0)
                        {
                            spBll.DoProduct(orpInfo.ProductId, orderInfo.CustomerId, slInfo.Id, false, 0, orpInfo.ReceiptQty, 0, (int)EnumData.EnumStep.收货, EnumData.EnumStatus.正常.ToString());
                            slpBll.DoProduct(slInfo.Id, orpInfo.ProductId, false, orpInfo.ReceiptQty, 0);
                        }

                        orpBll.Delete(Id);
                    }
                }

                return ResResult.Response(true, MC.M_Save_Ok, null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel UpdateOrderReceiptProduct(UpdateOrderReceiptProductFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                if (Id.Equals(Guid.Empty)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                var bll = new OrderReceiptProduct();
                var oldModel = bll.GetModel(Id);
                if (oldModel == null) return ResResult.Response(false, MC.Data_NotExist, null);
                oldModel.ReceiptQty = model.ReceiptAmount;
                oldModel.LastUpdatedDate = DateTime.Now;

                int effect = bll.Update(oldModel);

                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, MC.Response_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCbbOrderReceipt()
        {
            try
            {
                var cbbData = new List<ComboboxInfo>();
                cbbData.Add(new ComboboxInfo { Id = "-1", Text = "请选择" });

                var bll = new OrderReceipt();
                var list = bll.GetList();

                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        cbbData.Add(new ComboboxInfo { Id = item.Id.ToString(), Text = item.OrderCode });
                    }
                }

                return ResResult.Response(true, "", JsonConvert.SerializeObject(cbbData));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 上架任务

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetShelfMissionInfo(Guid Id)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                var bll = new ShelfMission();
                var modelInfo = bll.GetModel(Id);
                return ResResult.Response(true, "", JsonConvert.SerializeObject(modelInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetShelfMissionList(ShelfMissionModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new ShelfMission();

                var sb = new StringBuilder(300);
                var parms = new ParamsHelper();
                if (!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    sb.Append("and (sm.OrderCode like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                    parm.Value = model.Keyword;
                    parms.Add(parm);
                }
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sb.ToString(), parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel CreateShelfMission(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                var oBll = new OrderReceipt();
                var opBll = new OrderReceiptProduct();
                var slBll = new StockLocation();

                var smpList = new List<ShelfMissionProductInfo>();
                var currTime = DateTime.Now;
                var userId = WebCommon.GetUserId();
                var shelfMissionId = Guid.NewGuid();

                foreach (var item in items)
                {
                    var orderId = Guid.Parse(item);
                    var orderReceiptProductList = opBll.GetListByOrderId(orderId);
                    foreach (var orpItem in orderReceiptProductList)
                    {
                        if (orpItem.ReceiptQty <= 0)
                        {
                            var orderReceiptInfo = oBll.GetModel(orderId);
                            throw new ArgumentException(MC.GetString(MC.Params_OrderProductInvalidError, orderReceiptInfo.OrderCode));
                        }
                        var smInfo = new ShelfMissionProductInfo(shelfMissionId, orderId, orpItem.ProductId, orpItem.ReceiptQty, 0, "[]", EnumData.EnumStatus.正常.ToString(), currTime);

                        smpList.Add(smInfo);
                    }
                }

                var effect = 0;
                if (smpList.Count > 0)
                {
                    var rcBll = new RandomCode();
                    var orderCode = rcBll.GetOrderCode(((int)EnumData.EnumOrderPrefix.上架).ToString());

                    var smBll = new ShelfMission();
                    var smpBll = new ShelfMissionProduct();
                    var smInfo = new ShelfMissionInfo(shelfMissionId, userId, orderCode, 0, 0, EnumData.EnumOrderStatus.新建.ToString(), "", 0, false, currTime);

                    smBll.InsertByOutput(smInfo);
                    foreach (var smpItem in smpList)
                    {
                        effect += smpBll.Insert(smpItem);
                    }
                    foreach (var item in items)
                    {
                        oBll.SetNext(Guid.Parse(item), "", true);
                    }

                    smBll.SetTotalProduct(orderCode);
                }

                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveShelfMission(ShelfMissionFmModel model)
        {
            try
            {
                var Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);

                var modelInfo = new ShelfMissionInfo(Id, WebCommon.GetUserId(), model.OrderCode, 0, 0, EnumData.EnumOrderStatus.新建.ToString(), model.Remark, 0, false, DateTime.Now);

                var bll = new ShelfMission();
                int effect = -1;

                if (!Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                    var rcBll = new RandomCode();
                    var orderCode = rcBll.GetOrderCode(((int)EnumData.EnumOrderPrefix.上架).ToString());
                    modelInfo.OrderCode = orderCode;
                    modelInfo.Id = Guid.NewGuid();

                    effect = bll.InsertByOutput(modelInfo);

                    bll.SetTotalProduct(modelInfo.OrderCode);
                }

                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, MC.M_Save_Ok, modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteShelfMission(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var bll = new ShelfMission();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, null);
                }

                return ResResult.Response(true, MC.M_Save_Ok, null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetShelfMissionProductList(ShelfMissionProductModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new ShelfMissionProduct();

                var sqlWhere = "and ShelfMissionId = @OrderId ";
                var parm = new SqlParameter("@OrderId", model.OrderId);
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere, parm);

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
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

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 发货

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetOrderSendInfo(Guid Id)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                var bll = new OrderSend();
                var modelInfo = bll.GetModelByJoin(Id);
                return ResResult.Response(true, "", JsonConvert.SerializeObject(modelInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderSendList(OrderSendModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new OrderSend();

                var sb = new StringBuilder(300);
                var parms = new ParamsHelper();
                if (!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    sb.Append("and (o.OrderCode like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                    parm.Value = model.Keyword;
                    parms.Add(parm);
                }
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sb.ToString(), parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrderSend(OrderSendFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                var userId = WebCommon.GetUserId();

                var modelInfo = new OrderSendInfo(Id, userId, model.OrderCode, model.CustomerId, 0, 0, "", (byte)EnumData.EnumOrderStatus.新建, 0, false, DateTime.Now);

                var bll = new OrderSend();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                    var rcBll = new RandomCode();
                    var orderCode = rcBll.GetOrderCode(((int)EnumData.EnumOrderPrefix.发货).ToString());
                    modelInfo.OrderCode = orderCode;
                    modelInfo.Id = Guid.NewGuid();
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteOrderSend(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var bll = new OrderSend();

                var list = new List<object>();
                foreach (var item in items)
                {
                    if (bll.IsExistProduct(item)) throw new ArgumentException(MC.M_OrderExistProductError);
                    list.Add(item);
                }

                if (!bll.DeleteBatch(list)) return ResResult.Response(false, MC.M_Save_Error, null);
                return ResResult.Response(true, MC.M_Save_Ok, null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderSendProductList(OrderSendProductModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new OrderSendProduct();

                var sqlWhere = "and osp.OrderId = @OrderId ";
                var parm = new SqlParameter("@OrderId", model.OrderId);
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere, parm);

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrderSendProduct(Guid Id, string itemAppend)
        {
            try
            {
                var items = itemAppend.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                var oBll = new OrderSend();
                var oInfo = oBll.GetModel(Id);
                if (oInfo == null) throw new ArgumentException(MC.Data_NotExist);
                var slpBll = new StockLocationProduct();
                var currTime = DateTime.Now;

                var procusList = new List<string>();
                var list = new List<StockLocationProductInfo>();
                foreach (var item in items)
                {
                    var subItems = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var stockLocationId = Guid.Parse(subItems[0]);
                    var productId = Guid.Parse(subItems[1]);
                    var customerId = Guid.Parse(subItems[2]);
                    var qty = double.Parse(subItems[3]);

                    var slpInfo = new StockLocationProductInfo();
                    slpInfo.StockLocationId = stockLocationId;
                    slpInfo.ProductId = productId;
                    slpInfo.CustomerId = customerId;
                    slpInfo.MaxQty = qty;

                    list.Add(slpInfo);

                    var sProCus = string.Format("{0},{1}", productId, customerId);
                    if (!procusList.Contains(sProCus)) procusList.Add(sProCus);
                }

                #region 发货明细

                var ospBll = new OrderSendProduct();
                foreach (var item in procusList)
                {
                    var procusItems = item.Split(',');
                    var productId = Guid.Parse(procusItems[0]);
                    var customerId = Guid.Parse(procusItems[1]);

                    var totalQty = list.Where(m => m.ProductId.Equals(productId) && m.CustomerId.Equals(customerId)).Sum(m => m.MaxQty);
                    var ospInfo = ospBll.GetModel(Id, productId, customerId);
                    if (ospInfo != null)
                    {
                        ospInfo.Qty += totalQty;

                        MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                        ospBll.Update(ospInfo);
                    }
                    else
                    {
                        MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                        ospInfo = new OrderSendProductInfo(Id, productId, customerId, totalQty, 0, EnumData.EnumStatus.正常.ToString(), currTime);
                        ospBll.Insert(ospInfo);
                    }
                }



                #endregion

                oBll.SetStatus(Id.ToString());

                #region 库存库位货品

                var stepCode = (int)EnumData.EnumStep.发货;
                var spBll = new StockProduct();
                foreach (var item in list)
                {
                    spBll.DoProduct(item.ProductId, item.CustomerId, item.StockLocationId, stepCode, 0, 0, item.MaxQty);
                    slpBll.DoProduct(item.StockLocationId, item.ProductId, true, 0, item.MaxQty);
                }

                #endregion

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteOrderSendProduct(Guid orderId, string itemAppend)
        {
            try
            {
                throw new ArgumentException("该方法未实现");
                //if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                //var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //var bll = new OrderSendProduct();
                //var effect = 0;

                //foreach (var item in items)
                //{
                //    effect += bll.Delete(orderId, Guid.Parse(item));
                //}

                //if (effect < 1) ResResult.Response(false, MC.M_Save_Error, "");

                //return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 拣货

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetOrderPickedInfo(Guid Id)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);
                var bll = new OrderPicked();
                var modelInfo = bll.GetModel(Id);
                return ResResult.Response(true, "", JsonConvert.SerializeObject(modelInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderPickedList(OrderSendModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

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

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel CreateOrderPicked(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                var osBll = new OrderSend();
                var ospBll = new OrderSendProduct();
                var slpBll = new StockLocationProduct();

                var oppList = new List<OrderPickProductInfo>();
                var currTime = DateTime.Now;
                var userId = WebCommon.GetUserId();
                var Id = Guid.NewGuid();

                foreach (var item in items)
                {
                    var orderId = Guid.Parse(item);
                    var orderProductList = ospBll.GetListByOrderId(orderId);
                    foreach (var orpItem in orderProductList)
                    {
                        var sStockLocations = slpBll.GetNameByProductId(orpItem.ProductId);
                        var oppInfo = new OrderPickProductInfo(Id, orderId, orpItem.ProductId, orpItem.CustomerId, orpItem.Qty, 0, sStockLocations, EnumData.EnumStatus.正常.ToString(), currTime);

                        oppList.Add(oppInfo);
                    }
                }

                var effect = 0;
                if (oppList.Count > 0)
                {
                    var rcBll = new RandomCode();
                    var orderCode = rcBll.GetOrderCode(((int)EnumData.EnumOrderPrefix.拣货).ToString());

                    var opBll = new OrderPicked();
                    var oppBll = new OrderPickProduct();
                    var opInfo = new OrderPickedInfo(Id, userId, orderCode, 0, 0, (byte)EnumData.EnumOrderStatus.新建, "", 0, false, currTime);

                    opBll.InsertByOutput(opInfo);
                    foreach (var item in oppList)
                    {
                        effect += oppBll.Insert(item);
                    }
                    foreach (var item in items)
                    {
                        osBll.SetStatus(item,(byte)EnumData.EnumOrderStatus.待完成);
                    }

                    opBll.SetTotalProduct(orderCode);
                }

                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrderPicked(OrderSendFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                var userId = WebCommon.GetUserId();

                var modelInfo = new OrderSendInfo(Id, userId, model.OrderCode, model.CustomerId, 0, 0, "", (byte)EnumData.EnumOrderStatus.新建, 0, false, DateTime.Now);

                var bll = new OrderSend();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                    var rcBll = new RandomCode();
                    var orderCode = rcBll.GetOrderCode(((int)EnumData.EnumOrderPrefix.发货).ToString());
                    modelInfo.OrderCode = orderCode;
                    modelInfo.Id = Guid.NewGuid();
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteOrderPicked(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);
                var bll = new OrderSend();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrderPickProductList(OrderPickedProductModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new OrderPickProduct();
                IList<OrderPickProductInfo> list = null;

                var orderId = Guid.Empty;
                if (model.OrderId != null) Guid.TryParse(model.OrderId.ToString(), out orderId);
                if (!orderId.Equals(Guid.Empty))
                {
                    var sqlWhere = "and op.Id = @Id ";
                    var parm = new SqlParameter("@Id", model.OrderId);
                    list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere, parm);
                }
                else
                {
                    var sb = new StringBuilder(300);
                    sb.Append("and opp.Qty > 0 ");
                    var parms = new ParamsHelper();
                    if (!string.IsNullOrWhiteSpace(model.Keyword))
                    {
                        sb.Append("and (op.OrderCode like @Keyword or p.ProductCode like @Keyword or p.ProductName like @Keyword) ");
                        var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                        parm.Value = model.Keyword;
                        parms.Add(parm);
                    }
                    list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sb.ToString(), parms == null ? null : parms.ToArray());
                }

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
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

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteOrderPickProduct(Guid orderId, string itemAppend)
        {
            try
            {
                throw new ArgumentException("该方法未实现");

                //if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                //var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //var bll = new OrderPickProduct();
                //var effect = 0;

                //foreach (var item in items)
                //{
                //    effect += bll.Delete(orderId, Guid.Parse(item));
                //}

                //if (effect < 1) ResResult.Response(false, MC.M_Save_Error, "");

                //return ResResult.Response(true, MC.M_Save_Ok, "");
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
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new Pandian();

                var sb = new StringBuilder(300);
                var parms = new ParamsHelper();
                if (!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    sb.Append("and (pd.OrderCode like @Keyword or pd.Named like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                    parm.Value = model.Keyword;
                    parms.Add(parm);
                }
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sb.ToString(), parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetPandianInfo(Guid Id)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                var bll = new Pandian();
                return ResResult.Response(true, "", JsonConvert.SerializeObject(bll.GetModel(Id)));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePandian(PandianFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                var stockStartDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(model.StockStartDate)) DateTime.TryParse(model.StockStartDate, out stockStartDate);
                if (stockStartDate == DateTime.MinValue) stockStartDate = DateTime.Parse("1754-01-01");
                var stockEndDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(model.StockEndDate)) DateTime.TryParse(model.StockEndDate, out stockEndDate);
                if (stockEndDate == DateTime.MinValue) stockEndDate = DateTime.Parse("1754-01-01");

                var bll = new Pandian();
                if (Id.Equals(Guid.Empty))
                {
                    model.Id = Guid.NewGuid();
                }
                else
                {
                    var oldPandianInfo = bll.GetModel(Id);
                    if (oldPandianInfo == null) throw new ArgumentException(MC.Data_NotExist);
                    else if (oldPandianInfo.Status != EnumData.EnumOrderStatus.新建.ToString()) throw new ArgumentException(MC.M_RuleInvalidError);
                }
                var userId = WebCommon.GetUserId();

                var spBll = new StockProduct();
                var pdpList = spBll.GetPandianProductList(Guid.Parse(model.Id.ToString()), userId, stockStartDate, stockEndDate, model.Customers, model.Zones, model.StockLocations);
                if (pdpList == null || pdpList.Count == 0)
                {
                    return ResResult.Response(false, MC.M_NotExistDetailError, "");
                }

                var pdpBll = new PandianProduct();
                if (!Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                    #region 比较差异，并处理

                    var oldPdpList = pdpBll.GetList();
                    var qInsert = pdpList.Where(m => !oldPdpList.Any(mm => m.PandianId.Equals(mm.PandianId) && m.ProductId.Equals(mm.ProductId) && m.CustomerId.Equals(mm.CustomerId)));
                    var qDelete = oldPdpList.Where(m => !pdpList.Any(mm => m.PandianId.Equals(mm.PandianId) && m.ProductId.Equals(mm.ProductId) && m.CustomerId.Equals(mm.CustomerId)));
                    if (qInsert != null && qInsert.Count() > 0)
                    {
                        foreach (var item in qInsert)
                        {
                            pdpBll.Insert(item);
                        }
                    }
                    if (qDelete != null && qDelete.Count() > 0)
                    {
                        foreach (var item in qDelete)
                        {
                            pdpBll.Delete(item.PandianId, item.ProductId, item.CustomerId);
                        }
                    }

                    #endregion
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                    foreach (var item in pdpList)
                    {
                        pdpBll.Insert(item);
                    }
                }

                var modelInfo = new PandianInfo(Guid.Parse(model.Id.ToString()), userId, model.OrderCode, model.Named, model.AllowUsers, model.Remark, stockStartDate, stockEndDate, model.Customers, model.Zones, model.StockLocations, 0, EnumData.EnumOrderStatus.新建.ToString(), DateTime.Now);
                int effect = -1;
                if (Id.Equals(Guid.Empty))
                {
                    var rcBll = new RandomCode();
                    modelInfo.OrderCode = rcBll.GetRndCodeByDateTime(((int)EnumData.EnumStep.盘点).ToString());

                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeletePandian(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);
                var bll = new Pandian();
                foreach (var item in items)
                {
                    if (bll.IsExistProduct(item))
                    {
                        var pandianInfo = bll.GetModel(Guid.Parse(item));
                        throw new ArgumentException(MC.GetString(MC.Params_ExistDetailError, pandianInfo.OrderCode));
                    }
                }

                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
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
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
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

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + ",\"footer\":{\"TotalPan\":" + totals[0] + ",\"TotalYpan\":" + totals[1] + ",\"TotalNotPan\":" + totals[2] + "}}");
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

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                effect = bll.Update(oldInfo);
                effect += new Pandian().UpdateStatus(oldInfo.PandianId, EnumData.EnumOrderStatus.待完成.ToString());

                oldInfo.UserId = Guid.Empty;
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", JsonConvert.SerializeObject(oldInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeletePandianProduct(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var bll = new PandianProduct();
                int effect = 0;

                foreach (var item in items)
                {
                    var Ids = item.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    effect += bll.Delete(Guid.Parse(Ids[0]), Guid.Parse(Ids[1]), Guid.Parse(Ids[2]));
                }

                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");
                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 库存

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetStockProductList(StockProductModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                ParamsHelper parms = new ParamsHelper();
                StringBuilder sqlWhere = null;
                var isSelfView = false;

                var startDate = DateTime.MinValue;
                var endDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(model.StartDate)) DateTime.TryParse(model.StartDate, out startDate);
                if (!string.IsNullOrWhiteSpace(model.EndDate)) DateTime.TryParse(model.EndDate, out endDate);

                #region 查询条件

                if (HttpContext.Current.User.IsInRole("SelfView"))
                {
                    isSelfView = true;
                    if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                    sqlWhere.AppendFormat("and sp.CustomerId = (select FeatureId from FeatureUser fu where fu.UserId = '{0}') ", WebCommon.GetUserId());
                }
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

                return ResResult.Response(true, MC.Response_Ok, "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + ",\"IsSelfView\":\"" + isSelfView + "\"}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 库位管理

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
                        return ResResult.Response(true, "", "{\"total\":" + ospList.Count() + ",\"rows\":" + JsonConvert.SerializeObject(ospList) + "}");
                    }
                    else if (model.KeyName == "OrderPickProduct")
                    {
                        var oppslList = spBll.GetSelectProductListByStepName(model.PageIndex, model.PageSize, EnumData.EnumStep.拣货.ToString(), model.ProductId, model.CustomerId, model.Qty);
                        return ResResult.Response(true, "", "{\"total\":" + oppslList.Count() + ",\"rows\":" + JsonConvert.SerializeObject(oppslList) + "}");
                    }
                }

                var slpBll = new StockLocationProduct();
                switch (model.KeyName)
                {
                    case "ShelfMissionProduct":
                        var smpList = slpBll.GetListForShelfMissionProduct(Guid.Parse(model.ProductId.ToString()), model.Qty);
                        return ResResult.Response(true, "", "{\"total\":" + smpList.Count() + ",\"rows\":" + JsonConvert.SerializeObject(smpList) + "}");
                    default:
                        throw new ArgumentException(MC.GetString(MC.Params_SwitchNameNotExist, model.KeyName));
                }
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetStockLocationList(StockLocationModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new StockLocation();
                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;

                if (!string.IsNullOrWhiteSpace(model.ZoneIds))
                {
                    var Ids = model.ZoneIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (Ids.Length == 1)
                    {
                        parms = new ParamsHelper();
                        sqlWhere = new StringBuilder(50);
                        sqlWhere.AppendFormat("and ZoneId = @ZoneId ");
                        var parm = new SqlParameter("@ZoneId", SqlDbType.UniqueIdentifier);
                        parm.Value = Guid.Parse(Ids[0]);
                        parms.Add(parm);
                    }
                    else
                    {
                        sqlWhere = new StringBuilder(1000);
                        var sqlIn = new StringBuilder(300);
                        foreach (var Id in Ids)
                        {
                            sqlIn.AppendFormat("'{0}',", Id);
                        }
                        sqlWhere.AppendFormat("and ZoneId in({0}) ", sqlIn.ToString().Trim(','));
                    }
                }

                var list = bll.GetList(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCbbStockLocation()
        {
            try
            {
                var cbbData = new List<ComboboxInfo>();
                cbbData.Add(new ComboboxInfo { Id = "-1", Text = "请选择" });

                var bll = new StockLocation();
                var list = bll.GetList();

                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        cbbData.Add(new ComboboxInfo { Id = item.Id.ToString(), Text = item.Code });
                    }
                }

                return ResResult.Response(true, "", JsonConvert.SerializeObject(cbbData));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 库区管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCbbZone()
        {
            try
            {
                var cbbData = new List<ComboboxInfo>();
                cbbData.Add(new ComboboxInfo { Id = "-1", Text = "请选择" });

                var bll = new Zone();
                var list = bll.GetList();

                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        cbbData.Add(new ComboboxInfo { Id = item.Id.ToString(), Text = item.ZoneCode });
                    }
                }

                return ResResult.Response(true, "", JsonConvert.SerializeObject(cbbData));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 客户管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveCustomer(CustomerModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.CustomerCode)) return ResResult.Response(false, MC.Submit_Params_InvalidError, null);

                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);

                var modelInfo = new CustomerInfo(Id, WebCommon.GetUserId(), model.CustomerCode, model.CustomerName, model.ShortName, model.ContactMan, model.Email, model.Phone, model.TelPhone, model.Fax, model.Postcode, model.Address, model.Remark, DateTime.Now);

                var bll = new Customer();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);
                    effect = bll.Insert(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, "操作成功", null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteCustomer(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var bll = new Customer();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, "操作失败，请正确操作", null);
                }

                return ResResult.Response(true, MC.M_Save_Ok, null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCustomerList(ListModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new Customer();
                var list = bll.GetList(model.PageIndex, model.PageSize, out totalRecord, "", null);

                return ResResult.Response(true, "调用成功", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 供应商管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCbbSupplier()
        {
            try
            {
                var cbbData = new List<ComboboxInfo>();
                cbbData.Add(new ComboboxInfo { Id = "-1", Text = "请选择" });

                var bll = new Supplier();
                var list = bll.GetList();

                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        cbbData.Add(new ComboboxInfo { Id = item.Id.ToString(), Text = item.SupplierName });
                    }
                }

                return ResResult.Response(true, "", JsonConvert.SerializeObject(cbbData));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 库存预警管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetStockWarningList(ListModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new StockWarning();

                var sb = new StringBuilder(300);
                var parms = new ParamsHelper();
                if (!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    sb.Append("and (sw.Coded like @Keyword or z.ZoneName like @Keyword or sl.Code like @Keyword or sl.Named like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                    parm.Value = model.Keyword;
                    parms.Add(parm);
                }
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sb.ToString(), parms.ToArray());

                return ResResult.Response(true, "调用成功", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveStockWarning(StockWarningFmModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Coded)) return ResResult.Response(false, MC.Submit_Params_InvalidError, null);

                var zoneId = Guid.Empty;
                if (model.ZoneId != null) Guid.TryParse(model.ZoneId.ToString(), out zoneId);
                if (zoneId.Equals(Guid.Empty)) return ResResult.Response(false, MC.Submit_Params_InvalidError, null);

                var stockLocationId = Guid.Empty;
                if (model.StockLocationId != null) Guid.TryParse(model.StockLocationId.ToString(), out stockLocationId);
                if (stockLocationId.Equals(Guid.Empty)) return ResResult.Response(false, MC.Submit_Params_InvalidError, null);

                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                var userId = WebCommon.GetUserId();

                var modelInfo = new StockWarningInfo(Id, userId, zoneId, stockLocationId, model.Coded, model.ZoneProperty, model.StockLocationProperty, model.StockAmount, model.OverdueDay, model.MinQty, model.MaxQty, model.Remark, model.Sort, model.IsDisable, DateTime.Now);

                var bll = new StockWarning();
                int effect = -1;

                if (!Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);
                    effect = bll.Insert(modelInfo);
                }

                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, MC.M_Save_Ok, null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteStockWarning(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, null);
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var bll = new StockWarning();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, null);
                }

                return ResResult.Response(true, MC.M_Save_Ok, null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 货品管理

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetCategoryTree()
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                var bll = new Category();
                return ResResult.Response(true, "", bll.GetTreeJson());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveCategory(CategoryModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何参数", null);
                if (string.IsNullOrWhiteSpace(model.CategoryName)) return ResResult.Response(false, "分类名称不能为空字符串", null);
                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);

                var bll = new Category();
                int effect = 0;

                if (bll.IsExistCode(model.CategoryCode, Id))
                {
                    return ResResult.Response(false, MC.GetString(MC.Params_CodeExistError, model.CategoryCode), Id);
                }

                var modelInfo = new CategoryInfo(Id, WebCommon.GetUserId(), parentId, model.CategoryCode, model.CategoryName, model.Step.Trim(','), model.Sort, model.Remark, DateTime.Now);
                if (modelInfo.Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);
                    modelInfo.Id = Guid.NewGuid();
                    modelInfo.Step = modelInfo.Id.ToString() + "," + modelInfo.Step;
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, MC.M_Save_Ok, modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteCategory(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty))
                {
                    return ResResult.Response(false, "参数值无效", null);
                }

                var bll = new Category();
                if (bll.IsExistProduct(Id)) return ResResult.Response(false, "该节点存在物料列表信息，请先删除物料列表信息后再执行此操作", null);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);
                return ResResult.Response(bll.Delete(Id) > 0, "", null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveProduct(ProductFmModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.ProductCode)) return ResResult.Response(false, "客户代码不能为空字符串", null);

                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);

                Guid gSupplierId = Guid.Empty;
                if (model.SupplierId != null) Guid.TryParse(model.SupplierId.ToString(), out gSupplierId);

                var bll = new Product();
                if (bll.IsExistCode(model.ProductCode, Id))
                {
                    return ResResult.Response(false, MC.GetString(MC.Params_CodeExistError, model.ProductCode), Id);
                }

                var modelInfo = new ProductInfo(Id, WebCommon.GetUserId(), model.CategoryId, gSupplierId, model.ProductCode, model.ProductName, model.FullName, model.Specs, model.Price, model.MaterialQuality, model.Weight, model.MaxStore, model.MinStore, model.OutPackVolume, model.OutPackWeight, model.InPackVolume, model.InPackWeight, model.OutPackQty, model.InPackQty, model.ShelfLife, model.Sort, model.Remark, model.IsDisable, DateTime.Now);
                int effect = -1;
                if (Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);
                    effect = bll.Insert(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, null);

                return ResResult.Response(true, MC.M_Save_Ok, null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteProduct(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, "参数itemAppend不能为空字符串", null);
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var bll = new Product();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, "操作失败，请正确操作", null);
                }

                return ResResult.Response(true, "操作成功", null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetProductList(ProductModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;

                int totalRecord = 0;
                var bll = new Product();
                IList<ProductInfo> list = null;

                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;
                if (!string.IsNullOrWhiteSpace(model.Keyword))
                {
                    sqlWhere = new StringBuilder(100);
                    parms = new ParamsHelper();
                    sqlWhere.Append("and (ProductCode like @Keyword or ProductName like @Keyword or FullName like @Keyword) ");
                    parms.Add(new SqlParameter("@Keyword", "%" + model.Keyword + "%"));
                }
                if (model.CategoryId != null && !string.IsNullOrWhiteSpace(model.CategoryId.ToString()))
                {
                    list = bll.GetListByCategory(model.PageIndex, model.PageSize, out totalRecord, model.CategoryId, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
                }
                else
                {
                    list = bll.GetList(model.PageIndex, model.PageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
                }

                return ResResult.Response(true, "调用成功", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        #endregion

        #region 包装管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePackage(PackageModel model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.PackageCode)) return ResResult.Response(false, "包装代码不能为空字符串", null);
                if (!string.IsNullOrWhiteSpace(model.UnitXml))
                {
                    model.UnitXml = HttpUtility.UrlDecode(model.UnitXml);
                    try
                    {
                        var xel = XElement.Parse(model.UnitXml);
                    }
                    catch
                    {
                        return ResResult.Response(false, "参数UnitXml数据不满足xml数据，请正确操作", null);
                    }
                }

                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);

                var userId = WebCommon.GetUserId();

                Guid customerId = Guid.Empty;
                Guid.TryParse(model.CustomerId.ToString(), out customerId);
                Guid productId = Guid.Empty;
                Guid.TryParse(model.ProductId.ToString(), out productId);

                var modelInfo = new PackageInfo();
                modelInfo.Id = Id;
                modelInfo.UserId = userId;
                modelInfo.CustomerId = customerId;
                modelInfo.ProductId = productId;
                modelInfo.PackageCode = model.PackageCode;
                modelInfo.TotalPiece = model.TotalPiece;
                modelInfo.TotalInsidePackage = model.TotalInsidePackage;
                modelInfo.TotalBox = model.TotalBox;
                modelInfo.TotalTray = model.TotalTray;
                modelInfo.UnitXml = model.UnitXml;
                modelInfo.Remark = model.Remark;
                modelInfo.LastUpdatedDate = DateTime.Now;

                var bll = new Package();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);
                    effect = bll.Insert(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, "操作失败，数据库操作异常", null);

                return ResResult.Response(true, "操作成功", null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeletePackage(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, "参数itemAppend不能为空字符串", null);
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var bll = new Package();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, "操作失败，请稍后再重试...", null);
                }

                return ResResult.Response(true, "操作成功", null);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, null);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPackageList(DlgPackageModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

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

        #region 车辆

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetVehicleList(ListModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new Vehicle();

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, "", null);

                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveVehicle(VehicleFmModel model)
        {
            try
            {
                Guid Id = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out Id);
                var currTime = DateTime.Now;
                var userId = WebCommon.GetUserId();
                var licPic = Guid.Empty;
                if (model.LicPic != null) Guid.TryParse(model.LicPic.ToString(), out licPic);
                var driverIDPicture = Guid.Empty;
                if (model.DriverIDPicture != null) Guid.TryParse(model.DriverIDPicture.ToString(), out driverIDPicture);
                var modelInfo = new VehicleInfo(Id, userId, model.VehicleID, model.VehicleModel, model.Licence, licPic, model.OffenceRecord, model.DriverID, driverIDPicture, model.RewardRecord, model.Remark, model.Sort, model.IsDisable, currTime);

                var bll = new Vehicle();
                int effect = -1;

                if (Id.Equals(Guid.Empty))
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);
                    effect = bll.Insert(modelInfo);
                }
                else
                {
                    MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, MC.M_Save_Error, "");

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteVehicle(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);
                var bll = new Vehicle();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 图片、文件管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetSitePictureList(ListModel model)
        {
            try
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;
                var bll = new SitePicture();

                var list = bll.GetCbbList(model.PageIndex, model.PageSize, out totalRecord, model.Keyword);
                return ResResult.Response(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteSitePicture(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

                var bll = new SitePicture();
                if (!bll.DeleteBatch((IList<object>)items.ToList<object>()))
                {
                    return ResResult.Response(false, MC.M_Save_Error, "");
                }

                return ResResult.Response(true, MC.M_Save_Ok, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 私有

        #endregion
    }
}
