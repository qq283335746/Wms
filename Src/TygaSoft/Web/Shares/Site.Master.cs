using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TygaSoft.Web.Shares
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            Control ctl = this.LoadControl("~/WebUserControls/UCMenu.ascx");
            ctl.ID = "UCMenu";
            phUc.Controls.Add(ctl);
            if (HttpContext.Current.User.IsInRole("Users_Customer")) lbSiteTitle.InnerText = "矽云科技";
            else lbSiteTitle.InnerText = "仓储配送一体化平台";
        }
    }
}