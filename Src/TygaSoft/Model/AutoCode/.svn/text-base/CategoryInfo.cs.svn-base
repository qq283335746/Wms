using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class CategoryInfo
    {
        public CategoryInfo() { }

        public CategoryInfo(Guid id, Guid userId, Guid parentId, string categoryCode, string categoryName, string step, int sort, string remark, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.ParentId = parentId;
            this.CategoryCode = categoryCode;
            this.CategoryName = categoryName;
            this.Step = step;
            this.Sort = sort;
            this.Remark = remark;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ParentId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string Step { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
