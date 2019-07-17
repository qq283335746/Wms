using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaSkuModel")]
    public class PdaSkuModel
    {
        [DataMember]
        public double ExpectedAmount { get; set; }

        [DataMember]
        public double ReceiptAmount { get; set; }

        [DataMember]
        public object ProductId { get; set; }

        [DataMember]
        public object PackageId { get; set; }

        [DataMember]
        public string Unit { get; set; }

        [DataMember]
        public object StockLocationId { get; set; }

        [DataMember]
        public string StockLocationCode { get; set; }

        [DataMember]
        public string StockLocationName { get; set; }
    }
}
