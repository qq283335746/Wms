using System;
using System.Collections.Generic;

namespace TygaSoft.Model
{
    [Serializable]
    public class BarcodeTemplateFmInfo
    {
        public BarcodeTemplateFmInfo() { }

        public BarcodeTemplateFmInfo(string barcodeFormat, IList<KeyValueInfo> barcodeFormatList)
        {
            this.BarcodeFormat = barcodeFormat;
            this.BarcodeFormatList = barcodeFormatList;
        }

        public string BarcodeFormat { get; set; }

        public IList<KeyValueInfo> BarcodeFormatList { get; set; }
    }
}
