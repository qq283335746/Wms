using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.BLL;

namespace TygaSoft.Web.Admin.Base
{
    public partial class AddZone : System.Web.UI.Page
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
            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"])) Guid.TryParse(Request.QueryString["Id"], out Id);
            if (!Id.Equals(Guid.Empty))
            {
                var bll = new Zone();
                var model = bll.GetModel(Id);
                if (model != null)
                {
                    hId.Value = model.Id.ToString();
                    txtZoneCode.Value = model.ZoneCode;
                    txtZoneName.Value = model.ZoneName;
                    txtSquare.Value = model.Square;
                    txtDescr.Value = model.Descr;
                }
            }

        }
    }
}