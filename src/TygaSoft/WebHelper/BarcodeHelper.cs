using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;
using ZXing;
using ZXing.Common;
using TygaSoft.SysHelper;
using TygaSoft.Model;
using TygaSoft.WcfModel;

namespace TygaSoft.WebHelper
{
    public class BarcodeHelper
    {
        public string CreateBarcode(string barcode)
        {
            EncodingOptions options = null;
            options = new EncodingOptions
            {
                Width = 238,
                Height = 50,
                Margin = 1
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Options = options;
            writer.Format = BarcodeFormat.CODE_128;
            var bitmap = writer.Write(barcode);

            var filePath = string.Format("{0}/{1}.jpg", GetFileRootByBarcode(), barcode);
            var fullPath = FilesHelper.GetFullPathByWcf(filePath);

            if (File.Exists(fullPath))
            {
                return VirtualPathUtility.ToAbsolute(filePath);
            }

            bitmap.Save(fullPath);

            return VirtualPathUtility.ToAbsolute(filePath);
        }

        public string CreateBarcode(BarcodeInfo model,string name,bool isOldDelete)
        {
            EncodingOptions options = null;
            options = new EncodingOptions
            {
                Width = model.Width,
                Height = model.Height,
                Margin = model.Margin
            };

            var writer = new BarcodeWriter();
            writer.Options = options;
            writer.Format = (BarcodeFormat)Enum.Parse(typeof(BarcodeFormat), model.BarcodeFormat);
            var bitmap = writer.Write(model.Barcode);

            var filePath = string.Format("{0}/{1}.jpg", GetFileRootByBarcode(), string.IsNullOrEmpty(name) ? model.Barcode : name);
            var fullPath = FilesHelper.GetFullPathByWcf(filePath);

            if (File.Exists(fullPath))
            {
                if (isOldDelete) File.Delete(fullPath);
                else return VirtualPathUtility.ToAbsolute(filePath);
            }

            bitmap.Save(fullPath);
            return VirtualPathUtility.ToAbsolute(filePath);
        }

        public string CreateBarcodeBrowser(BarcodeInfo model)
        {
            EncodingOptions options = null;
            options = new EncodingOptions
            {
                Width = model.Width,
                Height = model.Height,
                Margin = model.Margin
            };

            BarcodeWriter writer = new BarcodeWriter();
            writer.Options = options;
            writer.Format = (BarcodeFormat)Enum.Parse(typeof(BarcodeFormat), model.BarcodeFormat);
            var bitmap = writer.Write(model.Barcode);

            var filePath = new TempFolder().GetTempUrl(string.Format("{0}.jpg", model.Barcode));
            var fullPath = FilesHelper.GetFullPathByWcf(filePath);

            if (File.Exists(fullPath))
            {
                return VirtualPathUtility.ToAbsolute(filePath);
            }

            bitmap.Save(fullPath);
            return VirtualPathUtility.ToAbsolute(filePath);
        }

        public BarcodeTemplateFmInfo GetBarcodeTemplateInfo()
        {
            return new BarcodeTemplateFmInfo("", EnumHelper.GetList(typeof(BarcodeFormat)));
        }

        private string GetFileRootByBarcode()
        {
            return VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(FilesHelper.FileRoot), "Barcode/" + DateTime.Now.ToString("yyyyMM") + "");
        }
    }
}
