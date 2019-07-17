using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "InfoneCustomerFmModel")]
    public class InfoneCustomerFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string Coded { get; set; }

        [DataMember]
        public string Named { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string InCompany { get; set; }

        [DataMember]
        public string TelPhone { get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public string PostCode { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string CompanyAbout { get; set; }
    }
}
