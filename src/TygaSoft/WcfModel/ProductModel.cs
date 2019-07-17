using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "ProductModel")]
    public class ProductModel
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public string Keyword { get; set; }

        [DataMember]
        public object CategoryId { get; set; }

    }
}
