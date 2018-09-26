<%@ Page Title="条码标签设置控件" Language="C#" MasterPageFile="~/Masters/Manages.Master" AutoEventWireup="true" CodeBehind="BarcodeCtr.aspx.cs" Inherits="TygaSoft.Web.MyTest.BarcodeCtr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .drag-item{
            display:block;padding:5px;border:1px solid #ccc;margin-bottom:2px; width:190px;background:#fafafa;color:#444;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <table id="table_1" style="width:450px;height:290px;">
        <tr><td style="border:1px solid #ccc;"></td><td style="border:1px solid #ccc;"></td><td style="border:1px solid #ccc;"></td><td style="border:1px solid #ccc;"></td></tr>
        <tr><td style="border:1px solid #ccc;"><span></span></td><td style="border:1px solid #ccc;"></td></tr>
        <tr><td style="border:1px solid #ccc;"></td><td style="border:1px solid #ccc;"></td></tr>
        <tr><td style="border:1px solid #ccc;"></td><td style="border:1px solid #ccc;"></td></tr>
        <tr><td style="border:1px solid #ccc;"></td><td style="border:1px solid #ccc;"></td></tr>
    </table>

    <div class="easyui-layout" data-options="fit:true,border:false">
        <div data-options="region:'west',title:'标签',split:true,border:false" style="width:230px;padding:10px;">
            <ul>
                <li><div class="drag-item">表格</div></li>
                <li><div class="drag-item">文本</div></li>
                <li><div class="drag-item">输入框</div></li>
                <li><div class="drag-item">线条</div></li>
                <li><div class="drag-item">条码占位符</div></li>
            </ul>
        </div>
        <div data-options="region:'center',title:'条码参数设置',border:false" style="padding:5px;">
            <div id="dropContainer" class="easyui-panel" data-options="fit:true" style="position:relative;overflow:hidden;width:500px;height:300px"></div>
        </div>
    </div>

    <div onclick="onCol(this,'11')" style="border:1px solid #ff0000;height:30px; width:500px;">11</div>
    <table border="1" style="width:500px;">
        <tr>
            <td onclick="onCol(this,'1')">1</td>
            <td onclick="onCol(this,'2')">2</td>
            <td onclick="onCol(this,'3')">3</td>
        </tr>
        <tr>
            <td>11</td>
            <td>22</td>
            <td>33</td>
        </tr>
    </table>

    <script type="text/javascript">

        $(function () {
            $('.drag-item').draggable({
                revert: true,
                proxy: 'clone',
                onDrag: function (e) {
                    var d = e.data;
                    if (d.left < 0) { d.left = 0 }
                    if (d.top < 0) { d.top = 0 }
                    if (d.left + $(d.target).outerWidth() > $(d.parent).width()) {
                        d.left = $(d.parent).width() - $(d.target).outerWidth();
                    }
                    if (d.top + $(d.target).outerHeight() > $(d.parent).height()) {
                        d.top = $(d.parent).height() - $(d.target).outerHeight();
                    }
                }
            });
            $('#dropContainer').droppable({
                accept: '.drag-item',
                onDragEnter: function () {
                    $(this).addClass('over');
                },
                onDragLeave: function () {
                    $(this).removeClass('over');
                },
                onDrop: function (e, source) {
                    $(this).removeClass('over');
                    if ($(source).hasClass('assigned')) {
                        $(this).append(source);
                    } else {
                        var c = $(source).clone().addClass('assigned');
                        $(this).empty().append(c);
                        c.draggable({
                            revert: true
                        });
                    }
                }
            });
        })

        function onCol(t, n) {
            var curr = $(t);
            if (curr.is('td')) alert('n--' + n);
        }
    </script>

</asp:Content>
