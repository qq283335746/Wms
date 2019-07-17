<%@ Page Title="仓储配送一体化平台" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TygaSoft.Web.Manages.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/wms/Scripts/Plugins/HighCharts/highcharts.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Plugins/HighCharts/highcharts-3d.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server" ClientIDMode="Static">
    <div class="atob fl"></div>
    <div class="process">
        <a href="/wms/a/tinstore.html?orderType=1"><div class="pretake">ASN预收货</div></a>
        <a href="/wms/a/tinstore.html"><div class="receipt">收货</div></a>
        <a href="/wms/a/ytinstore.html"><div class="shelve">上架</div></a>
        <a href="/wms/a/tsend.html"><div class="send">出库</div></a>
        <a href="/wms/a/asend.html"><div class="pick">拣货</div></a>
        <a href="/wms/a/tpandian.html"><div class="inventory">盘点</div></a>
        <a href="/wms/a/tstore.html"><div class="stock">库存查询</div></a>
    </div>
    <span class="clr"></span>
    <div id="container" runat="server"></div>

    <script type="text/javascript" src="/wms/Scripts/Admin/Default.js"></script>
    <script type="text/javascript">
        $(function () {
            Default.Init();
        })
    </script>

</asp:Content>
