using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.BLL;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Manages.Gzxy
{
    public partial class AddDeviceRepairRecord : System.Web.UI.Page
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

            Guid Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"])) Guid.TryParse(Request.QueryString["Id"], out Id);
            if (!Id.Equals(Guid.Empty))
            {
                var bll = new InfoneDeviceRepairRecord();
                var model = bll.GetModel(Id);
                if (model != null)
                {
                    hId.Value = model.Id.ToString();
                    txtRecordDate.Value = model.RecordDate.ToString("yyyy-MM-dd").Replace("1754-01-01", "");
                    txtCustomer.Value = model.Customer;
                    txtSerialNumber.Value = model.SerialNumber;
                    txtDeviceModel.Value = model.DeviceModel;
                    txtFaultCause.Value = model.FaultCause;
                    txtSolveMethod.Value = model.SolveMethod;
                    txtCustomerProblem.Value = model.CustomerProblem;
                    txtDevicePart.Value = model.DevicePart;
                    txtTreatmentSituation.Value = model.TreatmentSituation;
                    txtHandoverPerson.Value = model.HandoverPerson;
                    txtBackDate.Value = model.BackDate.ToString("yyyy-MM-dd").Replace("1754-01-01","");
                    txtRegisteredPerson.Value = model.RegisteredPerson;
                    txtRemark.Value = model.Remark;

                    bc.BindDdl(ddlIsBack, typeof(EnumData.EnumIsOk), model.IsBack ? "是" : "否", "");
                    bc.BindDdl(ddlWhetherFix, typeof(EnumData.EnumWhetherFix), model.WhetherFix, "");
                }
            }
            else
            {
                bc.BindDdl(ddlIsBack, typeof(EnumData.EnumIsOk), "", "");
                bc.BindDdl(ddlWhetherFix, typeof(EnumData.EnumWhetherFix), "", "");
            }
        }
    }
}