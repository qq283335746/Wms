using System;
using System.Runtime.Serialization;
namespace TygaSoft.WcfModel
{
    [DataContract(Name = "SupplierModel")]
    public class SupplierModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object UserId { get; set; }

        [DataMember]
        public string SupplierCode { get; set; }

        [DataMember]
        public string SupplierName { get; set; }

        [DataMember]
        public string ShortName { get; set; }

        [DataMember]
        public string ContactMan { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string TelPhone { get; set; }

        [DataMember]
        public string Fax { get; set; }

        [DataMember]
        public string Postcode { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
