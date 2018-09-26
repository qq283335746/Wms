<%@ Page Title="物流配送" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListLogisticsDistribution.aspx.cs" Inherits="TygaSoft.Web.Manages.ListLogisticsDistribution" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a id="lbtnAdd" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListLogisticsDistribution.OnAdd()"><span>新增</span></a>
        <a id="lbtnEdit" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListLogisticsDistribution.OnEdit()"><span>查看</span></a>
        <a id="lbtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListLogisticsDistribution.OnDel()"><span>删除</span></a>
        <a href="/wms/u/map.html" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-large-picture'"><span>地图展示</span></a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListLogisticsDistribution.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:false,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'ToDo', width:60,formatter:ListLogisticsDistribution.FToDo">操作</th>
                <th data-options="field: 'OrderCode', width:120">配送单号</th>
                <th data-options="field: 'Status', width:80">状态</th>
                <th data-options="field: 'TotalPackage', width:60">总件数</th>
                <th data-options="field: 'TotalVolume', width:60">总体积</th>
                <th data-options="field: 'TotalWeight', width:60">总重量</th>
                <th data-options="field: 'ToAddress', width:300">目的地</th>
                <th data-options="field: 'CompanyName', width:180">物流公司名称</th>
                <th data-options="field: 'VehicleID', width:100">车辆</th>
                <th data-options="field: 'TypeName', width:80">配送类型</th>
                <th data-options="field: 'Remark', width:300">备注</th>
                <th data-options="field: 'RecordDate', width:100,formatter:Common.FDate">记录日期</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/ListLogisticsDistribution.js"></script>
    <script type="text/javascript">
        $(function () {
            ListLogisticsDistribution.Init();
        })
    </script>

</asp:Content>
