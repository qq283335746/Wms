using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderReceiptProductAttrInfo
    {
        public OrderReceiptProductAttrInfo() { }

        public OrderReceiptProductAttrInfo(Guid orderProductId, string packageName, string supplierName, string produceDate, string qualityStatus, string purchaseOrderCode)
        {
            this.OrderProductId = orderProductId;
            this.PackageName = packageName;
            this.SupplierName = supplierName;
            this.ProduceDate = produceDate;
            this.QualityStatus = qualityStatus;
            this.PurchaseOrderCode = purchaseOrderCode;
        }

        public Guid OrderProductId { get; set; }
        public string PackageName { get; set; }
        public string SupplierName { get; set; }
        public string ProduceDate { get; set; }
        public string QualityStatus { get; set; }
        public string PurchaseOrderCode { get; set; }
    }
}
