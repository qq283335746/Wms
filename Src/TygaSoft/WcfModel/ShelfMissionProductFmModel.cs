using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "ShelfMissionProductFmModel")]
    public class ShelfMissionProductFmModel
    {
        [DataMember]
        public Guid ShelfMissionId { get; set; }

        [DataMember]
        public Guid OrderId { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public double Qty { get; set; }

        [DataMember]
        public string StockLocations { get; set; }
    }
}
