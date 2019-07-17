<%@ Page Title="库存查询" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListStockProduct.aspx.cs" Inherits="TygaSoft.Web.Admin.Store.ListStockProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        入库时间：从<input id="txtSStartDate" class="easyui-datebox" /> 到 <input id="txtSEndDate" class="easyui-datebox" />
        <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'">导入/导出</a>
        <div id="mmExcel" style="width:150px;">
            <div onclick="ListStockProduct.OnExport()">导出</div>
        </div>
        <a id="abtnWeChat" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="Wechat.DlgStockProduct()">发送微信通知</a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListStockProduct.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>

    <table id="dgStockProduct" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'ProductCode',width:80">货品代码</th>
                <th data-options="field:'ProductName',width:150">货品名称</th>
                <th data-options="field:'CustomerCode',width:80">客户代码</th>
                <th data-options="field:'CustomerName',width:150">客户名称</th>
                <th data-options="field:'Qty',width:60">数量</th>
                <th data-options="field:'UnQty',width:80">暂存区数量</th>
                <th data-options="field:'FreezeQty',width:60">冻结数量</th>
                <th data-options="field:'WarnMsg',width:300,formatter:ListStockProduct.FWarnMsg">库存预警信息</th>
         <%--       <th data-options="field:'StockLocationName',width:200">库位</th>
                <th data-options="field:'LastStepName',width:60">状态</th>--%>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Admin/Store/ListStockProduct.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/Notice/Wechat.js"></script>
    <script type="text/javascript">
        $(function () {
            ListStockProduct.Init();
        })
    </script>

</asp:Content>
