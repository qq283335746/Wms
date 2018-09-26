var ListOrderReceiptProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var dg = $("#dgOrderProduct");
        if (parseInt($('[id$=hOrderType]').val()) == 1) {
            dg.datagrid('hideColumn', 'PreOrderCode');
        }
        var pager = dg.datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListOrderReceiptProduct.LoadDg(pageNumber, pageSize);
            }
        });
        setTimeout(function () {
            ListOrderReceiptProduct.LoadDg(1, 10);
        }, 1);
    },
    LoadDg: function (pageIndex, pageSize) {
        var orderId = $.trim($("[id$=hId]").val());
        if (orderId == "") {
            return false;
        }
        var dg = $("#dgOrderProduct");
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","OrderId":"' + orderId + '"}}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetOrderReceiptProductList",
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
                //console.log('result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                dg.datagrid('loadData', JSON.parse(result.Data));
            }
        });
    },
    Add: function () {
        var Id = $.trim($("[id$=hId]").val());
        if (Id == "") {
            $.messager.alert('错误提示', "请先完成单号信息再执行此操作", 'error');
            return false;
        }
        var w = $(window).width();
        var h = $(window).height();
        if (w > 720) w = 720;
        else w = w * 0.9;
        if (h > 520) h = 520;
        else h = h * 0.9;
        $("#dlgAddOrderReceiptProduct").dialog({
            title: '新增明细信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/a/ainstore.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddOrderReceiptProduct.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddOrderReceiptProduct').dialog('close');
                }
            }]
        })
        return false;
    },
    Edit: function () {
        var rows = $("#dgOrderProduct").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var w = $(window).width();
        var h = $(window).height();
        if (w > 720) w = 720;
        else w = w * 0.9;
        if (h > 520) h = 520;
        else h = h * 0.9;
        $("#dlgAddOrderReceiptProduct").dialog({
            title: '编辑明细信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/a/ainstore.html?Id='+rows[0].Id+'',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddOrderReceiptProduct.Save();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddOrderReceiptProduct').dialog('close');
                }
            }]
        })
        return false;
    },
    Del: function () {
        var rows = $("#dgOrderProduct").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var orderId = $.trim($("[id$=hId]").val());
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/DeleteOrderReceiptProduct",
                    type: "post",
                    data: '{"orderId":"' + orderId + '","itemAppend":"' + itemAppend + '"}',
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
                        ListOrderReceiptProduct.LoadDg(1, 10);
                        jeasyuiFun.show("温馨提示", "操作成功！");
                    }
                });
            }
        });
    }
}