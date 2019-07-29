using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TygaSoft.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.Redirect("~/wms/u/t.html");
            }

            //if(User.Identity.IsAuthenticated)
            //{
            //    Response.Redirect("~/wms/u/t.html");
            //}
        }
    }
}