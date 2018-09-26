using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using TygaSoft.Model;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "http://TygaSoft.Services.PdaService")]
    public interface IPda
    {
        #region RFID

        [OperationContract(Name = "SaveRFIDQueue")]
        ResResultModel SaveRFIDQueue(RFIDModel model);

        #endregion

        #region MES

        [OperationContract(Name = "SaveMesOrder")]
        ResResultModel SaveMesOrder(MesOrderModel model);

        #endregion

        #region 条码扫描上传服务器处理

        [OperationContract(Name = "SaveBarcodeScanQueue")]
        ResResultModel SaveBarcodeScanQueue(BarcodeScanQueueModel model);

        #endregion

        #region 物流公司查单信息

        [OperationContract(Name = "GetLogisticsDistributionByOrdercode")]
        ResResultModel GetLogisticsDistributionByOrdercode(string OrderCode);

        [OperationContract(Name = "SaveLogisticsDistribution")]
        ResResultModel SaveLogisticsDistribution(PdaLogisticsDistributionModel model);

        #endregion

        #region 收货

        [OperationContract(Name = "GetOrderIdByReceipt")]
        ResResultModel GetOrderIdByReceipt(string orderNum);

        [OperationContract(Name = "GetSkuModel")]
        ResResultModel GetSkuModel(object orderId, string productCode);

        [OperationContract(Name = "SaveOrderReceiptRecord")]
        ResResultModel SaveOrderReceiptRecord(PdaOrderReceiptRecordModel model);

        #endregion

        #region 上架

        [OperationContract(Name = "GetShelfMissionList")]
        ResResultModel GetShelfMissionList(PdaShelfMissionModel model);

        [OperationContract(Name = "GetShelfMissionProductList")]
        ResResultModel GetShelfMissionProductList(PdaShelfMissionProductModel model);

        [OperationContract(Name = "SaveShelfMissionProduct")]
        ResResultModel SaveShelfMissionProduct(string itemAppend);

        #endregion

        #region 拣货

        [OperationContract(Name = "GetOrderPickedList")]
        ResResultModel GetOrderPickedList(OrderSendModel model);

        [OperationContract(Name = "GetOrderPickProductList")]
        ResResultModel GetOrderPickProductList(PdaOrderPickProductModel model);

        [OperationContract(Name = "SaveOrderPickProduct")]
        ResResultModel SaveOrderPickProduct(string itemAppend);

        #endregion

        #region 盘点

        [OperationContract(Name = "GetPandianList")]
        ResResultModel GetPandianList(ListModel model);

        [OperationContract(Name = "GetPandianProductList")]
        ResResultModel GetPandianProductList(ListModel model);

        [OperationContract(Name = "SavePandianProduct")]
        ResResultModel SavePandianProduct(PandianProductFmModel model);

        #endregion

        #region 库位货品

        [OperationContract(Name = "GetStockLocationProductList")]
        ResResultModel GetStockLocationProductList(StockLocationProductModel model);

        #endregion

        #region 库存

        [OperationContract(Name = "GetStockProductList")]
        ResResultModel GetStockProductList(StockProductModel model);

        #endregion

        #region 包装

        [OperationContract(Name = "GetCbbPackage")]
        IList<ComboboxInfo> GetCbbPackage();

        #endregion

        #region 单位

        [OperationContract(Name = "GetCbbUnit")]
        IList<ComboboxInfo> GetCbbUnit();

        #endregion

    }
}
