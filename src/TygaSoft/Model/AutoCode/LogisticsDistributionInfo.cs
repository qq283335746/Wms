using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class LogisticsDistributionInfo
    {
        public LogisticsDistributionInfo() { }

        public LogisticsDistributionInfo(Guid id, Guid userId, string orderCode, string refOrders, Guid companyId, string vehicles, double totalPackage, double totalVolume, double totalWeight, string toAddress, string typeName, string remark, string deliveryVehicleID, string driverName, string driverPhone, DateTime deliveryStartTime, string causeAbout, string deliveryStatus, string status, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.OrderCode = orderCode;
            this.RefOrders = refOrders;
            this.CompanyId = companyId;
            this.Vehicles = vehicles;
            this.TotalPackage = totalPackage;
            this.TotalVolume = totalVolume;
            this.TotalWeight = totalWeight;
            this.ToAddress = toAddress;
            this.TypeName = typeName;
            this.Remark = remark;
            this.DeliveryVehicleID = deliveryVehicleID;
            this.DriverName = driverName;
            this.DriverPhone = driverPhone;
            this.DeliveryStartTime = deliveryStartTime;
            this.CauseAbout = causeAbout;
            this.DeliveryStatus = deliveryStatus;
            this.Status = status;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public string RefOrders { get; set; }
        public Guid CompanyId { get; set; }
        public string Vehicles { get; set; }
        public double TotalPackage { get; set; }
        public double TotalVolume { get; set; }
        public double TotalWeight { get; set; }
        public string ToAddress { get; set; }
        public string TypeName { get; set; }
        public string Remark { get; set; }
        public string DeliveryVehicleID { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public DateTime DeliveryStartTime { get; set; }
        public string CauseAbout { get; set; }
        public string DeliveryStatus { get; set; }
        public string Status { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
