using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.WebHelper;
using TygaSoft.SysHelper;
using TygaSoft.BLL;

namespace TygaSoft.Web.Admin.InStore
{
    public partial class AddOrderReceiptProduct : System.Web.UI.Page
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
            BindControl bc = new BindControl();

            if (!Id.Equals(Guid.Empty))
            {
                var bll = new OrderReceiptProduct();
                var model = bll.GetModelByJoin(Id);
                if (model != null)
                {
                    hOrderProductId.Value = model.Id.ToString();
                    hProductId.Value = model.ProductId.ToString();
                    hPackageId.Value = model.PackageId.ToString();
                    txtExpectedQty.Value = model.ExpectedQty.ToString();
                    txtReceiptQty.Value = model.ReceiptQty.ToString();
                    txtProductPurchaseOrderCode.Value = model.PurchaseOrderCode;
                    txtSort.Value = model.Sort.ToString();
                    txtaProductRemark.Value = model.Remark;
                    txtPackageName.Value = model.PackageName;
                    txtSupplierName.Value = model.SupplierName;
                    txtProduceDate.Value = model.ProduceDate;
                    txtQualityStatus.Value = model.QualityStatus;
                    txtCheckQuantity.Value = model.CheckQuantity.ToString();
                    txtRejectQuantity.Value = model.RejectQuantity.ToString();
                    txtQCStatus.Value = model.QCStatus;

                    txtProduct.Value = model.ProductCode;
                    txtPackage.Value = model.PackageCode;

                    bc.BindDdl(ddlOrderReceiptStatus, typeof(EnumData.EnumReceiptOrderStatus), model.Status);
                    bc.BindDdl(ddlUnit, typeof(EnumData.EnumUnitLevel), model.Unit);
                    bc.BindDdl(ddlIsOk, typeof(EnumData.EnumIsOk), model.IsQCNeed ? "是":"否");
                }
            }
            else
            {
                hProductId.Value = Guid.Empty.ToString();
                hPackageId.Value = Guid.Empty.ToString();

                bc.BindDdl(ddlOrderReceiptStatus, typeof(EnumData.EnumReceiptOrderStatus), EnumData.EnumReceiptOrderStatus.新建.ToString());
                bc.BindDdl(ddlUnit, typeof(EnumData.EnumUnitLevel), "");
                bc.BindDdl(ddlIsOk, typeof(EnumData.EnumIsOk), "");
            }
        }
    }
}