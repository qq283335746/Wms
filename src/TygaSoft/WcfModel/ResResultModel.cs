using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "ResResultModel")]
    public class ResResultModel
    {
        [DataMember]
        public int ResCode { get; set; }

        [DataMember]
        public string Msg { get; set; }

        [DataMember]
        public object Data { get; set; }
    }
}
