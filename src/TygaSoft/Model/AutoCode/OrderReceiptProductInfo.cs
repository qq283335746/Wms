using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderReceiptProductInfo
    {
        public OrderReceiptProductInfo() { }

        public OrderReceiptProductInfo(Guid id, Guid userId, Guid orderId, Guid productId, Guid packageId, string unit, double expectedQty, double receiptQty, DateTime recordDate, string preOrderCode, string purchaseOrderCode, string status, int sort, string remark, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.OrderId = orderId;
            this.ProductId = productId;
            this.PackageId = packageId;
            this.Unit = unit;
            this.ExpectedQty = expectedQty;
            this.ReceiptQty = receiptQty;
            this.RecordDate = recordDate;
            this.PreOrderCode = preOrderCode;
            this.PurchaseOrderCode = purchaseOrderCode;
            this.Status = status;
            this.Sort = sort;
            this.Remark = remark;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid PackageId { get; set; }
        public string Unit { get; set; }
        public double ExpectedQty { get; set; }
        public double ReceiptQty { get; set; }
        public DateTime RecordDate { get; set; }
        public string PreOrderCode { get; set; }
        public string PurchaseOrderCode { get; set; }
        public string Status { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
