var ListVehicle = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        ListVehicle.LoadDg(1, 10);
    },
    SelectRow: null,
    LoadDg: function (pageIndex, pageSize) {
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetVehicleList",
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
    OnAdd: function () {
        ListVehicle.SelectRow = null;
        var wh = Common.GetWh(750, 460);
        if ($("body").find("#dlgAddVehicle").length == 0) {
            $("body").append("<div id=\"dlgAddVehicle\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddVehicle").dialog({
            title: '填写信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            href: '/wms/a/agbase.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddVehicle.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddVehicle').dialog('close');
                }
            }]
        })
        return false;
    },
    OnEdit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        ListVehicle.SelectRow = rows[0];
        var wh = Common.GetWh(750, 460);
        if ($("body").find("#dlgAddVehicle").length == 0) {
            $("body").append("<div id=\"dlgAddVehicle\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddVehicle").dialog({
            title: '编辑信息',
            width: wh[0],
            height: wh[1],
            closed: false,
            href: '/wms/a/agbase.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddVehicle.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddVehicle').dialog('close');
                }
            }]
        })
        return false;
    },
    OnDel: function () {
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
                    url: "/wms/Services/WmsService.svc/DeleteVehicle",
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
                        ListVehicle.LoadDg(1, 10);
                    }
                });
            }
        });
    }
}