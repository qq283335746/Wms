using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class SupplierInfo
    {
        public SupplierInfo() { }

        public SupplierInfo(Guid id, Guid userId, string supplierCode, string supplierName, string shortName, string contactMan, string email, string phone, string telPhone, string fax, string postcode, string address, string remark, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.SupplierCode = supplierCode;
            this.SupplierName = supplierName;
            this.ShortName = shortName;
            this.ContactMan = contactMan;
            this.Email = email;
            this.Phone = phone;
            this.TelPhone = telPhone;
            this.Fax = fax;
            this.Postcode = postcode;
            this.Address = address;
            this.Remark = remark;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ShortName { get; set; }
        public string ContactMan { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TelPhone { get; set; }
        public string Fax { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
