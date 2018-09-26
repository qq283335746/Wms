using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.WebHelper;
using TygaSoft.Model;
using TygaSoft.BLL;
using TygaSoft.SysHelper;

namespace TygaSoft.Web.Admin.Base
{
    public partial class AddProduct : System.Web.UI.Page
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

            var bll = new Product();
            if (!Id.Equals(Guid.Empty))
            {
                var model = bll.GetModel(Id);
                if (model != null)
                {
                    hId.Value = model.Id.ToString();
                    hCategoryId.Value = model.CategoryId.ToString();
                    txtProductCode.Value = model.ProductCode;
                    txtProductName.Value = model.ProductName;
                    txtFullName.Value = model.FullName;
                    txtSpecs.Value = model.Specs;
                    txtPrice.Value = model.Price.ToString();
                    txtMaterialQuality.Value = model.MaterialQuality;
                    txtWeight.Value = model.Weight.ToString();
                    txtMaxStore.Value = model.MaxStore.ToString();
                    txtMinStore.Value = model.MinStore.ToString();
                    txtOutPackVolume.Value = model.OutPackVolume.ToString();
                    txtOutPackWeight.Value = model.OutPackWeight.ToString();
                    txtInPackVolume.Value = model.InPackVolume.ToString();
                    txtInPackWeight.Value = model.InPackWeight.ToString();
                    txtOutPackQty.Value = model.OutPackQty.ToString();
                    txtInPackQty.Value = model.InPackQty.ToString();
                    txtShelfLife.Value = model.ShelfLife.ToString();
                    cbbSupplier.Value = model.SupplierId.ToString();
                    txtSort.Value = model.Sort.ToString();
                    txtRemark.Value = model.Remark;

                    bc.BindDdl(ddlIsDisable, typeof(EnumData.EnumIsDisable), model.IsDisable ? "禁用":"启用","");
                }
            }
            else
            {
                bc.BindDdl(ddlIsDisable, typeof(EnumData.EnumIsDisable), "", "");

                var categoryId = Guid.Empty;
                if (!string.IsNullOrWhiteSpace(Request.QueryString["categoryId"]))
                {
                    Guid.TryParse(Request.QueryString["categoryId"], out categoryId);
                }
                if (!categoryId.Equals(Guid.Empty))
                {
                    txtProductCode.Value = bll.CreateCode(categoryId);
                }
            }
        }
    }
}