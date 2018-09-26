using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class SiteMultiInfo
    {
        public SiteMultiInfo() { }

        public SiteMultiInfo(Guid id, string coded, string named, string siteLogo, string siteTitle, string cultureName, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.Coded = coded;
            this.Named = named;
            this.SiteLogo = siteLogo;
            this.SiteTitle = siteTitle;
            this.CultureName = cultureName;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public string Coded { get; set; }
        public string Named { get; set; }
        public string SiteLogo { get; set; }
        public string SiteTitle { get; set; }
        public string CultureName { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
