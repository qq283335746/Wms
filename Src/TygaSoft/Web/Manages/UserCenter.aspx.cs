using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TygaSoft.Web.Manages
{
    public partial class UserCenter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) Bind();
        }

        private void Bind()
        {
            //lbWelcome.InnerText = string.Format("欢迎您，{0}", User.Identity.Name);
        }
    }
}