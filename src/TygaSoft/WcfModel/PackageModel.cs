using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PackageModel")]
    public class PackageModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object CustomerId { get; set; }

        [DataMember]
        public object ProductId { get; set; }

        [DataMember]
        public string PackageCode { get; set; }

        [DataMember]
        public Double TotalPiece { get; set; }

        [DataMember]
        public Double TotalInsidePackage { get; set; }

        [DataMember]
        public Double TotalBox { get; set; }

        [DataMember]
        public Double TotalTray { get; set; }

        [DataMember]
        public string UnitXml { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
