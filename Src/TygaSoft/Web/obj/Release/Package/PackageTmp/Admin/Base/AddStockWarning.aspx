<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStockWarning.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.AddStockWarning" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新增/编辑库存预警信息</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span> 库存预警编号：</span>
            <div class="fl">
                <input id="txtCoded" class="easyui-validatebox txt170" data-options="required:true" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span> 库区：</span>
            <div class="fl">
                <input id="cbbZone" class="txt170" data-options="required:true,editable:false,validType:'select',onChange:AddStockWarning.OnZoneChange" style="height:22px;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span> 库区性质：</span>
            <div class="fl">
                <input id="cbbZoneProperty" class="txt170" data-options="required:true,editable:false" style="height:22px;" />
            </div>
            <span class="clr"></span>
        </div>
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span> 库位：</span>
            <div class="fl">
                <input id="cbbStockLocation" class="txt170" data-options="required:true,editable:false,validType:'select'" style="height:22px;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span>库位性质：</span>
            <div class="fl">
                <input id="cbbStockLocationProperty" class="txt170" data-options="required:true,editable:false,validType:'select'" style="height:22px;" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span>库存金额：</span>
            <div class="fl">
                <input id="txtStockAmount" class="easyui-validatebox txt170" data-options="required:true,validType:'price'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span>超期天数：</span>
            <div class="fl">
                <input id="txtOverdueDay" class="easyui-validatebox txt170" data-options="required:true,validType:'int'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span>最小库存量：</span>
            <div class="fl">
                <input id="txtMinQty" class="easyui-validatebox txt170" data-options="required:true,validType:'int'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span>最大库存量：</span>
            <div class="fl">
                <input id="txtMaxQty" class="easyui-validatebox txt170" data-options="required:true,validType:'int'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">排序：</span>
            <div class="fl">
                <input id="txtSort" class="easyui-validatebox txt170" data-options="validType:'int'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">启/禁用：</span>
            <div class="fl">
                <input id="cbbIsDisable" class="txt170" data-options="editable:false" style="height:22px;" />
            </div>
            <span class="clr"></span>
        </div>
        <div class="row-fl">
            <span class="rl">备注：</span>
            <div class="fl">
                <input id="txtRemark" class="txt458" />
            </div>
        </div>
        <span class="clr"></span>

        <input type="hidden" id="hId" />
    </form>

    <script type="text/javascript" src="/wms/Scripts/Admin/Base/AddStockWarning.js"></script>
    <script type="text/javascript">
        $(function () {
            AddStockWarning.Init();
        })
    </script>
</body>
</html>
