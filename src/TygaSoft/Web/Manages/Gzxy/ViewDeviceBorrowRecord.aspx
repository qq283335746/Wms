<%@ Page Title="借样记录" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ViewDeviceBorrowRecord.aspx.cs" Inherits="TygaSoft.Web.Manages.Gzxy.ViewDeviceBorrowRecord" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div style="width:780px;margin:0 auto;margin-top:20px;">
        <a href="/wms/u/tinfone.html?funType=0">
            <div style="float:left;width:45%;padding-right:10px;height:200px;line-height:200px;font-size:36px; background-color:#007979;color:#FFF;text-align:center;margin-right:20px;"> 
            借出
            </div>  
        </a>
        <a href="/wms/u/tinfone.html?funType=1">
            <div style="float:left;width:45%;padding-left:10px;height:200px;line-height:200px;font-size:36px;background-color:#C4C400;color:#003E3E;text-align:center;">
                借入
            </div>
        </a>
        
        <span class="clr"></span>
    </div>

</asp:Content>
