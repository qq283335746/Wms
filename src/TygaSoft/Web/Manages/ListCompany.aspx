<%@ Page Title="公司列表" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListCompany.aspx.cs" Inherits="TygaSoft.Web.Manages.ListCompany" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgCompanyToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListCompany.OnAdd()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListCompany.OnEdit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListCompany.OnDel()"><span>删除</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListCompany.OnFeatureUser()">分配登录账号</a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListCompany.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dgCompany" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgCompanyToolbar',onSelect:ListCompany.OnSelect">
        <thead>
            <tr>
		        <th data-options="field: 'Id', checkbox: true"></th> 
                <th data-options="field: 'Coded', width:100">公司编号</th> 
                <th data-options="field: 'Named', width:180">公司全称</th> 
                <th data-options="field: 'ShortName', width:150">公司简称</th> 
                <th data-options="field: 'InCompany', width:180">总公司</th> 
                <th data-options="field: 'ContactMan', width:80">联系人</th> 
                <th data-options="field: 'ContactPhone', width:80">联系方式</th> 
                <th data-options="field: 'TelPhone', width:80">电话</th> 
                <th data-options="field: 'Fax', width:80">传真</th> 
                <th data-options="field: 'PostCode', width:60">邮编</th> 
                <th data-options="field: 'RecordDate', width:100,formatter:Common.FDate">创建日期</th> 
                <th data-options="field: 'Address', width:200">地址</th> 
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/DlgUsers.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/ListCompany.js"></script>
    <script type="text/javascript">
        $(function () {
            ListCompany.Init();
        })
    </script>

</asp:Content>
