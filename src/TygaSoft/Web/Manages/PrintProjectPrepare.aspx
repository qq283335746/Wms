<%@ Page Title="项目报备打印" Language="C#" MasterPageFile="~/Masters/Blank.Master" AutoEventWireup="true" CodeBehind="PrintProjectPrepare.aspx.cs" Inherits="TygaSoft.Web.Manages.PrintProjectPrepare" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">
    <%--<div id="dgToolbar">
        <div class="fr">
            <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-print'" onclick="PrintProjectPrepare.OnPrint()"><span>打印</span></a>
        </div>
        <span class="clr"></span>
    </div>
    <div class="title"><h1>基本信息</h1></div>--%>

    <div id="loProject" data-options="border:false">
        <div data-options="region:'west',border:false,title:''" style="width:400px;padding:8px;">
            <div class="title"><h1>基本信息</h1></div>
            <div id="customerBox">
                <table style="width:100%;margin-top:10px;">
                    <tr><td style="width:80px;">客户编号：</td><td id="lbCode"></td></tr>
                    <tr><td>客户全称：</td><td id="lbName"></td></tr>
                    <tr><td>客户简称：</td><td id="lbShortName"></td></tr>
                    <tr><td>总公司：</td><td id="lbInCompany"></td></tr>
                    <tr><td>联系人：</td><td id="lbContactMan"></td></tr>
                    <tr><td>联系方式：</td><td id="lbContactPhone"></td></tr>
                    <tr><td>电话：</td><td id="lbTelPhone"></td></tr>
                    <tr><td>传真：</td><td id="lbFax"></td></tr>
                    <tr><td>邮编：</td><td id="lbPostCode"></td></tr>
                    <tr><td>地址：</td><td id="lbAddress"></td></tr>
                    <tr><td colspan="2">公司简介：</td></tr>
                    <tr><td colspan="2" id="lbCompanyAbout" style="text-indent:24px;"></td></tr>
                </table>
            </div>
            <div id="projectBox">
                <table style="width:100%;margin-top:10px;">
                    <tr><td style="width:80px;">大项目：</td><td id="lbProjectName"></td></tr>
                    <tr><td>项目来源：</td><td id="lbProjectSource"></td></tr>
                    <tr><td>客户：</td><td id="lbCustomer"></td></tr>
                    <tr><td>负责人：</td><td id="lbCustomerOfficial"></td></tr>
                    <tr><td>联系人：</td><td id="lbProjectContactMan"></td></tr>
                    <tr><td>联系方式：</td><td id="lbProjectContactPhone"></td></tr>
                    <tr><td>产品型号：</td><td id="lbSpecsModel"></td></tr>
                    <tr><td>预计数量：</td><td id="lbPreQty"></td></tr>
                    <tr><td>预计价格：</td><td id="lbPreAmount"></td></tr>
                    <tr><td>备注：</td><td id="lbRemark"></td></tr>
                    <tr><td colspan="2">项目情况介绍：</td></tr>
                    <tr><td colspan="2" id="lbProjectAbout" style="text-indent:24px;"></td></tr>
                </table>
            </div>
        </div>
        <div data-options="region:'center',border:false,title:''">
            <div id="dgToolbar">
                <div class="fr">
                    <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-print'" onclick="PrintProjectPrepare.OnPrint()"><span>打印</span></a>
                </div>
                <span class="clr"></span>
            </div>
            <table id="dgCustomer" title="客户列表" data-options="fit:true,fitColumns:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgToolbar'">
                <thead>
                    <tr>
                        <th data-options="field: 'Coded', width:100">客户编号</th> 
                        <th data-options="field: 'Named', width:180">客户全称</th> 
                        <th data-options="field: 'ShortName', width:150">客户简称</th> 
                        <th data-options="field: 'InCompany', width:180">总公司</th> 
                        <th data-options="field: 'ContactMan', width:80">联系人</th> 
                        <th data-options="field: 'ContactPhone', width:80">联系方式</th> 
                        <th data-options="field: 'TelPhone', width:60">电话</th> 
                        <th data-options="field: 'Fax', width:60">传真</th> 
                        <th data-options="field: 'PostCode', width:60">邮编</th> 
                        <th data-options="field: 'Address', width:200">地址</th> 
                    </tr>
                </thead>
            </table>
            <table id="dgProject" title="大项目列表" data-options="fit:true,fitColumns:true,rownumbers:true,singleSelect:false,border:true,striped:true,toolbar:'#dgToolbar'">
                <thead>
                    <tr>
                        <th data-options="field: 'ProjectName', width:180">大项目名称</th> 
                        <th data-options="field: 'ProjectSource', width:100">项目来源</th> 
                        <th data-options="field: 'CustomerCode', width:100">客户编码</th>
                        <th data-options="field: 'CustomerName', width:180">客户名称</th> 
                        <th data-options="field: 'CustomerOfficial', width:80">负责人</th> 
                        <th data-options="field: 'ContactMan', width:80">联系人</th> 
                        <th data-options="field: 'ContactPhone', width:80">联系方式</th> 
                        <th data-options="field: 'SpecsModel', width:100">产品型号</th> 
                        <th data-options="field: 'PreQty', width:80">预计数量</th> 
                        <th data-options="field: 'PreAmount', width:80">预计价格</th> 
                        <th data-options="field: 'Status', width:200">项目状态</th> 
                        <th data-options="field: 'Remark', width:200">备注</th> 
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <input type="hidden" id="hKey" runat="server" />
    <input type="hidden" id="hValue" runat="server" />
    <script type="text/javascript" src="/wms/Scripts/Manages/PrintProjectPrepare.js"></script>
    <script type="text/javascript">
        $(function () {
            PrintProjectPrepare.Init();
        })
    </script>

</asp:Content>
