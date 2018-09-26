<%@ Page Title="供应商管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListSupplier.aspx.cs" Inherits="TygaSoft.Web.Manages.Base.ListSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dgToolbar">
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListSupplier.OnAdd()"><span>新增</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListSupplier.OnEdit()"><span>编辑</span></a>
        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListSupplier.OnDel()"><span>删除</span></a>
        <div class="fr" style="padding:3px 5px 2px 5px;">
            <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListSupplier.OnSearch" style="width:250px;" />
        </div>
        <span class="clr"></span>
    </div>
    <table id="dg" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:false,striped:true,toolbar:'#dgToolbar'">
        <thead>
            <tr>
                <th data-options="field: 'Id', checkbox: true"></th>
                <th data-options="field: 'SupplierCode', width:100">供应商编号</th>
                <th data-options="field: 'SupplierName', width:180">供应商名称</th>
                <th data-options="field: 'ShortName', width:100">简称</th>
                <th data-options="field: 'ContactMan', width:80">联系人</th>
                <th data-options="field: 'Email', width:100">邮箱</th>
                <th data-options="field: 'Phone', width:80">手机</th>
                <th data-options="field: 'TelPhone', width:80">电话</th>
                <th data-options="field: 'Fax', width:80">传真</th>
                <th data-options="field: 'Postcode', width:60">邮编</th>
                <th data-options="field: 'Address', width:200">地址</th>
                <th data-options="field: 'Remark', width:200">备注</th>

            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/wms/Scripts/Manages/Base/ListSupplier.js"></script>
    <script type="text/javascript">
        $(function () {
            ListSupplier.Init();
        })
    </script>

</asp:Content>
