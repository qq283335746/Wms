<%@ Page Title="货主列表" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListCustomer.aspx.cs" Inherits="TygaSoft.Web.Manages.Base.ListCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListCustomer.OnAdd()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListCustomer.OnEdit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListCustomer.OnDel()"><span>删除</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListCustomer.OnFeatureUser()">分配登录账号</a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListCustomer.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dgCustomer" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgToolbar',onSelect:ListCustomer.OnSelect">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'Coded', width:100">代号</th>
                <th data-options="field: 'Named', width:100">名称</th>
                <th data-options="field: 'ShortName', width:100">简称</th>
                <th data-options="field: 'ContactMan', width:100">联系人</th>
                <th data-options="field: 'Email', width:100">邮箱</th>
                <th data-options="field: 'Phone', width:100">手机</th>
                <th data-options="field: 'TelPhone', width:100">电话</th>
                <th data-options="field: 'Fax', width:100">传真</th>
                <th data-options="field: 'Postcode', width:100">邮编</th>
                <th data-options="field: 'Address', width:100">地址</th>
                <th data-options="field: 'Remark', width:100">备注</th>

            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgUsers.js"></script>
    <script type="text/javascript" src="/wms/Scripts/DlgFiles.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/Base/ListCustomer.js"></script>
    <script type="text/javascript">
        $(function () {
            ListCustomer.Init();
        })
    </script>

</asp:Content>
