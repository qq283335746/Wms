using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TygaSoft.Web.Manages.Sys
{
    public partial class ListRoleMenu : System.Web.UI.Page
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
            if (Request.QueryString.AllKeys.Contains("allowRole"))
            {
                lbName.InnerText = "允许角色：" + Request.QueryString["allowRole"] + "【说明：选中的为允许操作，相反，未选中的为非允许操作】";
                hAllowRole.Value = Request.QueryString["allowRole"];
            }
            if (Request.QueryString.AllKeys.Contains("denyUser"))
            {
                lbName.InnerText = "拒绝用户：" + Request.QueryString["denyUser"] + "【说明：选中的为拒绝操作，相反，未选中的为允许操作】";
                hDenyUser.Value = Request.QueryString["denyUser"];
            }
        }
    }
}