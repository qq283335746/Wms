using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class ShelfMissionInfo
    {
        public ShelfMissionInfo() { }

        public ShelfMissionInfo(Guid id, Guid userId, string orderCode, double totalStayQty, double totalQty, string status, string remark, int sort, bool isDisable, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.OrderCode = orderCode;
            this.TotalStayQty = totalStayQty;
            this.TotalQty = totalQty;
            this.Status = status;
            this.Remark = remark;
            this.Sort = sort;
            this.IsDisable = isDisable;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public double TotalStayQty { get; set; }
        public double TotalQty { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public int Sort { get; set; }
        public bool IsDisable { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
