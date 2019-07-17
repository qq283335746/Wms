using System;

namespace TygaSoft.Model
{
    [Serializable]
    public class BarcodeTypeInfo
    {
        public BarcodeTypeInfo() { }

        public BarcodeTypeInfo(string from, string typeBody)
        {
            this.From = from;
            this.TypeBody = typeBody;
        }

        public string From { get; set; }
        public string TypeBody { get; set; }
    }
}
