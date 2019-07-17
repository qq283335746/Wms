using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "StockProductNoticeModel")]
    public class StockProductNoticeModel
    {
        [DataMember]
        public string OpenId { get; set; }

        [DataMember]
        public string Coded { get; set; }

        [DataMember]
        public string Named { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public float Qty { get; set; }
    }
}
