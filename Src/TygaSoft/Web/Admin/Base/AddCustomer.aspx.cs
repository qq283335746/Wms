using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.BLL;

namespace TygaSoft.Web.Admin.Base
{
    public partial class AddCustomer : System.Web.UI.Page
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
                var bll = new Customer();
                var model = bll.GetModel(Id);
                if (model != null)
                {
                    hId.Value = model.Id.ToString();
                    txtCustomerCode.Value = model.Coded;
                    txtCustomerName.Value = model.Named;
                    txtShortName.Value = model.ShortName;
                    txtContactMan.Value = model.ContactMan;
                    txtEmail.Value = model.Email;
                    txtPhone.Value = model.Phone;
                    txtTelPhone.Value = model.TelPhone;
                    txtFax.Value = model.Fax;
                    txtPostcode.Value = model.Postcode;
                    txtAddress.Value = model.Address;
                    txtRemark.Value = model.Remark;
                }
            }

        }
    }
}