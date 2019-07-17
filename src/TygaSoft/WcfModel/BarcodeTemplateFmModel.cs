using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "BarcodeTemplateFmModel")]
    public class BarcodeTemplateFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string JContent { get; set; }

        [DataMember]
        public bool IsDefault { get; set; }

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public string Barcode { get; set; }

        [DataMember]
        public string BarcodeFormat { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public int Margin { get; set; }
    }
}
