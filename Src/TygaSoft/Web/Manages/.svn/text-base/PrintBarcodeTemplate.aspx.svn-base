<%@ Page Title="打印条码标签" Language="C#" MasterPageFile="~/Masters/Blank.Master" AutoEventWireup="true" CodeBehind="PrintBarcodeTemplate.aspx.cs" Inherits="TygaSoft.Web.Manages.PrintBarcodeTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="printBtns" style="text-align:center;padding:10px;">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-print'" onclick="PrintBarcodeTemplate.OnPrint()"><span>打印</span></a>
    </div>
    <asp:Literal ID="ltrHtml" runat="server" />

    <script type="text/javascript" src="/wms/Scripts/Manages/PrintBarcodeTemplate.js"></script>
    <script type="text/javascript">
        $(function () {
            PrintBarcodeTemplate.Init();
        })
    </script>

</asp:Content>
