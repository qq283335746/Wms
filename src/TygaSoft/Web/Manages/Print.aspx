<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="TygaSoft.Web.Manages.Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>打印单</title>
    <link href="~/Styles/Main.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/Plugins/Jeasyui15/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/Plugins/Jeasyui15/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Print.css" rel="stylesheet" type="text/css" />
    <script src="/wms/Scripts/Plugins/Jeasyui15/jquery.min.js" type="text/javascript"></script>
    <script src="/wms/Scripts/Plugins/Jeasyui15/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/wms/Scripts/Plugins/Jeasyui15/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="/wms/Scripts/JeasyuiExtend.js" type="text/javascript"></script>
    <script src="/wms/Scripts/JeasyuiHelper.js" type="text/javascript"></script>
    <script src="/wms/Scripts/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="printBtns" class="printBtns">
            <ul class="ul_h">
                <li><a onclick="Print.OnPrint()" class="abtn">打印</a></li>
            </ul>
        </div>
        <div id="printContent" class="order-print">
            <div class="header">
                <ul class="ul_h wp33">
                    <li>
                        <div class="barcode">
                            <img id="imgBarcode" runat="server" width="238" height="50" alt="条码" />
                        </div>
                    </li>
                    <li>
                        <div id="lbTitle" runat="server" class="title"></div>
                    </li>
                    <li>
                        <div class="fr pdt40">
                            <span>打印日期：</span><span id="lbPrintDate" runat="server"></span>
                        </div>
                    </li>
                </ul>
                <span class="clr"></span>
            </div>
            <div class="body">
                <div class="row-fl col3">
                    <span class="rl120">订单号：</span>
                    <div class="fl">
                        <span id="lbPurchaseOrderCode" runat="server" class="lb"></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">供应商：</span>
                    <div class="fl">
                        <span id="lbSupplierName" runat="server" class="lb"></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">收货日期：</span>
                    <div class="fl">
                        <div class="w200 txt-bb"></div>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">预约入库组：</span>
                    <div class="fl">
                        <span class="lb">入库组</span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">入库性质：</span>
                    <div class="fl">
                        <span class="lb">订单性入库</span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">收货人：</span>
                    <div class="fl">
                        <div class="w200 txt-bb"></div>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">项号：</span>
                    <div class="fl">
                        <span class="lb"></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">送货人：</span>
                    <div class="fl">
                        <span></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">送货公司：</span>
                    <div class="fl">
                        <span class="lb"></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">车辆号码：</span>
                    <div class="fl">
                        <span class="lb"></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">卸货码头：</span>
                    <div class="fl">
                        <span class="lb"></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">计划到货时间：</span>
                    <div class="fl">
                        <span id="lbPlanArrivalTime" runat="server"></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">实际到货时间：</span>
                    <div class="fl">
                        <span id="lbActualArrivalTime" runat="server" class="lb"></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120"></span>
                    <div class="fl">
                        <span class="lb"></span>
                    </div>
                </div>
                <span class="clr"></span>
                <div class="row mgt10">
                    <asp:Literal runat="server" ID="ltrCargoList"></asp:Literal>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgBarcodeTemplate.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/Print.js"></script>
    <script type="text/javascript">
        $(function () {
            Print.Init();
        })
    </script>

</body>
</html>
