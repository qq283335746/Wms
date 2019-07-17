using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PandianProductFmModel")]
    public class PandianProductFmModel
    {
        [DataMember]
        public Guid PandianId { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public Guid CustomerId { get; set; }

        [DataMember]
        public string Zones { get; set; }

        [DataMember]
        public string StockLocations { get; set; }

        [DataMember]
        public double Qty { get; set; }
    }
}
