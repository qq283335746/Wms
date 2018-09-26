<%@ Page Title="拣货明细" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListOrderPickProduct.aspx.cs" Inherits="TygaSoft.Web.Admin.OutStore.ListOrderPickProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgOrderProductToolbar">
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListOrderPickProduct.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dgOrderProduct" class="easyui-datagrid" title="" data-options="pagination:true,rownumbers:true,singleSelect:true,fit:true,fitColumns:true,striped:true,border:true,toolbar:'#dgOrderProductToolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'ProductId',hidden:true"></th>
                <th data-options="field:'OrderId',hidden:true"></th>
                <th data-options="field:'OrderCode',width:100">单号</th>
                <th data-options="field:'ProductCode',width:100">货品</th>
                <th data-options="field:'ProductName',width:100">货品名称</th>
                <th data-options="field:'CustomerCode',width:100">客户代码</th>
                <th data-options="field:'CustomerName',width:150">客户名称</th>
                <th data-options="field:'Qty',width:60">已拣货量</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/ListOrderPickProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            ListOrderPickProduct.Init();
        })
    </script>

</asp:Content>
