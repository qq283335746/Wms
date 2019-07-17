<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSiteMulti.aspx.cs" Inherits="TygaSoft.Web.Manages.Sys.AddSiteMulti1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑站点信息</title>
</head>
<body>
    <form id="dlgFm" runat="server">
    <div>
        <div class="row"><span class="rl w100"><span class="cr">*</span> 编号：</span><div class="fl">
            <input id="txtCoded" class="easyui-validatebox txt w400" data-options="readonly:true" value="系统自动生成" /></div>
        </div>
        <div class="row mt10"><span class="rl w100"><span class="cr">*</span> 名称：</span><div class="fl">
            <input id="txtNamed" class="easyui-validatebox txt w400" data-options="required:true" /></div>
        </div>
        <div class="row mt10"><span class="rl w100">站点语言：</span>
            <div class="fl">
                <input id="cbbCulture" class="txt w400" />
            </div>
        </div>
        <div class="row mt10"><span class="rl w100">站点标题：</span><div class="fl">
            <input id="txtSiteTitle" class="easyui-textbox txta w400" data-options="multiline:true" /></div>
        </div>
        <div class="row mt10"><span class="rl w100">站点Logo：</span>
            <div class="fl">
                <img id="imgSiteLogo" src="/ccecc/Images/nopic.gif" alt="请选择图片" width="200" height="100" onclick="DlgFiles.DlgPictureSelect('Logo',false,SiteMulti.OnSiteLogoCallBack)" />
            </div>
        </div>
        
        <input type="hidden" id="hId" />
        <span class="clr"></span>
    </div>
    </form>

</body>
</html>
