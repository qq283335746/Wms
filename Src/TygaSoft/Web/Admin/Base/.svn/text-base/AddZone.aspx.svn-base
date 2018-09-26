<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddZone.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.AddZone" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row mt10">
            <span class="rl">库区代号：</span>
            <div class="fl">
                <input id="txtZoneCode" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" data-options="required:true" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">库区名称：</span>
            <div class="fl">
                <input id="txtZoneName" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" data-options="required:true" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">面积：</span>
            <div class="fl">
                <input id="txtSquare" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">备注：</span>
            <div class="fl">
                <input id="txtDescr" runat="server" clientidmode="Static" class="easyui-validatebox mtxt" />
            </div>
        </div>

        <input type="hidden" id="hId" runat="server" clientidmode="Static" />

        <script type="text/javascript" src="/wms/Scripts/Admin/Base/AddZone.js"></script>
        <script type="text/javascript">
            $(function () {
                AddZone.Init();
            })
        </script>
    </form>
</body>
</html>
