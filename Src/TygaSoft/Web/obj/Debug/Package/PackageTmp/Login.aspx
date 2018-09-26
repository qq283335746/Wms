<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TygaSoft.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录</title>
    <link href="~/Styles/Main.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/plugins/Jeasyui15/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/plugins/Jeasyui15/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/ui-v1.0.1.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Login.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/plugins/Jeasyui15/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/Plugins/Jeasyui15/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="Scripts/Plugins/Jeasyui15/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="Scripts/JeasyuiHelper.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="w login">
        <div class="main">
            <div class="loginl">
	            <img src="Images/loginl.jpg" alt="" />
            </div>
            <div class="loginr">
                
                <h1>登录</h1>
                <div id="loginInfo" class="content">
                    <div class="control-group">
			            <div class="warning clearfix" style="display:none;">
				            <span class="warning-ico"></span>
				            <span class="warning-con">用户名不能为空</span>
			            </div>
		            </div>
                    <div class="control-group">
                        <label class="placeholder" style="position: absolute; left: 5px; top: 0px; color: rgb(119, 119, 119); height: 40px; line-height: 40px; display:block;">用户名</label>
                        <input type="text" id="txtUserName" name="txtUserName" tabindex="1" maxlength="50" class="ui-input-h40" placeholder="" autocomplete="off" value="" />
                    </div>
                    <div class="control-group">
                        <label class="placeholder" style="position:absolute;left:5px;top:0px;color:#777777;height:40px;line-height:40px;display: block;">密码</label>
                        <input type="password" id="txtPsw" name="txtPsw" tabindex="2" maxlength="30" class="ui-input-h40" placeholder="" autocomplete="off" />
                    </div>
                    <div class="control-group">
                        <label class="placeholder" style="position:absolute;left:5px;top:0px;color:#777777;height:40px;line-height:40px;display: block;">验证码</label>
                        <input type="text" id="txtVc" name="txtVc" maxlength="4" tabindex="3" class="ui-input-h40" placeholder="" autocomplete="off" style="width:100px;" />
                        <img border="0" id="imgCode" src="Handlers/ValidateCode.ashx?vcType=login" alt="看不清，单击换一张" onclick="this.src='Handlers/ValidateCode.ashx?vcType=login&abc='+Math.random()" style="vertical-align:middle;line-height: 38px;height: 38px; width:100px; margin-bottom:3px;" />
                    </div>
                    <div class="control-group login-d">
		                <label class="label">&nbsp;</label>
		                <div class="fl">
		                <input type="checkbox" class="fl" id="cbRememberMe" name="cbRememberMe" checked="checked" value="1" onclick="if (this.checked == true) { this.value = '1'; } else { this.value = '0'; }" /><label class="fl" for="remember_me">记住用户名</label>			
		                </div>	

	                </div>
                    <div class="control-group">
                        <input type="submit" class="ui-submit-blue" id="btn-submit" value="登录" />
                    </div>
                </div>
                
            </div>
        </div>
    </div>
    
    <span class="clr"></span>
    <div class="footer">
        <div class="footerMain">
            <span>©  2015-2016</span>
        </div>
    </div>

    <asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

    </form>

    <script type="text/javascript" src="Scripts/Login.js"></script>
    <script type="text/javascript">
        $(function () {
            Login.Init();
        })
    </script>
</body>
</html>
