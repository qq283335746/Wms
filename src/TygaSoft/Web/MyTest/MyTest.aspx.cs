using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using TygaSoft.SysException;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.MyTest
{
    public partial class MyTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //new CustomException("MyTest--Page_Load");
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            Response.Write(HttpClientHelper.GetClientIp(HttpContext.Current));
        }
    }
}