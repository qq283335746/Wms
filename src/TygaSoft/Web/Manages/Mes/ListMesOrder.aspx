<%@ Page Title="订单跟踪列表" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListMesOrder.aspx.cs" Inherits="TygaSoft.Web.Manages.Mes.ListMesOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
     <%--   <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListMesOrder.OnAdd()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListMesOrder.OnEdit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListMesOrder.OnDel()"><span>删除</span></a>--%>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListMesOrder.OnSearch" style="width: 250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'OBarcode', width:100">订单条码</th>
                <th data-options="field: 'PBarcode', width:100">产品条码</th>
                <th data-options="field: 'PdBarcode', width:100">工序条码</th>
                <th data-options="field: 'PtBarcode', width:100">零件条码</th>
                <th data-options="field: 'Qty', width:100">数量</th>
                <th data-options="field: 'StartDate', width:100,formatter:Common.FDateTime">开始时间</th>
                <th data-options="field: 'EndDate', width:100,formatter:Common.FDateTime">结束时间</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/Mes/ListMesOrder.js"></script>
    <script type="text/javascript">
        $(function () {
            ListMesOrder.Init();
        })
    </script>

</asp:Content>
