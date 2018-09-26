<%@ Page Title="车辆管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListVehicle.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.ListVehicle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListVehicle.OnAdd()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListVehicle.OnEdit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListVehicle.OnDel()"><span>删除</span></a>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'VehicleID', width:100">车牌</th>
                <th data-options="field: 'VehicleModel', width:100">车型</th>
                <th data-options="field: 'Licence', width:100">证照</th>
                <th data-options="field: 'LicPic', hidden: true">证照照</th>
                <th data-options="field: 'OffenceRecord', width:100">违章记录</th>
                <th data-options="field: 'DriverID', width:100">司机身份证</th>
                <th data-options="field: 'DriverIDPicture', hidden: true">司机身份证照</th>
                <th data-options="field: 'RewardRecord', width:100">奖惩记录</th>
                <th data-options="field: 'Remark', width:100">备注</th>
                <th data-options="field: 'Sort', width:100">排序</th>
                <th data-options="field: 'IsDisable', hidden: true"></th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/ListVehicle.js"></script>
    <script type="text/javascript">
        $(function () {
            ListVehicle.Init();
        })
    </script>

</asp:Content>
