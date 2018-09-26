<%@ Page Title="盘点" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditPandianProduct.aspx.cs" Inherits="TygaSoft.Web.Admin.Pandian.EditPandianProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <input type="hidden" id="hId" runat="server" />
    <div class="easyui-panel" title="盘点单信息" style="margin-bottom: 10px;">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" href="/wms/a/ypandian.html">新建</a>
        <a id="lbtnSave" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" onclick="AddPandian.OnSave()">保存</a>
        <div class="easyui-tabs" data-options="border:false" style="height: auto;">
            <div title="常用" style="padding-top: 10px;">
                <div class="row-fl">
                    <span class="rl">盘点单号：</span>
                    <div class="fl">
                        <span id="lbOrderCode">系统自动生成</span>
                    </div>
                </div>
                <div class="row-fl">
                    <span class="rl"><span class="cr">*</span> 盘点单名称：</span>
                    <div class="fl">
                        <input id="txtNamed" class="easyui-validatebox txt200" data-options="required:true" />
                    </div>
                </div>
                <span class="clr"></span>
                <div class="row mb10">
                    <span class="rl"><span class="cr">*</span> 分配用户：</span>
                    <div class="fl" style="width:398px;">
                        <input id="cbgUser" name="cbgUser" data-options="idField:'Id',textField: 'Text', fit:true, fitColumns:true,multiple:true,rownumbers:true,onSelect:DlgUsers.OnSelect,panelWidth:398" />
                    </div>
                </div>
                <div class="row">
                    <span class="rl">备注：</span>
                    <div class="fl">
                        <textarea id="txtRemark" rows="3" cols="80" class="txta" style="width:389px;height:60px;"></textarea>
                    </div>
                </div>
            </div>
            <div title="盘点范围" style="padding-top: 10px;">
                <div class="row-fl">
                    <span class="rl">入库开始日期：</span>
                    <div class="fl" style="width:396px;">
                        <input id="txtStockStartDate" name="StockStartDate" class="easyui-datebox" data-options="editable:false" />
                        <span class="mlr10">-</span>
                        入库结束日期：<input id="txtStockEndDate" name="StockEndDate" class="easyui-datebox" data-options="editable:false" />
                    </div>
                </div>
                <div class="row-fl">
                    <span class="rl">货主：</span>
                    <div class="fl" style="width:396px;">
                        <input id="cbgCustomer" data-options="idField:'Id',textField: 'CustomerName', fit:true, fitColumns:true,multiple:true,rownumbers:true,onSelect:DlgCustomer.OnSelect,panelWidth:396" />
                    </div>
                </div>
                <span class="clr"></span>
                <div class="row-fl">
                    <span class="rl">库区：</span>
                    <div class="fl" style="width:354px;margin-right:42px;">
                        <input id="cbgZone" class="easyui-combogrid" data-options="idField:'Id',textField: 'ZoneName', fit:true, fitColumns:true,multiple:true,rownumbers:true,onClickRow:DlgZone.OnClickRow,onSelect:DlgZone.OnSelect,onSelectAll:DlgZone.OnSelectAll,panelWidth:354" />
                    </div>
                </div>
                <div class="row-fl">
                    <span class="rl">库位：</span>
                    <div class="fl" style="width:396px;">
                        <input id="cbgStockLocation" data-options="idField:'Id',textField: 'Named', fit:true, fitColumns:true,multiple:true,rownumbers:true,onSelect:DlgStockLocation.OnSelect,panelWidth:396" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="dgPandianProductToolbar" style="padding: 5px;">
        <ul class="h_ul">
            <li style="width: 30%; color: #3c8dbc;">已盘（<span id="lbTotalPan">0</span>）</li>
            <li style="width: 30%; color: #3c763d;">盘盈（<span id="lbTotalYPan">0</span>）</li>
            <li style="width: 30%; color: #a94442;">未盘（<span id="lbTotalNotPan">0</span>）</li>
        </ul>
        <span class="clr"></span>
        <div style="padding-top:5px;">
            <a id="lbtnDlgPandian" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListPandianProduct.OnDlgPandian()">盘点操作</a>
            <a id="lbtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListPandianProduct.OnDel()">删除</a>
         <%--   <a name="abtnSave" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'">提交盘点结果</a>--%>
        </div>
    </div>
    <table id="dgPandianProduct" class="easyui-datagrid" title="明细" data-options="pagination:true,rownumbers:true,singleSelect:false,fit:false,fitColumns:false,striped:true,border:true,toolbar:'#dgPandianProductToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'ProductId', checkbox: true"></th>
                <th data-options="field: 'ProductCode', width:100">货品编码</th>
                <th data-options="field: 'ProductName', width:200">货品名称</th>
                <th data-options="field: 'CustomerCode', width:100">货主编码</th>
                <th data-options="field: 'CustomerName', width:200">货主名称</th>
                <th data-options="field: 'Zones', width:200">库区</th>
                <th data-options="field: 'StockLocationCodes', width:200">库位</th>
                <th data-options="field: 'StayQty', width:60">账面数</th>
                <th data-options="field: 'UpdatedZones', width:200,hidden:true">修改后库区</th>
                <th data-options="field: 'UpdatedStockLocations',width:200,hidden:true">修改后库位</th>
                <th data-options="field: 'Qty', width:60">实盘数</th>
                <th data-options="field: 'FailQty', width:80">差异数量</th>
                <th data-options="field: 'Status', width:80">状态</th>
                <th data-options="field: 'UserName', width:100,formatter:ListPandianProduct.FUserName,hidden:true">盘点人</th>
                <th data-options="field: 'Remark', width:300,hidden:true">备注</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgUsers.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/DlgCustomer.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/DlgZone.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/DlgStockLocation.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/AddPandian.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/ListPandianProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            AddPandian.Init();
            ListPandianProduct.Init();
        })
    </script>

</asp:Content>
