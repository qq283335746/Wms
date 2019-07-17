<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DlgProduct.aspx.cs" Inherits="TygaSoft.Web.Manages.DlgProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>选择货品</title>
</head>
<body>
    <form id="dlgProductFm" runat="server">
    <div>
        <table id="dgProduct" data-options="fit:false,fitColumns:true,pagination:false,rownumbers:true,singleSelect:true,border:false,striped:true,onClickRow:DlgProduct.OnClickRow">
            <thead>
                <tr>
                    <th data-options="field: 'ProductId', checkbox: true"></th>
                    <th data-options="field: 'ProductCode', width:100">货品编码</th>
                    <th data-options="field: 'ProductName', width:100">货品名称</th>
                    <th data-options="field: 'Qty', width:60,formatter:DlgProduct.FQty">准备数量</th>
                    <th data-options="field: 'RealQty', width:60,editor:'numberbox'">实际数量</th>
                    <th data-options="field: 'StockLocations', width:120,editor:'text'">库位</th>
                </tr>
            </thead>
        </table>

        <input type="hidden" id="hStepName" runat="server" />
        <input type="hidden" id="hOrderId" runat="server" />
    </div>
    </form>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            DlgProduct.Init();
        })
    </script>
</body>
</html>
