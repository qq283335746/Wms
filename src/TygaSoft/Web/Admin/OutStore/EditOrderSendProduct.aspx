<%@ Page Title="发货单明细" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditOrderSendProduct.aspx.cs" Inherits="TygaSoft.Web.Admin.OutStore.EditOrderSendProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <input type="hidden" id="hId" runat="server" />
    <input type="hidden" id="hCustomerId" />

    <div class="easyui-panel" title="订单信息" style="margin-bottom:10px;">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" href="/wms/a/gsend.html">新建</a>
        <a id="lbtnSave" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save',disabled:true" onclick="AddOrderSend.OnSave()">保存</a>
        <a id="lbtnCreate" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save',disabled:true" onclick="AddOrderSend.OnCreate()">生成拣货任务</a>

        <div class="easyui-tabs" data-options="border:false">
            <div title="常用" style="padding:10px;">
               <div class="row - fl">
                    <span class="rl"><span class="cr">*</span> 单号：</span>
                    <div class="fl"><span id="lbOrderCode">系统自动生成</span></div>
                </div>
                <div class="row - fl">
                    <span class="rl"><span class="cr">*</span> 客户：</span>
                    <div class="fl">
                        <input id="txtCustomer" class="easyui-textbox" data-options="required:true,missingMessage:'请选择',invalidMessage:'请选择', icons:[{iconCls:'icon-search',
                                handler: function(e){
                                    DlgCustomer.OnDlg();
                                }
                            }]" style="width:220px" />
                    </div>
                </div>
                <span class="clr"></span>
            </div>
        </div>
    </div>

    <div id="dgProductToolbar">
        <a id="abtnAddProduct" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add',disabled:true" onclick="DlgStockLocationProduct.DlgOrderSendProduct()"><span>添加货品</span></a>
        <%--<a id="abtnDelProduct" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListOrderSendProduct.OnDel()"><span>删除</span></a>--%>
        <%--<a id="lbtnSavePick" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok',disabled:true" onclick="ListOrderSendProduct.OnSavePick()"><span>确定拣货</span></a>--%>
    </div>
    <table id="dgOrderProduct" class="easyui-datagrid" title="明细" data-options="pagination:true,rownumbers:true,singleSelect:true,fit:false,fitColumns:true,striped:true,toolbar:'#dgProductToolbar',border:true,onClickRow:ListOrderSendProduct.OnClickRow,onBeginEdit:ListOrderSendProduct.OnBeginEdit">
        <thead>
            <tr>
                <th data-options="field: 'ProductId', checkbox: true"></th>
                <th data-options="field: 'OrderId', hidden: true"></th>
                <th data-options="field: 'ProductCode', width:100">货品编码</th>
                <th data-options="field: 'ProductName', width:100">货品名称</th>
                <th data-options="field:'CustomerCode',width:100">客户代码</th>
                <th data-options="field:'CustomerName',width:150">客户名称</th>
                <th data-options="field: 'Qty', width:100">数量</th>
                <th data-options="field: 'PickQty', width:100">拣货数量</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgCustomer.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/DlgStockLocationProduct.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/AddOrderSend.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/ListOrderSendProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            AddOrderSend.Init();
            ListOrderSendProduct.Init();
        })
    </script>

</asp:Content>
