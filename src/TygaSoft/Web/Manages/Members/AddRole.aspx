<%@ Page Title="添加角色" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="AddRole.aspx.cs" Inherits="TygaSoft.Web.Manages.Members.AddRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="box">
  <div class="h pd5">填写信息</div>
  <div class="c pd10">
      <div class="row">
        <span class="fl rl"> <b class="cr">*</b>角色名称：</span>
        <div class="fl">
            <input type="text" id="txtRolename" runat="server"  maxlength="50" tabindex="1" class="easyui-validatebox txt" data-options="required:true" />
        </div>
        <span class="clr"></span>
      </div>
  </div>
</div>

<script type="text/javascript" src="/Scripts/Jeasyui.js"></script>

<script type="text/javascript">
    $(function () {

        //保存
        $("#btnSave").click(function () {
            OnSave();
        })
        $("#abtnSave").click(function () {
            OnSave();
        })
    })

    function historyGo() {
        history.go(-1);
    }

    //保存事件
    function OnSave() {
        var isValid = $('#form1').form('validate');
        if (!isValid) return false;

        $('#form1').submit();
    }
</script>

</asp:Content>
