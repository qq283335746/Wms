using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaOrderReceiptRecordModel")]
    public class PdaOrderReceiptRecordModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object OrderId { get; set; }

        [DataMember]
        public object ProductId { get; set; }

        [DataMember]
        public object PackageId { get; set; }

        [DataMember]
        public object StockLocationId { get; set; }

        [DataMember]
        public string Unit { get; set; }

        [DataMember]
        public Double Qty { get; set; }

        [DataMember]
        public string LPN { get; set; }

        [DataMember]
        public string OrderNum { get; set; }

    }
}
