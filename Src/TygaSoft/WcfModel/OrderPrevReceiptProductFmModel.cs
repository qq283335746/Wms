using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "OrderPrevReceiptProductFmModel")]
    public class OrderPrevReceiptProductFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object OrderId { get; set; }

        [DataMember]
        public object ProductId { get; set; }

        [DataMember]
        public object PackageId { get; set; }

        [DataMember]
        public string Unit { get; set; }

        [DataMember]
        public Double ExpectedAmount { get; set; }

        [DataMember]
        public Double ReceiptAmount { get; set; }

        [DataMember]
        public DateTime RecordDate { get; set; }

        [DataMember]
        public string PurchaseOrderCode { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public Int32 Sort { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string PackageName { get; set; }

        [DataMember]
        public string SupplierName { get; set; }

        [DataMember]
        public string ProduceDate { get; set; }

        [DataMember]
        public string QualityStatus { get; set; }

        [DataMember]
        public string ProductAttrPurchaseOrderCode { get; set; }

        [DataMember]
        public Double CheckQuantity { get; set; }

        [DataMember]
        public Double RejectQuantity { get; set; }

        [DataMember]
        public string QCStatus { get; set; }

        [DataMember]
        public Boolean IsQCNeed { get; set; }

    }
}
