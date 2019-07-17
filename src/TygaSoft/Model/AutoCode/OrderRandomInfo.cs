using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderRandomInfo
    {
        public OrderRandomInfo() { }

        public OrderRandomInfo(string orderCode, string prefix, DateTime lastUpdatedDate)
        {
            this.OrderCode = orderCode;
            this.Prefix = prefix;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public string OrderCode { get; set; }
        public string Prefix { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
