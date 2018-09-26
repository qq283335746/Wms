<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.AddProduct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建物料</title>
</head>
<body>
    <form id="dlgProductFm" runat="server">
    <div>
        <div class="row-fl">
            <span class="rl">物料代码：</span>
            <div class="fl">
                <input id="txtProductCode" runat="server" class="easyui-validatebox txt170" data-options="required:true" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">物料名称：</span>
            <div class="fl">
                <input id="txtProductName" runat="server" class="easyui-validatebox txt170" data-options="required:true" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">全名：</span>
            <div class="fl">
                <input id="txtFullName" runat="server" class="txt170" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">规格型号：</span>
            <div class="fl">
                <input id="txtSpecs" runat="server" class="txt170" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">价格：</span>
            <div class="fl">
                <input id="txtPrice" runat="server" placeholder="单位：元" class="txt170" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">材质：</span>
            <div class="fl">
                <input id="txtMaterialQuality" runat="server" class="txt170" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">重量：</span>
            <div class="fl">
                <input id="txtWeight" runat="server" placeholder="单位：千克" class="txt170" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">最大库存量：</span>
            <div class="fl">
                <input id="txtMaxStore" runat="server" class="txt170 w90" data-options="validType:'int'" />（单位：个）
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">最小库存量：</span>
            <div class="fl">
                <input id="txtMinStore" runat="server" class="txt170 w90" data-options="validType:'int'" />（单位：个）
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">保质期：</span>
            <div class="fl">
                <input id="txtShelfLife" runat="server" class="easyui-validatebox txt170 w90" />（单位：天）
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">外包装体积：</span>
            <div class="fl">
                <input id="txtOutPackVolume" runat="server" placeholder="单位：立方米" class="txt170" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">外包装重量：</span>
            <div class="fl">
                <input id="txtOutPackWeight" runat="server" placeholder="单位：千克" class="txt170" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">内包装体积：</span>
            <div class="fl">
                <input id="txtInPackVolume" runat="server" placeholder="单位：立方米" class="easyui-validatebox txt170" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">内包装重量：</span>
            <div class="fl">
                <input id="txtInPackWeight" runat="server" placeholder="单位：千克" class="easyui-validatebox txt170" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">外包装数量：</span>
            <div class="fl">
                <input id="txtOutPackQty" runat="server" placeholder="单位：个" class="easyui-validatebox txt170" data-options="validType:'int'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">内包装数量：</span>
            <div class="fl">
                <input id="txtInPackQty" runat="server" placeholder="单位：个" class="easyui-validatebox txt170" data-options="validType:'int'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">供应商：</span>
            <div class="fl">
                <input id="cbbSupplier" runat="server" class="easyui-combobox txt250" data-options="editable:false" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">排序：</span>
            <div class="fl">
                <input id="txtSort" runat="server" class="easyui-validatebox txt170" data-options="validType:'int'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">状态：</span>
            <div class="fl">
                <asp:DropDownList runat="server" ID="ddlIsDisable"></asp:DropDownList>
            </div>
        </div>
        <div class="row fl">
            <span class="rl">备注：</span>
            <div class="fl">
                <input id="txtRemark" runat="server" class="txt458" />
            </div>
        </div>

        <input type="hidden" id="hId" runat="server" />
        <input type="hidden" id="hCategoryId" runat="server" />
    </div>
    </form>

    <script type="text/javascript" src="../Scripts/Admin/Base/AddProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            AddProduct.Init();
        })
    </script>
</body>
</html>
