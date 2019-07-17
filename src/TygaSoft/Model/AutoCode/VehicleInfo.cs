using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class VehicleInfo
    {
        public VehicleInfo() { }

        public VehicleInfo(Guid id, Guid userId, string vehicleID, string vehicleModel, string licence, Guid licPic, string offenceRecord, string driverID, Guid driverIDPicture, string rewardRecord, string remark, int sort, bool isDisable, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.VehicleID = vehicleID;
            this.VehicleModel = vehicleModel;
            this.Licence = licence;
            this.LicPic = licPic;
            this.OffenceRecord = offenceRecord;
            this.DriverID = driverID;
            this.DriverIDPicture = driverIDPicture;
            this.RewardRecord = rewardRecord;
            this.Remark = remark;
            this.Sort = sort;
            this.IsDisable = isDisable;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string VehicleID { get; set; }
        public string VehicleModel { get; set; }
        public string Licence { get; set; }
        public Guid LicPic { get; set; }
        public string OffenceRecord { get; set; }
        public string DriverID { get; set; }
        public Guid DriverIDPicture { get; set; }
        public string RewardRecord { get; set; }
        public string Remark { get; set; }
        public int Sort { get; set; }
        public bool IsDisable { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
