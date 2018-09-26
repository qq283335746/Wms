using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class SiteRolesInfo
    {
        public SiteRolesInfo() { }

        public SiteRolesInfo(Guid applicationId, Guid id, string named, string lowerName, DateTime lastUpdatedDate)
        {
            this.ApplicationId = applicationId;
            this.Id = id;
            this.Named = named;
            this.LowerName = lowerName;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid ApplicationId { get; set; }
        public Guid Id { get; set; }
        public string Named { get; set; }
        public string LowerName { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
