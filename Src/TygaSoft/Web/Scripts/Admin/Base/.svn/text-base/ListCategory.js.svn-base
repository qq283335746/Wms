
var ListCategory = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent:function(){
    
    },
    InitData: function () {
        ListCategory.Load();
    },
    InitForm:function(){
        //$('#treeCt').tree();
    },
    Container: $("#dlgCategory"),
    Url: "",
    Load: function () {
        var t = $("#treeCt");

        $.ajax({
            url: "/wms/Services/WmsService.svc/GetCategoryTree",
            type: "GET",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                Common.OnProgressStart();
            },
            complete: function () {
                Common.OnProgressStop();
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return;
                }

                t.tree('loadData', JSON.parse(result.Data));
            }
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
        ListProduct.LoadDg(1, 10);
    },
    Add: function () {
        this.Url = "/wms/Services/WmsService.svc/SaveCategory";
        var t = $("#treeCt");
        var node = t.tree('getSelected');
        if (!node) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        ListCategory.Container.dialog({
            title: "新建",
            href: '/wms/a/ytbase.html?action=add&Id=' + node.id + '',
            closed: false,
            modal: true,
            iconCls: 'icon-save',
            buttons: [{
                id: 'btnSaveCategory', text: '保存', iconCls: 'icon-save', handler: function () {
                    ListCategory.Save();
                }
            }, {
                id: 'btnCancelSaveCategory', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgCategory').dialog('close');
                }
            }]
        });
    },
    Edit: function () {
        this.Url = "/wms/Services/WmsService.svc/SaveCategory";
        var t = $("#treeCt");
        var node = t.tree('getSelected');
        if (!node) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        ListCategory.Container.dialog({
            title: "编辑",
            href: '/wms/a/ytbase.html?action=edit&Id=' + node.id + '',
            closed: false,
            modal: true,
            iconCls: 'icon-save',
            buttons: [{
                id: 'btnSaveCategory', text: '保存', iconCls: 'icon-save', handler: function () {
                    ListCategory.Save();
                }
            }, {
                id: 'btnCancelSaveCategory', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgCategory').dialog('close');
                }
            }]
        });
    },
    Del: function () {
        var t = $("#treeCt");
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
            $.messager.confirm('温馨提醒', '确定要删除吗?', function (r) {
                if (r) {
                    $.ajax({
                        type: "POST",
                        url: "/wms/Services/WmsService.svc/DeleteCategory",
                        contentType: "application/json; charset=utf-8",
                        data: '{"Id":"' + node.id + '"}',
                        beforeSend: function () {
                            Common.OnProgressStart();
                        },
                        complete: function () {
                            Common.OnProgressStop();
                        },
                        success: function (result) {
                            if (result.ResCode != 1000) {
                                $.messager.alert('系统提示', result.Msg, 'info');
                                return false;
                            }
                            t.tree('remove', node.target);
                            jeasyuiFun.show("温馨提示", "操作成功！");
                        }
                    })
                }
            });
        }
    },
    Save: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;

        var Id = $.trim($("#hId").val());
        var parentId = $("#hParentId").val();
        var code = $.trim($("#txtCode").val());
        var name = $.trim($("#txtName").val());
        var sort = $.trim($("#txtSort").val());
        if (sort == "") sort = 0;

        var t = $("#treeCt");
        var currentNode = t.tree('getSelected');
        ListCategory.GetStep(t, currentNode);
        var step = ListCategory.Step;
        ListCategory.Step = "";

        var sData = '{"Id":"' + Id + '","ParentId":"' + parentId + '","CategoryCode":"' + code + '","CategoryName":"' + name + '","Remark":"' + $.trim($("#txtRemark").val()) + '","Sort":"' + sort + '","Step":"' + step + '"}';
        $.ajax({
            url: ListCategory.Url,
            type: "post",
            data: '{"model":' + sData + '}',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                Common.OnProgressStart();
            },
            complete: function () {
                Common.OnProgressStop();
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return;
                }
                jeasyuiFun.show("温馨提示", "保存成功！");

                if (currentNode.text == "请选择") {
                    t.tree('update', {
                        target: currentNode.target,
                        id: result.Data,
                        text: name
                    });
                    ListCategory.Container.dialog('close');
                    return false;
                }
                var parentNode = t.tree('getParent', currentNode.target);
                if (Id == "") {
                    t.tree('append', {
                        parent: currentNode.target,
                        data: [{
                            id: result.Data, text: name, "attributes": {
                                "parentId": parentId
                            },
                        }]
                    });
                }
                else {
                    t.tree('update', {
                        target: currentNode.target,
                        text: name
                    });
                }
                ListCategory.Container.dialog('close');
            }
        });
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
    }
}