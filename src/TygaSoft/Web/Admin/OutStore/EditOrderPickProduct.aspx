<%@ Page Title="新建拣货单" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditOrderPickProduct.aspx.cs" Inherits="TygaSoft.Web.Admin.OutStore.EditOrderPickProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <input type="hidden" id="hId" runat="server" />
    <input type="hidden" id="hCustomerId" />
    <div class="easyui-panel" title="订单信息" style="margin-bottom:10px;">
        <%--<a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" href="/wms/a/gsend.html">新建</a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" onclick="AddOrderPicked.OnSave()">保存</a>--%>
        <div class="easyui-tabs" data-options="border:false" style="height:auto;">
            <div title="常用" style="padding:10px;">
               <div class="row - fl">
                    <span class="rl"><span class="cr">*</span> 单号：</span>
                    <div class="fl"><span id="lbOrderCode">系统自动生成</span></div>
                </div>
                <span class="clr"></span>
            </div>
        </div>
    </div>

    <div id="dgProductToolbar">
        <%--<a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListOrderPickProduct.OnAdd()"><span>添加货品</span></a>--%>
        <%--<a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListOrderPickProduct.OnDel()"><span>删除</span></a>--%>
        <%--<a id="lbtnSavePick" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok',disabled:true" onclick="ListOrderPickProduct.OnSavePick()"><span>确定拣货</span></a>--%>
    </div>
    <table id="dgOrderProduct" class="easyui-datagrid" title="明细" data-options="pagination:true,rownumbers:true,singleSelect:true,fit:false,fitColumns:true,striped:true,toolbar:'#dgProductToolbar',border:true,onClickRow:ListOrderPickProduct.OnClickRow,onBeginEdit:ListOrderPickProduct.OnBeginEdit">
        <thead>
            <tr>
                <th data-options="field: 'OrderPickId', checkbox: true"></th>
                <th data-options="field: 'OrderId', hidden: true"></th>
                <th data-options="field: 'ProductId', hidden: true"></th>
                <th data-options="field: 'ProductCode', width:100">货品编码</th>
                <th data-options="field: 'ProductName', width:150">货品名称</th>
                <th data-options="field:'CustomerCode',width:100">客户代码</th>
                <th data-options="field:'CustomerName',width:150">客户名称</th>
                <th data-options="field: 'StayQty', width:60">数量</th>
                <th data-options="field: 'Qty', width:60">拣货数量</th>
                <th data-options="field:'StockLocations',width:300">库位</th>
                <th data-options="field:'f1',width:60,formatter:ListOrderPickProduct.FBtn"></th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgStockLocationProduct.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/AddOrderPicked.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/ListOrderPickProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            AddOrderPicked.Init();
            ListOrderPickProduct.Init();
        })
    </script>

</asp:Content>
