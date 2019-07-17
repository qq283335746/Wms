using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace TygaSoft.Web.WebUserControls.Customer
{
    public partial class UCCustomerMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) Bind();
        }

        private void Bind()
        {
            var sb = new StringBuilder(@"<li><a href=""/wms/u/y.html"">维修设备记录</a></li>");
            if (HttpContext.Current.User.IsInRole("项目报备专用"))
            {
                sb.Append(@"<li><a href=""/wms/u/a.html"">项目报备</a></li>");
            }
            ltrMenus.Text = sb.ToString();
        }
    }
}