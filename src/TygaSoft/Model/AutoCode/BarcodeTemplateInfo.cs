using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class BarcodeTemplateInfo
    {
        public BarcodeTemplateInfo() { }

        public BarcodeTemplateInfo(Guid id, Guid userId, string title, string jContent, bool isDefault, string typeName, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.Title = title;
            this.JContent = jContent;
            this.IsDefault = isDefault;
            this.TypeName = typeName;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string JContent { get; set; }
        public bool IsDefault { get; set; }
        public string TypeName { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
