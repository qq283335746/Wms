<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddShelfMission.aspx.cs" Inherits="TygaSoft.Web.Admin.InStore.AddShelfMission" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑上架任务</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row-fl">
            <span class="rl"><span class="cr">*</span> 入库需求号：</span>
            <div class="fl">
                <input id="cbbOrderReceipt" class="easyui-combobox txt200" data-options="editable:false,required:true,validType:'select'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">计划到货时间：</span>
            <div class="fl">
                <input id="txtAppointmentDate" class="easyui-datebox txt250" data-options="editable:false,required:true" />
            </div>
            <span class="clr"></span>
        </div>
        <div class="row-fl">
            <span class="rl">实际到货时间：</span>
            <div class="fl">
                <input id="txtPlanArrivalTime" class="easyui-datetimebox txt200" data-options="editable:false" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">供应商：</span>
            <div class="fl">
                <input id="cbbSupplier" class="easyui-combobox txt250" data-options="editable:false" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">订单号：</span>
            <div class="fl">
                <input id="txtPurchaseOrderNo" class="easyui-validatebox txt200" data-options="" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">状态：</span>
            <div class="fl">
                <input id="cbbIsDisable" class="easyui-combobox txt250" data-options="editable:false" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">备注：</span>
            <div class="fl">
                <input id="txtRemark" class="txt568" />
            </div>
        </div>
        <span class="clr"></span>

        <input type="hidden" id="hId" />
    </form>

    <script type="text/javascript" src="/wms/Scripts/Admin/InStore/AddShelfMission.js"></script>
    <script type="text/javascript">
        $(function () {
            AddShelfMission.Init();
        })
    </script>
</body>
</html>
