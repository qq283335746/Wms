using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ZoneInfo
    {
        public ZoneInfo() { }

        public ZoneInfo(Guid id, Guid userId, string zoneCode, string zoneName, string square, string descr, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.ZoneCode = zoneCode;
            this.ZoneName = zoneName;
            this.Square = square;
            this.Descr = descr;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName { get; set; }
        public string Square { get; set; }
        public string Descr { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
