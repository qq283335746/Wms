using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class PandianInfo
    {
        public PandianInfo() { }

        public PandianInfo(Guid id, Guid userId, string orderCode, string named, string allowUsers, string remark, DateTime stockStartDate, DateTime stockEndDate, string customers, string zones, string stockLocations, double totalQty, string status, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.OrderCode = orderCode;
            this.Named = named;
            this.AllowUsers = allowUsers;
            this.Remark = remark;
            this.StockStartDate = stockStartDate;
            this.StockEndDate = stockEndDate;
            this.Customers = customers;
            this.Zones = zones;
            this.StockLocations = stockLocations;
            this.TotalQty = totalQty;
            this.Status = status;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public string Named { get; set; }
        public string AllowUsers { get; set; }
        public string Remark { get; set; }
        public DateTime StockStartDate { get; set; }
        public DateTime StockEndDate { get; set; }
        public string Customers { get; set; }
        public string Zones { get; set; }
        public string StockLocations { get; set; }
        public double TotalQty { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
