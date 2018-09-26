<%@ Page Title="收货单明细" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditOrderReceiptProduct.aspx.cs" Inherits="TygaSoft.Web.Admin.InStore.EditOrderReceiptProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <input type="hidden" id="hId" runat="server" />
    <input type="hidden" id="hOrderType" runat="server" />

    <div class="easyui-panel" title="订单信息" style="margin-bottom:10px;">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="AddOrderReceipt.Add()">新建</a>
        <a id="lbtnSave" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" onclick="AddOrderReceipt.OnSave()">保存</a>
        <a id="lbtnCreate" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" onclick="AddOrderReceipt.OnCreate()">生成上架任务</a>
        <div class="easyui-tabs" data-options="border:false" style="height:auto;">
            <div title="常用" style="padding-top:10px;">
                <asp:PlaceHolder ID="phAddOrderReceipt" runat="server"></asp:PlaceHolder>
            </div>
            <div title="其它信息" style="padding-top:10px;padding-bottom:10px;">
                <table class="infoT">
                    <tr>
                        <td><div class="tar">上次收货日期：</div></td>
                        <td>
                            <input id="txtLastTakeDate" class="easyui-validatebox txt" readonly="readonly" />
                        </td>
                        <td><div class="tar">预期收货日期：</div></td>
                        <td>
                            <input id="txtExpectTakeDate" class="easyui-validatebox txt" readonly="readonly" />
                        </td>
                        <td><div class="tar">实际出货日期：</div></td>
                        <td>
                            <input id="txtSendDate" class="easyui-validatebox txt" readonly="readonly" />
                        </td>
                        <td><div class="tar">计划出货日期：</div></td>
                        <td>
                            <input id="txtPlanSendDate" class="easyui-validatebox txt" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td><div class="tar">RMA号：</div></td>
                        <td>
                           <input id="txtRMA" class="easyui-validatebox txt" /> 
                        </td>
                        <td><div class="tar">预期体积：</div></td>
                        <td>
                            <input id="txtExpectVolume" class="easyui-validatebox txt" />
                        </td>
                        <td><div class="tar">毛重：</div></td>
                        <td>
                            <input id="txtGW" class="easyui-validatebox txt" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
            </div>
            <div title="自定义" style="padding-top:10px;padding-right:100px;">
                <div id="customInfoT"></div>
            </div>
        </div>
    </div>

    <div id="dgOrderProductToolbar">
        <a id="lbtnAddProduct" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListOrderReceiptProduct.Add()"><span>新增</span></a>
        <a id="lbtnEditProduct" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListOrderReceiptProduct.Edit()"><span>编辑</span></a>
        <a id="lbtnDelProduct" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListOrderReceiptProduct.Del()"><span>删除</span></a>
    </div>
    <table id="dgOrderProduct" class="easyui-datagrid" title="明细" data-options="pagination:true,rownumbers:true,singleSelect:true,fit:false,fitColumns:true,striped:true,border:true,toolbar:'#dgOrderProductToolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'ProductId',hidden:true"></th>
                <th data-options="field:'OrderId',hidden:true"></th>
                <th data-options="field:'PreOrderCode',width:100">预收货单号</th>
                <th data-options="field:'ProductCode',width:100">货品</th>
                <th data-options="field:'ProductName',width:100">货品名称</th>
                <th data-options="field:'PackageCode',width:100">包装</th>
                <th data-options="field:'Unit',width:60">单位</th>
                <th data-options="field:'Status',width:60">状态</th>
                <th data-options="field:'ExpectedQty',width:60">预期量</th>
                <th data-options="field:'ReceiptQty',width:60">已收货量</th>
            </tr>
        </thead>
    </table>

    <div id="dlgAddOrderReceiptProduct"></div>
    <script type="text/javascript" src="/wms/Scripts/Manages/DlgCustomer.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/AddOrderReceipt.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Admin/InStore/ListOrderReceiptProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            AddOrderReceipt.Init();
            ListOrderReceiptProduct.Init();
        })
    </script>

</asp:Content>
