using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    public partial class OrderReceiptRecordInfo
    {
        public string BatchCode { get; set; }

        public string PackageCode { get; set; }

        public string PackageName { get; set; }

        public string StockLocationCode { get; set; }

        public string StockLocationName { get; set; }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string OrderNum { get; set; }

        public string OrderStatus { get; set; }

        public string ExpectedAmount { get; set; }

        public string ReceiptAmount { get; set; }

    }
}
