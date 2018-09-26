<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DlgPictureUpload.aspx.cs" Inherits="TygaSoft.Web.Manages.DlgPictureUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传文件</title>
</head>
<body>
    <form id="dlgUploadFm" runat="server" enctype="multipart/form-data">
        <div class="mb10">
            <input id="file1" data-options="prompt:'选择图片',buttonText: '选择文件'" style="width: 500px;" />
        </div>
        <asp:Literal runat="server" ID="ltrMyData"></asp:Literal>
    </form>
</body>
</html>
