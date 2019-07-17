using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "ListModel")]
    public class ListModel
    {
        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        [DataMember]
        public string Keyword { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public object ParentId { get; set; }
    }
}
