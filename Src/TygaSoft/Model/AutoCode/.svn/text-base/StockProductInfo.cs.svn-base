using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class StockProductInfo
    {
        public StockProductInfo() { }

        public StockProductInfo(Guid productId, Guid customerId, double qty, double unQty, double freezeQty, string stepCode, string lastStepName, string status, string stockLocations, string warnMsg, DateTime lastUpdatedDate)
        {
            this.ProductId = productId;
            this.CustomerId = customerId;
            this.Qty = qty;
            this.UnQty = unQty;
            this.FreezeQty = freezeQty;
            this.StepCode = stepCode;
            this.LastStepName = lastStepName;
            this.Status = status;
            this.StockLocations = stockLocations;
            this.WarnMsg = warnMsg;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public double Qty { get; set; }
        public double UnQty { get; set; }
        public double FreezeQty { get; set; }
        public string StepCode { get; set; }
        public string LastStepName { get; set; }
        public string Status { get; set; }
        public string StockLocations { get; set; }
        public string WarnMsg { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
