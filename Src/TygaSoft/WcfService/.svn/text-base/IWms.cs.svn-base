using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using TygaSoft.Model;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "http://TygaSoft.Services.WmsService")]
    public interface IWms
    {
        #region 通知

        [OperationContract(Name = "GetWechatUserList")]
        ResResultModel GetWechatUserList();

        [OperationContract(Name = "SendStockProductNotice")]
        ResResultModel SendStockProductNotice(StockProductNoticeModel model);

        #endregion

        #region 打印、条码

        [OperationContract(Name = "GetBarcodeTemplateInfo")]
        ResResultModel GetBarcodeTemplateInfo(BarcodeTemplateFmModel model);

        [OperationContract(Name = "GetBarcodeBrowser")]
        ResResultModel GetBarcodeBrowser(BarcodeTemplateFmModel model);

        [OperationContract(Name = "GetBarcodeTemplateList")]
        ResResultModel GetBarcodeTemplateList(ListModel model);

        [OperationContract(Name = "SaveBarcodeTemplate")]
        ResResultModel SaveBarcodeTemplate(BarcodeTemplateFmModel model);

        [OperationContract(Name = "DeleteBarcodeTemplate")]
        ResResultModel DeleteBarcodeTemplate(string itemAppend);

        [OperationContract(Name = "SaveSetDefault")]
        ResResultModel SaveSetDefault(BarcodeTemplateFmModel model);

        #endregion

        #region 预收货单、收货单

        [OperationContract(Name = "GetCbgOrderReceipt")]
        ResResultModel GetCbgOrderReceipt(OrderReceiptModel model);

        [OperationContract(Name = "GetOrderReceiptInfo")]
        ResResultModel GetOrderReceiptInfo(Guid Id);

        [OperationContract(Name = "GetOrderReceiptList")]
        ResResultModel GetOrderReceiptList(OrderReceiptModel model);

        [OperationContract(Name = "SaveOrderReceipt")]
        ResResultModel SaveOrderReceipt(OrderReceiptFmModel model);

        [OperationContract(Name = "DeleteOrderReceipt")]
        ResResultModel DeleteOrderReceipt(string itemAppend);

        [OperationContract(Name = "GetOrderReceiptProductList")]
        ResResultModel GetOrderReceiptProductList(OrderReceiptProductModel model);

        [OperationContract(Name = "SaveOrderReceiptProduct")]
        ResResultModel SaveOrderReceiptProduct(OrderReceiptProductFmModel model);

        [OperationContract(Name = "UpdateOrderReceiptProduct")]
        ResResultModel UpdateOrderReceiptProduct(UpdateOrderReceiptProductFmModel model);

        [OperationContract(Name = "DeleteOrderReceiptProduct")]
        ResResultModel DeleteOrderReceiptProduct(Guid orderId, string itemAppend);

        #endregion

        #region 库存

        //[OperationContract(Name = "GetOrderSelectProductList")]
        //ResResultModel GetOrderSelectProductList(OrderSelectProductModel model);

        [OperationContract(Name = "GetStockProductList")]
        ResResultModel GetStockProductList(StockProductModel model);

        #endregion

        #region 上架任务

        [OperationContract(Name = "CreateShelfMission")]
        ResResultModel CreateShelfMission(string itemAppend);

        [OperationContract(Name = "SaveShelfMissionProduct")]
        ResResultModel SaveShelfMissionProduct(string itemAppend);

        [OperationContract(Name = "GetShelfMissionInfo")]
        ResResultModel GetShelfMissionInfo(Guid Id);

        [OperationContract(Name = "GetShelfMissionProductList")]
        ResResultModel GetShelfMissionProductList(ShelfMissionProductModel model);

        [OperationContract(Name = "GetShelfMissionList")]
        ResResultModel GetShelfMissionList(ShelfMissionModel model);

        [OperationContract(Name = "SaveShelfMission")]
        ResResultModel SaveShelfMission(ShelfMissionFmModel model);

        [OperationContract(Name = "DeleteShelfMission")]
        ResResultModel DeleteShelfMission(string itemAppend);

        [OperationContract(Name = "GetCbbOrderReceipt")]
        ResResultModel GetCbbOrderReceipt();

        #endregion

        #region 发货

        [OperationContract(Name = "GetOrderSendInfo")]
        ResResultModel GetOrderSendInfo(Guid Id);

        [OperationContract(Name = "GetOrderSendProductList")]
        ResResultModel GetOrderSendProductList(OrderSendProductModel model);

        [OperationContract(Name = "SaveOrderSendProduct")]
        ResResultModel SaveOrderSendProduct(Guid Id, string itemAppend);

        [OperationContract(Name = "GetOrderSendList")]
        ResResultModel GetOrderSendList(OrderSendModel model);

        [OperationContract(Name = "SaveOrderSend")]
        ResResultModel SaveOrderSend(OrderSendFmModel model);

        [OperationContract(Name = "DeleteOrderSend")]
        ResResultModel DeleteOrderSend(string itemAppend);

        [OperationContract(Name = "DeleteOrderSendProduct")]
        ResResultModel DeleteOrderSendProduct(Guid orderId, string itemAppend);

        #endregion

        #region 拣货

        [OperationContract(Name = "GetOrderPickedInfo")]
        ResResultModel GetOrderPickedInfo(Guid Id);

        [OperationContract(Name = "GetOrderPickedList")]
        ResResultModel GetOrderPickedList(OrderSendModel model);

        [OperationContract(Name = "GetOrderPickProductList")]
        ResResultModel GetOrderPickProductList(OrderPickedProductModel model);

        [OperationContract(Name = "CreateOrderPicked")]
        ResResultModel CreateOrderPicked(string itemAppend);

        [OperationContract(Name = "SaveOrderPickProduct")]
        ResResultModel SaveOrderPickProduct(string itemAppend);

        #endregion

        #region 盘点

        [OperationContract(Name = "GetPandianList")]
        ResResultModel GetPandianList(ListModel model);

        [OperationContract(Name = "GetPandianInfo")]
        ResResultModel GetPandianInfo(Guid Id);

        [OperationContract(Name = "SavePandian")]
        ResResultModel SavePandian(PandianFmModel model);

        [OperationContract(Name = "DeletePandian")]
        ResResultModel DeletePandian(string itemAppend);

        [OperationContract(Name = "GetPandianProductList")]
        ResResultModel GetPandianProductList(ListModel model);

        [OperationContract(Name = "SavePandianProduct")]
        ResResultModel SavePandianProduct(PandianProductFmModel model);

        [OperationContract(Name = "DeletePandianProduct")]
        ResResultModel DeletePandianProduct(string itemAppend);

        #endregion

        #region 客户管理

        [OperationContract(Name = "SaveCustomer")]
        ResResultModel SaveCustomer(CustomerModel model);

        [OperationContract(Name = "DeleteCustomer")]
        ResResultModel DeleteCustomer(string itemAppend);

        [OperationContract(Name = "GetCustomerList")]
        ResResultModel GetCustomerList(ListModel model);

        #endregion

        #region 供应商管理

        [OperationContract(Name = "GetCbbSupplier")]
        ResResultModel GetCbbSupplier();

        #endregion

        #region 库区管理

        [OperationContract(Name = "GetCbbZone")]
        ResResultModel GetCbbZone();

        #endregion

        #region 库位管理

        [OperationContract(Name = "GetStockLocationProductList")]
        ResResultModel GetStockLocationProductList(StockLocationProductModel model);

        [OperationContract(Name = "GetStockLocationList")]
        ResResultModel GetStockLocationList(StockLocationModel model);

        [OperationContract(Name = "GetCbbStockLocation")]
        ResResultModel GetCbbStockLocation();

        #endregion

        #region 库存预警管理

        [OperationContract(Name = "GetStockWarningList")]
        ResResultModel GetStockWarningList(ListModel model);

        [OperationContract(Name = "SaveStockWarning")]
        ResResultModel SaveStockWarning(StockWarningFmModel model);

        [OperationContract(Name = "DeleteStockWarning")]
        ResResultModel DeleteStockWarning(string itemAppend);

        #endregion

        #region 货品管理

        [OperationContract(Name = "GetCategoryTree")]
        ResResultModel GetCategoryTree();

        [OperationContract(Name = "SaveCategory")]
        ResResultModel SaveCategory(CategoryModel model);

        [OperationContract(Name = "DeleteCategory")]
        ResResultModel DeleteCategory(Guid Id);

        [OperationContract(Name = "SaveProduct")]
        ResResultModel SaveProduct(ProductFmModel model);

        [OperationContract(Name = "DeleteProduct")]
        ResResultModel DeleteProduct(string itemAppend);

        [OperationContract(Name = "GetProductList")]
        ResResultModel GetProductList(ProductModel model);

        #endregion

        #region 包装管理

        [OperationContract(Name = "SavePackage")]
        ResResultModel SavePackage(PackageModel model);

        [OperationContract(Name = "DeletePackage")]
        ResResultModel DeletePackage(string itemAppend);

        [OperationContract(Name = "GetPackageList")]
        ResResultModel GetPackageList(DlgPackageModel model);

        #endregion

        #region 车辆

        [OperationContract(Name = "GetVehicleList")]
        ResResultModel GetVehicleList(ListModel model);

        [OperationContract(Name = "SaveVehicle")]
        ResResultModel SaveVehicle(VehicleFmModel model);

        [OperationContract(Name = "DeleteVehicle")]
        ResResultModel DeleteVehicle(string itemAppend);

        #endregion

        #region 图片、文件管理

        [OperationContract(Name = "GetSitePictureList")]
        ResResultModel GetSitePictureList(ListModel model);

        [OperationContract(Name = "DeleteSitePicture")]
        ResResultModel DeleteSitePicture(string itemAppend);

        //[OperationContract(Name = "HtmlToPdf")]
        //ResResultModel HtmlToPdf(ToPdfModel model);

        #endregion
    }
}
