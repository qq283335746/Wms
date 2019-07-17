using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "DlgZoneModel")]
    public class DlgZoneModel
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public string ZoneCode { get; set; }

        [DataMember]
        public string ZoneName { get; set; }

        [DataMember]
        public string Square { get; set; }
    }
}
