using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "InfoneProjectReportPrepareFmModel")]
    public class InfoneProjectReportPrepareFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object CustomerId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public string ProjectSource { get; set; }

        [DataMember]
        public string CustomerOfficial { get; set; }

        [DataMember]
        public string SpecsModel { get; set; }

        [DataMember]
        public int PreQty { get; set; }

        [DataMember]
        public decimal PreAmount { get; set; }

        [DataMember]
        public string ProjectAbout { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
