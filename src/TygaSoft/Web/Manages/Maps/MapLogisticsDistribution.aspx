<%@ Page Title="物流配送单地图" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="MapLogisticsDistribution.aspx.cs" Inherits="TygaSoft.Web.Manages.Maps.MapLogisticsDistribution" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="mapContainer" style="width:100%;height:100%;"></div>

    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=NnwxmFFvpZVb7GIAf6i7NMzUMj40wlXu"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/Maps/MapLogisticsDistribution.js"></script>
    <script type="text/javascript">
        $(function () {
            MapLogisticsDistribution.Init();
        })
    </script>
</asp:Content>
