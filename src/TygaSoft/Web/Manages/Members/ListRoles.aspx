<%@ Page Title="角色管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListRoles.aspx.cs" Inherits="TygaSoft.Web.Manages.Members.ListRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

<div id="toolbar" style="padding:5px;">
    <a class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="currFun.Add()">新建</a>
    <a id="lbtnEdit" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="currFun.Edit()">编辑</a>
    <a id="lbtnDel" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="currFun.Del()">删除</a>
    <a id="lbtnRoleMenu" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="currFun.RoleMenu()">角色授权</a>
</div>

<table id="bindT" class="easyui-datagrid" title="角色列表" data-options="rownumbers:true,fit:true,singleSelect:true,fitColumns:true,toolbar:'#toolbar',onSelect:currFun.OnSelect">
<thead>
    <tr>
        <th data-options="field:'f0',checkbox:true"></th>
        <th data-options="field:'f1',width:200">角色名称</th>
        <th data-options="field:'f2',width:150">添加/移除用户</th>
    </tr>
</thead>
<tbody>
<asp:Repeater ID="rpData" runat="server">
<ItemTemplate>
    <tr>
        <td><%#Eval("RoleId")%> </td>
        <td><%#Eval("RoleName") %></td>
        <td>
            <a href='amember.html?rName=<%#HttpUtility.UrlEncode(Eval("RoleName").ToString()) %>' class="abtn">管理</a>
        </td>
    </tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>

<div id="dlg" class="easyui-dialog" title="新建角色" data-options="resizable:true,modal:true,closed:true,
buttons:[{
			text:'保存',iconCls:'icon-ok',
			handler:function(){currFun.Save();}
		},{
			text:'取消',iconCls:'icon-cancel',
			handler:function(){$('#dlg').dialog('close');}
		}]" 
style="width:380px;height:130px; padding:10px;">
    <div class="row">
        <span class="fl rl"> <b class="cr">*</b>角色名称：</span>
        <div class="fl">
            <input type="text" id="txtRolename"  maxlength="50" tabindex="1" class="easyui-validatebox txt" data-options="required:true" />
        </div>
        <span class="clr"></span>
    </div>
</div>

<input type="hidden" id="hId" value="" />
    
<script type="text/javascript">
    var currFun = {
        OnSelect: function (index, row) {
            if (row.f1 == 'Administrators' || row.f1 == 'System' || row.f1 == 'Users' || row.f1 == 'Guest') {
                $('#lbtnEdit').linkbutton('disable');
                $('#lbtnDel').linkbutton('disable');
                if (row.f1 == 'Administrators') $('#lbtnRoleMenu').linkbutton('disable');
                else $('#lbtnRoleMenu').linkbutton('enable');
            }
            else {
                $('#lbtnEdit').linkbutton('enable');
                $('#lbtnDel').linkbutton('enable');
                $('#lbtnRoleMenu').linkbutton('enable');
            }
        },
        Add: function () {
            $("#hId").val("");
            $("#txtRolename").val("");
            currFun.Url = Common.AppName + "/Services/SecurityService.svc/SaveRole";
            $('#dlg').dialog({ title: '新建角色' });
            $('#dlg').dialog('open');
        },
        Edit: function () {
            var cbl = $('#bindT').datagrid("getSelections");
            if (!cbl || (cbl.length != 1)) {
                $.messager.alert('错误提示', '请选择一行且仅一行数据进行编辑', 'error');
                return false;
            }

            $('#dlg').dialog({ title: '编辑角色' });
            currFun.Url = Common.AppName + "/Services/SecurityService.svc/SaveRole";
            $("#hId").val(cbl[0].f0);
            $("#txtRolename").val(cbl[0].f1);
            $('#dlg').dialog('open');

            return false;
        },
        Del: function () {
            var cbl = $('#bindT').datagrid("getSelections");
            if (!cbl || cbl.length == 0) {
                $.messager.alert('错误提示', '请至少选择一行数据再进行操作', 'error');
                return false;
            }
            $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
                if (r) {
                    var itemsAppend = "";
                    for (var i = 0; i < cbl.length; i++) {
                        if (i > 0) itemsAppend += ",";
                        itemsAppend += cbl[i].f1;
                    }

                    $.ajax({
                        url: Common.AppName + "/Services/SecurityService.svc/DelRole",
                        type: "post",
                        data: '{"itemAppend":"' + itemsAppend + '"}',
                        contentType: "application/json; charset=utf-8",
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

                            currFun.DelRow();
                            jeasyuiFun.show("温馨提示", "保存成功！");
                            $('#dlg').dialog('close');
                        }
                    });
                }
            });
        },
        Save: function () {
            var isValid = $('#form1').form('validate');
            if (!isValid) return false;

            var roleId = $.trim($("#hId").val());
            var roleName = $("#txtRolename").val();

            var sData = '{"RoleId":"' + roleId + '","RoleName":"' + roleName + '"}';

            $.ajax({
                url: currFun.Url,
                type: "post",
                data: '{"model":' + sData + '}',
                contentType: "application/json; charset=utf-8",
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

                    if (roleId.length > 0) {
                        $('#bindT').datagrid('updateRow', {
                            index: currFun.GetRowIndex(),
                            row: {
                                f1: roleName,
                                f2: "<a href=\"amember.html?rName=" + roleName + "\">分配用戶</a>"
                            }
                        })
                    }
                    else {
                        window.location = window.location.href;
                    }

                    jeasyuiFun.show("温馨提示", "保存成功！");
                    $('#dlg').dialog('close');
                }
            });
        },
        GetRowIndex: function () {
            var dg = $('#bindT');
            var row = dg.datagrid("getSelected");
            if (!row || row.length == 0) {
                return -1;
            }

            return dg.datagrid('getRowIndex', row);
        },
        DelRow: function () {
            var dg = $('#bindT');
            var cbl = dg.datagrid("getSelections");
            if (!cbl || cbl.length == 0) {
                $.messager.alert('错误提示', '请至少选择一行数据再进行操作', 'error');
                return false;
            }

            for (var i = 0; i < cbl.length; i++) {
                var rowIndex = dg.datagrid('getRowIndex', cbl[i]);
                dg.datagrid('deleteRow', rowIndex);
            }
        },
        RoleMenu: function () {
            var rows = $('#bindT').datagrid("getSelections");
            if (!rows || (rows.length != 1)) {
                $.messager.alert('错误提示', '请选择一行且仅一行数据进行编辑', 'error');
                return false;
            }
            window.location = "../u/rolemenu.html?allowRole=" + rows[0].f1 + "";
        }
    }
</script>
</asp:Content>
