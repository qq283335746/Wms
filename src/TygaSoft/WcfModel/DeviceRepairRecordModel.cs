using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "DeviceRepairRecordModel")]
    public class DeviceRepairRecordModel
    {
        [DataMember]
        public string Keyword { get; set; }

        [DataMember]
        public string WhetherFix { get; set; }

        [DataMember]
        public string IsBack { get; set; }

        [DataMember]
        public string StartDate { get; set; }

        [DataMember]
        public string EndDate { get; set; }

        [DataMember]
        public string BackDate { get; set; }
    }
}
