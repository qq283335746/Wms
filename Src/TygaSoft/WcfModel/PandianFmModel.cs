using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PandianFmModel")]
    public class PandianFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string OrderCode { get; set; }

        [DataMember]
        public string Named { get; set; }

        [DataMember]
        public string AllowUsers { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string StockStartDate { get; set; }

        [DataMember]
        public string StockEndDate { get; set; }

        [DataMember]
        public string Customers { get; set; }

        [DataMember]
        public string Zones { get; set; }

        [DataMember]
        public string StockLocations { get; set; }
    }
}
