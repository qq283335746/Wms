<%@ Page Title="费用管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListCost.aspx.cs" Inherits="TygaSoft.Web.Admin.Cost.ListCost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox fr" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search'" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="rownumbers:true,singleSelect:true,fitColumns:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'HuopinCode',width:100">货品</th>
                <th data-options="field:'HuopinName',width:100">货品名称</th>
                <th data-options="field:'Capacity',width:100">容量</th>
                <th data-options="field:'SRukuStartDate',width:100">入库时间</th>
                <th data-options="field:'SRukuEndDate',width:100">截止时间</th>
                <th data-options="field:'UnitPrice',width:100">单价</th>
                <th data-options="field:'PayStatus',width:100">收款状态</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater runat="server" ID="rpData">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("Id") %></td>
                        <td><%#Eval("HuopinCode") %></td>
                        <td><%#Eval("HuopinName") %></td>
                        <td><%#Eval("Capacity") %></td>
                        <td><%#Eval("SRukuStartDate") %></td>
                        <td><%#Eval("SRukuEndDate") %></td>
                        <td><%#Eval("UnitPrice") %></td>
                        <td><%#Eval("PayStatus") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

    <asp:Label runat="server" ID="lbTotal"></asp:Label>

    <script type="text/javascript" src="/wms/Scripts/Admin/Cost/ListCost.js"></script>
    <script type="text/javascript">
        $(function () {
            ListCost.Init();
        })
    </script>

</asp:Content>
