using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "ToPdfModel")]
    public class ToPdfModel
    {
        [DataMember]
        public string Url { get; set; }
    }
}
