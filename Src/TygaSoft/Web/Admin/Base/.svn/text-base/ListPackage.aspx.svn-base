<%@ Page Title="包装列表" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListPackage.aspx.cs" Inherits="TygaSoft.Web.Admin.Base.ListPackage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="toolbar" style="padding:5px; display:none;">
        <a id="abtnAdd" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'">新建</a>
        <a id="abtnEdit" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'">编辑</a>
        <a id="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">删除</a>
    </div>

    <table id="dgT" class="easyui-datagrid" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,striped:true,toolbar:'#toolbar'">
        <thead>
            <tr>
                <th data-options="field:'f0',checkbox:true"></th>
                <th data-options="field:'f1',width:100">货主</th>
                <th data-options="field:'f2',width:100">货品</th>
                <th data-options="field:'f3'">包装编码</th>
                <th data-options="field:'f4',width:100">说明</th>
                <th data-options="field:'f5'">件</th>
                <th data-options="field:'f6'">内包装</th>
                <th data-options="field:'f7'">箱</th>
                <th data-options="field:'f8'">托盘</th>
                
            </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="rpData" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("Id")%></td>
                    <td><%#Eval("CustomerCode")%></td>
                    <td><%#Eval("ProductCode")%></td>
                    <td><%#Eval("PackageCode")%></td>
                    <td><%#Eval("Remark")%></td>
                    <td><%#Eval("TotalPiece")%></td>
                    <td><%#Eval("TotalInsidePackage")%></td>
                    <td><%#Eval("TotalBox")%></td>
                    <td><%#Eval("TotalTray")%></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </tbody>
    </table>

    <asp:Literal runat="server" ID="ltrMyData"></asp:Literal>

    <div id="dlgAddPackage"></div>

    <script type="text/javascript" src="../Scripts/Admin/Base/ListPackage.js"></script>

    <script type="text/javascript">
        var sPageIndex = 0;
        var sPageSize = 0;
        var sTotalRecord = 0;
        var sQueryStr = "";

        $(function () {
            var pageData = ListPackage.GetMyData("myDataForPage");
            sPageIndex = parseInt(pageData.PageIndex);
            sPageSize = parseInt(pageData.PageSize);
            sTotalRecord = parseInt(pageData.TotalRecord);
            sQueryStr = pageData.QueryStr.replace(/&amp;/g, '&');

            ListPackage.Init();
        })
    </script>

</asp:Content>
