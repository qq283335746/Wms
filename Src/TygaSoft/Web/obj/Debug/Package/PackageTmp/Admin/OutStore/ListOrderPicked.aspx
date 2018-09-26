<%@ Page Title="拣货任务" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListOrderPicked.aspx.cs" Inherits="TygaSoft.Web.Admin.OutStore.ListOrderPicked" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListOrderPicked.OnEdit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-print'" onclick="ListOrderPicked.Print()"><span>打印单</span></a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListOrderPicked.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'CustomerCode', hidden: true"></th>
                <th data-options="field: 'CustomerName', hidden: true">客户</th>
                <th data-options="field: 'OrderCode', width:100">拣货单号</th>
                <th data-options="field:'TotalStayQty',width:120">应拣量</th>
                <th data-options="field:'TotalQty',width:120">已拣量</th>
                <th data-options="field: 'StatusName', width:100">状态</th>
                <th data-options="field: 'UserName', width:100">操作人</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/ListOrderPicked.js"></script>
    <script type="text/javascript">
        $(function () {
            ListOrderPicked.Init();
        })
    </script>

</asp:Content>
