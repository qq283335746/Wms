using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderSendProductInfo
    {
        public OrderSendProductInfo() { }

        public OrderSendProductInfo(Guid orderId, Guid productId, Guid customerId, double qty, double pickQty, string status, DateTime lastUpdatedDate)
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            this.CustomerId = customerId;
            this.Qty = qty;
            this.PickQty = pickQty;
            this.Status = status;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public double Qty { get; set; }
        public double PickQty { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
