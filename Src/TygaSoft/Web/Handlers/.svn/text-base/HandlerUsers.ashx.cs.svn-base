using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using TygaSoft.DBUtility;
using TygaSoft.CustomProvider;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;
using TygaSoft.Model;
using TygaSoft.BLL;

namespace TygaSoft.Web.Handlers
{
    public class HandlerUsers : IHttpHandler
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
                if (string.IsNullOrWhiteSpace(reqName)) return;
                reqName = reqName.Trim();

                switch (reqName)
                {
                    case "GetMapStaticImage":
                        GetMapStaticImage(context);
                        break;
                    case "GetMesCategoryInfo":
                        GetMesCategoryInfo(context);
                        break;
                    case "GetMesCategoryTree":
                        GetMesCategoryTree(context);
                        break;
                    case "GetMesProductList":
                        GetList(context, reqName);
                        break;
                    case "GetMesOrderList":
                        GetList(context, reqName);
                        break;
                    case "GetRFIDList":
                        GetList(context, reqName);
                        break;
                    case "GetOrderSendList":
                        GetList(context, reqName);
                        break;
                    case "GetTotalOrderSendProduct":
                        GetTotalOrderSendProduct(context);
                        break;
                    case "GetZoneList":
                        GetList(context, reqName);
                        break;
                    case "GetStockLocationList":
                        GetList(context, reqName);
                        break;
                    case "GetSupplierList":
                        GetList(context, reqName);
                        break;
                    case "GetCustomerList":
                        GetList(context, reqName);
                        break;
                    case "GetLogisticsDistributionList":
                        GetList(context, reqName);
                        break;
                    case "GetLogisticsDistributionInfo":
                        GetLogisticsDistributionInfo(context);
                        break;
                    case "GetCompanyList":
                        GetList(context, reqName);
                        break;
                    case "GetVehicleList":
                        GetList(context, reqName);
                        break;
                    case "GetOrderPickProductInfo":
                        GetInfo(context, reqName);
                        break;
                    case "GetShelfMissionProductInfo":
                        GetInfo(context, reqName);
                        break;
                    case "GetFeatureUserInfo":
                        GetFeatureUserInfo(context);
                        break;
                    case "GetSiteMultiList":
                        GetList(context, reqName);
                        break;
                    case "GetInfoneDeviceBorrowRecordList":
                        GetList(context, reqName);
                        break;
                    case "GetInfoneDeviceRepairRecordList":
                        GetList(context, reqName);
                        break;
                    case "GetInfoneCustomerList":
                        GetList(context, reqName);
                        break;
                    case "GetInfoneProjectReportPrepareList":
                        GetList(context, reqName);
                        break;
                    case "GetInfoneCustomerInfo":
                        GetInfo(context, reqName);
                        break;
                    case "GetInfoneProjectReportPrepareInfo":
                        GetInfo(context, reqName);
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

        private void GetInfo(HttpContext context, string reqName)
        {
            MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

            var Id = context.Request.Form["Id"];

            switch (reqName)
            {
                case "GetOrderPickProductInfo":
                    GetOrderPickProductInfo(context, Guid.Parse(Id));
                    break;
                case "GetShelfMissionProductInfo":
                    GetShelfMissionProductInfo(context, Guid.Parse(Id));
                    break;
                case "GetInfoneCustomerInfo":
                    GetInfoneCustomerInfo(context, Guid.Parse(Id));
                    break;
                case "GetInfoneProjectReportPrepareInfo":
                    GetInfoneProjectReportPrepareInfo(context, Guid.Parse(Id));
                    break;
                default:
                    break;
            }
        }

        private void GetList(HttpContext context, string reqName)
        {
            MenusDataProxy.ValidateAccess((int)EnumData.EnumOperationAccess.浏览, true);

            int pageIndex = 1, pageSize = 10;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["PageIndex"])) int.TryParse(context.Request.Form["PageIndex"], out pageIndex);
            if (!string.IsNullOrWhiteSpace(context.Request.Form["PageSize"])) int.TryParse(context.Request.Form["PageSize"], out pageSize);
            var parentId = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["ParentId"])) Guid.TryParse(context.Request.Form["ParentId"], out parentId);
            var keyword = context.Request.Form["Keyword"];
            var typeName = context.Request.Form["TypeName"];

            switch (reqName)
            {
                case "GetMesProductList":
                    GetMesProductList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetMesOrderList":
                    GetMesOrderList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetRFIDList":
                    GetRFIDList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetOrderSendList":
                    GetOrderSendList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetLogisticsDistributionList":
                    GetLogisticsDistributionList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetZoneList":
                    GetZoneList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetStockLocationList":
                    GetStockLocationList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetSupplierList":
                    GetSupplierList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetCustomerList":
                    GetCustomerList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetCompanyList":
                    GetCompanyList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetVehicleList":
                    GetVehicleList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetSiteMultiList":
                    GetSiteMultiList(context, pageIndex, pageSize, keyword);
                    break;
                case "GetInfoneDeviceBorrowRecordList":
                    GetInfoneDeviceBorrowRecordList(context, pageIndex, pageSize, keyword, typeName);
                    break;
                case "GetInfoneCustomerList":
                    GetInfoneCustomerList(context, pageIndex, pageSize, keyword, typeName, parentId);
                    break;
                case "GetInfoneProjectReportPrepareList":
                    GetInfoneProjectReportPrepareList(context, pageIndex, pageSize, keyword, typeName, parentId);
                    break;
                case "GetInfoneDeviceRepairRecordList":
                    GetInfoneDeviceRepairRecordList(context, pageIndex, pageSize, keyword);
                    break;
                default:
                    break;
            }
        }

        #region 拣货

        public void GetOrderPickProductInfo(HttpContext context, Guid Id)
        {
            var orderId = Guid.Parse(context.Request.Form["OrderId"]);
            var productId = Guid.Parse(context.Request.Form["ProductId"]);
            var customerId = Guid.Parse(context.Request.Form["CustomerId"]);
            var bll = new OrderPickProduct();
            context.Response.Write(ResResult.ResJsonString(true, "", JsonConvert.SerializeObject(bll.GetModel(Id, orderId, productId, customerId))));
        }

        #endregion

        #region 上架

        public void GetShelfMissionProductInfo(HttpContext context, Guid Id)
        {
            var orderId = Guid.Parse(context.Request.Form["OrderId"]);
            var productId = Guid.Parse(context.Request.Form["ProductId"]);
            var bll = new ShelfMissionProduct();
            context.Response.Write(ResResult.ResJsonString(true, "", JsonConvert.SerializeObject(bll.GetModel(Id, orderId, productId))));
        }

        #endregion

        #region 订单

        private void GetOrderSendList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new OrderSend();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (o.OrderCode like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }
            var list = bll.GetList(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetTotalOrderSendProduct(HttpContext context)
        {
            var orderIds = context.Request.Form["OrderIds"];
            if (string.IsNullOrWhiteSpace(orderIds)) throw new ArgumentException(MC.Request_Params_InvalidError);

            var bll = new OrderSendProduct();
            var datas = bll.GetTotalByOrders(orderIds);

            var s = string.Format(@"{{""TotalPackage"":{0},""TotalVolume"":{1},""TotalWeight"":{2}}}", datas[0], datas[1], datas[2]);

            context.Response.Write(ResResult.ResJsonString(true, "", s));
        }

        #endregion

        #region 基础数据

        private void GetZoneList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new Zone();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (ZoneCode like @Keyword or ZoneName like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }

            var list = bll.GetList(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetStockLocationList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new StockLocation();
            IList<StockLocationInfo> list;
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            var zoneId = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["ZoneId"])) Guid.TryParse(context.Request.Form["ZoneId"], out zoneId);
            if (!zoneId.Equals(Guid.Empty))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(300);
                if (parms == null) parms = new ParamsHelper();
                sqlWhere.Append("and (ZoneId = @ZoneId) ");
                var parm = new SqlParameter("@ZoneId", SqlDbType.UniqueIdentifier);
                parm.Value = parm.Value = zoneId;
                parms.Add(parm);
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(200);
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append("and (sl.Code like @Keyword or sl.Named like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }

            if (!zoneId.Equals(Guid.Empty))
            {
                list = bll.GetList(sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
            }
            else
            {
                list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());
            }

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetSupplierList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new Supplier();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (SupplierCode like @Keyword or SupplierName like @Keyword or ShortName like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }

            var list = bll.GetList(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetCustomerList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new Customer();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (c.Coded like @Keyword or c.Named like @Keyword or c.ShortName like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }

            var list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetCompanyList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new Company();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (c.Coded like @Keyword or c.Named like @Keyword or c.ShortName like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }
            var list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetVehicleList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new Vehicle();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (v.VehicleID like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }
            var list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        #endregion

        #region 物流配送

        private void GetLogisticsDistributionList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new LogisticsDistribution();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;
            var isSelfView = false;

            if (HttpContext.Current.User.IsInRole("SelfView"))
            {
                isSelfView = true;
                if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                sqlWhere.AppendFormat("and lgd.CompanyId in (select FeatureId from FeatureUser fu where fu.UserId = '{0}' and TypeName = 'Company') ", WebCommon.GetUserId());
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                parms = new ParamsHelper();
                sqlWhere.Append("and (lgd.OrderCode like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }

            var list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + ",\"IsSelfView\":\"" + isSelfView + "\"}"));
        }

        private void GetLogisticsDistributionInfo(HttpContext context)
        {
            var Id = Guid.Empty;
            Guid.TryParse(context.Request.Form["Id"], out Id);
            if (Id.Equals(Guid.Empty))
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.Request_Params_InvalidError, ""));
                return;
            }
            var bll = new LogisticsDistribution();
            var oldInfo = bll.GetModel(Id);
            if (oldInfo == null)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.Data_NotExist, ""));
                return;
            }

            context.Response.Write(ResResult.ResJsonString(true, "", JsonConvert.SerializeObject(oldInfo)));
        }

        #endregion

        #region 地图

        private void GetMapStaticImage(HttpContext context)
        {
            var width = 0;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Width"])) int.TryParse(context.Request.Form["Width"], out width);
            var height = 0;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Height"])) int.TryParse(context.Request.Form["Height"], out height);
            var center = string.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Center"])) center = context.Request.Form["Center"].Trim();
            var markers = string.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["Markers"])) markers = context.Request.Form["Markers"].Trim();
            var markerStyles = string.Empty;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["MarkerStyles"])) markerStyles = context.Request.Form["MarkerStyles"].Trim();

            MapHelper mh = new MapHelper();
            var imageUrl = mh.GetStaticImage(width, height, center, markers, markerStyles);

            context.Response.Write(ResResult.ResJsonString(true, "", imageUrl));
        }

        #endregion

        #region 公共

        private void GetRFIDList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new RFID();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                parms = new ParamsHelper();
                sqlWhere.Append("and (r.TID like @Keyword or r.EPC like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }

            var list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetFeatureUserInfo(HttpContext context)
        {
            var userId = Guid.Parse(Membership.GetUser(context.Request.Form["UserName"]).ProviderUserKey.ToString());
            var typeName = context.Request.Form["TypeName"];

            var bll = new FeatureUser();
            FeatureUserInfo fuInfo = bll.GetModel(userId, typeName);
            if (fuInfo != null) context.Response.Write(ResResult.ResJsonString(true, "", JsonConvert.SerializeObject(fuInfo)));
            else context.Response.Write(ResResult.ResJsonString(true, "", "{}"));
        }

        #endregion

        #region 系统管理

        private void GetSiteMultiList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new SiteMulti();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (Coded like @Keyword or Named like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }

            var list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        #endregion

        #region MES

        private void GetMesCategoryTree(HttpContext context)
        {
            var bll = new MesCategory();
            context.Response.Write(ResResult.ResJsonString(true, "", bll.GetTreeJson()));
        }

        private void GetMesCategoryInfo(HttpContext context)
        {
            var Id = Guid.Empty;
            Guid.TryParse(context.Request.Form["Id"], out Id);
            if (Id.Equals(Guid.Empty))
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.Request_Params_InvalidError, ""));
                return;
            }
            var bll = new MesCategory();
            var oldInfo = bll.GetModel(Id);
            if (oldInfo == null)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.Data_NotExist, ""));
                return;
            }

            context.Response.Write(ResResult.ResJsonString(true, "", JsonConvert.SerializeObject(oldInfo)));
        }

        private void GetMesProductList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new MesProduct();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (Coded like @Keyword or Named like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }

            var list = bll.GetList(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetMesOrderList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new MesOrder();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (OBarcode like @Keyword or PBarcode like @Keyword or PdBarcode like @Keyword or PtBarcode like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }

            var list = bll.GetList(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        #endregion

        #region Infone

        public void GetInfoneProjectReportPrepareInfo(HttpContext context, Guid Id)
        {
            var bll = new InfoneProjectReportPrepare();
            context.Response.Write(ResResult.ResJsonString(true, "", JsonConvert.SerializeObject(bll.GetModelByJoin(Id))));
        }

        public void GetInfoneCustomerInfo(HttpContext context, Guid Id)
        {
            var bll = new InfoneCustomer();
            context.Response.Write(ResResult.ResJsonString(true, "", JsonConvert.SerializeObject(bll.GetModel(Id))));
        }

        private void GetInfoneCustomerList(HttpContext context, int pageIndex, int pageSize, string keyword, string typeName, Guid parentId)
        {
            var bll = new InfoneCustomer();
            if (typeName == "GetCustomersByProjectId" && parentId != Guid.Empty)
            {
                var cList = bll.GetCustomersByProjectId(parentId);
                context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + cList.Count + ",\"rows\":" + JsonConvert.SerializeObject(cList) + "}"));
                return;
            }

            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (c.Coded like @Keyword or c.Named like @Keyword or c.ShortName like @Keyword or c.ContactMan like @Keyword or c.ContactPhone like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }
            var list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? "" : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetInfoneProjectReportPrepareList(HttpContext context, int pageIndex, int pageSize, string keyword, string typeName, Guid parentId)
        {
            var bll = new InfoneProjectReportPrepare();
            if (typeName == "GetProjectsByCustomerId" && parentId != Guid.Empty)
            {
                var prpList = bll.GetProjectsByCustomerId(parentId);
                context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + prpList.Count + ",\"rows\":" + JsonConvert.SerializeObject(prpList) + "}"));
                return;
            }

            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                parms = new ParamsHelper();
                sqlWhere = new StringBuilder("and (prp.ProjectName like @Keyword or prp.SpecsModel like @Keyword or prp.ContactMan like @Keyword or prp.ContactPhone like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }
            var list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? "" : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetInfoneDeviceRepairRecordList(HttpContext context, int pageIndex, int pageSize, string keyword)
        {
            var bll = new InfoneDeviceRepairRecord();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;
            SqlParameter parm = null;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(2000);
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append(@"and (drr.Customer like @Keyword or drr.SerialNumber like @Keyword 
                                       or drr.DeviceModel like @Keyword or drr.FaultCause like @Keyword or drr.SolveMethod like @Keyword
                                       or drr.CustomerProblem like @Keyword or drr.DevicePart like @Keyword or drr.TreatmentSituation like @Keyword
                                       or drr.HandoverPerson like @Keyword or drr.RegisteredPerson like @Keyword or drr.Remark like @Keyword) ");

                parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 50);
                parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["StartDate"])) DateTime.TryParse(context.Request.Form["StartDate"], out startDate);
            if (!string.IsNullOrWhiteSpace(context.Request.Form["EndDate"])) DateTime.TryParse(context.Request.Form["EndDate"], out endDate);

            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append(@"and (drr.RecordDate between @StartDate and @EndDate) ");
                parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                parm.Value = startDate;
                parms.Add(parm);
                parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                parm.Value = DateTime.Parse(endDate.ToString("yyyy-MM-dd") + " 23:59:59");
                parms.Add(parm);
            }
            else
            {
                if (startDate != DateTime.MinValue)
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                    if (parms == null) parms = new ParamsHelper();

                    sqlWhere.Append(@"and (drr.RecordDate >= @StartDate) ");
                    parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                    parm.Value = startDate;
                    parms.Add(parm);
                }
                if (endDate != DateTime.MinValue)
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                    if (parms == null) parms = new ParamsHelper();

                    sqlWhere.Append(@"and (drr.RecordDate <= @EndDate) ");
                    parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                    parm.Value = DateTime.Parse(endDate.ToString("yyyy-MM-dd") + " 23:59:59");
                    parms.Add(parm);
                }
            }

            var backDate = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["BackDate"])) DateTime.TryParse(context.Request.Form["BackDate"], out backDate);
            if (backDate != DateTime.MinValue)
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(500);
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append(@"and drr.BackDate = @BackDate ");
                parm = new SqlParameter("@BackDate", SqlDbType.DateTime);
                parm.Value = backDate;
                parms.Add(parm);
            }
            if (!string.IsNullOrWhiteSpace(context.Request.Form["WhetherFix"]))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(300);
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append(@"and drr.WhetherFix = @WhetherFix ");
                parm = new SqlParameter("@WhetherFix", SqlDbType.NVarChar, 20);
                parm.Value = HttpUtility.UrlDecode(context.Request.Form["WhetherFix"].Trim());
                parms.Add(parm);
            }
            if (!string.IsNullOrWhiteSpace(context.Request.Form["IsBack"]))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(50);
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append(@"and drr.IsBack = @IsBack ");
                parm = new SqlParameter("@IsBack", SqlDbType.Bit);
                parm.Value = context.Request.Form["IsBack"].Trim() == "1";
                parms.Add(parm);
            }
            var list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? "" : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        private void GetInfoneDeviceBorrowRecordList(HttpContext context, int pageIndex, int pageSize, string keyword, string typeName)
        {
            var bll = new InfoneDeviceBorrowRecord();
            int totalRecord = 0;
            StringBuilder sqlWhere = null;
            ParamsHelper parms = null;

            #region 构造查询条件

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                if (parms == null) parms = new ParamsHelper();
                if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                sqlWhere.Append("and (Customer like @Keyword or SerialNumber like @Keyword or DeviceModel like @Keyword or DevicePart like @Keyword or PartStatus like @Keyword or ProjectAbout like @Keyword or SaleMan like @Keyword or Register like @Keyword or Remark like @Keyword) ");
                var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 20);
                parm.Value = parm.Value = "%" + keyword + "%";
                parms.Add(parm);
            }
            if(!string.IsNullOrWhiteSpace(typeName))
            {
                if(parms == null) parms = new ParamsHelper();
                if (sqlWhere == null) sqlWhere = new StringBuilder(100);
                sqlWhere.Append("and FunType=@FunType ");
                var parm = new SqlParameter("@FunType", SqlDbType.NVarChar,20);
                parm.Value = parm.Value = typeName;
                parms.Add(parm);
            }

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["StartDate"])) DateTime.TryParse(context.Request.Form["StartDate"], out startDate);
            if (!string.IsNullOrWhiteSpace(context.Request.Form["EndDate"])) DateTime.TryParse(context.Request.Form["EndDate"], out endDate);

            if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(700);
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append(@"and (RecordDate between @StartDate and @EndDate) ");
                var parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                parm.Value = startDate;
                parms.Add(parm);
                parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                parm.Value = DateTime.Parse(endDate.ToString("yyyy-MM-dd") + " 23:59:59");
                parms.Add(parm);
            }
            else
            {
                if (startDate != DateTime.MinValue)
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(700);
                    if (parms == null) parms = new ParamsHelper();

                    sqlWhere.Append(@"and (RecordDate >= @StartDate) ");
                    var parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                    parm.Value = startDate;
                    parms.Add(parm);
                }
                if (endDate != DateTime.MinValue)
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(700);
                    if (parms == null) parms = new ParamsHelper();

                    sqlWhere.Append(@"and (RecordDate <= @EndDate) ");
                    var parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                    parm.Value = DateTime.Parse(endDate.ToString("yyyy-MM-dd") + " 23:59:59");
                    parms.Add(parm);
                }
            }

            var backDate = DateTime.MinValue;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["BackDate"])) DateTime.TryParse(context.Request.Form["BackDate"], out backDate);
            if (backDate != DateTime.MinValue)
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(500);
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append(@"and BackDate = @BackDate ");
                var parm = new SqlParameter("@BackDate", SqlDbType.DateTime);
                parm.Value = backDate;
                parms.Add(parm);
            }
            var isBack = false;
            if (!string.IsNullOrWhiteSpace(context.Request.Form["IsBack"]) && bool.TryParse(context.Request.Form["IsBack"], out isBack))
            {
                if (sqlWhere == null) sqlWhere = new StringBuilder(100);
                if (parms == null) parms = new ParamsHelper();

                sqlWhere.Append(@"and IsBack = @IsBack ");
                var parm = new SqlParameter("@IsBack", SqlDbType.Bit);
                parm.Value = isBack;
                parms.Add(parm);
            }

            #endregion

            var list = bll.GetListByJoin(pageIndex, pageSize, out totalRecord, sqlWhere == null ? null : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

            context.Response.Write(ResResult.ResJsonString(true, "", "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}"));
        }

        #endregion
    }
}