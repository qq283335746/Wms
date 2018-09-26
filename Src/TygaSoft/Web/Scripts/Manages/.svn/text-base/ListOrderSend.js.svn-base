var ListOrderSend = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {

    },
    InitData: function () {
        ListOrderSend.AppId = $.trim($('[id$=lbAppId]').text());
        setTimeout(function () {
            ListOrderSend.LoadDg(1, 10);
        }, 100);
    },
    InitForm: function () {
        var pager = $("#dg").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListOrderSend.LoadDg(pageNumber, pageSize);
            }
        });
        if (ListOrderSend.AppId == '100003') {
            $('#lbtnCreate').find('.l-btn-text').text('Delivery Task');
        }
    },
    AppId:"",
	SelectRow: null,
	LoadDg: function (pageIndex, pageSize) {

	    var keyword = $("#txtKeyword").textbox('getValue');
	    var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","Keyword":"' + keyword + '"}}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetOrderSendList",
            type: "post",
            data: sData,
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
    FIsStopNext: function (value, row, index) {
        if (row.IsStopNext == true) return '<span class="cr">已生成</span>';
        return '<span class="abtn">未生成</span>';
    },
    OnEdit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        window.location = '/wms/a/gsend.html?Id=' + rows[0].Id + '';
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
                    url: "/wms/Services/WmsService.svc/DeleteOrderSend",
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
                        }, 1000)
                    }
                });
            }
        });
    },
    OnCreate: function () {
        var dg = $("#dg");
        var rows = dg.datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据再进行操作", 'error');
            return false;
        }
        var isAllRight = true;
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            if (row.Status != 0) {
                isAllRight = false;
                break;
            }
            if (i > 0) itemAppend += ",";
            itemAppend += row.Id;
        }
        if (!isAllRight) {
            $.messager.alert('错误提示', "存在已经生成拣货任务的行，请勿重复操作！", 'error');
            return false;
        }
        var confirmText = "确定要生成拣货任务吗？";
        if (ListOrderSend.AppId == '100003') {
            confirmText = "Delivery Task ？";
        }
        $.messager.confirm('温馨提醒', confirmText, function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/CreateOrderPicked",
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
                        for (var i = 0; i < rows.length; i++) {
                            var rowIndex = dg.datagrid('getRowIndex', rows[i]);
                            dg.datagrid('updateRow', {
                                index: rowIndex,
                                row: {
                                    Status: 1
                                }
                            });
                        }
                        dg.datagrid('acceptChanges');
                        jeasyuiFun.show("温馨提示", "操作成功！");
                    }
                });
            }
        });
    },
    OnSearch: function () {
        ListOrderSend.LoadDg(1, 10);
    },
    Print: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var Id = rows[0].Id;
        window.open("/wms/u/print.html?key=OrderSend&Id=" + Id + "");
    }
}