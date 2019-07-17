<%@ Page Title="新建上架单" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditShelfMissionProduct.aspx.cs" Inherits="TygaSoft.Web.Admin.InStore.EditShelfMissionProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <input type="hidden" id="hId" runat="server" />
    <div class="easyui-panel" title="上架单信息" style="margin-bottom:10px;">
    <%--    <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" href="/wms/a/gainstore.html">新建</a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" onclick="AddShelfMission.OnSave()">保存</a>  --%>
        <div class="easyui-tabs" data-options="border:false" style="height:auto;">
            <div title="常用" style="padding-top:10px;">
                <div class="row-fl">
                    <span class="rl"><span class="cr">*</span> 上架单号：</span>
                    <div class="fl">
                        <span id="lbOrderCode">系统自动生成</span>
                    </div>
                </div>
                <div class="row-fl">
                    <span class="rl">备注：</span>
                    <div class="fl">
                        <input id="txtRemark" class="txt568" />
                    </div>
                </div>
                <span class="clr"></span>
            </div>
        </div>
    </div>

    <table id="dgOrderProduct" class="easyui-datagrid" title="明细" data-options="pagination:true,rownumbers:true,singleSelect:true,fit:false,fitColumns:true,striped:true,border:true">
        <thead>
            <tr>
                <th data-options="field: 'ShelfMissionId', checkbox: true"></th>
                <th data-options="field: 'OrderId', hidden: true"></th>
                <th data-options="field: 'ProductId', hidden: true"></th>
                <th data-options="field: 'OrderReceiptCode', width:100">收货单号</th>
                <th data-options="field: 'ProductCode', width:100">货品编码</th>
                <th data-options="field: 'ProductName', width:100">货品名称</th>
                <th data-options="field: 'StayQty', width:60">待上架量</th>
                <th data-options="field: 'Qty', width:60">已上架量</th>
                <th data-options="field:'StockLocations',width:300,formatter:ListShelfMissionProduct.FStockLocations">库位</th>
                <th data-options="field:'f1',width:60,formatter:ListShelfMissionProduct.FBtn"></th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgStockLocationProduct.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Admin/InStore/AddShelfMission.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Admin/InStore/ListShelfMissionProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            AddShelfMission.Init();
            ListShelfMissionProduct.Init();
        })
    </script>

</asp:Content>
