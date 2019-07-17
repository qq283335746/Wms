<%@ Page Title="智能仓储配送平台" Language="C#" MasterPageFile="~/Masters/Blank.Master" AutoEventWireup="true" CodeBehind="UserCenter.aspx.cs" Inherits="TygaSoft.Web.Manages.UserCenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <%--<div style="text-align:center;margin-top:30px;color:#ff0000;">
        <span runat="server" id="lbWelcome"></span>
    </div>

     <div style="text-align:center;margin-top:30px">
        <a class="easyui-linkbutton" style="width:100%;height:40px" onclick="viewApp('100001')"><span style="font-size:16px">进入工业企业版</span></a>
     </div>
    <div style="text-align:center;margin-top:30px">
        <a class="easyui-linkbutton" style="width:100%;height:40px" onclick="viewApp('100002')"><span style="font-size:16px">进入专业物流版</span></a>
     </div>
    <script type="text/javascript">
        function viewApp(appId) {
            var postData = { "ReqName": "SaveApp", "AppId": "" + appId + "" };
            Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                window.location = "t.html";
            })
        }
    </script>--%>

</asp:Content>
