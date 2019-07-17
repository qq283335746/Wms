var ListStockWarning = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {
        
    },
    InitData: function () {
        setTimeout(function () {
            ListStockWarning.LoadDg(1, 10);
        }, 100);
    },
    InitForm: function () {
        var pager = $("#dg").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListStockWarning.LoadDg(pageNumber, pageSize);
            }
        });
    },
    LoadDg: function (pageIndex, pageSize) {
        var keyword = $.trim($("#txtKeyword").textbox('getValue'));
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","Keyword":"' + keyword + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetStockWarningList",
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
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                $("#dg").datagrid('loadData', eval("(" + result.Data + ")"));
            }
        });
    },
    Add: function () {
        ListStockWarning.SelectRow = null;
        var w = $(window).width();
        var h = $(window).height();
        if (w > 640) w = 640;
        else w = w * 0.9;
        if (h > 340) h = 340;
        else h = h * 0.9;
        if ($("body").find("#dlgAddStockWarning").length == 0) {
            $("body").append("<div id=\"dlgAddStockWarning\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddStockWarning").dialog({
            title: '新增库存预警信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/a/atbase.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddStockWarning.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddStockWarning').dialog('close');
                }
            }]
        })
        return false;
    },
    Edit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        ListStockWarning.SelectRow = rows[0];
        var w = $(window).width();
        var h = $(window).height();
        if (w > 640) w = 640;
        else w = w * 0.9;
        if (h > 340) h = 340;
        else h = h * 0.9;
        if ($("body").find("#dlgAddStockWarning").length == 0) {
            $("body").append("<div id=\"dlgAddStockWarning\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddStockWarning").dialog({
            title: '编辑库存预警信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/a/atbase.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddStockWarning.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddStockWarning').dialog('close');
                }
            }]
        })
        return false;
    },
    Del: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/DeleteStockWarning",
                    type: "post",
                    data: '{"itemAppend":"' + itemAppend + '"}',
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
                            return false;
                        }
                        jeasyuiFun.show("温馨提示", "操作成功！");
                        setTimeout(function () {
                            window.location.reload();
                        }, 1500)
                    }
                });
            }
        });
    },
    SelectRow: null,
    OnSearch: function () {
        ListStockWarning.LoadDg(1, 10);
    },
}