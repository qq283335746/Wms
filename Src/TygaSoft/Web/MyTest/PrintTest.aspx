<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintTest.aspx.cs" Inherits="TygaSoft.Web.MyTest.PrintTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>打印测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>

    <div class="barcode" style="width:156px;margin:0 auto;">
        <div id="printPanel" runat="server" style="width:60px;margin:0 auto;">
            <a href="/wms/MyTest/PrintTest.aspx?key=print" class="abtn">打印</a>
        </div>
        <div>
            <img id="imgBarcode" src="/wms/Files/Temp/20161114/2dhwp33n.nhq/123456789.emf" />
        </div>
        
    </div>
</body>
</html>
