<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DlgPictureSelect.aspx.cs" Inherits="TygaSoft.Web.Manages.DlgPictureSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择图片</title>
</head>
<body>
    <form id="dlgPictureFm" runat="server">
        <div class="easyui-layout" data-options="border:false,width:760,height:$(window).height()*0.9">
            <div id="pictureBox" data-options="region:'center',title:'',border:false">
                <asp:Repeater ID="rpData" runat="server">
                    <ItemTemplate>
                        <div class="row_col w110">
                            <img src='<%#string.Format("{0}{1}/PC/{1}_1{2}",Eval("FileDirectory"),Eval("RandomFolder"),Eval("FileExtension")) %>' alt="图片" width="100" height="90" code='<%#Eval("Id")%>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div data-options="region:'south',title:'',border:false" style="height:30px;">
                <div id="easyPager"></div>
            </div>
        </div>
        <asp:Literal runat="server" ID="ltrMyData"></asp:Literal>
    </form>
</body>
</html>
