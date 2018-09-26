<%@ Page Title="上架任务" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListShelfMission.aspx.cs" Inherits="TygaSoft.Web.Admin.InStore.ListShelfMission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
 <%--       <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" href="/wms/a/gainstore.html"><span>新增</span></a>--%>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListShelfMission.Edit()"><span>编辑</span></a>
      <%--  <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListShelfMission.Del()"><span>删除</span></a>--%>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-print'" onclick="ListShelfMission.Print()"><span>打印单</span></a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListShelfMission.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'OrderCode',width:120">单号</th>
                <th data-options="field:'TotalViewQty',width:120">待上架量</th>
                <th data-options="field:'TotalQty',width:120">已上架量</th>
                <th data-options="field:'Status',width:60">状态</th>
                <th data-options="field:'Remark',width:120">备注</th>
                <th data-options="field:'UserName',width:100">操作人</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Admin/InStore/ListShelfMission.js"></script>
    <script type="text/javascript">
        $(function () {
            ListShelfMission.Init();
        })
    </script>

</asp:Content>
