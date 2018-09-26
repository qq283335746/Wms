<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InStockDemandOrder.aspx.cs" Inherits="TygaSoft.Web.Admin.Print.InStockDemandOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>入库需求单</title>
    <link href="~/Scripts/Plugins/Jeasyui15/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/Plugins/Jeasyui15/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Print.css" rel="stylesheet" type="text/css" />
    <script src="/wms/Scripts/Plugins/Jeasyui15/jquery.min.js" type="text/javascript"></script>
    <script src="/wms/Scripts/Plugins/Jeasyui15/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/wms/Scripts/Plugins/jquery.PrintArea.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Common.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Admin/Print/InStockDemandOrder.js"></script>
    <script type="text/javascript">
        $(function () {
            InStockDemandOrder.Init();
        })
    </script>
    
</head>
<body>
    <div class="printBtns">
        <button onclick="InStockDemandOrder.OnPrint()">打印单</button>
    </div>
    <form id="form1" runat="server">
        <div id="printContent" class="inStockDemandOrder">
            <div class="header">
                <ul class="ul_h wp33">
                    <li>
                        <div class="barcode">
                            <img id="imgBarcodeImageUri" width="238" height="50" alt="条码" />
                        </div>
                    </li>
                    <li>
                        <div class="title">
                            入库需求单
                        </div>
                    </li>
                    <li>
                        <div class="fr pdt40">
                            <span>打印日期：</span>
                            <span id="lbPrintDate">2016-07-04 17:47:36</span>
                        </div>
                    </li>
                </ul>
                <span class="clr"></span>
            </div>
            <div class="body">
                <div class="row-fl col3">
                    <span class="rl120">入库需求号：</span>
                    <div class="fl">
                        <span class="w200 lb" id="lbOrderCode"></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">供应商：</span>
                    <div class="fl">
                        <span class="lb" id="lbSupplierName"></span>
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
                    <span class="rl120">订单号：</span>
                    <div class="fl">
                        <span class="lb" id="lbPurchaseOrderCode"></span>
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
                        <span id="lbPlanArrivalTime"></span>
                    </div>
                </div>
                <div class="row-fl col3">
                    <span class="rl120">实际到货时间：</span>
                    <div class="fl">
                        <span class="lb" id="lbActualArrivalTime"></span>
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
                    <table id="dgCargo" class="dgPrint">
                        <tr>
                            <th>序号</th>
                            <th>货品</th>
                            <th>货品名称</th>
                            <th>包装</th>
                            <th>单位</th>
                            <th>预期量</th>
                            <th>到货量</th>
                            <th>推荐库位</th>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <input type="hidden" id="hIsDoPrint" />
    </form>
</body>
</html>
