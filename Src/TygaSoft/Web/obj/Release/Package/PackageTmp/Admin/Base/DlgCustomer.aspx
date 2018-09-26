<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DlgCustomer.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.DlgCustomer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>客户列表弹出框</title>
</head>
<body>
    <form id="dlgCustomerFm" runat="server">
    <div>
        <table id="dgCustomer" class="easyui-datagrid"
            data-options="fitColumns:true,singleSelect:true,pagination:true,rownumbers:true,striped:true">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'Coded',width:80">客户代码</th>
                <th data-options="field:'Named',width:300">客户名称</th>
                <th data-options="field:'ShortName',width:100,align:'right'">简称</th>
            </tr>
        </thead>
    </table>
    </div>
    </form>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgCustomer.js"></script>
    <script type="text/javascript">
        $(function () {
            DlgCustomer.Init();
        })
    </script>
</body>
</html>
