using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class PandianProductInfo
    {
        public PandianProductInfo() { }

        public PandianProductInfo(Guid pandianId, Guid userId, Guid productId, Guid customerId, string zones, string stockLocations, double stayQty, string updatedZones, string updatedStockLocations, double qty, double failQty, string status, string remark, DateTime lastUpdatedDate)
        {
            this.PandianId = pandianId;
            this.UserId = userId;
            this.ProductId = productId;
            this.CustomerId = customerId;
            this.Zones = zones;
            this.StockLocations = stockLocations;
            this.StayQty = stayQty;
            this.UpdatedZones = updatedZones;
            this.UpdatedStockLocations = updatedStockLocations;
            this.Qty = qty;
            this.FailQty = failQty;
            this.Status = status;
            this.Remark = remark;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid PandianId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public string Zones { get; set; }
        public string StockLocations { get; set; }
        public double StayQty { get; set; }
        public string UpdatedZones { get; set; }
        public string UpdatedStockLocations { get; set; }
        public double Qty { get; set; }
        public double FailQty { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
