<%@ Page Title="多站点管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListSiteMulti.aspx.cs" Inherits="TygaSoft.Web.Manages.Sys.ListSiteMulti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="SiteMulti.Add()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="SiteMulti.Edit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="SiteMulti.Del()"><span>删除</span></a>
        <div class="fr">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:SiteMulti.OnSearch" style="width: 250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'Coded', width:100">站点编号</th>
                <th data-options="field: 'Named', width:100">站点名称</th>
                <th data-options="field: 'SiteLogo', width:100,formatter:Common.FImg">站点Logo</th>
                <th data-options="field: 'SiteTitle', width:100">站点标题</th>

            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/Sys/SiteMulti.js"></script>
    <script type="text/javascript" src="/wms/Scripts/DlgFiles.js"></script>
    <script type="text/javascript">
        $(function () {
            SiteMulti.Init();
        })
    </script>

</asp:Content>
