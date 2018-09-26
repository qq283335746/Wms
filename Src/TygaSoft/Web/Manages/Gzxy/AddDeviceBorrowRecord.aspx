<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDeviceBorrowRecord.aspx.cs" Inherits="TygaSoft.Web.Manages.Gzxy.AddDeviceBorrowRecord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑借样记录</title>
</head>
<body>
    <form id="dlgFm" runat="server">
    <div>
        <div class="row-fl"><span class="rl"><span class="cr">*</span> 日期：</span><div class="fl"><input tabindex="1" id="txtRecordDate" class="easyui-datebox txt200" data-options="required:true" style="height:20px;" /></div></div> 
        <div class="row-fl"><span class="rl">客户：</span><div class="fl"><input tabindex="2" id="txtCustomer" class="txt200" /></div></div> 
        <div class="row-fl"><span class="rl">序列号：</span><div class="fl"><input tabindex="3" id="txtSerialNumber" class="txt200" /></div></div> 
        <div class="row-fl"><span class="rl">客户联系人：</span><div class="fl"><input tabindex="4" id ="txtCustomerContact" class="txt200" /></div></div>
        <div class="row-fl"><span class="rl">型号（配置）：</span><div class="fl"><input tabindex="5" id="txtDeviceModel" class="easyui-validatebox txt w517" /></div></div> 
        <div class="row-fl"><span class="rl">配件明细：</span><div class="fl"><input tabindex="6" id="txtDevicePart" class="easyui-validatebox txt w517" /></div></div> 
        <div class="row-fl"><span class="rl">项目信息：</span><div class="fl"><input tabindex="7" id="txtProjectAbout" class="easyui-validatebox txt w517" /></div></div> 
        <div class="row-fl"><span class="rl">配件状况：</span>
            <div id="panelPartStatus" class="fl" style="width:207px;">
                <input tabindex="8" id="cbbPartStatus" data-options="editable:false" style="height:20px;width:80px;" />
            </div>
        </div> 
        <div class="row-fl"><div class="fl" style="margin-left:10px; display:none;"><input tabindex="9" id="txtPartStatus" class="txt200" style="width:427px;" /></div></div> 
        <span class="clr"></span>
        <div class="row-fl"><span class="rl">归还日期：</span><div class="fl"><input tabindex="10" id="txtBackDate" class="easyui-datebox txt200" style="height:20px;" /></div></div> 
        <div class="row-fl"><span class="rl">是否归还：</span><div class="fl"><input tabindex="11" id="cbbIsBack" class="txt200" data-options="editable:false" style="height:20px;" /></div></div> 
        <div class="row-fl"><span class="rl">业务员：</span><div class="fl"><input tabindex="12" id="txtSaleMan" class="txt200" /></div></div> 
        <div class="row-fl"><span class="rl">寄出单号：</span><div class="fl"><input tabindex="13" id ="txtSendOrderCode" class="txt200" /></div></div> 
        <div class="row-fl"><span class="rl">登记人：</span><div class="fl"><input tabindex="14" id="txtRegister" class="txt200" /></div></div> 
        <span class="clr"></span>
        <div class="row"><span class="rl">备注：</span>
            <div class="fl">
                <textarea tabindex="15" id="txtRemark" class="txta" style="width:516px;"></textarea>
            </div>
        </div> 
        
        <input type="hidden" id="hId" />
        <span class="clr"></span>
    </div>
    </form>
</body>
</html>
