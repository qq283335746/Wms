using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class StockWarningInfo
    {
        public StockWarningInfo() { }

        public StockWarningInfo(Guid id, Guid userId, Guid zoneId, Guid stockLocationId, string coded, string zoneProperty, string stockLocationProperty, decimal stockAmount, int overdueDay, double minQty, double maxQty, string remark, int sort, bool isDisable, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.ZoneId = zoneId;
            this.StockLocationId = stockLocationId;
            this.Coded = coded;
            this.ZoneProperty = zoneProperty;
            this.StockLocationProperty = stockLocationProperty;
            this.StockAmount = stockAmount;
            this.OverdueDay = overdueDay;
            this.MinQty = minQty;
            this.MaxQty = maxQty;
            this.Remark = remark;
            this.Sort = sort;
            this.IsDisable = isDisable;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ZoneId { get; set; }
        public Guid StockLocationId { get; set; }
        public string Coded { get; set; }
        public string ZoneProperty { get; set; }
        public string StockLocationProperty { get; set; }
        public decimal StockAmount { get; set; }
        public int OverdueDay { get; set; }
        public double MinQty { get; set; }
        public double MaxQty { get; set; }
        public string Remark { get; set; }
        public int Sort { get; set; }
        public bool IsDisable { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
