<%@ Page Title="维修设备记录列表" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListDeviceRepairRecord.aspx.cs" Inherits="TygaSoft.Web.Manages.Gzxy.ListDeviceRepairRecord" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <div>
            开始日期：<input id="txtStartDate" class="easyui-datebox" style="width:100px;" />
            结束日期：<input id="txtEndDate" class="easyui-datebox" style="width:100px;" />
            归还日期：<input id="txtSBackDate" class="easyui-datebox" style="width:100px;" />
            是否修好：<asp:DropDownList runat="server" ID="ddlSWhetherFix" ClientIDMode="Static" />
            是否归还：<asp:DropDownList runat="server" ID="ddlSIsBack" ClientIDMode="Static" />
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListDeviceRepairRecord.OnSearch" style="width:200px;" />
        </div>
        <div class="mt5">
            <a id="abtnAdd" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'">新建</a>
            <a id="abtnEdit" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="">编辑</a>
            <a id="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">删除</a>
            <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'">导入/导出</a>
            <div id="mmExcel" style="width:150px;">
                <div><a href="/wms/Files/Template/维修设备记录导入模板.xlsx">下载导入模板</a></div>
                <div onclick="ListDeviceRepairRecord.OnImport()">导入</div>
                <div onclick="ListDeviceRepairRecord.OnExport()">导出</div>
            </div>
        </div>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:false,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
		        <th data-options="field: 'Id', checkbox: true"></th> 
                <th data-options="field:'UserName',width:80">用户</th>
                <th data-options="field: 'RecordDate', width:100,formatter:Common.FDate">日期</th> 
                <th data-options="field: 'Customer',width:200">客户</th> 
                <th data-options="field: 'SerialNumber', width:180">序列号</th> 
                <th data-options="field: 'DeviceModel', width:80">型号</th> 
                <th data-options="field: 'FaultCause', width:300">故障原因</th> 
                <th data-options="field: 'SolveMethod', width:300">解决方案</th> 
                <th data-options="field: 'CustomerProblem', width:300">客户问题</th> 
                <th data-options="field: 'DevicePart', width:250">配件</th> 
                <th data-options="field: 'TreatmentSituation', width:100">处理情况</th> 
                <th data-options="field: 'WhetherFix', width:80">是否修好</th> 
                <th data-options="field: 'HandoverPerson', width:100">交接人</th> 
                <th data-options="field: 'IsBack', width:80,formatter:Common.FIsYes">是否归还</th> 
                <th data-options="field: 'BackDate', width:100,formatter:Common.FDate">归还日期</th> 
                <th data-options="field: 'RegisteredPerson', width:100">登记人</th> 
                <th data-options="field: 'Remark', width:400">备注</th> 
            </tr>
        </thead>
    </table>

    <div id="dlgAddDeviceRepairRecord"></div>
    <div id="dlgUpload" style="padding:10px;"></div>

    <script type="text/javascript" src="/wms/Scripts/DlgUpload.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/Gzxy/ListDeviceRepairRecord.js"></script>

    <script type="text/javascript">
        $(function () {
            ListDeviceRepairRecord.Init();
        })
    </script>


</asp:Content>
