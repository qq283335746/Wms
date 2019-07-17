<%@ Page Title="库位管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListStockLocation.aspx.cs" Inherits="TygaSoft.Web.Manages.Base.ListStockLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListStockLocation.OnAdd()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListStockLocation.OnEdit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListStockLocation.OnDel()"><span>删除</span></a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListStockLocation.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'Code', width:100">库位编号</th>
                <th data-options="field: 'Named', width:100">库位名称</th>
                <th data-options="field: 'ZoneCode', width:100">库区</th>
                <th data-options="field: 'ABC', width:100">ABC</th>
                <th data-options="field: 'UseStatus', width:100">标志</th>
                <th data-options="field: 'StockLocationType', width:100">库位类型</th>
                <th data-options="field: 'CtrType', width:100">种类</th>
                <th data-options="field: 'IsMixPlace', width:60,formatter:Common.FIsYes">混放货品</th>
                <th data-options="field: 'IsBatchNum', width:60,formatter:Common.FIsYes">混放批号</th>
                <th data-options="field: 'IsLoseId', width:60,formatter:Common.FIsYes">丢失ID</th>

            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/Base/ListStockLocation.js"></script>
    <script type="text/javascript">
        $(function () {
            ListStockLocation.Init();
        })
    </script>

</asp:Content>
