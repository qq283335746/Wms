using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class InfoneDeviceBorrowRecordInfo
    {
        public InfoneDeviceBorrowRecordInfo() { }

        public InfoneDeviceBorrowRecordInfo(Guid id, Guid userId, string customer, string customerContact, string serialNumber, string deviceModel, string devicePart, string partStatus, string projectAbout, string saleMan, string sendOrderCode, bool isBack, DateTime backDate, string register, string remark, string funType, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.Customer = customer;
            this.CustomerContact = customerContact;
            this.SerialNumber = serialNumber;
            this.DeviceModel = deviceModel;
            this.DevicePart = devicePart;
            this.PartStatus = partStatus;
            this.ProjectAbout = projectAbout;
            this.SaleMan = saleMan;
            this.SendOrderCode = sendOrderCode;
            this.IsBack = isBack;
            this.BackDate = backDate;
            this.Register = register;
            this.Remark = remark;
            this.FunType = funType;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Customer { get; set; }
        public string CustomerContact { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceModel { get; set; }
        public string DevicePart { get; set; }
        public string PartStatus { get; set; }
        public string ProjectAbout { get; set; }
        public string SaleMan { get; set; }
        public string SendOrderCode { get; set; }
        public bool IsBack { get; set; }
        public DateTime BackDate { get; set; }
        public string Register { get; set; }
        public string Remark { get; set; }
        public string FunType { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
