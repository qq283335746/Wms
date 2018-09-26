<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCompany.aspx.cs" Inherits="TygaSoft.Web.Manages.AddCompany" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑公司信息</title>
</head>
<body>
    <form id="dlgCompanyFm" runat="server">
        <div class="row-fl">
            <span class="rl">公司编号：</span>
            <div class="fl">
                <input id="txtCoded" class="txt200" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span>公司全称：</span>
            <div class="fl">
                <input id="txtNamed" class="easyui-validatebox txt200" data-options="required:true" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span>公司简称：</span>
            <div class="fl">
                <input id="txtShortName" class="easyui-validatebox txt200" data-options="required:true" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">总公司：</span>
            <div class="fl">
                <input id="txtInCompany" class="txt200" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">联系人：</span>
            <div class="fl">
                <input id="txtContactMan" class="txt200" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">联系方式：</span>
            <div class="fl">
                <input id="txtContactPhone" class="txt200" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">电话：</span>
            <div class="fl">
                <input id="txtTelPhone" class="easyui-validatebox txt200" data-options="validType:'telPhone'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">传真：</span>
            <div class="fl">
                <input id="txtFax" class="easyui-validatebox txt200" data-options="validType:'telPhone',invalidMessage:'请正确输入传真号码！'" />
            </div>
        </div>
        <div class="row - fl">
            <span class="rl">邮编：</span>
            <div class="fl">
                <input id="txtPostCode" class="easyui-validatebox txt200" data-options="validType:'int'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">地址：</span>
            <div class="fl">
                <input id="txtAddress" class="txt200" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">公司简介：</span>
            <div class="fl">
                <textarea id="txtCompanyAbout" rows="50" cols="100" style="width:524px;height:324px;"></textarea>
            </div>
        </div>

        <input type="hidden" id="hCompanyId" />
        <span class="clr"></span>
    </form>

    <script type="text/javascript" src="/wms/Scripts/Manages/AddCompany.js"></script>
    <script type="text/javascript">
        $(function () {
            AddCompany.Init();
        })
    </script>
</body>
</html>
