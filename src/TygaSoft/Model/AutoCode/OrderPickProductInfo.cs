using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderPickProductInfo
    {
        public OrderPickProductInfo() { }

        public OrderPickProductInfo(Guid orderPickId, Guid orderId, Guid productId, Guid customerId, double stayQty, double qty, string stockLocations, string status, DateTime lastUpdatedDate)
        {
            this.OrderPickId = orderPickId;
            this.OrderId = orderId;
            this.ProductId = productId;
            this.CustomerId = customerId;
            this.StayQty = stayQty;
            this.Qty = qty;
            this.StockLocations = stockLocations;
            this.Status = status;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid OrderPickId { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public double StayQty { get; set; }
        public double Qty { get; set; }
        public string StockLocations { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
