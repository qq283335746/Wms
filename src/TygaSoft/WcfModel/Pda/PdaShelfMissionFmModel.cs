using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaShelfMissionFmModel")]
    public class PdaShelfMissionFmModel
    {
        [DataMember]
        public Guid OrderId { get; set; }

        [DataMember]
        public string OrderCode { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public string ProductCode { get; set; }

        [DataMember]
        public int Qty { get; set; }

        [DataMember]
        public string StockLocation { get; set; }
    }
}
