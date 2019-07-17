
var ListRegion = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent:function(){
        $("[name=abtnAdd]").click(function () {
            ListRegion.Add();
        })
        $("[name=abtnEdit]").click(function () {
            ListRegion.Edit();
        })
        $("[name=abtnDel]").click(function () {
            ListRegion.Del();
        })
    },
    InitData:function(){
        this.Load();
    },
    Container: $("#dlgRegion"),
    Url: "",
    Load: function () {
        var t = $("#treeCt");

        $.ajax({
            url: "../Services/SecurityService.svc/GetRegionTree",
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
                t.tree({
                    data: eval("(" + result.Data + ")"),
                    animate: true,
                    onContextMenu: function (e, node) {
                        e.preventDefault();
                        $(this).tree('select', node.target);
                        $('#mmTree').menu('show', {
                            left: e.pageX,
                            top: e.pageY
                        });
                    }
                })
            }
        });
    },
    Add: function () {
        this.Url = "../Services/SecurityService.svc/SaveRegion";
        var t = $("#treeCt");
        var node = t.tree('getSelected');
        if (!node) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        ListRegion.Container.dialog({
            title: "新建菜单导航",
            href: '../a/ymenu.html?action=add&Id=' + node.id + '',
            closed: false,
            modal: true,
            iconCls: 'icon-save',
            buttons: [{
                id: 'btnSaveRegion', text: '保存', iconCls: 'icon-save', handler: function () {
                    ListRegion.Save();
                }
            }, {
                id: 'btnCancelSaveRegion', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgRegion').dialog('close');
                }
            }]
        });
    },
    Edit: function () {
        this.Url = "../Services/SecurityService.svc/SaveRegion";
        var t = $("#treeCt");
        var node = t.tree('getSelected');
        if (!node) {
            $.messager.alert('错误提示', '请选中一个节点再进行操作', 'error');
            return false;
        }
        $("#hCurrExpandNode").val(node.id);
        ListRegion.Container.dialog({
            title: "编辑菜单导航",
            href: '../a/ymenu.html?action=edit&Id=' + node.id + '',
            closed: false,
            modal: true,
            iconCls: 'icon-save',
            buttons: [{
                id: 'btnSaveRegion', text: '保存', iconCls: 'icon-save', handler: function () {
                    ListRegion.Save();
                }
            }, {
                id: 'btnCancelSaveRegion', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgRegion').dialog('close');
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
                        url: "../Services/SecurityService.svc/DeleteRegion",
                        contentType: "application/json; charset=utf-8",
                        data: '{"Id":"' + node.id + '"}',
                        beforeSend: function () {
                            $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                        },
                        complete: function () {
                            $.messager.progress('close');
                        },
                        success: function (result) {
                            if (result.ResCode != 1000) {
                                $.messager.alert('系统提示', result.Msg, 'info');
                                return false;
                            }
                            t.tree('remove', node.target);
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
        var sTitle = $.trim($("#txtTitle").val());
        var sort = $.trim($("#txtSort").val());
        if (sort == "") sort = 0;

        var sData = '{"Id":"' + Id + '","ParentId":"' + parentId + '","Title":"' + sTitle + '","Url":"' + $.trim($("#txtUrl").val()) + '","Descr":"' + $.trim($("#txtDescr").val()) + '","Sort":"' + sort + '"}';
        $.ajax({
            url: ListRegion.Url,
            type: "post",
            data: '{"model":' + sData + '}',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                $.messager.progress('close');
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return;
                }
                jeasyuiFun.show("温馨提示", "保存成功！");

                var t = $('#treeCt');
                var selected = t.tree('getSelected');
                if (selected.text == "请选择") {
                    t.tree('update', {
                        target: selected.target,
                        id: result.Data,
                        text: sTitle
                    });
                    ListRegion.Container.dialog('close');
                    return false;
                }
                var parentNode = t.tree('getParent', selected.target);
                if (Id == "") {
                    t.tree('append', {
                        parent: selected.target,
                        data: [{
                            id: result.Data, text: sTitle, "attributes": {
                                "parentId": parentId
                            },
                        }]
                    });
                }
                else {
                    t.tree('update', {
                        target: selected.target,
                        text: sTitle
                    });
                }
                ListRegion.Container.dialog('close');
            }
        });
    }
}