using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.BLL;

namespace TygaSoft.Web.Admin.Cost
{
    public partial class ListCost : System.Web.UI.Page
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
            var bll = new HwCost();
            var list = bll.GetList();
            var totalPrice = list.Sum(m => m.UnitPrice * decimal.Parse((m.RukuEndDate - m.RukuStartDate).TotalDays.ToString()));

            rpData.DataSource = list;
            rpData.DataBind();

            lbTotal.Text = "总额：" + totalPrice + "";
        }
    }
}