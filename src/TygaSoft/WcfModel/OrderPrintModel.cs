using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "OrderPrintModel")]
    public class OrderPrintModel
    {
        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public object Id { get; set; }
    }
}
