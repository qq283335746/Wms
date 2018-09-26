using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ShelfMissionProductInfo
    {
        public ShelfMissionProductInfo() { }

        public ShelfMissionProductInfo(Guid shelfMissionId, Guid orderId, Guid productId, double stayQty, double qty, string stockLocations, string status, DateTime lastUpdatedDate)
        {
            this.ShelfMissionId = shelfMissionId;
            this.OrderId = orderId;
            this.ProductId = productId;
            this.StayQty = stayQty;
            this.Qty = qty;
            this.StockLocations = stockLocations;
            this.Status = status;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid ShelfMissionId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public double StayQty { get; set; }
        public double Qty { get; set; }
        public string StockLocations { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
