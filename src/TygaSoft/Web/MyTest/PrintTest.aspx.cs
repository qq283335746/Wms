using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.MyTest
{
    public partial class PrintTest : System.Web.UI.Page
    {
        private bool isPrint = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            if(!string.IsNullOrWhiteSpace(Request.QueryString["key"]))
            {
                isPrint = Request.QueryString["key"].Contains("print");
                if (isPrint) printPanel.Visible = false;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (isPrint)
            {
                TextWriter myWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
                base.Render(htmlWriter);

                FilesHelper.HtmlToPdf(myWriter.ToString(), Request.Url.AbsoluteUri);
            }
            else
            {
                base.Render(writer);
            }
        }
    }
}