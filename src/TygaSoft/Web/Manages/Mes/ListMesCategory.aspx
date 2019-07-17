<%@ Page Title="IP17" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListMesCategory.aspx.cs" Inherits="TygaSoft.Web.Manages.Mes.ListMesCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'west',split:true" style="width:300px;">
            <div class="mtb10">
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListCategory.Add()">新建</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="ListCategory.Edit()">编辑</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListCategory.Del()">删除</a>
            </div>
            <div id="mmTree" class="easyui-menu" style="width:120px;">
                <div onclick="ListCategory.Add()" data-options="iconCls:'icon-add'">添加</div>
                <div onclick="ListCategory.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
                <div onclick="ListCategory.Del()" data-options="iconCls:'icon-remove'">删除</div>
            </div> 
            <ul id="tCategory" class="easyui-tree" data-options="animate:true,onSelect:ListCategory.OnCategorySelect"></ul>
        </div>
        <div data-options="region:'center'" style="padding:5px;">
            <div id="dgProductToolbar" style="display:none;">
                <a id="abtnAddProduct" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add',disabled:true" onclick="ListCategory.OnAddProduct()">新建</a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="">编辑</a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="ListCategory.OnDelProduct()">删除</a>
                <div class="fr" style="padding:3px 5px 2px 5px;">
                    <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListCategory.OnSearchProduct" style="width:250px;" />
                </div>
                <span class="clr"></span>
            </div>
            <table id="dgProduct" class="easyui-datagrid" title="关键部件清单" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,striped:true,toolbar:'#dgProductToolbar'">
                <thead>
                    <tr>
                        <th data-options="field:'Id',checkbox:true"></th>
                        <th data-options="field:'Coded',width:100">部件编号</th>
                        <th data-options="field:'Named',width:100">部件名称</th>
                        <th data-options="field:'UseQty',width:100">用量</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <script type="text/javascript" src="/wms/Scripts/Manages/Mes/EditMesCategory.js"></script>
    <script type="text/javascript">
        $(function () {
            try {
                ListCategory.Init();
                //ListProduct.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }
        
        })

    
    </script>

</asp:Content>
