using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "UpdateOrderReceiptProductFmModel")]
    public class UpdateOrderReceiptProductFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public float ReceiptAmount { get; set; }
    }
}
