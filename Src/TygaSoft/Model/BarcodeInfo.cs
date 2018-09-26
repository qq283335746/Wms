using System;
using System.Collections.Generic;

namespace TygaSoft.Model
{
    [Serializable]
    public class BarcodeInfo
    {
        public BarcodeInfo() { }
        public BarcodeInfo(string barcode,string barcodeFormat,int width,int height,int margin,string imageUrl)
        {
            this.Barcode = barcode;
            this.BarcodeFormat = barcodeFormat;
            this.Width = width;
            this.Height = height;
            this.Margin = margin;
            this.ImageUrl = imageUrl;
        }

        public string Barcode { get; set; }

        public string BarcodeFormat { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Margin { get; set; }

        public string ImageUrl { get; set; }

    }
}
