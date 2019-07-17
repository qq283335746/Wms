<%@ Page Title="货品管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="ListCategory.aspx.cs" Inherits="TygaSoft.Web.Admin.Sys.ListCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="easyui-layout" data-options="fit:true">
        <div data-options="region:'west',split:true" style="width: 400px;">
            <div class="mtb10">
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListCategory.Add()">新建</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="ListCategory.Edit()">编辑</a>
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListCategory.Del()">删除</a>
            </div>
            <div id="mmTree" class="easyui-menu" style="width: 120px;">
                <div onclick="ListCategory.Add()" data-options="iconCls:'icon-add'">添加</div>
                <div onclick="ListCategory.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
                <div onclick="ListCategory.Del()" data-options="iconCls:'icon-remove'">删除</div>
            </div>
            <ul id="treeCt" class="easyui-tree" data-options="animate:true,onContextMenu:ListCategory.OnContextMenu,onSelect:ListCategory.OnCategorySelect"></ul>
        </div>
        <div data-options="region:'center'" style="padding: 5px;">
            <div id="dgProductToolbar" style="padding:3px 5px 1px 0;display: none;">
                <a name="abtnAdd" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'">新建</a>
                <a name="abtnEdit" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="">编辑</a>
                <a name="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">删除</a>
                <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'">导入/导出</a>
                <div class="fr">
                    <input id="txtKeyword" class="easyui-textbox" data-options="prompt:'关键字',buttonText:'查询',buttonIcon:'icon-search',onClickButton:ListProduct.OnSearch" style="width: 250px;" />
                </div>
                <span class="clr"></span>
            </div>
            <div id="mmExcel" style="width: 150px;">
                <div onclick="ListProduct.OnImport()">批量导入</div>
                <div onclick="ListProduct.OnExport()">导出</div>
            </div>
            <table id="dgProduct" class="easyui-datagrid" title="物料列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,striped:true,toolbar:'#dgProductToolbar'">
                <thead>
                    <tr>
                        <th data-options="field:'Id',checkbox:true"></th>
                        <th data-options="field:'ProductCode',width:100">物料代码</th>
                        <th data-options="field:'ProductName',width:100">物料名称</th>
                        <th data-options="field:'FullName',width:100">全名</th>
                        <th data-options="field:'Specs',width:100">规格型号</th>
                        <th data-options="field:'Sort',width:100">排序</th>
                        <th data-options="field:'Remark',width:100">备注</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <div id="dlgUpload" style="padding: 10px;"></div>
    <div id="dlgCategory" style="width: 540px; height: 300px; padding: 10px;"></div>

    <script type="text/javascript" src="/wms/Scripts/DlgUpload.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Admin/Base/ListCategory.js"></script>
    <script type="text/javascript" src="/wms/Scripts/Admin/Base/ListProduct.js"></script>
    <script type="text/javascript">
        $(function () {
            try {
                ListCategory.Init();
                ListProduct.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }

        })


    </script>

</asp:Content>
