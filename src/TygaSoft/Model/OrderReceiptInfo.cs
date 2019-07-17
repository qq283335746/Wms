using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    public partial class OrderReceiptInfo
    {
        public string UserName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string StatusName { get; set; }
        public string SSettlementDate { get; set; }
        public string SRecordDate { get; set; }

        public string SLastTakeDate { get; set; }
        public string SExpectTakeDate { get; set; }
        public string SSendDate { get; set; }
        public string SPlanSendDate { get; set; }
        public string RMA { get; set; }
        public double ExpectVolume { get; set; }
        public double GW { get; set; }
        public string CustomAttr { get; set; }
    }
}
