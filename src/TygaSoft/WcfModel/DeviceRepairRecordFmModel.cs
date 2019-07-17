using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "DeviceRepairRecordFmModel")]
    public class DeviceRepairRecordFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string RecordDate { get; set; }

        [DataMember]
        public string Customer { get; set; }

        [DataMember]
        public string SerialNumber { get; set; }

        [DataMember]
        public string DeviceModel { get; set; }

        [DataMember]
        public string FaultCause { get; set; }

        [DataMember]
        public string SolveMethod { get; set; }

        [DataMember]
        public string CustomerProblem { get; set; }

        [DataMember]
        public string DevicePart { get; set; }

        [DataMember]
        public string TreatmentSituation { get; set; }

        [DataMember]
        public string WhetherFix { get; set; }

        [DataMember]
        public string HandoverPerson { get; set; }

        [DataMember]
        public string IsBack { get; set; }

        [DataMember]
        public string BackDate { get; set; }

        [DataMember]
        public string RegisteredPerson { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
