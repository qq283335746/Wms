var ListShelfMission = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {

    },
    InitData: function () {
        setTimeout(function () {
            ListShelfMission.LoadDg(1, 10);
        }, 100);
    },
    InitForm: function () {
        var pager = $("#dg").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListShelfMission.LoadDg(pageNumber, pageSize);
            }
        });
    },
    LoadDg: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var dg = $('#dg');
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","Keyword":"' + keyword + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetShelfMissionList",
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
                dg.datagrid('loadData', eval("(" + result.Data + ")"));
            }
        });
    },
    Edit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        window.location = '/wms/a/gainstore.html?Id=' + rows[0].Id + '';
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
                    url: "/wms/Services/WmsService.svc/DeleteShelfMission",
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
    Print: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var Id = rows[0].Id;
        window.open("/wms/u/print.html?key=shelve&Id=" + Id + "");
    },
    OnSearch: function () {
        ListShelfMission.LoadDg(1, 10);
    }
}