<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProjectReportPrepare.aspx.cs" Inherits="TygaSoft.Web.Manages.Gzxy.AddProjectReportPrepare" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建/编辑大项目信息</title>
</head>
<body>
    <form id="dlgProjectFm" runat="server">
        <div class="row-fl"><span class="rl"><span class="cr">*</span> 大项目：</span>
            <div class="fl h24">
                <input id="txtProjectName" class="txt200" />
            </div>
        </div>
        <div class="row-fl"><span class="rl">项目来源：</span>
            <div class="fl h24">
                <input id="txtProjectSource" class="txt200" />
            </div>
        </div>
        <div class="row-fl"><span class="rl">客户：</span>
            <div class="fl h24">
                <input id="cbgCustomer" data-options="idField:'Id',textField: 'Named', fitColumns:true,multiple:false,rownumbers:true,onSelect:DlgCustomer.OnSelect,panelWidth:526" style="width:208px;" />
            </div>
        </div>
        <div class="row-fl"><span class="rl"><span class="cr">*</span> 负责人：</span>
            <div class="fl h24">
                <input id="txtCustomerOfficial" class="easyui-validatebox txt200" data-options="required:true" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">联系人：</span>
            <div class="fl h24">
                <input id="txtContactMan" class="txt200" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl">联系方式：</span>
            <div class="fl h24">
                <input id="txtContactPhone" class="txt200" />
            </div>
        </div>
        <div class="row-fl"><span class="rl">产品型号：</span>
            <div class="fl h24">
                <input id="txtSpecsModel" class="txt200" />
            </div>
        </div>
        <div class="row-fl"><span class="rl">预计数量：</span>
            <div class="fl h24">
                <input id="txtPreQty" class="easyui-validatebox txt200" data-options="validType:'int'" />
            </div>
        </div>
        <div class="row-fl"><span class="rl">预计价格：</span>
            <div class="fl h24">
                <input id="txtPreAmount" class="easyui-validatebox txt200" data-options="validType:'int'" />
            </div>
        </div>
         <div class="row-fl">
            <span class="rl">项目状态：</span>
            <div class="fl h24">
                <input id="txtStatus" class="txt200" />
            </div>
        </div>
        <div class="row-fl"><span class="rl">备注：</span>
            <div class="fl">
                <textarea id="txtRemark" rows="3" cols="80" style="width:524px;height:60px;"></textarea>
            </div>
        </div>
        <div class="row-fl"><span class="rl">项目情况介绍：</span>
            <div class="fl">
                <textarea id="txtProjectAbout" rows="50" cols="100" style="width:524px;height:274px;"></textarea>
            </div>
        </div>

        <input type="hidden" id="hId" />
        <span class="clr"></span>
    </form>

    <script type="text/javascript" src="/wms/Scripts/Manages/Gzxy/AddProjectReportPrepare.js"></script>
    <script type="text/javascript">
        $(function () {
            AddProjectReportPrepare.Init();
        })
    </script>
</body>
</html>
