<%@ Page Title="库存预警管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListStockWarning.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.ListStockWarning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar" style="padding:3px 5px 1px 0;">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListStockWarning.Add()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListStockWarning.Edit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListStockWarning.Del()"><span>删除</span></a>
        <div class="fr">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListStockWarning.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'ZoneId',hidden:true"></th>
                <th data-options="field:'StockLocationId',hidden:true"></th>
                <th data-options="field:'Coded',width:100">库存预警编号</th>
                <th data-options="field:'ZoneName',width:100">所属库区</th>
                <th data-options="field:'ZoneProperty',width:100">库区性质</th>
                <th data-options="field:'StockLocationCode',width:80">库位编号</th>
                <th data-options="field:'StockLocationProperty',width:100">库位性质</th>
                <th data-options="field:'StockAmount',width:80">库存金额</th>
                <th data-options="field:'OverdueDay',width:80">超期天数</th>
                <th data-options="field:'MinQty',width:80">最小库存量</th>
                <th data-options="field:'MaxQty',width:80">最大库存量</th>
                <th data-options="field:'Remark',width:300">备注</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Admin/Base/ListStockWarning.js"></script>
    <script type="text/javascript">
        $(function () {
            ListStockWarning.Init();
        })
    </script>

</asp:Content>
