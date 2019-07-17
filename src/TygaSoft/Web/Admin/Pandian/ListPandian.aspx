<%@ Page Title="盘点单列表" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListPandian.aspx.cs" Inherits="TygaSoft.Web.Admin.Pandian.ListPandian" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a href="/wms/a/ypandian.html" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListPandian.OnEdit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListPandian.OnDel()"><span>删除</span></a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListPandian.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'OrderCode', width:100">盘点单号</th>
                <th data-options="field: 'Named', width:100">盘点名称</th>
                <th data-options="field: 'Remark', width:100">备注</th>
                <th data-options="field: 'Status', width:100">状态</th>
                <th data-options="field: 'UserName', width:100">操作人</th>

            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/ListPandian.js"></script>
    <script type="text/javascript">
        $(function () {
            ListPandian.Init();
        })
    </script>

</asp:Content>
