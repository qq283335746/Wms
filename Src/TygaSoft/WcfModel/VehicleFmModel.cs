using System;
using System.Runtime.Serialization;

namespace TygaSoft.Model
{
    [DataContract(Name = "VehicleFmModel")]
    public partial class VehicleFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string VehicleID { get; set; }

        [DataMember]
        public string VehicleModel { get; set; }

        [DataMember]
        public string Licence { get; set; }

        [DataMember]
        public object LicPic { get; set; }

        [DataMember]
        public string OffenceRecord { get; set; }

        [DataMember]
        public string DriverID { get; set; }

        [DataMember]
        public object DriverIDPicture { get; set; }

        [DataMember]
        public string RewardRecord { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public int Sort { get; set; }

        [DataMember]
        public bool IsDisable { get; set; }
    }
}
