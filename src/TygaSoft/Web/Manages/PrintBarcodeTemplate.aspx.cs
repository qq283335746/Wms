using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Manages
{
    public partial class PrintBarcodeTemplate : System.Web.UI.Page
    {
        //private bool isPrint = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) Bind();
        }

        private void Bind()
        {
            var key = Request.QueryString["key"];
            switch (key)
            {
                case "PrintTemplate-PrintTest":
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["barcodeTemplateId"]))
                    {
                        var barcodeTemplateId = Guid.Empty;
                        if (Guid.TryParse(Request.QueryString["barcodeTemplateId"], out barcodeTemplateId))
                        {
                            var bll = new BarcodeTemplate();
                            var barcodeTemplateInfo = bll.GetModel(barcodeTemplateId);
                            if (barcodeTemplateInfo != null) ltrHtml.Text = barcodeTemplateInfo.JContent;
                        }
                    }
                    break;
                default:
                    break;
            }
            //isPrint = Request.QueryString["Print"] == "1";
        }

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    if (isPrint)
        //    {
        //        var width = int.Parse(Request.QueryString["width"]);
        //        var height = int.Parse(Request.QueryString["height"]);
        //        var sizeFWidth = float.Parse(Request.QueryString["sizeFWidth"]);
        //        var sizeFHeight = int.Parse(Request.QueryString["sizeFHeight"]);
        //        var marginTop = int.Parse(Request.QueryString["marginTop"]);
        //        var marginRight = int.Parse(Request.QueryString["marginRight"]);
        //        var marginBottom = int.Parse(Request.QueryString["marginBottom"]);
        //        var marginLeft = int.Parse(Request.QueryString["marginLeft"]);

        //        TextWriter myWriter = new StringWriter();
        //        HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
        //        base.Render(htmlWriter);

        //        FilesHelper.HtmlToPdf(new PdfInfo(myWriter.ToString(), Request.Url.AbsoluteUri,width,height,sizeFWidth,sizeFHeight, marginTop, marginRight, marginBottom, marginLeft));
        //    }
        //    else
        //    {
        //        base.Render(writer);
        //    }
        //}
    }
}