
var ListCategory = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent:function(){
    
    },
    InitData: function () {
        ListCategory.LoadTree();
        ListCategory.LoadDgProduct(1,10);
    },
    Container: $("#dlgMesCategory"),
    Url: "",
    LoadTree: function () {
        var t = $("#tCategory");

        Common.AjaxPost("/wms/h/users.html", { ReqName: 'GetMesCategoryTree' }, function (result) {
            //console.log('result--' + result.Data);
            t.tree('loadData', JSON.parse(result.Data));
        });
    },
    OnContextMenu: function (e, node) {
        e.preventDefault();
        $(this).tree('select', node.target);
        $('#mmTree').menu('show', {
            left: e.pageX,
            top: e.pageY
        });
    },
    OnCategorySelect: function (node) {
        if (!ListCategory.CheckProductParent()) $('#abtnAddProduct').linkbutton('disable');
        else $('#abtnAddProduct').linkbutton('enable');
    },
    SAction:'',
    Add: function () {
        this.SAction = 'Add';
        var t = $("#tCategory");
        var node = t.tree('getSelected');
        if (!node) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        var rootNode = t.tree('getRoot');
        var parentNode = t.tree('getParent', node.target);
        var parentId = node.attributes.parentId;
        var jContent = $(ListCategory.GetFormString());
        var panels = jContent.filter('#dlgMesCategoryFm').children();
        if (!parentNode) {
            if (node.text == '请选择') panels.filter(":not(:first)").remove();
            else {
                console.log(9999);
                panels.filter(":first").remove();
                if (node.id == rootNode.id) panels.filter(":gt(2)").remove();
                else panels.filter(":gt(1)").remove();
            }
        }
        else {
            panels.filter(":first").remove();
            if (ListCategory.SAction == 'Add') {
                panels.find('#txtCoded').parent().prev().text("工序编号：");
                panels.find('#txtNamed').parent().prev().text("工序名称：");
            }
        }
        //console.log(parentNode);
        //return false;
        if ($("body").find("#dlgMesCategory").length == 0) {
            $("body").append("<div id=\"dlgMesCategory\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(500, 250);
        $("#dlgMesCategory").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                text: '保存', iconCls: 'icon-save', handler: function () {
                    ListCategory.OnSave();
                }
            }, {
                text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgMesCategory').dialog('close');
                }
            }],
            content: jContent[0].outerHTML
        });
        return false;
    },
    Edit: function () {
        this.SAction = 'Edit';
        var t = $("#tCategory");
        var node = t.tree('getSelected');
        if (!node) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }

        ListCategory.GetMesCategoryInfo(t,node);
    },
    GetFormString: function () {
        var s = '<form id="dlgMesCategoryFm">';
        s += '<div class="row"><span class="rl"><span class="cr">*</span> 根节点：</span><div class="fl"><input id ="txtNamed" class="easyui-validatebox txt200" data-options="required:true" /></div></div>';
        s += '<div class="row"><span class="rl"><span class="cr">*</span> 产品编号：</span><div class="fl"><input id ="txtCoded" class="easyui-validatebox txt200" data-options="required:true" /></div></div>  ';
        s += '<div class="row mt10"><span class="rl"><span class="cr">*</span> 产品名称：</span><div class="fl"><input id ="txtNamed" class="easyui-validatebox txt200" data-options="required:true" /></div></div>';
        s += '<div class="row mt10"><span class="rl">工位：</span><div class="fl"> <input id ="txtWorkStation" class="txt200" /></div></div> ';
        s += '<div class="row mt10"><span class="rl">标准工时：</span><div class="fl"><input id ="txtStandardHours" class="txt200" /></div></div> ';
        s += '<div class="row mt10"><span class="rl">排序：</span><div class="fl"><input id ="txtSort" class="txt200" /></div></div> ';
        s += '<span class="clr"></span>';
        s += '</form>';
        return s;
    },
    GetProductFmString: function () {
        var s = '<form id="dlgMesProductFm">';
        s += '<div class="row"><span class="rl"><span class="cr">*</span> 零件编号：</span><div class="fl"><input id ="txtProductCoded" class="easyui-validatebox txt200" data-options="required:true" /></div></div>  ';
        s += '<div class="row mt10"><span class="rl"><span class="cr">*</span> 零件名称：</span><div class="fl"><input id ="txtProductNamed" class="easyui-validatebox txt200" data-options="required:true" /></div></div>';
        s += '<div class="row mt10"><span class="rl">用量：</span><div class="fl"> <input id ="txtProductUseQty" class="txt200" /></div></div> ';
        s += '<div class="row mt10"><span class="rl">排序：</span><div class="fl"><input id ="txtProductSort" class="txt200" /></div></div> ';
        s += '<span class="clr"></span>';
        s += '</form>';
        return s;
    },
    GetMesCategoryInfo: function (t,node) {
        var postData = { ReqName: 'GetMesCategoryInfo', Id: node.id };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            console.log('GetMesCategoryInfo--' + JSON.stringify(result));
            var jData = JSON.parse(result.Data);
            var jContent = $(ListCategory.GetFormString());
            var panels = jContent.filter('#dlgMesCategoryFm').children();
            var rootNode = t.tree('getRoot');
            var parentNode = t.tree('getParent', node.target);
            if (node.id == rootNode.id) {
                panels.filter(":not(:first)").remove();
            }
            else {
                if (rootNode.id == parentNode.id) {
                    panels.filter(":first").remove();
                    panels.filter(":gt(1)").remove();
                }
            }
            if ($("body").find("#dlgMesCategory").length == 0) {
                $("body").append("<div id=\"dlgMesCategory\" style=\"padding:10px;\"></div>");
            }
            var wh = Common.GetWh(500, 250);
            $("#dlgMesCategory").dialog({
                title: '填写信息',
                width: wh[0],
                height: wh[1],
                closed: false,
                modal: true,
                iconCls: 'icon-edit',
                buttons: [{
                    text: '保存', iconCls: 'icon-save', handler: function () {
                        ListCategory.OnSave();
                    }
                }, {
                    text: '取消', iconCls: 'icon-cancel', handler: function () {
                        $('#dlgMesCategory').dialog('close');
                    }
                }],
                content: jContent[0].outerHTML,
                onOpen: function () {
                    $('#txtNamed').val(jData.Named);
                    $('#txtCoded').val(jData.Coded);
                    $('#txtWorkStation').val(jData.WorkStation);
                    $('#txtStandardHours').val(jData.StandardHours);
                    $('#txtSort').val(jData.Sort);
                }
            });
        })
    },
    Del: function () {
        var t = $("#tCategory");
        var node = t.tree('getSelected');
        if (!node) {
            $.messager.alert('错误提示', "请选中一个节点再进行操作", 'error');
            return false;
        }

        try {
            var childNodes = t.tree('getChildren', node.target);
            if (childNodes && childNodes.length > 0) {
                $.messager.alert('错误提示', "请先删除子节点再删除此节点", 'error');
                return false;
            }
        }
        catch (e) {
            $.messager.alert('错误提示', "请先删除子节点再删除此节点", 'error');
            return false;
        }

        if (node) {
            $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
                if (r) {
                    var postData = { "ReqName": "DeleteMesCategory", "Id": "" + node.id + "" };
                    Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                        t.tree('remove', node.target);
                        jeasyuiFun.show("温馨提示", "操作成功！");
                    })
                }
            });
        }
    },
    OnSave: function () {
        var isValid = $('#dlgMesCategoryFm').form('validate');
        if (!isValid) return false;

        var t = $("#tCategory");
        var currentNode = t.tree('getSelected');
        ListCategory.GetStep(t, currentNode);
        var step = ListCategory.Step;
        ListCategory.Step = "";
        var parentNode = t.tree('getParent', currentNode.target);

        var parentId = currentNode.id;
        if (ListCategory.SAction != 'Add') {
            parentId = currentNode.attributes.parentId;
        }
        var coded = $.trim($("#txtCoded").val());
        var named = $.trim($("#txtNamed").val());
        var step = step;
        var workStation = $.trim($("#txtWorkStation").val());
        var standardHours = $.trim($("#txtStandardHours").val());
        var sort = $.trim($("#txtSort").val());
        var remark = $.trim($("#txtRemark").val());

        var postData = { "ReqName": "SaveMesCategory", "ParentId": "" + parentId + "", "Coded": "" + coded + "", "Named": "" + named + "", "Step": "" + step + "", "WorkStation": "" + workStation + "", "StandardHours": "" + standardHours + "", "Sort": "" + sort + "", "Remark": "" + remark + "" };
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            //console.log('SaveMesCategory--' + JSON.stringify(result));
            jeasyuiFun.show("温馨提示", "保存成功！");
            var jData = JSON.parse(result.Data);
            if (currentNode.text == "请选择") {
                t.tree('update', {
                    target: currentNode.target,
                    id: jData.Id,
                    text: jData.Named
                });
                $('#dlgMesCategory').dialog('close');
                return false;
            }
            
            if (ListCategory.SAction == 'Add') {
                t.tree('append', {
                    parent: currentNode.target,
                    data: [{
                        id: jData.Id, text: jData.Named, "attributes": {
                            "parentId": parentId
                        },
                    }]
                });
            }
            else {
                t.tree('update', {
                    target: currentNode.target,
                    text: jData.Named
                });
            }
            $('#dlgMesCategory').dialog('close');
        })
    },
    Step:"",
    GetStep: function (t, node) {
        if (node) {
            ListCategory.Step += node.id + ",";
            var pNode = t.tree('getParent', node.target);
            if (pNode) {
                ListCategory.GetStep(t, pNode, ListCategory.Step);
            }
        }
    },
    CheckProductParent:function(){
        var t = $("#tCategory");
        var node = t.tree('getSelected');
        if (!node) {
            return null;
        }
        var rootNode = t.tree('getRoot');
        var parentNode = t.tree('getParent', node.target);
        if (!parentNode) return null;
        var parentRoot = t.tree('getParent', parentNode.target);
        if (parentRoot && rootNode && parentRoot.id == rootNode.id) return node;
        return null;
    },
    OnAddProduct: function () {
        var node = ListCategory.CheckProductParent();
        if (!node) {
            $.messager.alert('错误提示', '请选择属于工序节点再进行操作', 'error');
            return false;
        }
        var jContent = $(ListCategory.GetProductFmString());
        var panels = jContent.filter('#dlgMesProductFm').children();
        if ($("body").find("#dlgMesProduct").length == 0) {
            $("body").append("<div id=\"dlgMesProduct\" style=\"padding:10px;\"></div>");
        }
        var wh = Common.GetWh(500, 250);
        $("#dlgMesProduct").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                text: '保存', iconCls: 'icon-save', handler: function () {
                    ListCategory.OnSaveProduct();
                }
            }, {
                text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgMesProduct').dialog('close');
                }
            }],
            content: jContent[0].outerHTML
        });
    },
    OnDelProduct: function () {
        var rows = $("#dgProduct").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                var postData = { "ReqName": "DeleteMesProduct", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "保存成功！");
                    setTimeout(function () {
                        ListCategory.LoadDgProduct(1, 10);
                    }, 700);
                });
            }
        });
    },
    OnSaveProduct: function () {
        var isValid = $('#dlgMesProductFm').form('validate');
        if (!isValid) return false;

        var node = ListCategory.CheckProductParent();
        if (!node) {
            $.messager.alert('错误提示', '请选择属于工序节点再进行操作', 'error');
            return false;
        }
        
        var categoryId = node.id;
        var coded = $.trim($("#txtProductCoded").val());
        var named = $.trim($("#txtProductNamed").val());
        var useQty = $.trim($("#txtProductUseQty").val());
        var sort = $.trim($("#txtProductSort").val());

        var postData = { "ReqName": "SaveMesProduct", "CategoryId": "" + categoryId + "", "Coded": "" + coded + "", "Named": "" + named + "", "UseQty": "" + useQty + "", "Sort": "" + sort + "", "Remark": "" };
        Common.AjaxPost("/wms/h/content.html", postData, function (result) {
            $('#dlgMesProduct').dialog('close');
            jeasyuiFun.show("温馨提示", "保存成功！");
            setTimeout(function () {
                ListCategory.LoadDgProduct(1, 10);
            }, 700);
        });
    },
    LoadDgProduct: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var postData = { "ReqName": "GetMesProductList", "PageIndex": "" + pageIndex + "", "PageSize": "" + pageSize + "", "Keyword": "" + keyword + "" };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            console.log('result--' + JSON.stringify(result));
            $("#dgProduct").datagrid('loadData', JSON.parse(result.Data));
        })
    },
    OnSearchProduct: function () {
        ListCategory.LoadDgProduct(1, 10);
    }
}