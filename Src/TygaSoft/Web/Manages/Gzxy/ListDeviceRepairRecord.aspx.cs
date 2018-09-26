using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.WebHelper;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysHelper;

namespace TygaSoft.Web.Manages.Gzxy
{
    public partial class ListDeviceRepairRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) BindCtr();
        }

        private void BindCtr()
        {
            BindControl bc = new BindControl();
            if (!string.IsNullOrWhiteSpace(Request.QueryString["isBack"]))
            {
                bc.BindDdl(ddlSIsBack, typeof(EnumData.EnumIsOk), Request.QueryString["isBack"] == "1" ? "是" : "否");
            }
            else
            {
                bc.BindDdl(ddlSIsBack, typeof(EnumData.EnumIsOk), "");
            }
            bc.BindDdl(ddlSWhetherFix, typeof(EnumData.EnumWhetherFix), Request.QueryString["whetherFix"]);
        }
    }
}