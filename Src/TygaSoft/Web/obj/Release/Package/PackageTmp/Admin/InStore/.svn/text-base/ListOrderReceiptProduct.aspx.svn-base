<%@ Page Title="收货记录" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListOrderReceiptProduct.aspx.cs" Inherits="TygaSoft.Web.Admin.InStore.ListOrderReceiptProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgOrderProductToolbar">
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListOrderReceiptProduct.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dgOrderProduct" class="easyui-datagrid" data-options="pagination:true,rownumbers:true,singleSelect:true,fit:true,fitColumns:true,striped:true,border:true,toolbar:'#dgOrderProductToolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'ProductId',hidden:true"></th>
                <th data-options="field:'OrderId',hidden:true"></th>
                <th data-options="field:'OrderCode',width:100">单号</th>
                <th data-options="field:'PreOrderCode',width:100">预收货单号</th>
                <th data-options="field:'ProductCode',width:100">货品</th>
                <th data-options="field:'ProductName',width:100">货品名称</th>
                <th data-options="field:'PackageCode',width:100">包装</th>
                <th data-options="field:'Unit',width:60">单位</th>
                <th data-options="field:'ReceiptQty',width:60">已收货量</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/ListOrderReceiptProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            ListOrderReceiptProduct.Init();
        })
    </script>

</asp:Content>
