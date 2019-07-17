<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="TygaSoft.Web.MyTest.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

        <table style="width:400px;">
            <tr><td></td><td></td><td rowspan="5"></td></tr>
            <tr><td></td><td></td></tr>
            <tr><td></td><td></td></tr>
            <tr><td></td><td></td></tr>
            <tr><td></td><td></td></tr>
        </table>
        </div>
    <div>
        <asp:TreeView ID="TreeView1" runat="server">
            <Nodes>
                <asp:TreeNode Text="新建节点" Value="新建节点">
                    <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="新建节点" Value="新建节点">
                    <asp:TreeNode Text="新建节点" Value="新建节点">
                        <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                    </asp:TreeNode>
                </asp:TreeNode>
                <asp:TreeNode Text="新建节点" Value="新建节点">
                    <asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
                </asp:TreeNode>
            </Nodes>
        </asp:TreeView>
        <asp:Repeater runat="server" ID="rpData" OnItemCommand="rpData_ItemCommand">
            <ItemTemplate>
                <div>
                    <input type="checkbox" runat="server" id="cbItem" name="cbItem" value='<%#Eval("Name") %>' /><%#Eval("Name") %>
                </div>
               
            </ItemTemplate>
            <FooterTemplate>
                <asp:LinkButton runat="server" ID="lbtnDel" CommandName="Del">删除</asp:LinkButton>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
