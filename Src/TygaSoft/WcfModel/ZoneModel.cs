using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "ZoneModel")]
    public class ZoneModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object UserId { get; set; }

        [DataMember]
        public string ZoneCode { get; set; }

        [DataMember]
        public string ZoneName { get; set; }

        [DataMember]
        public string Square { get; set; }

        [DataMember]
        public string Descr { get; set; }
    }
}
