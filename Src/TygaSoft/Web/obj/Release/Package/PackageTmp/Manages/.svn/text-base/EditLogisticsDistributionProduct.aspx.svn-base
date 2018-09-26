<%@ Page Title="物流配送明细" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditLogisticsDistributionProduct.aspx.cs" Inherits="TygaSoft.Web.Manages.EditLogisticsDistributionProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <input type="hidden" id="hId" runat="server" />
    <div class="pdb5">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" href="/wms/u/yt.html">新建</a>
        <a id="lbtnSave" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" onclick="AddLogisticsDistribution.OnSave()">保存</a>
    </div>
    <div id="tabsOne" class="easyui-tabs" data-options="fit:true,onSelect:AddLogisticsDistribution.OnTabsSelect">
        <div title="常用" style="padding-top:10px;padding-bottom:10px;">
            <div class="row mb10">
                <span class="rl">配送单号：</span>
                <div class="fl">
                    <span id="lbOrderCode">系统自动生成</span>
                </div>
            </div>
            <div class="row mb10">
                <span class="rl"><span class="cr">*</span> 出库单：</span>
                <div class="fl" style="width:398px;height:22px;">
                    <input id="cbgOrder" name="cbgOrder" data-options="idField:'Id',textField: 'OrderCode', fit:true, fitColumns:true,multiple:true,rownumbers:true,required:true,onSelect:DlgOrder.OnSelect,panelWidth:398" />
                </div>
            </div>
            <div class="row mb10">
                <span class="rl">件数：</span>
                <div class="fl">
                    <span id="lbTotalPackage"></span>
                </div>
            </div>
            <div class="row mb10">
                <span class="rl">体积（立方米）：</span>
                <div class="fl">
                    <span id="lbTotalVolume"></span>
                </div>
            </div>
            <div class="row mb10">
                <span class="rl">重量（KG）：</span>
                <div class="fl">
                    <span id="lbTotalWeight"></span>
                </div>
            </div>
            <span class="clr"></span>
            <div class="row mt10">
                <span class="rl">目的地：</span>
                <div class="fl">
                    <input id="txtToAddress" placeholder="省 市 区 具体地址" class="txt" style="width:389px;" />
                </div>
            </div>
            <div class="row mt10">
                <span class="rl">备注：</span>
                <div class="fl">
                    <textarea id="txtRemark" rows="3" cols="80" class="txta" style="width:389px;height:60px;"></textarea>
                </div>
            </div>
        </div>
        <div title="自配送" style="padding:20px;">
            <div class="row mb10">
                <span class="rl" style="width:60px;">车辆：</span>
                <div class="fl" style="width:398px;">
                    <input id="cbgVehicle" name="cbgVehicle" data-options="idField:'Id',textField: 'VehicleID', fit:true, fitColumns:true,multiple:true,rownumbers:true,onSelect:DlgVehicle.OnSelect,panelWidth:398" />
                </div>
            </div>
        </div>
        <div title="找物流公司" style="padding:20px;">
            <div class="row mb10">
                <span class="rl" style="width:90px;">物流公司：</span>
                <div class="fl" style="width:398px;">
                    <input id="cbgCompany" name="cbgCompany" data-options="idField:'Id',textField: 'Named', fit:true, fitColumns:true,multiple:false,rownumbers:true,onSelect:DlgCompany.OnSelect,panelWidth:398" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgOrder.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/DlgCompany.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/DlgVehicle.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/AddLogisticsDistribution.js"></script>
    <script type="text/javascript">
        $(function () {
            AddLogisticsDistribution.Init();
        })
    </script>

</asp:Content>
