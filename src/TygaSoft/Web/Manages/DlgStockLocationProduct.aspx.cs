using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TygaSoft.Web.Manages
{
    public partial class DlgStockLocationProduct : System.Web.UI.Page
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
            var keyName = Request.QueryString["keyName"];
            hKey.Value = keyName;
            switch (keyName)
            {
                case "ShelfMissionProduct":
                    hValue.Value = string.Format("{0}|{1}|{2}|{3}", Request.QueryString["shelfMissionId"], Request.QueryString["orderId"], Request.QueryString["productId"], Request.QueryString["qty"]);
                    break;
                case "OrderSendProduct":
                    hValue.Value = string.Format("{0}", Request.QueryString["orderId"]);
                    break;
                case "OrderPickProduct":
                    hValue.Value = string.Format("{0}|{1}|{2}|{3}|{4}", Request.QueryString["orderPickId"], Request.QueryString["orderId"], Request.QueryString["productId"], Request.QueryString["customerId"], Request.QueryString["qty"]);
                    break;
                default:
                    break;
            }
        }
    }
}