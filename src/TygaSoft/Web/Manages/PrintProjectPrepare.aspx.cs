using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.BLL;

namespace TygaSoft.Web.Manages
{
    public partial class PrintProjectPrepare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) Bind();
        }

        private void Bind()
        {
            hKey.Value = Request.QueryString["key"];
            hValue.Value = Request.QueryString["Id"];
        }
    }
}