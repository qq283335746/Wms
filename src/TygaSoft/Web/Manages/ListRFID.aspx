<%@ Page Title="RFID" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListRFID.aspx.cs" Inherits="TygaSoft.Web.Manages.ListRFID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
   
    <div id="dgToolbar">
        <a id="lbtnScan" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListRFID.OnScan()">开始扫描</a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListRFID.OnDel()"><span>删除</span></a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListRFID.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'TID',checkbox:true"></th>
                <th data-options="field: 'EPC',width:100">货品编号</th>
                <th data-options="field: 'ProductName',width:150">货品名称</th>
                <th data-options="field: 'FullName',width:180">全名</th>
                <th data-options="field: 'Specs',width:100">规格型号</th>
                <th data-options="field: 'LastUpdatedDate',width:150,formatter:Common.FDateTime">时间</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/ListRFID.js"></script>
    <script type="text/javascript">
        $(function () {
            ListRFID.Init();
        })
    </script>

</asp:Content>
