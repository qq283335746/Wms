using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;
using TygaSoft.BLL;

namespace TygaSoft.Web.Admin.Base
{
    public partial class AddPackage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindUnit();
                Bind();
            }
        }

        private void Bind()
        {
            var Id = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"])) Guid.TryParse(Request.QueryString["Id"], out Id);
            if (!Id.Equals(Guid.Empty))
            {
                var bll = new Package();
                var model = bll.GetModelByJoin(Id);
                if (model != null)
                {
                    hId.Value = model.Id.ToString();
                    hCustomerId.Value = model.CustomerId.ToString();
                    hProductId.Value = model.ProductId.ToString();
                    txtCustomer.Value = model.CustomerCode;
                    txtProduct.Value = model.ProductCode;
                    txtPackageCode.Value = model.PackageCode;

                    txtTotalPiece.Value = model.TotalPiece.ToString();
                    txtTotalInsidePackage.Value = model.TotalInsidePackage.ToString();
                    txtTotalBox.Value = model.TotalBox.ToString();
                    txtTotalTray.Value = model.TotalTray.ToString();
                    txtaRemark.Value = model.Remark;

                    if (!string.IsNullOrWhiteSpace(model.UnitXml))
                    {
                        var root = XElement.Parse(model.UnitXml);
                        var qMainUnit = root.Element("MainUnit").Elements();
                        txtGW.Value = qMainUnit.First(x => x.Attribute("Code").Value == "GW").Value;
                        txtNW.Value = qMainUnit.First(x => x.Attribute("Code").Value == "NW").Value;
                        txtWidth.Value = qMainUnit.First(x => x.Attribute("Code").Value == "Width").Value;
                        txtWide.Value = qMainUnit.First(x => x.Attribute("Code").Value == "Wide").Value;
                        txtHigh.Value = qMainUnit.First(x => x.Attribute("Code").Value == "High").Value;
                        txtVolume.Value = qMainUnit.First(x => x.Attribute("Code").Value == "Volume").Value;

                        var qInsidePackage = root.Element("InsidePackage").Elements();
                        txtInsideGW.Value = qInsidePackage.First(x => x.Attribute("Code").Value == "InsideGW").Value;
                        txtInsideNW.Value = qInsidePackage.First(x => x.Attribute("Code").Value == "InsideNW").Value;
                        txtInsideWidth.Value = qInsidePackage.First(x => x.Attribute("Code").Value == "InsideWidth").Value;
                        txtInsideWide.Value = qInsidePackage.First(x => x.Attribute("Code").Value == "InsideWide").Value;
                        txtInsideHigh.Value = qInsidePackage.First(x => x.Attribute("Code").Value == "InsideHigh").Value;
                        txtInsideVolume.Value = qInsidePackage.First(x => x.Attribute("Code").Value == "InsideVolume").Value;

                        var qBoxPackage = root.Element("BoxPackage").Elements();
                        txtBoxGW.Value = qBoxPackage.First(x => x.Attribute("Code").Value == "BoxGW").Value;
                        txtBoxNW.Value = qBoxPackage.First(x => x.Attribute("Code").Value == "BoxNW").Value;
                        txtBoxWidth.Value = qBoxPackage.First(x => x.Attribute("Code").Value == "BoxWidth").Value;
                        txtBoxWide.Value = qBoxPackage.First(x => x.Attribute("Code").Value == "BoxWide").Value;
                        txtBoxHigh.Value = qBoxPackage.First(x => x.Attribute("Code").Value == "BoxHigh").Value;
                        txtBoxVolume.Value = qBoxPackage.First(x => x.Attribute("Code").Value == "BoxVolume").Value;

                        var qTray = root.Element("Tray").Elements();
                        txtTrayGW.Value = qTray.First(x => x.Attribute("Code").Value == "TrayGW").Value;
                        txtTrayNW.Value = qTray.First(x => x.Attribute("Code").Value == "TrayNW").Value;
                        txtTrayWidth.Value = qTray.First(x => x.Attribute("Code").Value == "TrayWidth").Value;
                        txtTrayWide.Value = qTray.First(x => x.Attribute("Code").Value == "TrayWide").Value;
                        txtTrayHigh.Value = qTray.First(x => x.Attribute("Code").Value == "TrayHigh").Value;
                        txtTrayVolume.Value = qTray.First(x => x.Attribute("Code").Value == "TrayVolume").Value;
                        txtEachLayerQty.Value = qTray.First(x => x.Attribute("Code").Value == "EachLayerQty").Value;
                        txtLayerHighQty.Value = qTray.First(x => x.Attribute("Code").Value == "LayerHighQty").Value;
                    }
                    
                }
            }
            else
            {
                BindUnit();
            }
        }

        private void BindUnit()
        {
            BindControl bc = new BindControl();
            bc.BindDdl(ddlPiece, typeof(EnumData.EnumUnitLevel), EnumData.EnumUnitLevel.件.ToString());
            bc.BindDdl(ddlInsidePackage, typeof(EnumData.EnumUnitLevel), EnumData.EnumUnitLevel.内包装.ToString());
            bc.BindDdl(ddlBox, typeof(EnumData.EnumUnitLevel), EnumData.EnumUnitLevel.箱.ToString());
            bc.BindDdl(ddlTray, typeof(EnumData.EnumUnitLevel), EnumData.EnumUnitLevel.托盘.ToString());
        }
    }
}