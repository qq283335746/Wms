using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaShelfMissionProductModel")]
    public class PdaShelfMissionProductModel
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public object ShelfMissionId { get; set; }

        [DataMember]
        public object OrderId { get; set; }
    }
}
