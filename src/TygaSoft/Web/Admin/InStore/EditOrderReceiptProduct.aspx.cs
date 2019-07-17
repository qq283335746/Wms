using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Admin.InStore
{
    public partial class EditOrderReceiptProduct : System.Web.UI.Page
    {
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
            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"])) Guid.TryParse(Request.QueryString["Id"], out Id);
            if (!Id.Equals(Guid.Empty)) hId.Value = Id.ToString();

            var orderType = ((int)EnumData.EnumOrderPrefix.收货).ToString();
            if (!string.IsNullOrWhiteSpace(Request.QueryString["orderType"])) orderType = Request.QueryString["orderType"];
            hOrderType.Value = orderType;

            Control ctl = null;
            if (orderType == ((int)EnumData.EnumStep.预收货).ToString())
            {
                Page.Title = "ASN预收货";
                ctl = this.LoadControl("~/WebUserControls/UCAddPreOrderReceipt.ascx");
            }
            else
            {
                ctl = this.LoadControl("~/WebUserControls/UCAddOrderReceipt.ascx");
            }
            ctl.ID = "UCAddOrderReceipt";
            phAddOrderReceipt.Controls.Clear();
            phAddOrderReceipt.Controls.Add(ctl);
        }
    }
}