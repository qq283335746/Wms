using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "ShelfMissionFmModel")]
    public class ShelfMissionFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string OrderCode { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
