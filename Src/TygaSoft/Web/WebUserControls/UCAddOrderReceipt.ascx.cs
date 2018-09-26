using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.WebUserControls
{
    public partial class UCAddOrderReceipt : System.Web.UI.UserControl
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
            BindControl bc = new BindControl();
            bc.BindDdl(ddlOrderReceiptStatus, typeof(EnumData.EnumReceiptOrderStatus), EnumData.EnumReceiptOrderStatus.新建.ToString());
            bc.BindDdl(ddlOrderReceiptType, typeof(EnumData.EnumReceiptOrderType), EnumData.EnumReceiptOrderType.采购入库.ToString());
        }
    }
}