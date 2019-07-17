var DlgStockLocationProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var tabs = $('#tabsStockLocation').tabs();
        var dgBest = $('#dgStockLocationProductBest').datagrid();
        var dgOther = $('#dgStockLocationProductOther').datagrid();
        var keyName = $('[id$=hKey]').val();

        if (keyName != 'OrderSendProduct') {
            dgBest.datagrid('hideColumn', 'CustomerCode');
            dgBest.datagrid('hideColumn', 'CustomerName');
        }
        if (keyName == 'OrderSendProduct' || keyName == 'OrderPickProduct') {
            dgBest.datagrid('getColumnOption', 'MaxQty').title = '货品数量';
            dgBest.datagrid();
            dgOther.datagrid('getColumnOption', 'MaxQty').title = '货品数量';
            dgOther.datagrid();
        }
        if (keyName == 'ShelfMissionProduct' || keyName == 'OrderPickProduct') {
            dgBest.datagrid('hideColumn', 'ProductCode');
            dgBest.datagrid('hideColumn', 'ProductName');
            dgOther.datagrid('hideColumn', 'ProductCode');
            dgOther.datagrid('hideColumn', 'ProductName');
        }
        switch (keyName) {
            case 'OrderSendProduct':
                var tabBest = tabs.tabs('getTab', 0);
                tabs.tabs('update', {
                    tab: tabBest,
                    options: {
                        title: '库位货品列表',
                    }
                });
                tabs.tabs('close', 1);
                break;
            case 'OrderPickProduct':
                break;
            default:
                break;
        }

        setTimeout(function () {
            DlgStockLocationProduct.GetStockLocationProductList(1, 1000);
            //DlgStockLocationProduct.GetStockLocationProductBestList(1, 100);
            //DlgStockLocationProduct.GetStockLocationProductOtherList(1, 100);
        }, 0);
    },
    DlgShelfMissionProduct: function (t) {
        var curr = $(t);

        if ($("body").find("#dlgStockLocationProduct").length == 0) {
            $("body").append("<div id=\"dlgStockLocationProduct\"></div>");
        }
        var w = $(window).width();
        if (w > 780) w = 780;
        else w = w * 0.8;
        var h = $(window).height();
        if (h > 500) h = 500;
        else h = h * 0.95;

        var sHref = '/wms/u/tdlg.html?' + curr.attr('code') + '';
        $("#dlgStockLocationProduct").dialog({
            title: '上架操作',
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: sHref,
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnOkDlgStockLocationProduct', text: '确定', iconCls: 'icon-ok', handler: function () {
                    DlgStockLocationProduct.OnSaveShelfMissionProduct();
                }
            }, {
                id: 'btnCancelDlgStockLocationProduct', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgStockLocationProduct').dialog('close');
                }
            }]
        })
    },
    DlgOrderSendProduct: function (t) {
        var Id = $('[id$=hId]').val();

        if ($("body").find("#dlgStockLocationProduct").length == 0) {
            $("body").append("<div id=\"dlgStockLocationProduct\"></div>");
        }
        var w = $(window).width();
        if (w > 1100) w = 1100;
        else w = w * 0.9;
        var h = $(window).height();
        if (h > 700) h = 700;
        else h = h * 0.95;

        var sHref = '/wms/u/tdlg.html?keyName=OrderSendProduct&orderId=' + Id + '';
        $("#dlgStockLocationProduct").dialog({
            title: '发货操作',
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: sHref,
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnOkDlgStockLocationProduct', text: '确定', iconCls: 'icon-ok', handler: function () {
                    DlgStockLocationProduct.OnSaveOrderSendProduct();
                }
            }, {
                id: 'btnCancelDlgStockLocationProduct', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgStockLocationProduct').dialog('close');
                }
            }]
        })
    },
    DlgOrderPickProduct: function (t) {
        var curr = $(t);

        if ($("body").find("#dlgStockLocationProduct").length == 0) {
            $("body").append("<div id=\"dlgStockLocationProduct\"></div>");
        }
        var w = $(window).width();
        if (w > 960) w = 960;
        else w = w * 0.9;
        var h = $(window).height();
        if (h > 500) h = 500;
        else h = h * 0.95;

        var sHref = '/wms/u/tdlg.html?' + curr.attr('code') + '';
        $("#dlgStockLocationProduct").dialog({
            title: '拣货操作',
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: sHref,
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnOkDlgStockLocationProduct', text: '确定', iconCls: 'icon-ok', handler: function () {
                    DlgStockLocationProduct.OnSaveOrderPickProduct();
                }
            }, {
                id: 'btnCancelDlgStockLocationProduct', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgStockLocationProduct').dialog('close');
                }
            }]
        })
    },
    OnClickRowByBest: function (index, row) {
        var dg = $('#dgStockLocationProductBest');
        dg.datagrid('beginEdit', index);
    },
    OnBeginEditByBest: function (index, row) {

    },
    OnClickRowByOther: function (index, row) {
        var dg = $('#dgStockLocationProductOther');
        dg.datagrid('beginEdit', index);
    },
    OnBeginEditByOther: function (index, row) {

    },
    GetStockLocationProductList: function (pageIndex, pageSize) {
        var dgBest = $('#dgStockLocationProductBest');
        var dgOther = $('#dgStockLocationProductOther');
        var valueItems = $('[id$=hValue]').val().split('|');

        var keyName = $('[id$=hKey]').val();
        var sUrl = '/wms/Services/WmsService.svc/GetStockLocationProductList';
        var sData = '';

        switch (keyName) {
            case 'ShelfMissionProduct':
                sData = '{"model":{"KeyName":"' + keyName + '","PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","ProductId":"' + valueItems[2] + '","Qty":"' + valueItems[3] + '"}}';
                break;
            case 'OrderSendProduct':
                sData = '{"model":{"KeyName":"' + keyName + '","PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","Qty":"0"}}';
                break;
            case 'OrderPickProduct':
                sData = '{"model":{"KeyName":"' + keyName + '","PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","ProductId":"' + valueItems[2] + '","CustomerId":"' + valueItems[3] + '","Qty":"' + valueItems[4] + '"}}';
                break;
            default:
                break;
        }
       
        //console.log('sData--' + sData);
        $.ajax({
            url: sUrl,
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                //console.log('result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                var jData = JSON.parse(result.Data);
                if (keyName == 'OrderSendProduct') {
                    DlgStockLocationProduct.SetDataByOrderSendProduct(jData);
                }
                else {
                    DlgStockLocationProduct.SetData(jData);
                }
            }
        });
    },
    SetDataByOrderSendProduct:function(data){
        var dg = $('#dgStockLocationProductBest');
        var dgData = dg.datagrid('getData');
        for (var i = 0; i < data.rows.length; i++) {
            var item = data.rows[i];
            dgData.rows.push(item);
        }
        dg.datagrid('loadData', dgData);
    },
    SetData: function (data) {
        var dgBest = $('#dgStockLocationProductBest');
        var dgOther = $('#dgStockLocationProductOther');
        var dgBestData = dgBest.datagrid('getData');
        var dgOtherData = dgOther.datagrid('getData');
        var hasBestRow = false;
        for (var i = 0; i < data.rows.length; i++) {
            var item = data.rows[i];
            if (item.IsBest) {
                hasBestRow = true;
                dgBestData.rows.push(item);
            }
            else {
                dgOtherData.rows.push(item);
            }
        }
        if (hasBestRow) dgBest.datagrid('loadData', dgBestData);
        dgOther.datagrid('loadData', dgOtherData);
    },
    GetStockLocationProductBestList: function (pageIndex, pageSize) {
        var dg = $('#dgStockLocationProductBest');
        var valueItems = $('[id$=hValue]').val().split('|');

        var keyName = $('[id$=hKey]').val();
        switch (keyName) {
            case 'OrderPickProduct':
                dg.datagrid('getColumnOption', 'MaxQty').title = '准备数量';
                dg.datagrid();
                break;
            default:
                break;
        }
        var url = '';
        var sData = '{"model":{"KeyName":"' + keyName + '","PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","ProductId":"' + valueItems[2] + '","Qty":"' + valueItems[3] + '"}}';
        //console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetStockLocationProductBestList",
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
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
    GetStockLocationProductOtherList: function (pageIndex, pageSize) {
        var dg = $('#dgStockLocationProductOther');
        var valueItems = $('[id$=hValue]').val().split('|');

        var keyName = $('[id$=hKey]').val();
        switch (keyName) {
            case 'OrderPickProduct':
                dg.datagrid('getColumnOption', 'MaxQty').title = '准备数量';
                dg.datagrid();
                break;
            default:
                break;
        }

        var sData = '{"model":{"KeyName":"' + keyName + '","PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","ProductId":"' + valueItems[2] + '","Qty":"' + valueItems[3] + '"}}';
        //console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetStockLocationProductOtherList",
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                //console.log('result2--' + JSON.stringify(result));
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
    OnSaveShelfMissionProduct: function () {
        var dgBest = $('#dgStockLocationProductBest');
        var dgOther = $('#dgStockLocationProductOther');
        dgBest.datagrid('acceptChanges');
        dgOther.datagrid('acceptChanges');
        var bestRows = dgBest.datagrid('getRows');
        var otherRows = dgOther.datagrid('getRows');
        var errMsg = '';
        var errIndex = 0;
        var valueItems = $('[id$=hValue]').val().split('|');
        var totalQty = parseFloat(valueItems[3]);
        var totalQty2 = 0;
        var sAppend = '';
        var appendIndex = 0;
        if (bestRows && bestRows.length > 0) {
            for (var i = 0; i < bestRows.length; i++) {
                var row = bestRows[i];
                var qty = parseFloat(row.Qty);
                if (qty > 0) {
                    if (qty > parseFloat(row.MaxQty)) {
                        if (errIndex > 0) errMsg += '<br />';
                        errMsg += '在推荐库位列表中，第“' + (i + 1) + '”行的数量超出了库位最大容量范围，请正确操作！';
                        dgBest.datagrid('beginEdit', i);
                        errIndex++;
                        break;
                    }
                    else {
                        totalQty2 = totalQty2 + qty;
                        if (appendIndex > 0) sAppend += '|';
                        sAppend += '' + row.StockLocationId + ',' + row.Qty + '';
                        appendIndex++;
                    }
                }
            }
        }
        if (otherRows && otherRows.length > 0) {
            for (var i = 0; i < otherRows.length; i++) {
                var row = otherRows[i];
                var qty = parseFloat(row.Qty);
                if (qty > 0) {
                    if (qty > parseFloat(row.MaxQty)) {
                        if (errIndex > 0) errMsg += '<br />';
                        errMsg += '在其它库位列表中，第“' + (i + 1) + '”行的数量超出了库位最大容量范围，请正确操作！';
                        dgOther.datagrid('beginEdit', i);
                        errIndex++;
                        break;
                    }
                    else {
                        totalQty2 = totalQty2 + qty;
                        if (appendIndex > 0) sAppend += '|';
                        sAppend += '' + row.StockLocationId + ',' + row.Qty + '';
                        appendIndex++;
                    }
                }
            }
        }
        if (errMsg != '') {
            $.messager.alert('错误提示', errMsg, 'error');
            return false;
        }
        if (totalQty2 > totalQty) {
            $.messager.alert('错误提示', '输入的数量（' + totalQty2 + '）累计超出了应上架数量（' + totalQty + '）', 'error');
            return false;
        }
        if (sAppend == '') {
            $.messager.alert('错误提示', '无任何可保存的数据！', 'error');
            return false;
        }
        sAppend = '' + valueItems[0] + '$' + valueItems[1] + '$' + valueItems[2] + '$' + sAppend + '';
        //console.log('sAppend--' + sAppend);
        var sData = '{ "itemAppend": "' + sAppend + '" }';
        //return false;
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
                //console.log('result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                jeasyuiFun.show("温馨提示", "保存成功！");
                $('#dlgStockLocationProduct').dialog('close');
                ListShelfMissionProduct.UpdateRow(valueItems[0], valueItems[1], valueItems[2]);
            }
        });
    },
    OnSaveOrderSendProduct: function () {
        var valueItems = $('[id$=hValue]').val().split('|');
        var Id = valueItems[0];
        if (!Id || Id == '') {
            $.messager.alert('提示', '请先完成单据信息后再执行此操作', 'error');
            return false;
        }

        var dgBest = $('#dgStockLocationProductBest');
        dgBest.datagrid('acceptChanges');
        var bestRows = dgBest.datagrid('getRows');
        var errMsg = '';
        var errIndex = 0;
        
        var sAppend = '';
        var appendIndex = 0;
        if (bestRows && bestRows.length > 0) {
            for (var i = 0; i < bestRows.length; i++) {
                var row = bestRows[i];
                var qty = parseFloat(row.Qty);
                if (qty > 0) {
                    if (qty > parseFloat(row.MaxQty)) {
                        if (errIndex > 0) errMsg += '<br />';
                        errMsg += '第“' + (i + 1) + '”行的数量超出了货品数量范围，请正确操作！';
                        dgBest.datagrid('beginEdit', i);
                        errIndex++;
                        break;
                    }
                    else {
                        if (appendIndex > 0) sAppend += '|';
                        sAppend += '' + row.StockLocationId + ',' + row.ProductId + ',' + row.CustomerId + ',' + row.Qty + '';
                        appendIndex++;
                    }
                }
            }
        }
        if (errMsg != '') {
            $.messager.alert('错误提示', errMsg, 'error');
            return false;
        }
        if (sAppend == '') {
            $.messager.alert('错误提示', '无任何可保存的数据！', 'error');
            return false;
        }

        var sData = '{"Id": "' + Id + '", "itemAppend": "' + sAppend + '" }';
        //console.log('sData--' + sData);
        //return false;
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveOrderSendProduct",
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
                //console.log('result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                ListOrderSendProduct.LoadDg(1, 50, Id);
                AddOrderSend.GetOrderSendInfo(Id);

                jeasyuiFun.show("温馨提示", "保存成功！");
                $('#dlgStockLocationProduct').dialog('close');
            }
        });
    },
    OnSaveOrderPickProduct: function () {
        var dgBest = $('#dgStockLocationProductBest');
        var dgOther = $('#dgStockLocationProductOther');
        dgBest.datagrid('acceptChanges');
        dgOther.datagrid('acceptChanges');
        var bestRows = dgBest.datagrid('getRows');
        var otherRows = dgOther.datagrid('getRows');
        var errMsg = '';
        var errIndex = 0;
        var valueItems = $('[id$=hValue]').val().split('|');
        var totalQty = parseFloat(valueItems[4]);
        var totalQty2 = 0;
        var sAppend = '';
        var appendIndex = 0;
        if (bestRows && bestRows.length > 0) {
            for (var i = 0; i < bestRows.length; i++) {
                var row = bestRows[i];
                var qty = parseFloat(row.Qty);
                if (qty > 0) {
                    if (qty > parseFloat(row.MaxQty)) {
                        if (errIndex > 0) errMsg += '<br />';
                        errMsg += '在推荐库位列表中，第“' + (i + 1) + '”行的数量超出了范围，请正确操作！';
                        dgBest.datagrid('beginEdit', i);
                        errIndex++;
                        break;
                    }
                    else {
                        totalQty2 = totalQty2 + qty;
                        if (appendIndex > 0) sAppend += '|';
                        sAppend += '' + row.StockLocationId + ',' + row.Qty + '';
                        appendIndex++;
                    }
                }
            }
        }
        if (otherRows && otherRows.length > 0) {
            for (var i = 0; i < otherRows.length; i++) {
                var row = otherRows[i];
                var qty = parseFloat(row.Qty);
                if (qty > 0) {
                    if (qty > parseFloat(row.MaxQty)) {
                        if (errIndex > 0) errMsg += '<br />';
                        errMsg += '在其它库位列表中，第“' + (i + 1) + '”行的数量超出了范围，请正确操作！';
                        dgOther.datagrid('beginEdit', i);
                        errIndex++;
                        break;
                    }
                    else {
                        totalQty2 = totalQty2 + qty;
                        if (appendIndex > 0) sAppend += '|';
                        sAppend += '' + row.StockLocationId + ',' + row.Qty + '';
                        appendIndex++;
                    }
                }
            }
        }
        if (errMsg != '') {
            $.messager.alert('错误提示', errMsg, 'error');
            return false;
        }
        if (totalQty2 > totalQty) {
            $.messager.alert('错误提示', '输入的数量（' + totalQty2 + '）累计超出了范围（' + totalQty + '）', 'error');
            return false;
        }
        if (sAppend == '') {
            $.messager.alert('错误提示', '无任何可保存的数据！', 'error');
            return false;
        }
        sAppend = '' + valueItems[0] + '$' + valueItems[1] + '$' + valueItems[2] + '$' + valueItems[3] + '$' + sAppend + '';
        
        var sData = '{ "itemAppend": "' + sAppend + '" }';
        //console.log('sData--' + sData);
        //return false;
        $.ajax({
            url: "/wms/Services/WmsService.svc/SaveOrderPickProduct",
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
                //console.log('result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                ListOrderPickProduct.UpdateRow(valueItems[0], valueItems[1], valueItems[2], valueItems[3]);

                $('#dlgStockLocationProduct').dialog('close');
                jeasyuiFun.show("温馨提示", "保存成功！");
            }
        });
    }

}