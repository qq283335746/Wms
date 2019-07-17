using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class MesCategoryInfo
    {
        public MesCategoryInfo() { }

        public MesCategoryInfo(Guid id, Guid userId, Guid parentId, string coded, string named, string step, string workStation, double standardHours, string barcode, int sort, string remark, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.ParentId = parentId;
            this.Coded = coded;
            this.Named = named;
            this.Step = step;
            this.WorkStation = workStation;
            this.StandardHours = standardHours;
            this.Barcode = barcode;
            this.Sort = sort;
            this.Remark = remark;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ParentId { get; set; }
        public string Coded { get; set; }
        public string Named { get; set; }
        public string Step { get; set; }
        public string WorkStation { get; set; }
        public double StandardHours { get; set; }
        public string Barcode { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
