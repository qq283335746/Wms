using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "StockWarningFmModel")]
    public class StockWarningFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object ZoneId { get; set; }

        [DataMember]
        public object StockLocationId { get; set; }

        [DataMember]
        public string Coded { get; set; }

        [DataMember]
        public string ZoneProperty { get; set; }

        [DataMember]
        public string StockLocationProperty { get; set; }

        [DataMember]
        public Decimal StockAmount { get; set; }

        [DataMember]
        public Int32 OverdueDay { get; set; }

        [DataMember]
        public double MinQty { get; set; }

        [DataMember]
        public double MaxQty { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public int Sort { get; set; }

        [DataMember]
        public Boolean IsDisable { get; set; }
    }
}
