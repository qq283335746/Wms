<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DlgUsers.aspx.cs" Inherits="TygaSoft.Web.Manages.Sys.DlgUsers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户列表弹出框</title>
</head>
<body>
    <div id="dgDlgUsersToolbar">

    </div>
    <table id="dgDlgUsers" class="easyui-datagrid"
           data-options="rownumbers:true,fit:true, fitColumns:true,singleSelect:false,pagination:true,border:false,toolbar:'#dgDlgUsersToolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'UserName',width:100">用户名</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/Sys/DlgUsers.js"></script>
    <script type="text/javascript">
        $(function () {
            DlgUsers.Init();
        })
    </script>
</body>
</html>
