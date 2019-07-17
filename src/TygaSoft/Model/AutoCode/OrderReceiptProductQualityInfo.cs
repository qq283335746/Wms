using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderReceiptProductQualityInfo
    {
        public OrderReceiptProductQualityInfo() { }

        public OrderReceiptProductQualityInfo(Guid orderProductId, double checkQuantity, double rejectQuantity, string qCStatus, bool isQCNeed)
        {
            this.OrderProductId = orderProductId;
            this.CheckQuantity = checkQuantity;
            this.RejectQuantity = rejectQuantity;
            this.QCStatus = qCStatus;
            this.IsQCNeed = isQCNeed;
        }

        public Guid OrderProductId { get; set; }
        public double CheckQuantity { get; set; }
        public double RejectQuantity { get; set; }
        public string QCStatus { get; set; }
        public bool IsQCNeed { get; set; }
    }
}
