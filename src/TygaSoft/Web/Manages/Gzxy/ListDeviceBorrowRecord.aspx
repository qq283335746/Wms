<%@ Page Title="借样记录" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListDeviceBorrowRecord.aspx.cs" Inherits="TygaSoft.Web.Manages.Gzxy.ListDeviceBorrowRecord" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="DeviceBorrowRecord.Add()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="DeviceBorrowRecord.Edit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="DeviceBorrowRecord.Del()"><span>删除</span></a>
        <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'">导入/导出</a>
        <div id="mmExcel" style="width:150px;">
            <div onclick="DeviceBorrowRecord.OnExport()">导出</div>
        </div>
	    <div class="fr">
            开始日期：<input id="txtSStartDate" class="easyui-datebox" style="width:100px;" />
            结束日期：<input id="txtSEndDate" class="easyui-datebox" style="width:100px;" />
            归还日期：<input id="txtSBackDate" class="easyui-datebox" style="width:100px;" />
            是否归还：<input id="cbbSIsBack" class="txt200" style="height:20px;width:80px;" />
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:DeviceBorrowRecord.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:false,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
		        <th data-options="field: 'Id', checkbox: true"></th> 
                <th data-options="field: 'SRecordDate', width:100">日期</th> 
                <th data-options="field: 'Customer', width:180">客户</th> 
                <th data-options="field: 'CustomerContact', width:180">客户联系人</th> 
                <th data-options="field: 'SerialNumber', width:120">序列号</th> 
                <th data-options="field: 'DeviceModel', width:250">型号与配置</th> 
                <th data-options="field: 'DevicePart', width:250">配件明细</th> 
                <th data-options="field: 'PartStatus', width:100">配件状况</th> 
                <th data-options="field: 'ProjectAbout', width:250">项目信息</th> 
                <th data-options="field: 'SaleMan', width:100">业务员</th> 
                <th data-options="field: 'SendOrderCode', width:100">寄出单号</th> 
                <th data-options="field: 'SIsBack', width:80">是否归还</th> 
                <th data-options="field: 'SBackDate', width:100">归还日期</th> 
                <th data-options="field: 'Register', width:100">登记人</th> 
                <th data-options="field: 'Remark', width:300">备注</th> 
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/Gzxy/DeviceBorrowRecord.js"></script>
    <script type="text/javascript">
        $(function () {
            DeviceBorrowRecord.Init();
        })
    </script>

</asp:Content>
