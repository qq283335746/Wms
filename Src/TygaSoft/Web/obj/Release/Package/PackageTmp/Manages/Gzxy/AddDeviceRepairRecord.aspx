<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDeviceRepairRecord.aspx.cs" Inherits="TygaSoft.Web.Manages.Gzxy.AddDeviceRepairRecord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新增/编辑维修设备记录</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row mt10">
            <span class="rl">日期：</span>
            <div class="fl">
                <input id="txtRecordDate" runat="server" class="easyui-datebox txt" data-options="required:true" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">客户：</span>
            <div class="fl">
                <input id="txtCustomer" runat="server" class="easyui-validatebox txt w500" data-options="required:true" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">序列号：</span>
            <div class="fl">
                <input id="txtSerialNumber" runat="server" class="easyui-validatebox txt w500" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">型号：</span>
            <div class="fl">
                <input id="txtDeviceModel" runat="server" class="easyui-validatebox txt w500" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">故障原因：</span>
            <div class="fl">
                <input id="txtFaultCause" runat="server" class="easyui-validatebox txt w500" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">解决方案：</span>
            <div class="fl">
                <input id="txtSolveMethod" runat="server" class="easyui-validatebox txt w500" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">客户问题：</span>
            <div class="fl">
                <input id="txtCustomerProblem" runat="server" class="easyui-validatebox txt w500" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">配件：</span>
            <div class="fl">
                <input id="txtDevicePart" runat="server" class="easyui-validatebox txt w500" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">处理情况：</span>
            <div class="fl">
                <input id="txtTreatmentSituation" runat="server" class="easyui-validatebox txt w500" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">是否修好：</span>
            <div class="fl">
                <asp:DropDownList runat="server" ID="ddlWhetherFix"></asp:DropDownList>
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">交接人：</span>
            <div class="fl">
                <input id="txtHandoverPerson" runat="server" class="easyui-validatebox txt w500" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">是否归还：</span>
            <div class="fl">
                <asp:DropDownList runat="server" ID="ddlIsBack"></asp:DropDownList>
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">归还日期：</span>
            <div class="fl">
                <input id="txtBackDate" runat="server" class="easyui-datebox txt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">登记人：</span>
            <div class="fl">
                <input id="txtRegisteredPerson" runat="server" class="easyui-validatebox txt w500" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">备注：</span>
            <div class="fl">
                <input id="txtRemark" runat="server" class="easyui-validatebox txt w500" />
            </div>
        </div>

        <input type="hidden" id="hId" runat="server" />
        
    </form>

    <script type="text/javascript" src="/wms/Scripts/Manages/Gzxy/AddDeviceRepairRecord.js"></script>
    <script type="text/javascript">
        $(function () {
            AddDeviceRepairRecord.Init();
        })
    </script>
</body>
</html>
