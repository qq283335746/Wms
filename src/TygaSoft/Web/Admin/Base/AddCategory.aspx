<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCategory.aspx.cs" Inherits="TygaSoft.Web.Admin.Sys.AddCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑分类</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row">
            <span class="rl">上级分类：</span>
            <div class="fl">
                <span id="lbParent"></span>
                <input type="hidden" id="hParentId" runat="server" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">代号：</span>
            <div class="fl">
                <input runat="server" id="txtCode" class="easyui-validatebox mtxt" data-options="required:true" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">名称：</span>
            <div class="fl">
                <input runat="server" id="txtName" class="easyui-validatebox mtxt" data-options="required:true" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">排序：</span>
            <div class="fl">
                <input runat="server" id="txtSort" class="easyui-validatebox mtxt" data-options="validType:'int'" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">备注：</span>
            <div class="fl">
                <input runat="server" id="txtRemark" class="mtxt" />
            </div>
        </div>
        <input type="hidden" runat="server" id="hId" />
    </form>

    <script type="text/javascript">
        $(function () {
            var node = $("#treeCt").tree('find', $("#hParentId").val());
            if (node) {
                $("#lbParent").text(node.text);
            }
        })
    </script>
</body>
</html>
