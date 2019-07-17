using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;
using TygaSoft.CustomProvider;
using TygaSoft.Model;
using TygaSoft.BLL;

namespace TygaSoft.Web.Handlers
{
    public class HandlerContent : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            try
            {
                string reqName = "";
                switch (context.Request.HttpMethod.ToUpper())
                {
                    case "GET":
                        reqName = context.Request.QueryString["ReqName"];
                        break;
                    case "POST":
                        reqName = context.Request.Form["ReqName"];
                        break;
                    default:
                        break;
                }
                if (string.IsNullOrWhiteSpace(reqName))
                {
                    context.Response.Write(ResResult.ResJsonString(false, MC.Request_Params_InvalidError, ""));
                    return;
                }

                switch (reqName.Trim())
                {
                    case "SaveMesCategory":
                        SaveMesCategory(context);
                        break;
                    case "SaveMesProduct":
                        SaveMesProduct(context);
                        break;
                    case "SaveMenuAccess":
                        SaveMenuAccess(context);
                        break;
                    case "SaveLogisticsDistribution":
                        SaveLogisticsDistribution(context);
                        break;
                    case "SaveFeatureUser":
                        SaveFeatureUser(context);
                        break;
                    case "SaveSiteMulti":
                        SaveSiteMulti(context);
                        break;
                    case "SaveZone":
                        SaveZone(context);
                        break;
                    case "SaveStockLocation":
                        SaveStockLocation(context);
                        break;
                    case "SaveSupplier":
                        SaveSupplier(context);
                        break;
                    case "SaveCompany":
                        SaveCompany(context);
                        break;
                    case "SaveBarcodeTemplate":
                        SaveBarcodeTemplate(context);
                        break;
                    case "DeleteMesCategory":
                        DeleteMesCategory(context);
                        break;
                    case "DeleteMesProduct":
                        Delete(context, reqName);
                        break;
                    case "DeleteOrderSendProduct":
                        Delete(context, reqName);
                        break;
                    case "DeleteLogisticsDistribution":
                        Delete(context, reqName);
                        break;
                    case "DeleteRFID":
                        Delete(context, reqName);
                        break;
                    case "DeleteSiteMulti":
                        Delete(context, reqName);
                        break;
                    case "DeleteZone":
                        Delete(context, reqName);
                        break;
                    case "DeleteStockLocation":
                        Delete(context, reqName);
                        break;
                    case "DeleteSupplier":
                        Delete(context, reqName);
                        break;
                    case "DeleteCompany":
                        Delete(context, reqName);
                        break;
                    case "SaveInfoneCustomer":
                        SaveInfoneCustomer(context);
                        break;
                    case "SaveInfoneProject":
                        SaveInfoneProject(context);
                        break;
                    case "SaveInfoneDeviceBorrowRecord":
                        SaveInfoneDeviceBorrowRecord(context);
                        break;
                    case "SaveInfoneDeviceRepairRecord":
                        SaveInfoneDeviceRepairRecord(context);
                        break;
                    case "DeleteInfoneDeviceBorrowRecord":
                        Delete(context, reqName);
                        break;
                    case "DeleteInfoneCustomer":
                        Delete(context, reqName);
                        break;
                    case "DeleteInfoneProjectReportPrepare":
                        Delete(context, reqName);
                        break;
                    case "DeleteInfoneDeviceRepairRecord":
                        Delete(context, reqName);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void Delete(HttpContext context, string reqName)
        {
            var itemAppend = context.Request.Form["ItemAppend"];
            if (string.IsNullOrWhiteSpace(itemAppend)) throw new ArgumentException(MC.Request_Params_InvalidError);
            var items = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.删除, true);

            switch (reqName)
            {
                case "DeleteMesProduct":
                    DeleteMesProduct(context, items.ToList<object>());
                    break;
                case "DeleteOrderSendProduct":
                    DeleteOrderSendProduct(context, items.ToList<object>());
                    break;
                case "DeleteLogisticsDistribution":
                    DeleteLogisticsDistribution(context, items.ToList<object>());
                    break;
                case "DeleteRFID":
                    DeleteRFID(context, items.ToList<object>());
                    break;
                case "DeleteSiteMulti":
                    DeleteSiteMulti(context, items.ToList<object>());
                    break;
                case "DeleteZone":
                    DeleteZone(context, items.ToList<object>());
                    break;
                case "DeleteStockLocation":
                    DeleteStockLocation(context, items.ToList<object>());
                    break;
                case "DeleteSupplier":
                    DeleteSupplier(context, items.ToList<object>());
                    break;
                case "DeleteCompany":
                    DeleteCompany(context, items.ToList<object>());
                    break;
                case "DeleteInfoneDeviceBorrowRecord":
                    DeleteInfoneDeviceBorrowRecord(context, items.ToList<object>());
                    break;
                case "DeleteInfoneCustomer":
                    DeleteInfoneCustomer(context, items.ToList<object>());
                    break;
                case "DeleteInfoneProjectReportPrepare":
                    DeleteInfoneProjectReportPrepare(context, items.ToList<object>());
                    break;
                case "DeleteInfoneDeviceRepairRecord":
                    DeleteInfoneDeviceRepairRecord(context, items.ToList<object>());
                    break;
                default:
                    break;
            }
        }

        #region 出库

        private void DeleteOrderSendProduct(HttpContext context, IList<object> items)
        {
            var bll = new OrderSendProduct();
            var effect = 0;
            foreach (var item in items)
            {
                var subItems = item.ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var orderId = Guid.Parse(subItems[0]);
                var productId = Guid.Parse(subItems[1]);
                var customerId = Guid.Parse(subItems[2]);
                var ospInfo = bll.GetModel(orderId, productId, customerId);
                if (ospInfo == null) throw new ArgumentException(MC.Data_NotExist);

                new StockProduct().DoProduct(productId, customerId, (int)EnumData.EnumStep.发货, false, 0, 0, ospInfo.Qty);

                effect += bll.Delete(orderId, productId, customerId);
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        #endregion

        #region 公共

        private void SaveBarcodeTemplate(HttpContext context)
        {
            try
            {
                var isInsert = false;
                Guid Id = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(context.Request.Form["Id"])) Guid.TryParse(context.Request.Form["Id"], out Id);
                if (Id.Equals(Guid.Empty))
                {
                    Id = Guid.NewGuid();
                    isInsert = true;
                }
                var sTypeName = context.Request.Form["TypeName"].Trim();
                var userId = WebCommon.GetUserId();
                BarcodeTemplateInfo modelInfo = null;
                var sTitle = context.Request.Form["Title"].Trim();
                var sJContent = HttpUtility.UrlDecode(context.Request.Form["JContent"]).Trim();
                var isDefault = bool.Parse(context.Request.Form["IsDefault"]);

                if (string.IsNullOrWhiteSpace(sTitle) && string.IsNullOrWhiteSpace(sJContent) && string.IsNullOrWhiteSpace(sTypeName))
                {
                    throw new ArgumentException(MC.M_RuleInvalidError);
                }

                if (sTypeName == "Barcode")
                {
                    var sBarcode = context.Request.Form["Barcode"].Trim();
                    var sBarcodeFormat = context.Request.Form["BarcodeFormat"].Trim();
                    var width = int.Parse(context.Request.Form["Width"]);
                    var height = int.Parse(context.Request.Form["Height"]);
                    var margin = int.Parse(context.Request.Form["Margin"]);

                    var barcodeInfo = new BarcodeInfo(sBarcode, sBarcodeFormat, width, height, margin, "");
                    barcodeInfo.ImageUrl = new BarcodeHelper().CreateBarcode(barcodeInfo, Id.ToString(), true);
                    modelInfo = new BarcodeTemplateInfo(Id, userId, sTitle, JsonConvert.SerializeObject(barcodeInfo), isDefault, sTypeName, DateTime.Now);
                }
                else
                {
                    modelInfo = new BarcodeTemplateInfo(Id, userId, sTitle, sJContent, isDefault, sTypeName, DateTime.Now);
                }

                var bll = new BarcodeTemplate();
                int effect = -1;

                if (isInsert)
                {
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));

                context.Response.Write(ResResult.ResJsonString(true, "", ""));
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        private void DeleteRFID(HttpContext context, IList<object> items)
        {
            var bll = new RFID();
            var effect = 0;
            foreach (var item in items)
            {
                var subItems = item.ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                effect += bll.Delete(subItems[0], subItems[1]);
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Error, ""));
                return;
            }
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void SaveFeatureUser(HttpContext context)
        {
            MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);
            if (!HttpContext.Current.User.IsInRole("Administrators")) throw new ArgumentException(MC.Role_InvalidError);

            var featureId = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["AppId"])) Guid.TryParse(context.Request.Form["AppId"], out featureId);
            if (!string.IsNullOrWhiteSpace(context.Request.Form["FeatureId"])) Guid.TryParse(context.Request.Form["FeatureId"], out featureId);
            var typeName = context.Request.Form["TypeName"];
            var userName = context.Request.Form["UserName"];
            var userId = Guid.Parse(Membership.GetUser(userName).ProviderUserKey.ToString());

            var fuBll = new FeatureUser();
            fuBll.DoFeatureUser(userId, featureId, typeName, true);

            context.Response.Write(ResResult.ResJsonString(true, "", ""));
        }

        #endregion

        #region 系统管理

        private void SaveSiteMulti(HttpContext context)
        {
            var Request = context.Request;

            #region 请求参数集

            Guid id = Guid.Empty;
            if (Request.Form["Id"] != null) Guid.TryParse(Request.Form["Id"], out id);
            string coded = string.Empty;
            if (Request.Form["Coded"] != null) coded = Request.Form["Coded"].Trim();
            string named = string.Empty;
            if (Request.Form["Named"] != null) named = Request.Form["Named"].Trim();
            string siteLogo = string.Empty;
            if (Request.Form["SiteLogo"] != null) siteLogo = Request.Form["SiteLogo"].Trim();
            string siteTitle = string.Empty;
            if (Request.Form["SiteTitle"] != null) siteTitle = Request.Form["SiteTitle"].Trim();
            string cultureName = string.Empty;
            if (Request.Form["CultureName"] != null) cultureName = Request.Form["CultureName"].Trim();

            #endregion

            var currTime = DateTime.Now;

            var modelInfo = new SiteMultiInfo(id, coded, named, siteLogo, siteTitle, cultureName, currTime);

            var bll = new SiteMulti();
            int effect = 0;

            if (id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                var code = bll.GetCode();
                modelInfo.Coded = code;
                effect = bll.Insert(modelInfo);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                var oldInfo = bll.GetModel(id);
                modelInfo.Coded = oldInfo.Coded;
                effect = bll.Update(modelInfo);
            }
            if (effect > 0) context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
            else context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
        }

        private void DeleteSiteMulti(HttpContext context, IList<object> items)
        {
            var bll = new SiteMulti();
            if (bll.DeleteBatch(items)) context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
            else context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
        }

        private void SaveMenuAccess(HttpContext context)
        {
            if (!(HttpContext.Current.User.IsInRole("Administrators") || HttpContext.Current.User.IsInRole("System"))) throw new ArgumentException(MC.Role_InvalidError);

            var sRoleName = context.Request.Form["RoleName"];
            var sUserName = context.Request.Form["UserName"];
            var sMenuItemJson = context.Request.Form["MenuItemJson"];

            if (string.IsNullOrWhiteSpace(sMenuItemJson)) throw new ArgumentException(MC.Request_Params_InvalidError);
            sMenuItemJson = HttpUtility.UrlDecode(sMenuItemJson);
            if (string.IsNullOrWhiteSpace(sRoleName) && string.IsNullOrWhiteSpace(sUserName)) throw new ArgumentException(MC.Request_Params_InvalidError);
            List<SiteMenusAccessItemInfo> list = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(sMenuItemJson);
            var accessId = Guid.Empty;
            var isRole = !string.IsNullOrWhiteSpace(sRoleName);
            var accessType = isRole ? "Roles" : "Users";
            if (isRole)
            {
                if (sRoleName.ToLower() == "administrators") throw new ArgumentException(MC.GetString(MC.Params_SaveRoleAccessError, sRoleName));

                var roleBll = new SiteRoles();
                accessId = roleBll.GetAspnetModel(Membership.ApplicationName, sRoleName).Id;
            }
            else
            {
                if (Roles.GetRolesForUser(sUserName).Contains("administrators")) throw new ArgumentException(MC.GetString(MC.Params_SaveUserAccessError, sUserName));

                accessId = Guid.Parse(Membership.GetUser(sUserName).ProviderUserKey.ToString());
            }
            var menuBll = new SiteMenus();
            var maBll = new SiteMenusAccess();
            List<SiteMenusAccessItemInfo> maitems = null;
            var appId = new Applications().GetAspnetAppId(Membership.ApplicationName);
            var menusAccessInfo = maBll.GetModel(appId, accessId);
            if (menusAccessInfo != null) maitems = JsonConvert.DeserializeObject<List<SiteMenusAccessItemInfo>>(menusAccessInfo.OperationAccess);
            else maitems = new List<SiteMenusAccessItemInfo>();

            foreach (var item in list)
            {
                var menuId = Guid.Parse(item.MenuId.ToString());

                var itemIndex = maitems.FindIndex(m => m.MenuId.Equals(menuId));
                if (itemIndex > -1) maitems[itemIndex] = item;
                else maitems.Add(item);
            }

            if (menusAccessInfo != null)
            {
                menusAccessInfo.OperationAccess = JsonConvert.SerializeObject(maitems);
                maBll.Update(menusAccessInfo);
            }
            else
            {
                menusAccessInfo = new SiteMenusAccessInfo(appId, accessId, JsonConvert.SerializeObject(maitems), accessType);
                maBll.Insert(menusAccessInfo);
            }

            context.Response.Write(ResResult.ResJsonString(true, "", ""));
        }

        #endregion

        #region 基础数据

        private void SaveStockLocation(HttpContext context)
        {
            var Request = context.Request;

            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.Form["Id"])) Guid.TryParse(Request.Form["Id"], out Id);

            #region 请求参数集

            Guid zoneId = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.Form["ZoneId"])) Guid.TryParse(Request.Form["ZoneId"], out zoneId);
            if (zoneId.Equals(Guid.Empty)) throw new ArgumentException(MC.Submit_Params_InvalidError);
            double width = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Width"])) double.TryParse(Request.Form["Width"], out width);
            double wide = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Wide"])) double.TryParse(Request.Form["Wide"], out wide);
            double high = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["High"])) double.TryParse(Request.Form["High"], out high);
            double volume = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Volume"])) double.TryParse(Request.Form["Volume"], out volume);
            double cubage = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Cubage"])) double.TryParse(Request.Form["Cubage"], out cubage);
            double row = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Row"])) double.TryParse(Request.Form["Row"], out row);
            double layer = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Layer"])) double.TryParse(Request.Form["Layer"], out layer);
            double col = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Col"])) double.TryParse(Request.Form["Col"], out col);
            double passway = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Passway"])) double.TryParse(Request.Form["Passway"], out passway);
            double xc = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Xc"])) double.TryParse(Request.Form["Xc"], out xc);
            double yc = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Yc"])) double.TryParse(Request.Form["Yc"], out yc);
            double zc = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Zc"])) double.TryParse(Request.Form["Zc"], out zc);
            double orientation = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["Orientation"])) double.TryParse(Request.Form["Orientation"], out orientation);
            double stackLimit = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["StackLimit"])) double.TryParse(Request.Form["StackLimit"], out stackLimit);
            double groundTrayQty = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["GroundTrayQty"])) double.TryParse(Request.Form["GroundTrayQty"], out groundTrayQty);
            double carryWeight = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["CarryWeight"])) double.TryParse(Request.Form["CarryWeight"], out carryWeight);
            double levelNum = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["LevelNum"])) double.TryParse(Request.Form["LevelNum"], out levelNum);
            double insertTaskSeq = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["InsertTaskSeq"])) double.TryParse(Request.Form["InsertTaskSeq"], out insertTaskSeq);
            double inventoryGroupId = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["InventoryGroupId"])) double.TryParse(Request.Form["InventoryGroupId"], out inventoryGroupId);

            var isMixPlace = false;
            if (!string.IsNullOrWhiteSpace(Request.Form["IsMixPlace"])) bool.TryParse(Request.Form["IsMixPlace"], out isMixPlace);
            var isBatchNum = false;
            if (!string.IsNullOrWhiteSpace(Request.Form["IsBatchNum"])) bool.TryParse(Request.Form["IsBatchNum"], out isBatchNum);
            var isLoseId = false;
            if (!string.IsNullOrWhiteSpace(Request.Form["IsLoseId"])) bool.TryParse(Request.Form["IsLoseId"], out isLoseId);

            var sCoded = Request.Form["Coded"].Trim();
            var sNamed = Request.Form["Named"].Trim();
            var sStockLocationType = Request.Form["StockLocationType"].Trim();
            var sStockLocationDeal = Request.Form["StockLocationDeal"].Trim();
            var sUseStatus = Request.Form["UseStatus"].Trim();
            var sRemark = Request.Form["Remark"].Trim();
            var sRouteSeq = Request.Form["RouteSeq"].Trim();
            var sStatus = Request.Form["Status"].Trim();
            var sWarehouse = Request.Form["Warehouse"].Trim();
            var sLevelNum = Request.Form["LevelNum"].Trim();
            var sCtrType = Request.Form["CtrType"].Trim();
            var sABC = Request.Form["ABC"].Trim();
            var sPickArea = Request.Form["PickArea"].Trim();
            var sPickMethod = Request.Form["PickMethod"].Trim();

            #endregion

            var currTime = DateTime.Now;

            var modelInfo = new StockLocationInfo(Id, WebCommon.GetUserId(), zoneId, sCoded, sNamed, width, wide, high, volume, cubage, row, layer, col, passway, xc, yc, zc, orientation, sStockLocationType, stackLimit, groundTrayQty, sStockLocationDeal, carryWeight, sUseStatus, sRemark, currTime);
            var slcModel = new StockLocationCtrInfo(modelInfo.Id, sRouteSeq, isMixPlace, isBatchNum, isLoseId, sStatus, sWarehouse, levelNum, sCtrType, sABC, insertTaskSeq, sPickArea, sPickMethod, inventoryGroupId);

            var bll = new StockLocation();
            var slcBll = new StockLocationCtr();
            int effect = -1;

            if (Id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                modelInfo.Id = Guid.NewGuid();
                effect = bll.InsertByOutput(modelInfo);
                slcModel.StockLocationId = modelInfo.Id;
                effect += slcBll.Insert(slcModel);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                effect = bll.Update(modelInfo);
                effect += slcBll.Update(slcModel);
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }

            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void DeleteStockLocation(HttpContext context, IList<object> items)
        {
            var bll = new StockLocation();
            if (!bll.DeleteBatch(items))
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void SaveZone(HttpContext context)
        {
            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Id"])) Guid.TryParse(context.Request.Form["Id"], out Id);
            var currTime = DateTime.Now;

            var sCoded = context.Request.Form["Coded"].Trim();
            var sNamed = context.Request.Form["Named"].Trim();
            var sSquare = context.Request.Form["Square"].Trim();
            var sDescr = context.Request.Form["Descr"].Trim();

            var modelInfo = new ZoneInfo(Id, WebCommon.GetUserId(), sCoded, sNamed, sSquare, sDescr, currTime);

            var bll = new Zone();
            int effect = -1;

            if (Id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, false);
                effect = bll.Insert(modelInfo);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                effect = bll.Update(modelInfo);
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }

            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void DeleteZone(HttpContext context, IList<object> items)
        {
            var bll = new Zone();
            if (!bll.DeleteBatch(items))
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void SaveSupplier(HttpContext context)
        {
            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Id"])) Guid.TryParse(context.Request.Form["Id"], out Id);
            var currTime = DateTime.Now;

            var sCoded = context.Request.Form["Coded"].Trim();
            var sNamed = context.Request.Form["Named"].Trim();
            var sShortName = context.Request.Form["ShortName"].Trim();
            var sContactMan = context.Request.Form["ContactMan"].Trim();
            var sEmail = context.Request.Form["Email"].Trim();
            var sPhone = context.Request.Form["Phone"].Trim();
            var sTelPhone = context.Request.Form["TelPhone"].Trim();
            var sFax = context.Request.Form["Fax"].Trim();
            var sPostcode = context.Request.Form["Postcode"].Trim();
            var sAddress = context.Request.Form["Address"].Trim();
            var sRemark = context.Request.Form["Remark"].Trim();

            var modelInfo = new SupplierInfo(Id, WebCommon.GetUserId(), sCoded, sNamed, sShortName, sContactMan, sEmail, sPhone, sTelPhone, sFax, sPostcode, sAddress, sRemark, currTime);

            var bll = new Supplier();
            int effect = -1;

            if (Id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, false);
                effect = bll.Insert(modelInfo);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);
                effect = bll.Update(modelInfo);
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }

            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void DeleteSupplier(HttpContext context, IList<object> items)
        {
            var bll = new Supplier();
            if (!bll.DeleteBatch(items))
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void SaveCompany(HttpContext context)
        {
            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Id"])) Guid.TryParse(context.Request.Form["Id"], out Id);
            var currTime = DateTime.Now;

            var sCoded = context.Request.Form["Coded"].Trim();
            var sNamed = context.Request.Form["Named"].Trim();
            var sShortName = context.Request.Form["ShortName"].Trim();
            var sInCompany = context.Request.Form["InCompany"].Trim();
            var sContactMan = context.Request.Form["ContactMan"].Trim();
            var sContactPhone = context.Request.Form["ContactPhone"].Trim();
            var sTelPhone = context.Request.Form["TelPhone"].Trim();
            var sFax = context.Request.Form["Fax"].Trim();
            var sPostCode = context.Request.Form["PostCode"].Trim();
            var sAddress = context.Request.Form["Address"].Trim();
            var sCompanyAbout = context.Request.Form["CompanyAbout"].Trim();
            var sCompanyType = EnumData.EnumCompanyType.物流公司.ToString();

            var modelInfo = new CompanyInfo(Id, WebCommon.GetUserId(), sCoded, sNamed, sShortName, sInCompany, sContactMan, sContactPhone, sTelPhone, sFax, sPostCode, sAddress, sCompanyAbout, sCompanyType, currTime, currTime);

            var bll = new Company();
            int effect = -1;

            if (Id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, false);
                effect = bll.Insert(modelInfo);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, false);

                var oldInfo = bll.GetModel(Id);
                modelInfo.RecordDate = oldInfo.RecordDate;
                effect = bll.Update(modelInfo);
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }

            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void DeleteCompany(HttpContext context, IList<object> items)
        {
            var bll = new Company();
            if (!bll.DeleteBatch(items))
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        #endregion

        #region 物流配送

        private void SaveLogisticsDistribution(HttpContext context)
        {
            var Request = context.Request;

            #region 请求参数集

            Guid id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.Form["Id"])) Guid.TryParse(Request.Form["Id"], out id);
            Guid userId = WebCommon.GetUserId();
            string orderCode = string.Empty;
            if (Request.Form["OrderCode"] != null) orderCode = Request.Form["OrderCode"].Trim();
            string refOrders = string.Empty;
            if (Request.Form["RefOrders"] != null) refOrders = Request.Form["RefOrders"].Trim();
            Guid companyId = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.Form["CompanyId"])) Guid.TryParse(Request.Form["CompanyId"], out companyId);
            string vehicles = string.Empty;
            if (Request.Form["Vehicles"] != null) vehicles = Request.Form["Vehicles"].Trim();
            double totalPackage = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["TotalPackage"])) double.TryParse(Request.Form["TotalPackage"], out totalPackage);
            double totalVolume = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["TotalVolume"])) double.TryParse(Request.Form["TotalVolume"], out totalVolume);
            double totalWeight = 0d;
            if (!string.IsNullOrWhiteSpace(Request.Form["TotalWeight"])) double.TryParse(Request.Form["TotalWeight"], out totalWeight);
            string toAddress = string.Empty;
            if (Request.Form["ToAddress"] != null) toAddress = Request.Form["ToAddress"].Trim();
            string typeName = string.Empty;
            if (Request.Form["TypeName"] != null) typeName = Request.Form["TypeName"].Trim();
            string remark = string.Empty;
            if (Request.Form["Remark"] != null) remark = Request.Form["Remark"].Trim();
            string deliveryVehicleID = string.Empty;
            if (Request.Form["DeliveryVehicleID"] != null) deliveryVehicleID = Request.Form["DeliveryVehicleID"].Trim();
            string driverName = string.Empty;
            if (Request.Form["DriverName"] != null) driverName = Request.Form["DriverName"].Trim();
            string driverPhone = string.Empty;
            if (Request.Form["DriverPhone"] != null) driverPhone = Request.Form["DriverPhone"].Trim();
            DateTime deliveryStartTime = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(Request.Form["DeliveryStartTime"])) DateTime.TryParse(Request.Form["DeliveryStartTime"], out deliveryStartTime);
            if (deliveryStartTime == DateTime.MinValue) deliveryStartTime = DateTime.Parse("1754-01-01");
            string causeAbout = string.Empty;
            if (Request.Form["CauseAbout"] != null) causeAbout = Request.Form["CauseAbout"].Trim();
            string deliveryStatus = string.Empty;
            if (Request.Form["DeliveryStatus"] != null) deliveryStatus = Request.Form["DeliveryStatus"].Trim();
            string status = string.Empty;
            if (Request.Form["Status"] != null) status = Request.Form["Status"].Trim();

            #endregion

            var currTime = DateTime.Now;
            var modelInfo = new LogisticsDistributionInfo(id, userId, orderCode, refOrders, companyId, vehicles, totalPackage, totalVolume, totalWeight, toAddress, typeName, remark, deliveryVehicleID, driverName, driverPhone, deliveryStartTime, causeAbout, deliveryStatus, status, currTime, currTime);

            var bll = new LogisticsDistribution();
            int effect = 0;

            if (id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, false);

                var rcBll = new RandomCode();
                modelInfo.OrderCode = rcBll.GetOrderCode(((int)EnumData.EnumOrderPrefix.配送).ToString());
                effect = bll.Insert(modelInfo);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, false);

                var oldInfo = bll.GetModel(id);
                modelInfo.OrderCode = oldInfo.OrderCode;
                modelInfo.RecordDate = oldInfo.RecordDate;
                effect = bll.Update(modelInfo);
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }

            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void DeleteLogisticsDistribution(HttpContext context, IList<object> items)
        {
            var bll = new LogisticsDistribution();
            if (!bll.DeleteBatch(items))
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        #endregion

        #region MES

        private void SaveMesCategory(HttpContext context)
        {
            var Request = context.Request;

            #region 请求参数集

            Guid id = Guid.Empty;
            if (Request.Form["Id"] != null) Guid.TryParse(Request.Form["Id"], out id);
            Guid parentId = Guid.Empty;
            if (Request.Form["ParentId"] != null) Guid.TryParse(Request.Form["ParentId"], out parentId);
            string coded = string.Empty;
            if (Request.Form["Coded"] != null) coded = Request.Form["Coded"].Trim();
            string named = string.Empty;
            if (Request.Form["Named"] != null) named = Request.Form["Named"].Trim();
            string step = string.Empty;
            if (Request.Form["Step"] != null) step = Request.Form["Step"].Trim().Trim(',');
            string workStation = string.Empty;
            if (Request.Form["WorkStation"] != null) workStation = Request.Form["WorkStation"].Trim();
            double standardHours = 0d;
            if (Request.Form["StandardHours"] != null) double.TryParse(Request.Form["StandardHours"], out standardHours);
            int sort = 0;
            if (Request.Form["Sort"] != null) int.TryParse(Request.Form["Sort"], out sort);
            string remark = string.Empty;
            if (Request.Form["Remark"] != null) remark = Request.Form["Remark"].Trim();
            Guid userId = WebCommon.GetUserId();

            #endregion

            var currTime = DateTime.Now;
            var bll = new MesCategory();
            if (bll.IsExistCode(coded, id)) throw new ArgumentException(MC.GetString(MC.Params_CodeExistError, coded));

            var barcode = "";
            if(!parentId.Equals(Guid.Empty))
            {
                var stepLen = step.Split(',').Length;
                if (stepLen < 2) barcode = "P" + coded;
                else barcode = "WP" + coded;
            }

            var modelInfo = new MesCategoryInfo(id, userId, parentId, coded, named, step, workStation, standardHours, barcode, sort, remark, currTime);

            
            int effect = 0;

            if (id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                modelInfo.Id = Guid.NewGuid();
                effect = bll.InsertByOutput(modelInfo);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                effect = bll.Update(modelInfo);
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }

            modelInfo.Named = string.Format("{0} {1}", modelInfo.Coded, modelInfo.Named);
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, JsonConvert.SerializeObject(modelInfo)));
        }

        private void DeleteMesCategory(HttpContext context)
        {
            var Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Id"])) Guid.TryParse(context.Request.Form["Id"], out Id);
            if(Id.Equals(Guid.Empty)) throw new ArgumentException(MC.Request_Params_InvalidError);
            var bll = new MesCategory();
            if(bll.Delete(Id) > 0) context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
            else context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
        }

        private void SaveMesProduct(HttpContext context)
        {
            var Request = context.Request;

            #region 请求参数集

            Guid id = Guid.Empty;
            if (Request.Form["Id"] != null) Guid.TryParse(Request.Form["Id"], out id);
            Guid categoryId = Guid.Empty;
            if (Request.Form["CategoryId"] != null) Guid.TryParse(Request.Form["CategoryId"], out categoryId);
            string coded = string.Empty;
            if (Request.Form["Coded"] != null) coded = Request.Form["Coded"].Trim();
            string named = string.Empty;
            if (Request.Form["Named"] != null) named = Request.Form["Named"].Trim();
            double useQty = 0d;
            if (Request.Form["UseQty"] != null) double.TryParse(Request.Form["UseQty"], out useQty);
            int sort = 0;
            if (Request.Form["Sort"] != null) int.TryParse(Request.Form["Sort"], out sort);
            string remark = string.Empty;
            if (Request.Form["Remark"] != null) remark = Request.Form["Remark"].Trim();

            #endregion

            Guid userId = WebCommon.GetUserId();
            var currTime = DateTime.Now;
            var bll = new MesProduct();
            if (bll.IsExistCode(coded, id)) throw new ArgumentException(MC.GetString(MC.Params_CodeExistError, coded));
            var barcode = "pt"+coded;
            var modelInfo = new MesProductInfo(id, userId, categoryId, coded, named, useQty, barcode, sort, remark, currTime);

            int effect = 0;

            if (id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                effect = bll.Insert(modelInfo);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                effect = bll.Update(modelInfo);
            }
            if(effect > 0) context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
            else context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
        }

        private void DeleteMesProduct(HttpContext context, IList<object> items)
        {
            var bll = new MesProduct();
            if(bll.DeleteBatch(items)) context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
            else context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
        }

        #endregion

        #region Infone

        private void SaveInfoneCustomer(HttpContext context)
        {
            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Id"])) Guid.TryParse(context.Request.Form["Id"], out Id);
            var currTime = DateTime.Now;

            var sCoded = context.Request.Form["Coded"].Trim();
            var sNamed = context.Request.Form["Named"].Trim();
            var sShortName = context.Request.Form["ShortName"].Trim();
            var sInCompany = context.Request.Form["InCompany"].Trim();
            var sContactMan = context.Request.Form["ContactMan"].Trim();
            var sContactPhone = context.Request.Form["ContactPhone"].Trim();
            var sTelPhone = context.Request.Form["TelPhone"].Trim();
            var sFax = context.Request.Form["Fax"].Trim();
            var sPostCode = context.Request.Form["PostCode"].Trim();
            var sAddress = context.Request.Form["Address"].Trim();
            var sCompanyAbout = HttpUtility.UrlDecode(context.Request.Form["CompanyAbout"].Trim());

            var modelInfo = new InfoneCustomerInfo(Id, WebCommon.GetUserId(), sCoded, sNamed, sShortName, sInCompany, sContactMan, sContactPhone, sTelPhone, sFax, sPostCode, sAddress, sCompanyAbout, currTime, currTime);

            var bll = new InfoneCustomer();
            int effect = -1;

            if (Id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, false);
                effect = bll.Insert(modelInfo);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, false);

                var oldInfo = bll.GetModel(Id);
                modelInfo.RecordDate = oldInfo.RecordDate;
                effect = bll.Update(modelInfo);
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }

            context.Response.Write(ResResult.ResJsonString(true, "", ""));
        }

        private void SaveInfoneProject(HttpContext context)
        {
            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Id"])) Guid.TryParse(context.Request.Form["Id"], out Id);
            var currTime = DateTime.Now;

            var customerId = Guid.Empty;
            Guid.TryParse(context.Request.Form["CustomerId"], out customerId);
            var sProjectName = context.Request.Form["ProjectName"].Trim();
            var sProjectSource = context.Request.Form["ProjectSource"].Trim();
            var sCustomerOfficial = context.Request.Form["CustomerOfficial"].Trim();
            var sContactMan = context.Request.Form["ContactMan"].Trim();
            var sContactPhone = context.Request.Form["ContactPhone"].Trim();
            var sSpecsModel = context.Request.Form["SpecsModel"].Trim();
            var preQty = 0;
            int.TryParse(context.Request.Form["PreQty"], out preQty);
            decimal preAmount = 0;
            decimal.TryParse(context.Request.Form["PreAmount"], out preAmount);
            var sProjectAbout = HttpUtility.UrlDecode(context.Request.Form["ProjectAbout"].Trim());
            var sStatus = context.Request.Form["Status"].Trim();
            var sRemark = HttpUtility.UrlDecode(context.Request.Form["Remark"].Trim());

            var modelInfo = new InfoneProjectReportPrepareInfo(Id, WebCommon.GetUserId(), customerId, sProjectName, sProjectSource, sCustomerOfficial, sContactMan, sContactPhone, sSpecsModel, preQty, preAmount, sProjectAbout, sStatus, sRemark, currTime, currTime);

            var bll = new InfoneProjectReportPrepare();
            int effect = -1;

            if (Id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, false);
                effect = bll.Insert(modelInfo);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, false);

                var oldInfo = bll.GetModel(Id);
                modelInfo.RecordDate = oldInfo.RecordDate;
                effect = bll.Update(modelInfo);
            }
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }

            context.Response.Write(ResResult.ResJsonString(true, "", ""));
        }

        private void DeleteInfoneCustomer(HttpContext context, IList<object> items)
        {
            var bll = new InfoneCustomer();
            if (!bll.DeleteBatch(items))
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void DeleteInfoneProjectReportPrepare(HttpContext context, IList<object> items)
        {
            var bll = new InfoneProjectReportPrepare();
            if (!bll.DeleteBatch(items))
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void SaveInfoneDeviceRepairRecord(HttpContext context)
        {
            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Id"])) Guid.TryParse(context.Request.Form["Id"], out Id);
            DateTime recordDate = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["RecordDate"])) DateTime.TryParse(context.Request.Form["RecordDate"], out recordDate);
            if (recordDate == DateTime.MinValue) recordDate = DateTime.Parse("1754-01-01");
            DateTime backDate = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["BackDate"])) DateTime.TryParse(context.Request.Form["BackDate"], out backDate);
            if (backDate == DateTime.MinValue) backDate = DateTime.Parse("1754-01-01");
            var sCustomer = context.Request.Form["Customer"].Trim();
            var sSerialNumber = context.Request.Form["SerialNumber"].Trim();
            if (!string.IsNullOrWhiteSpace(sSerialNumber)) sSerialNumber = HttpUtility.UrlDecode(sSerialNumber);
            var sDeviceModel = context.Request.Form["DeviceModel"].Trim();
            var sFaultCause = context.Request.Form["FaultCause"].Trim();
            var sSolveMethod = context.Request.Form["SolveMethod"].Trim();
            var sCustomerProblem = context.Request.Form["CustomerProblem"].Trim();
            var sDevicePart = context.Request.Form["DevicePart"].Trim();
            var sTreatmentSituation = context.Request.Form["TreatmentSituation"].Trim();
            var sWhetherFix = context.Request.Form["WhetherFix"].Trim();
            var sHandoverPerson = context.Request.Form["HandoverPerson"].Trim();
            var isBack = context.Request.Form["IsBack"].Trim() == "1" ? true : false;
            var sRegisteredPerson = context.Request.Form["RegisteredPerson"].Trim();
            var sRemark = context.Request.Form["Remark"].Trim();

            var modelInfo = new InfoneDeviceRepairRecordInfo(Id, WebCommon.GetUserId(), recordDate, sCustomer, sSerialNumber, sDeviceModel, sFaultCause, sSolveMethod, sCustomerProblem, sDevicePart, sTreatmentSituation, sWhetherFix, sHandoverPerson, isBack, backDate, sRegisteredPerson, sRemark, DateTime.Now);

            var bll = new InfoneDeviceRepairRecord();
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
            if (effect < 1)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }

            context.Response.Write(ResResult.ResJsonString(true, "", ""));
        }

        private void DeleteInfoneDeviceRepairRecord(HttpContext context, IList<object> items)
        {
            var bll = new InfoneDeviceRepairRecord();
            if (!bll.DeleteBatch(items))
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
                return;
            }
            context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
        }

        private void SaveInfoneDeviceBorrowRecord(HttpContext context)
        {
            var Request = context.Request;

            #region 请求参数集

            Guid id = Guid.Empty;
            if (Request.Form["Id"] != null) Guid.TryParse(Request.Form["Id"], out id);
            Guid userId = Guid.Empty;
            if (Request.Form["UserId"] != null) Guid.TryParse(Request.Form["UserId"], out userId);
            string customer = string.Empty;
            if (Request.Form["Customer"] != null) customer = Request.Form["Customer"].Trim();
            string customerContact = string.Empty;
            if (Request.Form["CustomerContact"] != null) customerContact = Request.Form["CustomerContact"].Trim();
            string serialNumber = string.Empty;
            if (Request.Form["SerialNumber"] != null) serialNumber = Request.Form["SerialNumber"].Trim();
            string deviceModel = string.Empty;
            if (Request.Form["DeviceModel"] != null) deviceModel = Request.Form["DeviceModel"].Trim();
            string devicePart = string.Empty;
            if (Request.Form["DevicePart"] != null) devicePart = Request.Form["DevicePart"].Trim();
            string partStatus = string.Empty;
            if (Request.Form["PartStatus"] != null) partStatus = Request.Form["PartStatus"].Trim();
            string projectAbout = string.Empty;
            if (Request.Form["ProjectAbout"] != null) projectAbout = Request.Form["ProjectAbout"].Trim();
            string saleMan = string.Empty;
            if (Request.Form["SaleMan"] != null) saleMan = Request.Form["SaleMan"].Trim();
            string sendOrderCode = string.Empty;
            if (Request.Form["SendOrderCode"] != null) sendOrderCode = Request.Form["SendOrderCode"].Trim();
            bool isBack = false;
            if (Request.Form["IsBack"] != null) bool.TryParse(Request.Form["IsBack"], out isBack);
            DateTime backDate = DateTime.Parse("1754-01-01");
            if (Request.Form["BackDate"] != null) DateTime.TryParse(Request.Form["BackDate"], out backDate);
            if (backDate == DateTime.MinValue) backDate = DateTime.Parse("1754-01-01");
            string register = string.Empty;
            if (Request.Form["Register"] != null) register = Request.Form["Register"].Trim();
            string remark = string.Empty;
            if (Request.Form["Remark"] != null) remark = Request.Form["Remark"].Trim();
            string funType = string.Empty;
            if (Request.Form["FunType"] != null) funType = Request.Form["FunType"].Trim();
            DateTime recordDate = DateTime.Parse("1754-01-01");
            if (Request.Form["RecordDate"] != null) DateTime.TryParse(Request.Form["RecordDate"], out recordDate);
            if (recordDate == DateTime.MinValue) recordDate = DateTime.Parse("1754-01-01");

            #endregion

            var currTime = DateTime.Now;

            var modelInfo = new InfoneDeviceBorrowRecordInfo(id, userId, customer, customerContact, serialNumber, deviceModel, devicePart, partStatus, projectAbout, saleMan,sendOrderCode, isBack, backDate, register, remark, funType, recordDate, currTime);

            var bll = new InfoneDeviceBorrowRecord();
            int effect = 0;

            if (id.Equals(Guid.Empty))
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.新增, true);

                effect = bll.Insert(modelInfo);
            }
            else
            {
                MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.编辑, true);

                effect = bll.Update(modelInfo);
            }
            if (effect > 0) context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
            else context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
        }

        private void DeleteInfoneDeviceBorrowRecord(HttpContext context, IList<object> items)
        {
            var bll = new InfoneDeviceBorrowRecord();
            if (bll.DeleteBatch(items)) context.Response.Write(ResResult.ResJsonString(true, MC.M_Save_Ok, ""));
            else context.Response.Write(ResResult.ResJsonString(false, MC.M_Save_Error, ""));
        }

        #endregion
    }
}