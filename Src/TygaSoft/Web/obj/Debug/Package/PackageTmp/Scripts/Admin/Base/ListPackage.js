var ListPackage = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        $("#abtnAdd").click(function () {
            ListPackage.Add();
        })
        $("#abtnEdit").click(function () {
            ListPackage.Edit();
        })
        $("#abtnDel").click(function () {
            ListPackage.Del();
        })
    },
    InitData: function () {

    },
    GetMyData: function (clientId) {
        var myData = $("#" + clientId + "").html();
        return eval("(" + myData + ")");
    },
    Grid: function (pageIndex, pageSize) {
        var pager = $('#dgT').datagrid('getPager');
        $(pager).pagination({
            total: sTotalRecord,
            pageNumber: sPageIndex,
            pageSize: sPageSize,
            onSelectPage: function (pageNumber, pageSize) {
                if (sQueryStr.length == 0) {
                    window.location = "?pageIndex=" + pageNumber + "&pageSize=" + pageSize + "";
                }
                else {
                    window.location = "?" + sQueryStr + "&pageIndex=" + pageNumber + "&pageSize=" + pageSize + "";
                }
            }
        });
    },
    Add: function () {
        $("#dlgAddPackage").dialog({
            title: '新增包装信息',
            width: 780,
            height: 500,
            closed: false,
            href: '/wms/a/ybase.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddPackage.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddPackage').dialog('close');
                }
            }]
        })
        return false;
    },
    Edit: function () {
        var rows = $("#dgT").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        $("#dlgAddPackage").dialog({
            title: '编辑包装信息',
            width: 780,
            height: 500,
            closed: false,
            href: '/wms/a/ybase.html?Id=' + rows[0].f0 + '',
            modal: true,
            iconCls: 'icon-edit',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddPackage.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddPackage').dialog('close');
                }
            }]
        })
        return false;
    },
    Del: function () {
        var rows = $("#dgT").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].f0;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/DeletePackage",
                    type: "post",
                    data: '{"itemAppend":"' + itemAppend + '"}',
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
                            return false;
                        }
                        jeasyuiFun.show("温馨提示", "操作成功！");
                        setTimeout(function () {
                            window.location.reload();
                        }, 1000);
                    }
                });
            }
        });
    }
}