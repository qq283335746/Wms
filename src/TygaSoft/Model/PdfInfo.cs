using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    public class PdfInfo
    {
        public PdfInfo() { }
        public PdfInfo(string html,string baseUrl,int width,int height,float sizeFWidth,float sizeFHeight,int marginTop, int marginRight,int marginBottom,int marginLeft)
        {
            this.Html = html;
            this.BaseUrl = baseUrl;
            this.Width = width;
            this.Height = height;
            this.SizeFWidth = sizeFWidth;
            this.SizeFHeight = sizeFHeight;
            this.MarginTop = marginTop;
            this.MarginRight = marginRight;
            this.MarginBottom = marginBottom;
            this.MarginLeft = marginLeft;
        }

        public string Html { get; set; }
        public string BaseUrl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float SizeFWidth { get; set; }
        public float SizeFHeight { get; set; }
        public int MarginTop { get; set; }
        public int MarginRight { get; set; }
        public int MarginBottom { get; set; }
        public int MarginLeft { get; set; }
    }
}
