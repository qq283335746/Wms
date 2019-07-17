using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderReceiptRecordInfo
    {
        public OrderReceiptRecordInfo() { }

        public OrderReceiptRecordInfo(Guid id, Guid orderId, Guid userId, Guid productId, Guid packageId, Guid stockLocationId, string unit, double qty, string lPN, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.OrderId = orderId;
            this.UserId = userId;
            this.ProductId = productId;
            this.PackageId = packageId;
            this.StockLocationId = stockLocationId;
            this.Unit = unit;
            this.Qty = qty;
            this.LPN = lPN;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid PackageId { get; set; }
        public Guid StockLocationId { get; set; }
        public string Unit { get; set; }
        public double Qty { get; set; }
        public string LPN { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
