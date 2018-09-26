using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.Model;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;

namespace TygaSoft.Web.Manages
{
    public partial class Print : System.Web.UI.Page
    {
        private bool isPrint = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            try
            {
                var key = Request.QueryString["key"];
                if (string.IsNullOrWhiteSpace(key))
                {
                    MessageBox.Messager(this.Page, MC.Request_Params_InvalidError, MC.AlertTitle_Error, "error");
                    return;
                }
                var Id = Guid.Empty;
                Guid.TryParse(Request.QueryString["Id"], out Id);
                if(!Id.Equals(Guid.Empty)) CreateHtml(key.Trim(), Id);

                isPrint = Request.QueryString["print"] == "1";
            }
            catch (Exception ex)
            {
                MessageBox.Messager(this.Page, ex.Message, MC.AlertTitle_Ex_Error, "error");
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (isPrint)
            {
                TextWriter myWriter = new StringWriter();
                HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
                base.Render(htmlWriter);

                FilesHelper.HtmlToPdf(myWriter.ToString(), Request.Url.AbsoluteUri);
            }
            else
            {
                base.Render(writer);
            }
        }

        private void CreateHtml(string key, Guid Id)
        {
            var sbCargoListHtml = new StringBuilder();
            var po = new PrintOrder();
            PrintOrderInfo poInfo = null;
            var index = 0;
            switch (key)
            {
                case "PreOrderReceipt":
                    poInfo = po.GetPrintPreOrderReceipt(Id);
                    sbCargoListHtml.Append("<tr><th>序号</th><th>货品</th><th>货品名称</th><th>包装</th><th>单位</th><th>预期量</th></tr>");
                    if (poInfo.CargoList != null && poInfo.CargoList.Count > 0)
                    {
                        foreach (var item in poInfo.CargoList)
                        {
                            index++;
                            sbCargoListHtml.Append(@"<tr><td>" + index + "</td><td>" + item.ProductCode + "</td><td>" + item.ProductName + "</td><td>" + item.PackageCode + "</td><td>" + item.UnitName + "</td><td>" + item.Qty + "</td></tr>");
                        }
                    }
                    break;
                case "OrderReceipt":
                    poInfo = po.GetPrintOrderReceipt(Id);
                    sbCargoListHtml.Append("<tr><th>序号</th><th>货品</th><th>货品名称</th><th>包装</th><th>单位</th><th>已收货量</th></tr>");
                    if (poInfo.CargoList != null && poInfo.CargoList.Count > 0)
                    {
                        foreach (var item in poInfo.CargoList)
                        {
                            index++;
                            sbCargoListHtml.Append(@"<tr><td>" + index + "</td><td>" + item.ProductCode + "</td><td>" + item.ProductName + "</td><td>" + item.PackageCode + "</td><td>" + item.UnitName + "</td><td>" + item.Qty + "</td></tr>");
                        }
                    }
                    break;
                case "shelve":
                    poInfo = po.GetPrintShelfMission(Id);
                    sbCargoListHtml.Append("<tr><th>序号</th><th>货品</th><th>货品名称</th><th>收货单号</th><th>待上架量</th><th>已上架量</th></tr>");
                    if (poInfo.CargoList != null && poInfo.CargoList.Count > 0)
                    {
                        foreach (var item in poInfo.CargoList)
                        {
                            index++;
                            sbCargoListHtml.Append(@"<tr><td>" + index + "</td><td>" + item.ProductCode + "</td><td>" + item.ProductName + "</td><td>" + item.OrderCode + "</td><td>" + item.StayQty + "</td><td>" + item.Qty + "</td></tr>");
                        }
                    }
                    break;
                case "OrderSend":
                    poInfo = po.GetPrintOrderSend(Id);
                    sbCargoListHtml.Append("<tr><th>序号</th><th>货品</th><th>货品名称</th><th>客户代码</th><th>客户名称</th><th>数量</th></tr>");
                    if (poInfo.CargoList != null && poInfo.CargoList.Count > 0)
                    {
                        foreach (var item in poInfo.CargoList)
                        {
                            index++;
                            sbCargoListHtml.Append(@"<tr><td>" + index + "</td><td>" + item.ProductCode + "</td><td>" + item.ProductName + "</td><td>" + item.CustomerCode + "</td><td>" + item.CustomerName + "</td><td>" + item.Qty + "</td></tr>");
                        }
                    }
                    break;
                case "OrderPick":
                    poInfo = po.GetPrintOrderPick(Id);
                    sbCargoListHtml.Append("<tr><th>序号</th><th>货品</th><th>货品名称</th><th>客户代码</th><th>客户名称</th><th>待拣量</th><th>已拣量</th></tr>");
                    if (poInfo.CargoList != null && poInfo.CargoList.Count > 0)
                    {
                        foreach (var item in poInfo.CargoList)
                        {
                            index++;
                            sbCargoListHtml.Append(@"<tr><td>" + index + "</td><td>" + item.ProductCode + "</td><td>" + item.ProductName + "</td><td>" + item.CustomerCode + "</td><td>" + item.CustomerName + "</td><td>" + item.StayQty + "</td><td>" + item.Qty + "</td></tr>");
                        }
                    }
                    break;
                default:
                    break;
            }
            imgBarcode.Src = poInfo.BarcodeImageUri;
            lbTitle.InnerText = poInfo.Title;
            lbPrintDate.InnerText = poInfo.SPrintDate;
            lbPurchaseOrderCode.InnerText = poInfo.PurchaseOrderCode;
            lbSupplierName.InnerText = poInfo.SupplierName;
            lbPlanArrivalTime.InnerText = poInfo.SPlanArrivalTime;
            lbActualArrivalTime.InnerText = poInfo.SActualArrivalTime;

            ltrCargoList.Text = string.Format(@"<table id=""dgCargo"" class=""dgPrint"">{0}</table>", sbCargoListHtml.ToString());
        }
    }
}