using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.BLL;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Admin.Base
{
    public partial class AddStockLocation : System.Web.UI.Page
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
                Page.Title = "编辑库位";
                var bll = new StockLocation();
                var model = bll.GetModelByJoin(Id);
                if (model != null)
                {
                    hId.Value = model.Id.ToString();
                    hZoneId.Value = model.ZoneId.ToString();
                    txtCode.Value = model.Code;
                    txtName.Value = model.Named;
                    txtZone.Value = model.ZoneCode;
                    txtRow.Value = model.Row.ToString();
                    txtLayer.Value = model.Layer.ToString();
                    txtCol.Value = model.Col.ToString();
                    txtPassway.Value = model.Passway.ToString();
                    txtWidth.Value = model.Width.ToString();
                    txtWide.Value = model.Wide.ToString();
                    txtHigh.Value = model.High.ToString();
                    txtVolume.Value = model.Volume.ToString();
                    txtCubage.Value = model.Cubage.ToString();
                    txtStackLimit.Value = model.StackLimit.ToString();
                    txtCarryWeight.Value = model.CarryWeight.ToString();
                    txtX.Value = model.Xc.ToString();
                    txtY.Value = model.Yc.ToString();
                    txtZ.Value = model.Zc.ToString();
                    txtOrientation.Value = model.Orientation.ToString();
                    txtGroundTrayQty.Value = model.GroundTrayQty.ToString();
                    txtRouteSeq.Value = model.RouteSeq;
                    txtInsertTaskSeq.Value = model.InsertTaskSeq.ToString();
                    txtStatus.Value = model.Status;
                    txtWarehouse.Value = model.Warehouse;
                    txtLevelNum.Value = model.LevelNum.ToString();
                    txtPickArea.Value = model.PickArea;
                    txtInventoryGroupId.Value = model.InventoryGroupId.ToString();
                    txtPickMethod.Value = model.PickMethod;

                    BindControl bc = new BindControl();
                    bc.BindDdl(ddlIsMixPlace, typeof(EnumData.EnumIsOk), model.IsMixPlace.ToString());
                    bc.BindDdl(ddlIsBatchNum, typeof(EnumData.EnumIsOk), model.IsBatchNum.ToString());
                    bc.BindDdl(ddlIsLoseId, typeof(EnumData.EnumIsOk), model.IsLoseId.ToString());
                    bc.BindDdl(ddlStockLocationType, typeof(EnumData.EnumStockLocationType), model.StockLocationType);
                    bc.BindDdl(ddlCtrType, typeof(EnumData.EnumStockLocationCtrType), model.CtrType);
                    bc.BindDdl(ddlABC, typeof(EnumData.EnumAbc), model.ABC);
                    bc.BindDdl(ddlStockLocationDeal, typeof(EnumData.EnumStockLocationDeal), model.StockLocationDeal);
                    bc.BindDdl(ddlUseStatus, typeof(EnumData.EnumStockLocationUseStatus), model.UseStatus);
                }
            }
            else
            {
                txtWidth.Value = "0.00000";
                txtWide.Value = "0.00000";
                txtHigh.Value = "0.00000";
                txtVolume.Value = "0.00000";
                txtCubage.Value = "0.00000";
                txtRow.Value = "0.00000";
                txtLayer.Value = "0.00000";
                txtCol.Value = "0.00000";
                txtPassway.Value = "0.00000";
                txtX.Value = "0.00000";
                txtY.Value = "0.00000";
                txtZ.Value = "0.00000";
                txtOrientation.Value = "0.00000";
                txtStackLimit.Value = "0.00000";
                txtGroundTrayQty.Value = "0.00000";
                txtCarryWeight.Value = "0.00000";
                txtLevelNum.Value = "0.00000";
                txtInsertTaskSeq.Value = "0.00000";
                txtInventoryGroupId.Value = "0.00000";

                BindControl bc = new BindControl();
                bc.BindDdl(ddlIsMixPlace, typeof(EnumData.EnumIsOk), "");
                bc.BindDdl(ddlIsBatchNum, typeof(EnumData.EnumIsOk), "");
                bc.BindDdl(ddlIsLoseId, typeof(EnumData.EnumIsOk), "");
                bc.BindDdl(ddlStockLocationType, typeof(EnumData.EnumStockLocationType), "", "");
                bc.BindDdl(ddlCtrType, typeof(EnumData.EnumStockLocationCtrType), "", EnumData.EnumStockLocationCtrType.常规存储.ToString());
                bc.BindDdl(ddlABC, typeof(EnumData.EnumAbc), "", "");
                bc.BindDdl(ddlStockLocationDeal, typeof(EnumData.EnumStockLocationDeal), "","");
                bc.BindDdl(ddlUseStatus, typeof(EnumData.EnumStockLocationUseStatus), "","");
            }
        }

    }
}