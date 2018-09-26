using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Transactions;
using System.Web.Configuration;
using TygaSoft.WebHelper;
using TygaSoft.DBUtility;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.Web.Handlers
{
    /// <summary>
    /// HandlerUpload 的摘要说明
    /// </summary>
    public class HandlerUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
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
                    case "ImportDeviceRepairRecord":
                        ImportDeviceRepairRecord(context);
                        break;
                    case "ImportProduct":
                        ImportProduct(context);
                        break;
                    case "ExportStockProduct":
                        ExportStockProduct(context);
                        break;
                    case "ExportDeviceBorrowRecord":
                        ExportDeviceBorrowRecord(context);
                        break;
                    case "ExportDeviceRepairRecord":
                        ExportDeviceRepairRecord(context);
                        break;
                    case "Logo":
                        UploadSitePicture(context, reqName);
                        break;
                    case "Vehicle":
                        UploadSitePicture(context, reqName);
                        break;
                    default:
                        throw new ArgumentException(MC.Request_Params_InvalidError);
                }
            }
            catch(Exception ex)
            {
                context.Response.Write("{\"success\": false,\"message\": \"" + ex.Message + "\"}");
            }
            
        }

        private void ImportDeviceRepairRecord(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                HttpFileCollection files = context.Request.Files;
                if (files.Count == 0)
                {
                    context.Response.Write("{\"success\": false,\"message\": \"未找到任何可上传的文件，请检查！\"}");
                    return;
                }
                foreach (string item in files.AllKeys)
                {
                    HttpPostedFile file = files[item];
                    if (file == null || file.ContentLength == 0)
                    {
                        continue;
                    }
                    ImportDeviceRepairRecord(context, file);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        private void ImportProduct(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                HttpFileCollection files = context.Request.Files;
                if (files.Count == 0)
                {
                    context.Response.Write("{\"success\": false,\"message\": \"未找到任何可上传的文件，请检查！\"}");
                    return;
                }
                foreach (string item in files.AllKeys)
                {
                    HttpPostedFile file = files[item];
                    if (file == null || file.ContentLength == 0)
                    {
                        continue;
                    }
                    ImportProduct(context, file);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        private void ImportDeviceRepairRecord(HttpContext context, HttpPostedFile file)
        {
            var dt = OpenXmlHelper.Import(file.InputStream);
            var drc = dt.Rows;
            if (drc.Count == 0)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.Import_NotDataError, ""));
                return;
            }

            var currTime = DateTime.Now;
            DateTime time = DateTime.MinValue;
            var list = new List<InfoneDeviceRepairRecordInfo>();
            var userId = WebCommon.GetUserId();
            foreach (DataRow dr in drc)
            {
                if (dr["日期"] != null) DateTime.TryParse(dr["日期"].ToString(), out time);
                if (time == DateTime.MinValue) throw new ArgumentException(MC.Import_InvalidError);
                var backDate = DateTime.MinValue;
                DateTime.TryParse(dr["归还日期"].ToString(), out backDate);
                if (backDate == DateTime.MinValue) backDate = DateTime.Parse("1754-01-01");

                var modelInfo = new InfoneDeviceRepairRecordInfo(Guid.Empty, userId, time, dr["客户"].ToString(), dr["序列号"].ToString(), dr["型号"].ToString(), dr["故障原因"].ToString(), dr["解决方案"].ToString(), dr["客户问题"].ToString(), dr["配件"].ToString(), dr["处理情况"].ToString(), dr["是否修好"].ToString(), dr["交接人"].ToString(), dr["是否归还"].ToString() == "是" ? true : false, backDate, dr["登记人"].ToString(), dr["备注"].ToString(), currTime);
                list.Add(modelInfo);
            }

            var bll = new InfoneDeviceRepairRecord();
            var index = 0;

            foreach (var model in list)
            {
                model.UserId = userId;
                if (bll.Insert(model) < 1) throw new ArgumentException(string.Format("{0}", index > 0 ? "部分数据已经成功导入，但是执行到第“" + index + "”行时发生异常" : "数据导入失败，行“" + index + "”发生异常"));
                index++;
            }
            context.Response.Write(ResResult.ResJsonString(true, "导入成功", ""));
        }

        private void ImportProduct(HttpContext context, HttpPostedFile file)
        {
            var dt = OpenXmlHelper.Import(file.InputStream);
            var drc = dt.Rows;
            if (drc.Count == 0)
            {
                context.Response.Write(ResResult.ResJsonString(false, MC.Import_NotDataError, ""));
                return;
            }

            var currTime = DateTime.Now;
            var list = new List<ImportProductInfo>();

            foreach (DataRow dr in drc)
            {
                var sort = 0;
                if (dr["排序"] != null) Int32.TryParse(dr["排序"].ToString(), out sort);

                var modelInfo = new ImportProductInfo();
                modelInfo.Sort = sort;
                modelInfo.Coded = dr["物料代码"].ToString().Trim();
                modelInfo.Name = dr["物料名称"].ToString().Trim();
                modelInfo.FullName = dr["全名"].ToString().Trim();
                modelInfo.SpeModels = dr["规格型号"].ToString().Trim();
                modelInfo.Remark = dr["备注"].ToString().Trim();
                modelInfo.CodeItems = modelInfo.Coded.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                modelInfo.CodeItemsLen = modelInfo.CodeItems.Length;

                if (string.IsNullOrWhiteSpace(modelInfo.Coded) || string.IsNullOrWhiteSpace(modelInfo.Name)) throw new ArgumentException("存在物料代码或物料名称为空字符串的行，请核对后再重试！");
                modelInfo.IsCategory = modelInfo.Remark.Contains("类");

                list.Add(modelInfo);
            }

            var cBll = new Category();
            var pBll = new Product();
            var okIndex = 0;
            var userId = WebCommon.GetUserId();

            var categoryList = list.Where(m => m.IsCategory).OrderBy(m => m.CodeItemsLen);
            var productList = list.Where(m => !m.IsCategory).OrderBy(m => m.CodeItemsLen);
            var categoryRoot = cBll.GetRootModel();

            var cLen = categoryList.Count();
            var pLen = productList.Count();

            foreach (var item in categoryList)
            {
                var parentId = categoryRoot.Id;
                if (item.CodeItemsLen > 1)
                {
                    var parentCode = "";
                    for (var j = 0; j < (item.CodeItemsLen - 1); j++)
                    {
                        if (j > 0) parentCode += ".";
                        parentCode += item.CodeItems[j];
                    }
                    var parentInfo = cBll.GetModelByCode(parentCode);
                    if (parentInfo != null) parentId = parentInfo.Id;
                }
                var categoryInfo = cBll.GetModelByCode(item.Coded);
                if (categoryInfo == null)
                {
                    var currId = Guid.NewGuid();
                    categoryInfo = new CategoryInfo(currId, userId, parentId, item.Coded, item.Name, string.Format("{0},{1}", currId, parentId), item.Sort, item.Remark, currTime);
                    cBll.InsertByOutput(categoryInfo);
                    okIndex++;
                }
                var currCategoryProductList = productList.Where(m => m.Coded.StartsWith(item.Coded) && m.CodeItemsLen == (item.CodeItemsLen + 1));
                if (currCategoryProductList != null && currCategoryProductList.Count() > 0)
                {
                    foreach (var pItem in currCategoryProductList)
                    {
                        if (!pBll.IsExistCode(pItem.Coded, Guid.Empty))
                        {
                            var productInfo = new ProductInfo(Guid.NewGuid(), userId, categoryInfo.Id, Guid.Empty, pItem.Coded, pItem.Name, pItem.FullName, pItem.SpeModels, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, pItem.Sort, pItem.Remark, false, currTime);
                            pBll.InsertByOutput(productInfo);
                            okIndex++;
                        }

                    }
                }
            }
            context.Response.Write(ResResult.ResJsonString(true, "导入成功", ""));
        }

        private void ExportDeviceRepairRecord(HttpContext context)
        {
            try
            {
                #region 动态创建查询条件

                var sqlWhere = new StringBuilder(1000);
                var parms = new ParamsHelper();
                SqlParameter parm = null;

                var keyword = string.Empty;
                if (!string.IsNullOrWhiteSpace(context.Request.QueryString["keyword"])) keyword = HttpUtility.UrlDecode(context.Request.QueryString["keyword"]).Trim();
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    sqlWhere.Append(@"and (drr.Customer like @Customer or drr.SerialNumber like @SerialNumber 
                                       or drr.DeviceModel like @DeviceModel or drr.FaultCause like @FaultCause or drr.SolveMethod like @SolveMethod 
                                       or drr.CustomerProblem like @CustomerProblem or drr.DevicePart like @DevicePart or drr.TreatmentSituation like @TreatmentSituation
                                       or drr.HandoverPerson like @HandoverPerson or drr.RegisteredPerson like @RegisteredPerson or drr.Remark like @Remark) ");

                    parms.Add(new SqlParameter("@Customer", "%" + keyword + "%"));
                    parms.Add(new SqlParameter("@SerialNumber", "%" + keyword + "%"));
                    parms.Add(new SqlParameter("@DeviceModel", "%" + keyword + "%"));
                    parms.Add(new SqlParameter("@FaultCause", "%" + keyword + "%"));
                    parms.Add(new SqlParameter("@SolveMethod", "%" + keyword + "%"));
                    parms.Add(new SqlParameter("@CustomerProblem", "%" + keyword + "%"));
                    parms.Add(new SqlParameter("@DevicePart", "%" + keyword + "%"));
                    parms.Add(new SqlParameter("@TreatmentSituation", "%" + keyword + "%"));
                    parms.Add(new SqlParameter("@HandoverPerson", "%" + keyword + "%"));
                    parms.Add(new SqlParameter("@RegisteredPerson", "%" + keyword + "%"));
                    parms.Add(new SqlParameter("@Remark", "%" + keyword + "%"));
                }
                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(context.Request.QueryString["startDate"]))
                {
                    DateTime.TryParse(context.Request.QueryString["startDate"], out startDate);
                }
                if (!string.IsNullOrWhiteSpace(context.Request.QueryString["endDate"]))
                {
                    DateTime.TryParse(context.Request.QueryString["endDate"], out endDate);
                }
                if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
                {
                    sqlWhere.AppendFormat(@"and (drr.RecordDate between @StartDate and @EndDate) ");
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
                        sqlWhere.AppendFormat(@"and (drr.RecordDate >= @StartDate) ");
                        parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        parm.Value = startDate;
                        parms.Add(parm);
                    }
                    if (endDate != DateTime.MinValue)
                    {
                        sqlWhere.AppendFormat(@"and (drr.RecordDate <= @EndDate) ");
                        parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        parm.Value = DateTime.Parse(endDate.ToString("yyyy-MM-dd") + " 23:59:59");
                        parms.Add(parm);
                    }
                }

                if (!string.IsNullOrWhiteSpace(context.Request.QueryString["backDate"]))
                {
                    sqlWhere.AppendFormat(@"and drr.BackDate = @BackDate ");
                    parm = new SqlParameter("@BackDate", SqlDbType.DateTime);
                    parm.Value = DateTime.Parse(context.Request.QueryString["backDate"]);
                    parms.Add(parm);
                }
                if (!string.IsNullOrWhiteSpace(context.Request.QueryString["whetherFix"]))
                {
                    sqlWhere.AppendFormat(@"and drr.WhetherFix = @WhetherFix ");
                    parm = new SqlParameter("@WhetherFix", SqlDbType.NVarChar, 20);
                    parm.Value = HttpUtility.UrlDecode(context.Request.QueryString["whetherFix"].Trim());
                    parms.Add(parm);
                }
                if (!string.IsNullOrWhiteSpace(context.Request.QueryString["isBack"]))
                {
                    sqlWhere.AppendFormat(@"and drr.IsBack = @IsBack ");
                    parm = new SqlParameter("@IsBack", SqlDbType.Bit);
                    parm.Value = context.Request.QueryString["isBack"].Trim() == "1";
                    parms.Add(parm);
                }

                #endregion

                var bll = new InfoneDeviceRepairRecord();
                var dt = bll.GetExportToExcelData(sqlWhere == null ? "" : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                OpenXmlHelper.Export(context, dt);
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        private void ExportDeviceBorrowRecord(HttpContext context)
        {
            try
            {
                StringBuilder sqlWhere = null;
                ParamsHelper parms = null;

                #region 构造查询条件

                var keyword = context.Request.QueryString["Keyword"];
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    if (parms == null) parms = new ParamsHelper();
                    if (sqlWhere == null) sqlWhere = new StringBuilder(1000);
                    sqlWhere.Append("and (Customer like @Keyword or SerialNumber like @Keyword or DeviceModel like @Keyword or DevicePart like @Keyword or PartStatus like @Keyword or ProjectAbout like @Keyword or SaleMan like @Keyword or Register like @Keyword or Remark like @Keyword) ");
                    var parm = new SqlParameter("@Keyword", SqlDbType.NVarChar, 20);
                    parm.Value = parm.Value = "%" + keyword + "%";
                    parms.Add(parm);
                }
                var typeName = context.Request.QueryString["TypeName"];
                if (!string.IsNullOrWhiteSpace(typeName))
                {
                    if (parms == null) parms = new ParamsHelper();
                    if (sqlWhere == null) sqlWhere = new StringBuilder(100);
                    sqlWhere.Append("and FunType=@FunType ");
                    var parm = new SqlParameter("@FunType", SqlDbType.NVarChar, 20);
                    parm.Value = parm.Value = typeName;
                    parms.Add(parm);
                }

                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(context.Request.QueryString["StartDate"])) DateTime.TryParse(context.Request.QueryString["StartDate"], out startDate);
                if (!string.IsNullOrWhiteSpace(context.Request.QueryString["EndDate"])) DateTime.TryParse(context.Request.QueryString["EndDate"], out endDate);

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
                if (!string.IsNullOrWhiteSpace(context.Request.QueryString["BackDate"])) DateTime.TryParse(context.Request.QueryString["BackDate"], out backDate);
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
                if (!string.IsNullOrWhiteSpace(context.Request.QueryString["IsBack"]) && bool.TryParse(context.Request.QueryString["IsBack"], out isBack))
                {
                    if (sqlWhere == null) sqlWhere = new StringBuilder(100);
                    if (parms == null) parms = new ParamsHelper();

                    sqlWhere.Append(@"and IsBack = @IsBack ");
                    var parm = new SqlParameter("@IsBack", SqlDbType.Bit);
                    parm.Value = isBack;
                    parms.Add(parm);
                }

                #endregion

                var bll = new InfoneDeviceBorrowRecord();
                var dt = bll.GetDsExport(sqlWhere == null ? "" : sqlWhere.ToString(), parms == null ? null : parms.ToArray());

                OpenXmlHelper.Export(context, dt.Tables[0]);
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        private void ExportStockProduct(HttpContext context)
        {
            var bll = new StockProduct();
            var ds = bll.GetExportData();
            var dt = ds.Tables[0];
            OpenXmlHelper.Export(context, dt);
        }

        private void UploadSitePicture(HttpContext context,string funType)
        {
            HttpFileCollection files = context.Request.Files;
            if (files.Count == 0)
            {
                context.Response.Write("{\"success\": false,\"message\": \"未找到任何可上传的文件，请检查！\"}");
                return;
            }

            var userId = WebCommon.GetUserId();
            int effect = 0;
            ImagesHelper ih = new ImagesHelper();

            using (TransactionScope scope = new TransactionScope())
            {
                foreach (string item in files.AllKeys)
                {
                    HttpPostedFile file = files[item];
                    if (file == null || file.ContentLength == 0) continue;

                    int fileSize = file.ContentLength;
                    int uploadFileSize = int.Parse(WebConfigurationManager.AppSettings["UploadFileSize"]);
                    if (fileSize > uploadFileSize) throw new ArgumentException("文件【" + file.FileName + "】大小超出字节" + uploadFileSize + "，无法上传，请正确操作！");
                    if (!UploadFilesHelper.IsFileValidated(file.InputStream, fileSize)) throw new ArgumentException("文件【" + file.FileName + "】为受限制的文件，请正确操作！");

                    //string fileName = file.FileName;
                    var bll = new SitePicture();
                    //if (bll.IsExist(userId, file.FileName, fileSize)) throw new ArgumentException("文件【" + file.FileName + "】已存在，请勿重复上传！");

                    string originalUrl = UploadFilesHelper.UploadOriginalFile(file, "SitePicture");

                    var model = new SitePictureInfo(Guid.Empty, userId, VirtualPathUtility.GetFileName(originalUrl), fileSize, VirtualPathUtility.GetExtension(originalUrl).ToLower(), VirtualPathUtility.GetDirectory(originalUrl.Replace("~", "")), Path.GetFileNameWithoutExtension(context.Server.MapPath(originalUrl)), funType, DateTime.Now);
                    CreateThumbnailImage(context, ih, context.Server.MapPath(originalUrl));

                    bll.Insert(model);
                    effect++;
                }

                scope.Complete();
            }

            if (effect > 0) context.Response.Write("{\"success\": true,\"message\": \"已成功上传文件数：" + effect + "个\"}");
            else context.Response.Write("{\"success\": false,\"message\": \"上传失败，请检查！\"}");
        }

        #region 私有方法

        private void CreateThumbnailImage(HttpContext context, ImagesHelper ih, string originalPath)
        {
            var ext = Path.GetExtension(originalPath);
            var rndFolder = Path.GetFileNameWithoutExtension(originalPath);
            string[] platformNames = Enum.GetNames(typeof(EnumData.Platform));
            var index = 0;
            foreach (string name in platformNames)
            {
                string sizeAppend = WebConfigurationManager.AppSettings[name];
                string[] sizeArr = sizeAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < sizeArr.Length; i++)
                {
                    string bmsPath = string.Format("{0}\\{1}_{2}{3}{4}", Path.GetDirectoryName(originalPath), rndFolder, index, i+1, ext);
                    string[] wh = sizeArr[i].Split('*');

                    ih.CreateThumbnailImage(originalPath, bmsPath, int.Parse(wh[0]), int.Parse(wh[1]), "DB", ext);
                }
                index++;
            }
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}