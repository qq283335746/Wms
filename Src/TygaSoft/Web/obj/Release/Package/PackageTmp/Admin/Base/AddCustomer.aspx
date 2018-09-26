<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCustomer.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.AddCustomer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑客户</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row mt10">
            <span class="rl">货主代号：</span>
            <div class="fl">
                <input id="txtCustomerCode" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" data-options="required:true" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">货主名称：</span>
            <div class="fl">
                <input id="txtCustomerName" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" data-options="required:true" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">简称：</span>
            <div class="fl">
                <input id="txtShortName" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">联系人：</span>
            <div class="fl">
                <input id="txtContactMan" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">邮箱：</span>
            <div class="fl">
                <input id="txtEmail" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">手机：</span>
            <div class="fl">
                <input id="txtPhone" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">电话：</span>
            <div class="fl">
                <input id="txtTelPhone" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">传真：</span>
            <div class="fl">
                <input id="txtFax" runat="server" class="easyui-validatebox mtxt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">邮编：</span>
            <div class="fl">
                <input id="txtPostcode" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">地址：</span>
            <div class="fl">
                <input id="txtAddress" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">备注：</span>
            <div class="fl">
                <input id="txtRemark" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" />
            </div>
        </div>

        <input type="hidden" id="hId" runat="server" clientidmode="Static" />
        
    </form>

    <script type="text/javascript" src="../Scripts/Admin/Base/AddCustomer.js"></script>
    <script type="text/javascript">
        $(function () {
            AddCustomer.Init();
        })
    </script>
</body>
</html>
