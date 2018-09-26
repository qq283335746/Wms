<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DlgUpload.aspx.cs" Inherits="TygaSoft.Web.Manages.DlgUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>上传文件</title>
</head>
<body>
    <form id="dlgUploadFm" runat="server" enctype="multipart/form-data">
        <div class="mb10">
            <input id="file1" data-options="prompt:'请选择文件',buttonText: '选择文件',multiple:true" style="width: 500px;" />
        </div>
        <asp:Literal runat="server" ID="ltrMyData"></asp:Literal>
    </form>
</body>
</html>
