using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.Model;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Masters
{
    public partial class Blank : System.Web.UI.MasterPage
    {
        private bool isPrint = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) isPrint = Request.QueryString["Print"] == "1";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (isPrint)
            {
                TextWriter myWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
                base.Render(htmlWriter);

                var width = int.Parse(Request.QueryString["w"]);
                var height = int.Parse(Request.QueryString["h"]);
                var sizeFWidth = float.Parse(Request.QueryString["sfw"]);
                var sizeFHeight = int.Parse(Request.QueryString["sfh"]);
                var marginTop = int.Parse(Request.QueryString["mt"]);
                var marginRight = int.Parse(Request.QueryString["mr"]);
                var marginBottom = int.Parse(Request.QueryString["mb"]);
                var marginLeft = int.Parse(Request.QueryString["ml"]);
                FilesHelper.HtmlToPdf(new PdfInfo(myWriter.ToString(), Request.Url.AbsoluteUri, width, height, sizeFWidth, sizeFHeight, marginTop, marginRight, marginBottom, marginLeft));

                //FilesHelper.HtmlToPdf(myWriter.ToString(), Request.Url.AbsoluteUri);
            }
            else
            {
                base.Render(writer);
            }
        }
    }
}