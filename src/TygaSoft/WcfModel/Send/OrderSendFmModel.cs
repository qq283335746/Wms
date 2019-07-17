using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "OrderSendFmModel")]
    public class OrderSendFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string OrderCode { get; set; }

        [DataMember]
        public Guid CustomerId { get; set; }
    }
}
