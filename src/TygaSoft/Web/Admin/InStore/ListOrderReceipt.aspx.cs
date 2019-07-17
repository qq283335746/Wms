using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using TygaSoft.WebHelper;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.SysHelper;

namespace TygaSoft.Web.Admin.InStore
{
    public partial class ListOrderReceipt : System.Web.UI.Page
    {
        int orderType = (int)EnumData.EnumStep.收货;

        protected void Page_Load(object sender, EventArgs e)
        {
            SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SitemapHelper.DoSiteMapResolve);

            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["orderType"])) int.TryParse(Request.QueryString["orderType"], out orderType);
            hOrderType.Value = orderType.ToString();

            if (orderType == (int)EnumData.EnumStep.预收货) Page.Title = "ASN预收货";
        }
    }
}