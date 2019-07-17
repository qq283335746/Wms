<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUserProfile.aspx.cs" Inherits="TygaSoft.Web.Manages.Members.AddUserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户个性化设置</title>
</head>
<body>
    <form id="dlgAddUserProfileFm" runat="server">
    <div>
        
    </div>
    </form>

    <div class="row-fl">
        <span class="rl w134">99站点Logo：</span>
        <div class="fl" style="width:210px;">
            <img id="imgLogo" src="/wms/Images/nopic.gif" alt="站点Logo" width="100" height="100" onclick="DlgFiles.DlgPictureSelect('Vehicle',false,AddVehicle.CallBackByLicPic)" />
        </div>
    </div>

    <script type="text/javascript" src="/wms/Scripts/DlgFiles.js"></script>
    
</body>
</html>
