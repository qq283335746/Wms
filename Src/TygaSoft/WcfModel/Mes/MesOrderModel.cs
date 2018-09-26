using System;
using System.Runtime.Serialization;

namespace TygaSoft.Model
{
    [DataContract(Name = "MesOrderModel")]
    public partial class MesOrderModel
    {
        [DataMember]
        public string OBarcode { get; set; }

        [DataMember]
        public string PBarcode { get; set; }

        [DataMember]
        public string PdBarcode { get; set; }

        [DataMember]
        public string PtBarcode { get; set; }

        [DataMember]
        public double Qty { get; set; }
    }
}
