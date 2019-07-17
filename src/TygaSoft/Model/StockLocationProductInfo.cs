using System;

namespace TygaSoft.Model
{
    public partial class StockLocationProductInfo
    {
        public string StockLocationCode { get; set; }
        public string StockLocationName { get; set; }
        public Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public Guid CustomerId { get; set; }
        public double MaxQty { get; set; }
        public bool IsHas { get; set; }
        public double Volume { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsBest { get; set; }
    }
}
