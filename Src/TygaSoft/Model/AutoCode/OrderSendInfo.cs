using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderSendInfo
    {
        public OrderSendInfo() { }

        public OrderSendInfo(Guid id, Guid userId, string orderCode, Guid customerId, double stayQty, double qty, string remark, byte status, int sort, bool isDisable, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.OrderCode = orderCode;
            this.CustomerId = customerId;
            this.StayQty = stayQty;
            this.Qty = qty;
            this.Remark = remark;
            this.Status = status;
            this.Sort = sort;
            this.IsDisable = isDisable;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public Guid CustomerId { get; set; }
        public double StayQty { get; set; }
        public double Qty { get; set; }
        public string Remark { get; set; }
        public byte Status { get; set; }
        public int Sort { get; set; }
        public bool IsDisable { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
