<%@ Page Title="修改密码" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="UpdatePsw.aspx.cs" Inherits="TygaSoft.Web.Manages.Members.UpdatePsw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="row mt10">
        <span class="rl">当前密码：</span>
        <div class="fl">
            <input type="password" id="txtOldPsw" class="easyui-validatebox txt" data-options="required:true,validType:'psw'" />
        </div>
    </div>

    <div class="row mt10">
        <span class="rl">新密码：</span>
        <div class="fl">
            <input type="password" id="txtPsw" class="easyui-validatebox txt" data-options="required:true,validType:'psw'" />
        </div>
    </div>

    <div class="row mt10">
        <span class="rl">确认密码：</span>
        <div class="fl">
            <input type="password" id="txtCfmPsw" class="easyui-validatebox txt" data-options="required:true" validType="cfmPsw['#txtPsw']" />
        </div>
    </div>

    <div class="row mt10">
        <span class="rl">&nbsp;</span>
        <div class="fl">
            <a class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="currFun.Save()">保 存</a>
        </div>
    </div>

<script type="text/javascript">
    $(function () {
        $(document).bind("keydown", function (e) {
            var key = e.which;
            if (key == 13) {
                currFun.Save();
            }
        })
    })

    var currFun = {
        Save: function () {
            var isValid = $('#form1').form('validate');
            if (!isValid) return false;
            var oldPsw = $.trim($('#txtOldPsw').val());
            var psw = $.trim($('#txtPsw').val());
            var sData = '{"oldPassword": "' + oldPsw + '", "password": "' + psw + '" }';

            $.ajax({
                url: Common.AppName + "/Services/SecurityService.svc/ChangePassword",
                type: "post",
                contentType: "application/json; charset=utf-8",
                data: sData,
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
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        $('#txtOldPsw').val('');
                        $('#txtPsw').val('');
                    }, 1000);
                }
            });
        }
    }
</script>

</asp:Content>
