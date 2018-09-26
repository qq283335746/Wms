<%@ Page Title="授权" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListRoleMenu.aspx.cs" Inherits="TygaSoft.Web.Manages.Sys.ListRoleMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="tgMenuToolbar">
      <%--  <input id="cbbSelect" />--%>
        <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'">全选/全不选</a>
        <div id="mmExcel" style="width:150px;">
            <div onclick="ListRoleMenu.OnSelectOption(1)">全选</div>
            <div onclick="ListRoleMenu.OnSelectOption(2)">全不选</div>
            <div onclick="ListRoleMenu.OnSelectOption(3)">反选</div>
        </div>
        <a name="abtnSave" class="easyui-linkbutton" data-options="iconCls:'icon-save',plain:true">提 交</a><br />
        <span runat="server" id="lbName" style="color:#ff0000;"></span>
    </div>
    <table id="tgMenu" class="easyui-treegrid" data-options="idField: 'Id',treeField: 'Title',rownumbers: true,fit:true,lines:true,fitColumns:true,toolbar:'#tgMenuToolbar',onClickCell:ListRoleMenu.OnClickCellByTgMenu">
		<thead>
			<tr>
                <th data-options="field:'Id',hidden:true"></th>
				<th data-options="field:'Title',width:250">菜单导航</th>
				<th data-options="field:'IsView',width:80,formatter:ListRoleMenu.FormatterCheckbox">访问</th>
                <th data-options="field:'IsAdd',width:80,formatter:ListRoleMenu.FormatterCheckbox">新增</th>
                <th data-options="field:'IsEdit',width:80,formatter:ListRoleMenu.FormatterCheckbox">编辑</th>
                <th data-options="field:'IsDel',width:80,formatter:ListRoleMenu.FormatterCheckbox">删除</th>
			</tr>
		</thead>
	</table>

    <input type="hidden" runat="server" id="hAllowRole" ClientIDMode="Static" />
    <input type="hidden" runat="server" id="hDenyUser" ClientIDMode="Static" />

    <script type="text/javascript" src="/wms/Scripts/Manages/Sys/ListRoleMenu.js"></script>
    <script type="text/javascript">
        $(function () {
            try {
                ListRoleMenu.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }

        })
    </script>

</asp:Content>
