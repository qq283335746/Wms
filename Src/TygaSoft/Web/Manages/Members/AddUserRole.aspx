<%@ Page Title="用户角色分配" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="AddUserRole.aspx.cs" Inherits="TygaSoft.Web.Manages.Members.AddUserRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div class="easyui-panel" title="用户分配角色" data-options="fit:true" style="padding:10px;"> 

<div>将 “<span runat="server" id="lbTitle"></span> ” 添加到角色：</div>
<div class="mt10">
    <asp:Label ID="lbMsg" runat="server"></asp:Label>
    <asp:CheckBoxList ID="cbList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" />
</div>

<div class="mt10 tc">
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save'" onclick="currFun.Save()">保 存</a>
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
            var cbl = $("[id$=cbList]").find(":checkbox:checked");
            if (cbl == undefined || cbl.length == 0) {
                $.messager.alert('错误提示', '没有找到任何角色，请检查', 'error');
                return false;
            }

            $('#form1').submit();
        }
    }
</script>

</asp:Content>
