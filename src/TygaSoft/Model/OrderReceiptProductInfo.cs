using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    public partial class OrderReceiptProductInfo
    {
        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string PackageCode { get; set; }

        public string PackageName { get; set; }

        public string SupplierName { get; set; }

        public string StatusName { get; set; }

        public string ProduceDate { get; set; }

        public string QualityStatus { get; set; }

        public string ProductAttrPurchaseOrderNum { get; set; }

        public Double CheckQuantity { get; set; }

        public Double RejectQuantity { get; set; }

        public string QCStatus { get; set; }

        public Boolean IsQCNeed { get; set; }

        public string StockLocationAppendId { get; set; }

        public string StockLocationAppendName { get; set; }

        public string StockLocationJson { get; set; }

        public string OrderCode { get; set; }

        public int OrderType { get; set; }
    }
}
