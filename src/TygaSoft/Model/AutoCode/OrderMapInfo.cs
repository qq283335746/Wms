using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderMapInfo
    {
        public OrderMapInfo() { }

        public OrderMapInfo(Guid userId, string orderCode, string lnglat, string iP, string provinceCity, string address, string platform, DateTime lastUpdatedDate)
        {
            this.UserId = userId;
            this.OrderCode = orderCode;
            this.Lnglat = lnglat;
            this.IP = iP;
            this.ProvinceCity = provinceCity;
            this.Address = address;
            this.Platform = platform;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public string Lnglat { get; set; }
        public string IP { get; set; }
        public string ProvinceCity { get; set; }
        public string Address { get; set; }
        public string Platform { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
