<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddVehicle.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.AddVehicle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>新建/编辑车辆信息</title>
</head>
<body>
    <form id="dlgFm" runat="server">

        <div class="row-fl">
            <span class="rl w134"><span class="cr">*</span> 车牌：</span>
            <div class="fl h24">
                <input id="txtVehicleID" class="easyui-validatebox txt200" data-options="required:true" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w134"><span class="cr">*</span> 车型：</span>
            <div class="fl h24">
                <input id="txtVehicleModel" class="easyui-validatebox txt200" data-options="required:true,validType:'numberAndEnglish'" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w134">违章记录：</span>
            <div class="fl h24">
                <input id="txtOffenceRecord" class="easyui-validatebox txt200" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w134">奖惩记录：</span>
            <div class="fl h24">
                <input id="txtRewardRecord" class="easyui-validatebox txt200" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w134">证照：</span>
            <div class="fl h24">
                <input id="txtLicence" class="easyui-validatebox txt200" />
            </div>
        </div>
        <div class="row-fl">
            <span class="rl w134">司机身份证：</span>
            <div class="fl h24">
                <input id="txtDriverID" class="easyui-validatebox txt200" />
            </div>
        </div>
        <div class="row-fl"><span class="rl w134">证照照片：</span>
            <div class="fl" style="width:210px;">
                <img id="imgLicPic" src="/wms/Images/nopic.gif" alt="上传图片" width="100" height="100" onclick="DlgFiles.DlgPictureSelect('Vehicle',false,AddVehicle.CallBackByLicPic)" />
            </div>
        </div>
        <div class="row-fl"><span class="rl w134">司机身份证照片：</span>
            <div class="fl">
                <img id="imgDriverIDPicture" src="/wms/Images/nopic.gif" alt="上传图片" width="100" height="100" onclick="DlgFiles.DlgPictureSelect('Vehicle',false,AddVehicle.CallBackByDriverIDPicture)" />
            </div>
        </div>
        
        <div class="row-fl"><span class="rl w134">排序：</span><div class="fl">
            <input id="txtSort" class="txt200" /></div>
        </div>
        <div class="row-fl"><span class="rl w134">备注：</span>
            <div class="fl"><textarea id="txtRemark" class="txta" cols="80" rows="3" style="width:548px;"></textarea></div>
        </div>

        <span class="clr"></span>
        <input type="hidden" id="hId" />
    </form>

    <script type="text/javascript" src="/wms/Scripts/DlgFiles.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/AddVehicle.js"></script>
    <script type="text/javascript">
        $(function () {
            AddVehicle.Init();
        })
    </script>
</body>
</html>
