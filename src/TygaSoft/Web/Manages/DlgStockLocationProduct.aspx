<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DlgStockLocationProduct.aspx.cs" Inherits="TygaSoft.Web.Manages.DlgStockLocationProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>库位货品</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="hidden" id="hKey" runat="server" />
        <input type="hidden" id="hValue" runat="server" />
    </div>
    </form>

    <div id="tabsStockLocation" class="easyui-tabs" data-options="fit:true,border:false">
        <div title="推荐库位" style="padding:10px;">
            <table id="dgStockLocationProductBest" data-options="pagination:false,rownumbers:true,singleSelect:true,fit:true,fitColumns:true,striped:true,border:true,onClickRow:DlgStockLocationProduct.OnClickRowByBest,onBeginEdit:DlgStockLocationProduct.OnBeginEditByBest">
                <thead>
                    <tr>
                        <th data-options="field:'StockLocationCode', width:100">库位代码</th>
                        <th data-options="field:'StockLocationName', width:150">库位名称</th>
                        <th data-options="field:'CustomerCode', width:100">客户代码</th>
                        <th data-options="field:'CustomerName', width:200">客户名称</th>
                        <th data-options="field:'ProductCode', width:100">货品代码</th>
                        <th data-options="field:'ProductName', width:200">货品名称</th>
                        <th data-options="field: 'MaxQty',width:80">库位最大容量</th>
                        <th data-options="field: 'Qty',width:60,editor:'numberbox'">数量</th>
                    </tr>
                </thead>
            </table>
        </div>
        <div title="其它库位" style="padding:10px;">
            <table id="dgStockLocationProductOther" data-options="pagination:false,rownumbers:true,singleSelect:true,fit:true,fitColumns:true,striped:true,border:true,onClickRow:DlgStockLocationProduct.OnClickRowByOther,onBeginEdit:DlgStockLocationProduct.OnBeginEditByOther">
                <thead>
                    <tr>
                        <th data-options="field:'StockLocationCode', width:100">库位代码</th>
                        <th data-options="field:'StockLocationName', width:150">库位名称</th>
                        <th data-options="field:'ProductCode', width:100">货品代码</th>
                        <th data-options="field:'ProductName', width:150">货品名称</th>
                        <th data-options="field: 'MaxQty',width:100">库位最大容量</th>
                        <th data-options="field: 'Qty',width:100,editor:'numberbox'">数量</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            DlgStockLocationProduct.Init();
        })
    </script>

</body>
</html>
