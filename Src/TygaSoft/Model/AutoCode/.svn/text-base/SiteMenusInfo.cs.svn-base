using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class SiteMenusInfo
    {
        public SiteMenusInfo() { }

        public SiteMenusInfo(Guid applicationId, Guid id, Guid parentId, string idStep, string title, string url, string descr, int sort, DateTime lastUpdatedDate)
        {
            this.ApplicationId = applicationId;
            this.Id = id;
            this.ParentId = parentId;
            this.IdStep = idStep;
            this.Title = title;
            this.Url = url;
            this.Descr = descr;
            this.Sort = sort;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid ApplicationId { get; set; }
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string IdStep { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Descr { get; set; }
        public int Sort { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
