using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "UpdateOrderPrevReceiptProductFmModel")]
    public class UpdateOrderPrevReceiptProductFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public float ReceiptAmount { get; set; }
    }
}
