using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "BestStockLocationModel")]
    public class BestStockLocationModel
    {
        [DataMember]
        public object ProductId { get; set; }

        [DataMember]
        public int ExpectedQty { get; set; }
    }
}
