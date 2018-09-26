using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "DlgCustomerModel")]
    public class DlgCustomerModel
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string ShortName { get; set; }
    }
}
