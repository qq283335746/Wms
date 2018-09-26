using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrderReceiptAttrInfo
    {
        public OrderReceiptAttrInfo() { }

        public OrderReceiptAttrInfo(Guid orderId, DateTime lastTakeDate, DateTime expectTakeDate, DateTime sendDate, DateTime planSendDate, string rMA, double expectVolume, double gW, string customAttr)
        {
            this.OrderId = orderId;
            this.LastTakeDate = lastTakeDate;
            this.ExpectTakeDate = expectTakeDate;
            this.SendDate = sendDate;
            this.PlanSendDate = planSendDate;
            this.RMA = rMA;
            this.ExpectVolume = expectVolume;
            this.GW = gW;
            this.CustomAttr = customAttr;
        }

        public Guid OrderId { get; set; }
        public DateTime LastTakeDate { get; set; }
        public DateTime ExpectTakeDate { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime PlanSendDate { get; set; }
        public string RMA { get; set; }
        public double ExpectVolume { get; set; }
        public double GW { get; set; }
        public string CustomAttr { get; set; }
    }
}
