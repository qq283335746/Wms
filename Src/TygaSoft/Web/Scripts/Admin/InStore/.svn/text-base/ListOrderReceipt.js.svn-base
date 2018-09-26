var ListOrderReceipt = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        $("#abtnAdd").click(function () {
            ListOrderReceipt.Add();
        })
        $("#abtnEdit").click(function () {
            ListOrderReceipt.Edit();
        })
        $("#abtnDel").click(function () {
            ListOrderReceipt.Del();
        })
        $("#lbtnCreate").click(function () {
            ListOrderReceipt.OnCreate();
        })
    },
    InitData: function () {
        this.InitForm();
        setTimeout(function () {
            ListOrderReceipt.LoadDg(1, 10);
        }, 100);
    },
    InitForm: function () {
        var dg = $("#dg");
        if (parseInt($('[id$=hOrderType]').val()) == 1) {
            $("#lbtnCreate").hide();
            dg.datagrid('getColumnOption', 'IsStopNextText').title = '生成收货单';
            dg.datagrid();
        }
        var pager = dg.datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListOrderReceipt.LoadDg(pageNumber, pageSize);
            },
            onRefresh: function (pageNumber, pageSize) {
                ListOrderReceipt.LoadDg(pageNumber, pageSize);
            }
        });
    },
    LoadDg: function (pageIndex, pageSize) {
        var keyword = $("#txtKeyword").textbox('getValue');
        var orderType = parseInt($('[id$=hOrderType]').val());
        var dg = $('#dg');
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","OrderType":"' + orderType + '","Keyword":"' + keyword + '"}';

        $.ajax({
            url: "/wms/Services/WmsService.svc/GetOrderReceiptList",
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
    FIsStopNext: function (value, row, index) {
        if (row.IsStopNext == true) return '<span class="cr">已生成</span>';
        return '<span class="c-g">未生成</span>';
    },
    Add: function () {
        var orderType = $.trim($('[id$=hOrderType]').val());
        window.location = '/wms/a/atinstore.html?orderType=' + orderType + '';
    },
    Edit: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var orderType = $.trim($('[id$=hOrderType]').val());
        window.location = '/wms/a/atinstore.html?Id=' + rows[0].Id + '&orderType=' + orderType + '';
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
        //console.log('itemAppend--' + itemAppend);
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/DeleteOrderReceipt",
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
                        ListOrderReceipt.LoadDg(1, 10);
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
            if (row.IsStopNext) {
                isAllRight = false;
                break;
            }
            if (i > 0) itemAppend += ",";
            itemAppend += row.Id;
        }
        if (!isAllRight) {
            $.messager.alert('错误提示', "存在已经生成上架任务的行，请勿重复操作！", 'error');
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要生成上架任务吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/CreateShelfMission",
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
                                    IsStopNext: true
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
        ListOrderReceipt.LoadDg(1, 10);
    },
    Print: function () {
        var rows = $("#dg").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var Id = rows[0].Id;
        var sKey = parseInt($('[id$=hOrderType]').val()) == 1 ? 'PreOrderReceipt' : 'OrderReceipt';
        window.open("/wms/u/print.html?key=" + sKey + "&Id=" + Id + "");
    }
}