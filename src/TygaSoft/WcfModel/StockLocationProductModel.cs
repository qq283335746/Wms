using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "StockLocationProductModel")]
    public class StockLocationProductModel
    {
        [DataMember]
        public string KeyName { get; set; }

        [DataMember]
        public object ProductId { get; set; }

        [DataMember]
        public object CustomerId { get; set; }

        [DataMember]
        public double Qty { get; set; }

        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }
    }
}
