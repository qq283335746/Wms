using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderReceiptInfo
    {
        public OrderReceiptInfo() { }

        public OrderReceiptInfo(Guid id, Guid userId, Guid customerId, Guid supplierId, string orderCode, int orderType, string preOrderCode, string purchaseOrderCode, string typeName, DateTime settlementDate, DateTime recordDate, bool isStopNext, byte status, int sort, string remark, DateTime lastUpdatedDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.CustomerId = customerId;
            this.SupplierId = supplierId;
            this.OrderCode = orderCode;
            this.OrderType = orderType;
            this.PreOrderCode = preOrderCode;
            this.PurchaseOrderCode = purchaseOrderCode;
            this.TypeName = typeName;
            this.SettlementDate = settlementDate;
            this.RecordDate = recordDate;
            this.IsStopNext = isStopNext;
            this.Status = status;
            this.Sort = sort;
            this.Remark = remark;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SupplierId { get; set; }
        public string OrderCode { get; set; }
        public int OrderType { get; set; }
        public string PreOrderCode { get; set; }
        public string PurchaseOrderCode { get; set; }
        public string TypeName { get; set; }
        public DateTime SettlementDate { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsStopNext { get; set; }
        public byte Status { get; set; }
        public int Sort { get; set; }
        public string Remark { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
