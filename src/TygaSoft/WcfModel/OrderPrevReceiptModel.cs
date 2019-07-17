using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "OrderPrevReceiptModel")]
    public class OrderPrevReceiptModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string OrderCode { get; set; }

        [DataMember]
        public object CustomerId { get; set; }

        [DataMember]
        public object SupplierId { get; set; }

        [DataMember]
        public string PurchaseOrderCode { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string SettlementDate { get; set; }

        [DataMember]
        public string RecordDate { get; set; }

        [DataMember]
        public Int32 Sort { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string LastTakeDate { get; set; }

        [DataMember]
        public string ExpectTakeDate { get; set; }

        [DataMember]
        public string SendDate { get; set; }

        [DataMember]
        public string PlanSendDate { get; set; }

        [DataMember]
        public string RMA { get; set; }

        [DataMember]
        public Double ExpectVolume { get; set; }

        [DataMember]
        public Double GW { get; set; }

        [DataMember]
        public string CustomAttr { get; set; }
    }
}
