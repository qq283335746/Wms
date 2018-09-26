<%@ Page Title="条码标签模板管理" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="BarcodeCtr.aspx.cs" Inherits="TygaSoft.Web.Admin.Print.BarcodeCtr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .drag-item, .item {
            padding: 5px;
            border: 1px solid #ccc;
            margin-bottom: 2px;
            background: #fafafa;
            color: #444;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="tabsPrintTemplate" class="easyui-tabs" data-options="fit:true">
        <div title="模板列表" style="padding-top:10px;">
            <div id="dgPrintTemplateToolbar">
                <a id="lbtnSdpt" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="BarcodeCtr.OnSetDefault()"><span>设为默认模板</span></a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" onclick="BarcodeCtr.OnEdit()"><span>编辑</span></a>
                <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="BarcodeCtr.OnDel()"><span>删除</span></a>
            </div>
            <table id="dgPrintTemplate" class="easyui-datagrid" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:true,striped:true,toolbar:'#dgPrintTemplateToolbar',onSelect:BarcodeCtr.OnSelect">
                <thead>
                    <tr>
		                <th data-options="field: 'Id', checkbox: true"></th> 
                        <th data-options="field: 'Title', width:300">标题</th> 
                        <th data-options="field: 'IsDefault', width:80,formatter: function(value,row,index){return value == true ? '是':'否';}">是否默认</th> 
                    </tr>
                </thead>
            </table>
        </div>
        <div title="模板设计" style="padding-top:10px;">
            <div class="easyui-layout" data-options="fit:true,border:false">
 <%--               <div id="dragContainer" data-options="region:'west',title:'可用组件',split:true" style="width: 230px; padding: 5px;">
                    <ul>
                        <li>
                            <div class="drag-item">文本</div>
                        </li>
                        <li>
                            <div class="drag-item">输入框</div>
                        </li>
                        <li>
                            <div class="drag-item">线条</div>
                        </li>
                        <li>
                            <div class="drag-item">条码占位符</div>
                        </li>
                    </ul>
                </div>--%>
                <div data-options="region:'center',title:'预览区'" style="padding: 8px;">
                    <div class="easyui-panel" style="padding: 5px; margin-bottom: 8px;">
                        <label onclick="BarcodeCtr.OnIsMergeCell()">
                            <input type="checkbox" id="cbIsMergeCell" value="0" />合并单元格
                        </label>
                        <a id="lbtnMergeCell" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok',disabled:true" onclick="BarcodeCtr.OnMergeCell()">合并单元格</a>
                        <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'" onclick="BarcodeCtr.OnCellClear()">清空内容</a>
                        <%--<a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'" onclick="BarcodeCtr.OnOk()">确定</a>--%>
                        <div class="fr">
                            <a class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" onclick="BarcodeCtr.OnSave()"><span>保存</span></a>
                            <a id="lbtnSaveAs" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save',disabled:true" onclick="BarcodeCtr.OnSaveAs()"><span>另存为</span></a>
                            <a id="lbtnPrint" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-large-picture',disabled:true" onclick="BarcodeCtr.OnPrint()"><span>测试打印</span></a>
                        </div>
                    </div>
                    <div id="dropContainer" class="easyui-panel" data-options="fit:true,border:false"></div>
                </div>
                <div id="pgContainer" data-options="region:'east',title:'样式属性'" style="width: 300px; padding: 5px;">
                    <div>
                        <table id="pgTable" data-options="showHeader:false,showGroup:true,onEndEdit:BarcodeCtr.OnPgTableAcceptChanges" style="width: 100%;"></table>
                    </div>
                    <div>
                        <table id="pgTd" class="easyui-propertygrid" data-options="showHeader: false,showGroup: true,autoRowHeight:false,onBeforeEdit:BarcodeCtr.OnTdBeforeEdit,onEndEdit:BarcodeCtr.OnPgTdAcceptChanges" style="width: 100%;"></table>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script src="/wms/Scripts/Manages/DlgBarcodeTemplate.js"></script>
    <script src="/wms/Scripts/Manages/BarcodeCtr.js"></script>
    <script type="text/javascript">
        $(function () {
            BarcodeCtr.Init();
        })
    </script>

</asp:Content>
