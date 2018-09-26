using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TygaSoft.Web.Manages
{
    public partial class DlgProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hStepName.Value = Request.QueryString["stepName"];
                hOrderId.Value = Request.QueryString["orderId"];
            }
        }
    }
}