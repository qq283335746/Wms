<%@ Page Title="发货单列表" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListOrderSend.aspx.cs" Inherits="TygaSoft.Web.Admin.OutStore.ListOrderSend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" href="/wms/a/gsend.html" ><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListOrderSend.OnEdit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListOrderSend.OnDel()"><span>删除</span></a>
        <a id="lbtnCreate" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" onclick="ListOrderSend.OnCreate()">生成拣货任务</a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-print'" onclick="ListOrderSend.Print()"><span>打印单</span></a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListOrderSend.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'CustomerCode', hidden: true"></th>
                <th data-options="field: 'CustomerName', hidden: true">客户</th>
                <th data-options="field: 'OrderCode', width:100">发货单</th>
                <th data-options="field: 'StayQty', width:100">出库量</th>
                <th data-options="field: 'Qty', width:100">已拣货量</th>
                <th data-options="field:'UserName',width:100">操作人</th>
                <th data-options="field: 'StatusName', width:100,formatter:Common.FStatus">状态</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/ListOrderSend.js"></script>
    <script type="text/javascript">
        $(function () {
            ListOrderSend.Init();
        })
    </script>

</asp:Content>
