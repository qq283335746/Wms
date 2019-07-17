using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "OrderSendProductFmModel")]
    public class OrderSendProductFmModel
    {
        [DataMember]
        public Guid OrderId { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public double Qty { get; set; }

        [DataMember]
        public Guid StockLocationId { get; set; }
    }
}
