<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyBarcodeCtr.aspx.cs" Inherits="TygaSoft.Web.MyTest.MyBarcodeCtr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>条码标签设置控件</title>
    <style type="text/css">
        .drag-item{
            display:block;padding:5px;border:1px solid #ccc;margin-bottom:2px; width:190px;background:#fafafa;color:#444;
        }
    </style>
    <link href="~/Styles/Main.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/Plugins/Jeasyui144/themes/bootstrap/easyui.css" rel="stylesheet" type="text/css" />
    <link href="~/Scripts/Plugins/Jeasyui144/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="~/Styles/Print.css" rel="stylesheet" type="text/css" />
    <script src="/wms/Scripts/Plugins/Jeasyui144/jquery.min.js" type="text/javascript"></script>
    <script src="/wms/Scripts/Plugins/Jeasyui144/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/wms/Scripts/Plugins/Jeasyui144/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="/wms/Scripts/JeasyuiExtend.js" type="text/javascript"></script>
    <script src="/wms/Scripts/JeasyuiHelper.js" type="text/javascript"></script>
    <script src="/wms/Scripts/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>

   <%-- <div class="easyui-panel" style="position:relative;overflow:hidden;width:500px;height:300px">
       <div class="easyui-draggable" data-options="onDrag:onDrag,revert: false,proxy: 'clone'" style="width:100px;height:100px;background:#fafafa;border:1px solid #ccc;">
        </div>
        <ul>
            <li><div class="drag-item">表格</div></li>
            <li><div class="drag-item">文本</div></li>
            <li><div class="drag-item">输入框</div></li>
            <li><div class="drag-item">线条</div></li>
            <li><div class="drag-item">条码占位符</div></li>
            <li><div class="drag-item"></div></li>
            <li><div class="drag-item"></div></li>
            <li><div class="drag-item"></div></li>
            <li><div class="drag-item"></div></li>
            <li><div class="drag-item"></div></li>
        </ul>
    </div>--%>
    <script>
        //function onDrag(e){
        //    var d = e.data;
        //    if (d.left < 0){d.left = 0}
        //    if (d.top < 0){d.top = 0}
        //    if (d.left + $(d.target).outerWidth() > $(d.parent).width()){
        //        d.left = $(d.parent).width() - $(d.target).outerWidth();
        //    }
        //    if (d.top + $(d.target).outerHeight() > $(d.parent).height()){
        //        d.top = $(d.parent).height() - $(d.target).outerHeight();
        //    }
        //}

        //$(function () {
        //    $('.drag-item').draggable({
        //        revert: false,
        //        proxy: 'clone',
        //        onDrag: function (e) {
        //            var d = e.data;
        //            if (d.left < 0) { d.left = 0 }
        //            if (d.top < 0) { d.top = 0 }
        //            if (d.left + $(d.target).outerWidth() > $(d.parent).width()) {
        //                d.left = $(d.parent).width() - $(d.target).outerWidth();
        //            }
        //            if (d.top + $(d.target).outerHeight() > $(d.parent).height()) {
        //                d.top = $(d.parent).height() - $(d.target).outerHeight();
        //            }
        //        }
        //    });
        //})
    </script>

    <div class="easyui-layout" data-options="fit:true,border:false">
        <div data-options="region:'west',title:'标签',split:true,border:false" style="width:230px;padding:10px;">
            <div class="dragContainer">
                <ul>
                    <li><div class="drag-item">表格</div></li>
                    <li><div class="drag-item">文本</div></li>
                    <li><div class="drag-item">输入框</div></li>
                    <li><div class="drag-item">线条</div></li>
                    <li><div class="drag-item">条码占位符</div></li>
                    <li><div class="drag-item"></div></li>
                    <li><div class="drag-item"></div></li>
                    <li><div class="drag-item"></div></li>
                    <li><div class="drag-item"></div></li>
                    <li><div class="drag-item"></div></li>
                </ul>
            </div>
        </div>
        <div data-options="region:'center',title:'条码参数设置',border:false" style="padding:5px;">
            <div id="dropContainer" style="position:relative;overflow:hidden;width:500px;height:300px"></div>
        </div>
    </div>

    <%--<div onclick="onCol(this,'11')" style="border:1px solid #ff0000;height:30px; width:500px;">11</div>
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
    </table>--%>

    <script type="text/javascript">

        $(function () {
            $('.dragContainer .drag-item').draggable({
                revert: false,
                onDrag: function (e) {
                    //var d = e.data;
                    //if (d.left < 0) { d.left = 0 }
                    //if (d.top < 0) { d.top = 0 }
                    //if (d.left + $(d.target).outerWidth() > $(d.parent).width()) {
                    //    d.left = $(d.parent).width() - $(d.target).outerWidth();
                    //}
                    //if (d.top + $(d.target).outerHeight() > $(d.parent).height()) {
                    //    d.top = $(d.parent).height() - $(d.target).outerHeight();
                    //}
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

            $('.dragContainer').droppable({
                accept:'.assigned',
                onDragEnter:function(e,source){
                    $(source).addClass('trash');
                },
                onDragLeave:function(e,source){
                    $(source).removeClass('trash');
                },
                onDrop:function(e,source){
                    $(source).remove();
                }
            });
        })
    </script>

</body>
</html>
