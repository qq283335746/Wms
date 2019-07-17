using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class FeatureUserInfo
    {
        public FeatureUserInfo() { }

        public FeatureUserInfo(Guid userId, Guid featureId, string typeName, DateTime lastUpdatedDate)
        {
            this.UserId = userId;
            this.FeatureId = featureId;
            this.TypeName = typeName;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid UserId { get; set; }
        public Guid FeatureId { get; set; }
        public string TypeName { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
