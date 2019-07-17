<%@ Page Title="项目报备" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="EditProjectReportPrepare.aspx.cs" Inherits="TygaSoft.Web.Manages.Gzxy.EditProjectReportPrepare" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="easyui-tabs" data-options="fit:true">
        <div title="客户" style="margin-top:5px;">
            <div id="dgCustomerToolbar">
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListCustomer.OnAdd()"><span>新增</span></a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListCustomer.OnEdit()"><span>编辑</span></a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListCustomer.OnDel()"><span>删除</span></a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-tip'" onclick="ListCustomer.OnView()"><span>明细</span></a>
                <div class="fr">
                    <input id="txtKeywordCustomer" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListCustomer.OnSearch" style="width:250px;" />
                </div>
                <span class="clr"></span>
            </div>
            <table id="dgCustomer" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgCustomerToolbar'">
                <thead>
                    <tr>
		                <th data-options="field: 'Id', checkbox: true"></th> 
                        <th data-options="field: 'Coded', width:100">客户编号</th> 
                        <th data-options="field: 'Named', width:180">客户全称</th> 
                        <th data-options="field: 'ShortName', width:150">客户简称</th> 
                        <th data-options="field: 'InCompany', width:180">总公司</th> 
                        <th data-options="field: 'ContactMan', width:80">联系人</th> 
                        <th data-options="field: 'ContactPhone', width:80">联系方式</th> 
                        <th data-options="field: 'TelPhone', width:80">电话</th> 
                        <th data-options="field: 'Fax', width:80">传真</th> 
                        <th data-options="field: 'PostCode', width:60">邮编</th> 
                        <th data-options="field: 'SRecordDate', width:100">创建日期</th> 
                        <th data-options="field: 'Address', width:200">地址</th> 
                    </tr>
                </thead>
            </table>
        </div>
        <div title="大项目" style="margin-top:5px;">
            <div id="dgProjectToolbar">
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="ListProjectReportPrepare.OnAdd()"><span>新增</span></a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="ListProjectReportPrepare.OnEdit()"><span>编辑</span></a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListProjectReportPrepare.OnDel()"><span>删除</span></a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-tip'" onclick="ListProjectReportPrepare.OnView()"><span>明细</span></a>
                <div class="fr">
                    <input id="txtKeywordProject" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListProjectReportPrepare.OnSearch" style="width:250px;" />
                </div>
                <span class="clr"></span>
            </div>
            <table id="dgProject" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgProjectToolbar'">
                <thead>
                    <tr>
		                <th data-options="field: 'Id', checkbox: true"></th> 
                        <th data-options="field: 'ProjectName', width:180">大项目名称</th> 
                        <th data-options="field: 'ProjectSource', width:100">项目来源</th> 
                        <th data-options="field: 'CustomerCode', width:100">客户编码</th>
                        <th data-options="field: 'CustomerShortName', width:180">客户名称</th> 
                        <th data-options="field: 'CustomerOfficial', width:80">负责人</th> 
                        <th data-options="field: 'ContactMan', width:80">联系人</th> 
                        <th data-options="field: 'ContactPhone', width:80">联系方式</th> 
                        <th data-options="field: 'SpecsModel', width:100">产品型号</th> 
                        <th data-options="field: 'PreQty', width:80">预计数量</th> 
                        <th data-options="field: 'PreAmount', width:80">预计价格</th> 
                        <th data-options="field: 'SRecordDate', width:100">创建日期</th> 
                        <th data-options="field: 'Remark', width:200">备注</th> 
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <script type="text/javascript" src="/wms/Scripts/Manages/Gzxy/DlgCustomer.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/Gzxy/ListCustomer.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Manages/Gzxy/ListProjectReportPrepare.js"></script>
    <script type="text/javascript">
        $(function () {
            ListCustomer.Init();
            ListProjectReportPrepare.Init();
        })
    </script>

</asp:Content>
