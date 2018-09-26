<%@ Page Title="包装" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="TabPackage.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.TabPackage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="easyui-panel" data-options="border:false" style="padding:5px;">
        <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'">新建</a>
        <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'">保存</a>
        <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">删除</a>
    </div>

    <div id="tabPackage" class="easyui-tabs" data-options="onSelect:TabPackage.OnTabSelect" style="height:auto;"></div>

    <script type="text/javascript" src="../Scripts/Admin/Base/TabPackage.js"></script>
    <script type="text/javascript">
        $(function () {
            TabPackage.Init();
        })
    </script>

</asp:Content>
