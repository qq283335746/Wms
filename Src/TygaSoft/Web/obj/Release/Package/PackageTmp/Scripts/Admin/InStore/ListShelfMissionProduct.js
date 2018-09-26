var ListShelfMissionProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var orderId = $('[id$=hId]').val();
        setTimeout(function () {
            if (orderId != '') ListShelfMissionProduct.LoadDg(1, 50, orderId);
        }, 100)
    },
    InitForm: function () {
        var orderId = $('[id$=hId]').val();
        var dg = $('#dgOrderProduct');
        var pager = dg.datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListShelfMissionProduct.LoadDg(pageNumber, pageSize, orderId);
            }
        });
    },
    SelectRow: null,
    LoadDg: function (pageIndex, pageSize, orderId) {
        var dg = $('#dgOrderProduct');
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","OrderId":"' + orderId + '"}}';
        //console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetShelfMissionProductList",
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
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                dg.datagrid('loadData', JSON.parse(result.Data));
            }
        });
    },
    OnSaveRows: function () {
        var dg = $('#dgOrderProduct');
        dg.datagrid('acceptChanges');
        var rows = dg.datagrid('getRows');
        if (!rows || rows.length < 1) return false;
        var postData = [];
        var sAppend = '';
        var appendIndex = 0;
        var errMsgAppend = '';
        var errIndex = 0;
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            if (row.Qty > row.StayQty || row.StockLocations == '') {
                if (errIndex > 0) errMsgAppend += '<br />';
                if (row.Qty > row.StayQty) errMsgAppend += '第“' + (i + 1) + '”行的上架数量超出了范围，请正确操作！';
                if (row.StockLocations == '') errMsgAppend += '第“' + (i + 1) + '”行的库位不能为空字符串，请正确操作！';
                dg.datagrid('beginEdit', i);
                errIndex++;
                break;
            }
            else {
                if (row.Qty > 0 && row.StockLocations != '') {
                    if (appendIndex > 0) sAppend += '|';
                    sAppend += row.ShelfMissionId + ',' + row.OrderId + ',' + row.ProductId + ',' + row.Qty + ',' + row.StockLocations;
                    appendIndex++;
                }
            }
        }
        if (errMsgAppend != '') {
            $.messager.alert('错误提示', errMsgAppend, 'error');
            return false;
        }

        var sData = { itemAppend: sAppend };
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveShelfMissionProduct",
            type: "post",
            data: sData,
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
                jeasyuiFun.show("温馨提示", "保存成功！");
            }
        });

    },
    FBtn: function (value, row, index) {
        //var qStr = '' + row.ShelfMissionId + '|' + row.OrderId + '|' + row.ProductId + '';
        var qty = parseFloat(row.StayQty) - parseFloat(row.Qty);
        if (qty > 0) {
            var qStr = 'keyName=ShelfMissionProduct&shelfMissionId=' + row.ShelfMissionId + '&orderId=' + row.OrderId + '&productId=' + row.ProductId + '&qty=' + qty;
            return '<a class="abtn" code="' + qStr + '" onclick="DlgStockLocationProduct.DlgShelfMissionProduct(this)">操作</a>';
        }
        return '已完成';
    },
    FStockLocations: function (value, row, index) {
        var s = '';
        var jStockLocations = JSON.parse(value);
        for (var i = 0; i < jStockLocations.length; i++) {
            if (i > 0) s += '，';
            s += jStockLocations[i].StockLocationCode;
        }
        return s;
    },
    OnAdd: function () {
        var orderId = $('[id$=hId]').val();
        if (!orderId || orderId == '') {
            $.messager.alert('错误提示', '请先完成单号信息再进行此操作！', 'error');
            return false;
        }

        var w = $(window).width();
        var h = $(window).height();
        if (w > 1000) w = 1000;
        else w = w * 0.9;
        if (h > 700) h = 700;
        else h = h * 0.9;
        if ($("body").find("#dlgSelectProduct").length == 0) {
            $("body").append("<div id=\"dlgSelectProduct\"></div>");
        }
        $("#dlgSelectProduct").dialog({
            title: '选择货品',
            width: w,
            height: h,
            closed: false,
            href: '/wms/u/tproduct.html?stepName=收货&orderId=' + orderId + '',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSelect', text: '确定', iconCls: 'icon-save', handler: function () {
                    DlgProduct.OnSave();
                }
            }, {
                id: 'btnCancelSelect', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgSelectProduct').dialog('close');
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
        var orderId = $('[id$=hId]').val();
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/DeleteShelfMissionProduct",
                    type: "post",
                    data: '{"orderId":"' + orderId + '","itemAppend":"' + itemAppend + '"}',
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
                        ListShelfMissionProduct.LoadDg(1, 50, orderId);
                    }
                });
            }
        });
    },
    UpdateRow: function (Id, orderId, productId) {
        var dg = $('#dgOrderProduct');
        var postData = { "ReqName": "GetShelfMissionProductInfo", "Id": "" + Id + "", "OrderId": "" + orderId + "", "ProductId": "" + productId + "" };
        Common.AjaxPost("/wms/h/users.html", postData, function (result) {
            var rowData = JSON.parse(result.Data);
            var selectedRow = dg.datagrid('getSelections')[0];
            var selectedIndex = dg.datagrid('getRowIndex', selectedRow);
            dg.datagrid('updateRow', {
                index: selectedIndex,
                row: {
                    Qty: rowData.Qty,
                    StockLocations: rowData.StockLocations
                }
            });
        });
    }
}