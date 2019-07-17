using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class InfoneCustomerInfo
    {
        public InfoneCustomerInfo() { }

        public InfoneCustomerInfo(Guid id, Guid userId, string coded, string named, string shortName, string inCompany, string contactMan, string contactPhone, string telPhone, string fax, string postCode, string address, string companyAbout, DateTime recordDate, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.Coded = coded;
            this.Named = named;
            this.ShortName = shortName;
            this.InCompany = inCompany;
            this.ContactMan = contactMan;
            this.ContactPhone = contactPhone;
            this.TelPhone = telPhone;
            this.Fax = fax;
            this.PostCode = postCode;
            this.Address = address;
            this.CompanyAbout = companyAbout;
            this.RecordDate = recordDate;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Coded { get; set; }
        public string Named { get; set; }
        public string ShortName { get; set; }
        public string InCompany { get; set; }
        public string ContactMan { get; set; }
        public string ContactPhone { get; set; }
        public string TelPhone { get; set; }
        public string Fax { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string CompanyAbout { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
