using System;
using System.Runtime.Serialization;
namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaLogisticsDistributionModel")]
    public class PdaLogisticsDistributionModel
    {
        [DataMember]
        public string OrderCode { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Platform { get; set; }

        [DataMember]
        public string Lnglat { get; set; }
    }
}
