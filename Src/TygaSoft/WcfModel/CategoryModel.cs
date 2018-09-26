using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "CategoryModel")]
    public class CategoryModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object ParentId { get; set; }

        [DataMember]
        public string CategoryCode { get; set; }

        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public string Step { get; set; }

        [DataMember]
        public Int32 Sort { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
