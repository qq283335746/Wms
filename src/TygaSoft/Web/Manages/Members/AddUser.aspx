<%@ Page Title="新建/编辑用户" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="TygaSoft.Web.Manages.Members.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="fl mr10">
        <div class="easyui-panel" title="创建用户" style="padding:10px;width:410px;"> 
            <div class="row">
                <span class="fl rl"><b class="cr">*</b>用户名：</span>
                <div class="fl">
                    <input type="text" id="txtUserName" maxlength="50"  tabindex="1" class="easyui-validatebox txt" data-options="required:true" />
                </div>
                <span class="clr"></span>
            </div>
            <div class="row mt10">
                <span class="fl rl"><b class="cr">*</b>设置密码：</span>
                <div class="fl">
                    <input type="password" id="txtPsw" maxlength="50" tabindex="2" class="easyui-validatebox txt" data-options="required:true" />
                </div>
                <span class="clr"></span>
            </div>
            <div class="row mt10">
                <span class="fl rl"><b class="cr">*</b>确认密码：</span>
                <div class="fl">
                    <input type="password" id="txtCfmPsw" maxlength="30" tabindex="3" class="easyui-validatebox txt" data-options="required:true" validType="cfmPsw['#txtPsw']" />
                </div>
                <span class="clr"></span>
            </div>
            <div class="row mt10">
                <span class="fl rl">&nbsp;</span>
                <div class="fl">
                    <input type="checkbox" id="cbIsApproved" name="cbIsApproved" checked="checked" /><label for="cbIsApproved">活动用户</label>
                </div>
                <span class="clr"></span>
            </div>
            <div class="row mt10">
                <span class="fl rl">&nbsp;</span>
                <div class="fl">
                    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="currFun.Save()">保 存</a>
                </div>
                <span class="clr"></span>
            </div>
        </div>
    </div>
    <div class="fl">
        <div id="toolbarRole" style="padding:5px;">
            为此用户选择角色: 
        </div>
        <table id="dgRole" class="easyui-datagrid" title="角色" data-options="rownumbers:true,singleSelect:true,toolbar:'#toolbarRole'", style="width:300px;">
            <thead>
            <tr>
                <th data-options="hidden:true,field:'IsInRole'"></th>
                <th data-options="field:'RoleName',formatter:currFun.RoleFormatter"></th>
            </tr>
        </thead>
        </table>
    </div>

    <asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

 <script type="text/javascript">
     $(function () {
         $(document).bind("keydown", function (e) {
             var key = e.which;
             if (key == 13) {
                 currFun.Save();
             }
         })
         //输入用户名鼠标离开事件
         $("#txtUserName").blur(function () {
             $.ajax({
                 url: Common.AppName + "/Services/SecurityService.svc/CheckUserName",
                 type: "post",
                 data: '{"userName":"' + $("#txtUserName").val() + '"}',
                 contentType: "application/json; charset=utf-8",
                 success: function (result) {

                     if (result.ResCode != 1000) {
                         if (result.Data > 0) {
                             $("#txtUserName").val("");
                             $.messager.alert('系统提示', "已存在相同用户，请换一个再重试", 'error');
                         }
                     }
                 }
             });
         })

         currFun.Init();
     })

     var currFun = {
         Init: function () {
             currFun.BindRole();
         },
         GetMyData: function (obj) {
             var myData = obj.html();
             return eval("(" + myData + ")");
         },
         BindRole: function () {
             $("#dgRole").datagrid('loadData', currFun.GetMyData($("#myDataForRole")));
         },
         RoleFormatter: function (value, row, index) {
             var isInRole = row.IsInRole == "True";
             if (isInRole) {
                 return "<input type=\"checkbox\" checked=\"checked\" value=\"" + value + "\" onclick=\"currFun.CbIsInRole(this)\" />" + value;
             }
             return "<input type=\"checkbox\" value=\"" + value + "\" onclick=\"currFun.CbIsInRole(this)\" />" + value;
         },
         CbIsInRole: function (h) {
             try {
                 var $_obj = $(h);
                 var dg = $("#dgRole");
                 var rows = dg.datagrid('getRows');
                 for (var i = 0; i < rows.length; i++) {
                     if (rows[i].RoleName == $_obj.val()) {
                         var rowIndex = dg.datagrid('getRowIndex', rows[i]);
                         var isInRoleText = "";
                         var isInRole = $_obj.is(":checked");
                         if (isInRole) {
                             isInRoleText = "True";
                         }
                         else {
                             isInRoleText = "False";
                         }

                         dg.datagrid('updateRow', {
                             index: rowIndex,
                             row: {
                                 IsInRole: isInRoleText
                             }
                         });
                     }
                 }
             }
             catch (e) {
                 $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
             }
         },
         Save: function () {
             var isValid = $('#form1').form('validate');
             if (!isValid) return false;

             try {
                 var userName = $.trim($("#txtUserName").val());
                 var psw = $.trim($("#txtPsw").val());
                 var cfmPsw = $.trim($("#txtCfmPsw").val());
                 var isApproved = $("#cbIsApproved").is(":checked");
                 var roleAppend = "";

                 var dg = $("#dgRole");
                 var rows = dg.datagrid('getRows');
                 if (rows) {
                     for (var i = 0; i < rows.length; i++) {
                         if (rows[i].IsInRole == "True") {
                             roleAppend += rows[i].RoleName + ",";
                         }
                     }
                 }

                 var sData = '{"UserName":"' + userName + '","Password":"' + psw + '","CfmPsw":"' + cfmPsw + '","IsApproved":"' + isApproved + '","RoleName":"' + roleAppend + '"}';

                 $.ajax({
                     url: Common.AppName + "/Services/SecurityService.svc/SaveUser",
                     type: "post",
                     contentType: "application/json; charset=utf-8",
                     data: '{"model":' + sData + '}',
                     beforeSend: function () {
                         $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                     },
                     complete: function () {
                         $.messager.progress('close');
                     },
                     success: function (result) {

                         if (result.ResCode != 1000) {
                             $.messager.alert('系统提示', result.Msg, 'info');
                             return false;
                         }

                         jeasyuiFun.show("温馨提示", "保存成功！");
                     }
                 });
             }
             catch (e) {
                 $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
             }
         }
     }
</script>

</asp:Content>
