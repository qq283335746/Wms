using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class InfoneProjectReportPrepareInfo
    {
        public InfoneProjectReportPrepareInfo() { }

        public InfoneProjectReportPrepareInfo(Guid id, Guid userId, Guid customerId, string projectName, string projectSource, string customerOfficial, string contactMan, string contactPhone, string specsModel, int preQty, decimal preAmount, string projectAbout, string status, string remark, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.CustomerId = customerId;
            this.ProjectName = projectName;
            this.ProjectSource = projectSource;
            this.CustomerOfficial = customerOfficial;
            this.ContactMan = contactMan;
            this.ContactPhone = contactPhone;
            this.SpecsModel = specsModel;
            this.PreQty = preQty;
            this.PreAmount = preAmount;
            this.ProjectAbout = projectAbout;
            this.Status = status;
            this.Remark = remark;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectSource { get; set; }
        public string CustomerOfficial { get; set; }
        public string ContactMan { get; set; }
        public string ContactPhone { get; set; }
        public string SpecsModel { get; set; }
        public int PreQty { get; set; }
        public decimal PreAmount { get; set; }
        public string ProjectAbout { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
