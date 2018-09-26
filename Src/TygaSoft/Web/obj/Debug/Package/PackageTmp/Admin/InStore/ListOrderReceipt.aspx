<%@ Page Title="收货单列表" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListOrderReceipt.aspx.cs" Inherits="TygaSoft.Web.Admin.InStore.ListOrderReceipt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar" style="padding:3px 5px 1px 0;">
        <a id="abtnAdd" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'">新建</a>
        <a id="abtnEdit" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'">编辑</a>
        <a id="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">删除</a>
        <a id="lbtnCreate" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'">生成上架任务</a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-print'" onclick="ListOrderReceipt.Print()"><span>打印单</span></a>
        <div class="fr">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListOrderReceipt.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'OrderCode',width:100">单号</th>
                <th data-options="field:'CustomerCode',width:80">货主代号</th>
                <th data-options="field:'CustomerName',width:180">货主名称</th>
                <th data-options="field:'TypeName',width:100">类型</th>
                <th data-options="field:'StatusName',width:80">状态</th>
                <th data-options="field:'SSettlementDate',width:100">上次收货日期</th>
                <th data-options="field:'SupplierName',width:100">供应商名称</th>
                <th data-options="field:'PurchaseOrderCode',width:100">采购订单</th>
                <th data-options="field:'UserName',width:100">操作人</th>
                <th data-options="field:'IsStopNextText',width:100,formatter:ListOrderReceipt.FIsStopNext">生成上架任务</th>
            </tr>
        </thead>
    </table>

    <input type="hidden" id="hOrderType" runat="server" />

    <script type="text/javascript" src="/wms/Scripts/Admin/InStore/ListOrderReceipt.js"></script>
    <script type="text/javascript">
        $(function () {
            ListOrderReceipt.Init();
        })
    </script>

</asp:Content>
