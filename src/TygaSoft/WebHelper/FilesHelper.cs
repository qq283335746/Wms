using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web;
using SelectPdf;
using TygaSoft.Model;

namespace TygaSoft.WebHelper
{
    public class FilesHelper
    {
        public static readonly string FileRoot = ConfigurationManager.AppSettings["FilesRoot"];

        public static void HtmlToPdf(string html, string baseUrl)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(html, baseUrl);
            doc.Save(HttpContext.Current.Response, false, string.Format("{0}{1}.pdf", DateTime.Now.ToString("yyMMddHHmm"), (new Random().Next(999)).ToString().PadLeft(3,'0')));
            doc.Close();
        }

        public static void HtmlToPdf(PdfInfo model)
        {
            HtmlToPdf converter = new HtmlToPdf();
            var isCustom = false;
            if (model.Width > 0)
            {
                isCustom = true;
                converter.Options.WebPageWidth = model.Width;
            }
            if (model.Height > 0)
            {
                isCustom = true;
                converter.Options.WebPageHeight = model.Height;
            }
            else converter.Options.WebPageHeight = int.Parse(converter.Options.PdfPageCustomSize.Height.ToString());
            if (model.MarginTop > -1)
            {
                isCustom = true;
                converter.Options.MarginTop = model.MarginTop;
            }
            if(model.MarginRight > -1)
            {
                isCustom = true;
                converter.Options.MarginRight = model.MarginRight;
            }
            if(model.MarginBottom > -1)
            {
                isCustom = true;
                converter.Options.MarginBottom = model.MarginBottom;
            }
            if(model.MarginLeft > -1)
            {
                isCustom = true;
                converter.Options.MarginLeft = model.MarginLeft;
            }
            if(isCustom)
            {
                converter.Options.PdfPageSize = PdfPageSize.Custom;
                converter.Options.PdfPageCustomSize = new System.Drawing.SizeF(model.SizeFWidth > 0 ? model.SizeFWidth : converter.Options.WebPageWidth, model.SizeFHeight > 0 ? model.SizeFHeight: converter.Options.PdfPageCustomSize.Height);
                converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                converter.Options.WebPageFixedSize = true;
            }

            PdfDocument doc = converter.ConvertHtmlString(model.Html, model.BaseUrl);
            doc.Save(HttpContext.Current.Response, false, string.Format("{0}{1}.pdf", DateTime.Now.ToString("yyMMddHHmm"), (new Random().Next(999)).ToString().PadLeft(3, '0')));
            doc.Close();
        }

        public static string GetRandomFolder(string key, DateTime currTime,bool isOldRemove)
        {
            var dir = string.Format("{0}/{1}/{2}/{3}", FileRoot, key, currTime.ToString("yyyyMM"), (new Random().NextDouble() * int.MaxValue).ToString().PadLeft(10, '0'));
            var fullPath = HttpContext.Current.Server.MapPath(dir);
            if (!Directory.Exists(fullPath)) Directory.CreateDirectory(fullPath);

            return dir;
        }

        public static string GetFullPathByWcf(string virtualPath)
        {
            var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(virtualPath);
            if (!Directory.Exists(Path.GetDirectoryName(fullPath))) Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            return fullPath;
        }

        public static string CreateDateTimeString()
        {
            //确保产生的字符串唯一性，使用线程休眠
            Thread.Sleep(2);
            Random random = new Random(); ;
            return DateTime.Now.ToString("yyyyMMddHHmmssffff", System.Globalization.DateTimeFormatInfo.InvariantInfo) + random.Next(0, 9999).ToString().PadLeft(4, '0');
        }

        public static string GetFormatDateTime()
        {
            Thread.Sleep(2);
            return DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }
    }
}
