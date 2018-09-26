var DlgProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        DlgProduct.InitForm();
        setTimeout(function () {
            var container = DlgProduct.Container;
            var dg = container.find('#dgProduct');
            var stepName = $.trim(container.find('[id$=hStepName]').val());
            DlgProduct.Load(dg, 1, 1, stepName);
        }, 100)
    },
    Container: $('#dlgSelectProduct'),
    InitForm:function(){
        var container = DlgProduct.Container;
        var stepName = $.trim(container.find('[id$=hStepName]').val());
        var dg = container.find('#dgProduct');
        dg.datagrid();
        switch (stepName) {
            case '收货':
                break;
            case '上架':
                dg.datagrid('hideColumn', 'StockLocations');
                break;
        }
    },
    Load: function (dg,pageIndex, pageSize,stepName) {
        var url = '/wms/Services/WmsService.svc/GetOrderSelectProductList';
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","StepName":"' + stepName + '"}}';
        //console.log('GetOrderSelectProductList--sData--' + sData);

        $.ajax({
            url: url,
            type: "post",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                //console.log('GetOrderSelectProductList--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                var jData = JSON.parse(result.Data);
                var rowsLen = jData.rows.length;
                if (pageIndex == 1) dg.datagrid('loadData', jData);
                else {
                    var dgData = dg.datagrid('getData');
                    dgData.total += rowsLen;
                    for (var i = 0; i < rowsLen; i++) {
                        dgData.rows.push(jData.rows[i]);
                    }
                    dg.datagrid('loadData', dgData);
                }
                if (rowsLen > 0) {
                    pageIndex++;
                    DlgProduct.Load(dg, pageIndex, pageSize, stepName);
                }
            }
        });
    },
    FQty: function (value, row, index) {
        var container = DlgProduct.Container;
        var stepName = $.trim(container.find('[id$=hStepName]').val());
        switch (stepName) {
            case '收货':
                return row.UnQty;
                break;
            case '上架':
                return row.Qty;
                break;
        }
    },
    EditIndex: -1,
    OnClickRow: function (index, row) {
        var container = DlgProduct.Container;
        var dg = container.find('#dgProduct');
        //DlgProduct.EditIndex = index;
        //var rows = dg.datagrid('getRows');
        //for (var i = 0; i < rows.length; i++) {
        //    if (i != index) {
        //        dg.datagrid('endEdit', i);
        //    }
        //}
        dg.datagrid('beginEdit', index);
    },
    OnAfterEdit: function (index, row, changes) {
        //var container = DlgProduct.Container;
        //var dg = container.find('#dgProduct');
        //if (!$.isEmptyObject(changes)) {
        //    if (changes.RealQty > row.Qty) {
        //        $.messager.alert('错误提示', '实际数量超出了范围', 'error');
        //        setTimeout(function () {
        //            dg.datagrid('selectRow', index);
        //        }, 100);
        //        return false;
        //    }
        //}
    },
    OnSave: function () {
        var container = DlgProduct.Container;
        var dg = container.find('#dgProduct');
        dg.datagrid('acceptChanges');
        var stepName = $.trim(container.find('[id$=hStepName]').val());
        var Id = $('[id$=hId]').val();
        var orderId = $.trim(container.find('[id$=hOrderId]').val());
        
        var rows = dg.datagrid('getRows');
        if (rows.length < 1) {
            $.messager.alert('提示', '无任何可操作数据！', 'info');
            return;
        }
        var totalSave = 0;
        try {
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].RealQty > 0) {
                    var url = '';
                    var sData = '';
                    switch (stepName) {
                        case "收货":
                            if (rows[i].StockLocations == '') {
                                $.messager.alert('提示', '库位不能为空字符串，请正确操作！', 'info');
                                return false;
                            }
                            url = '/wms/Services/WmsService.svc/SaveShelfMissionProduct';
                            sData = '{"model":{"OrderId":"' + orderId + '","ProductId":"' + rows[i].ProductId + '","Qty":"' + rows[i].RealQty + '","StockLocations":"' + rows[i].StockLocations + '"}}';
                            break;
                        case "上架":
                            url = '/wms/Services/WmsService.svc/SaveOrderSendProduct';
                            sData = '{"model":{"OrderId":"' + orderId + '","ProductId":"' + rows[i].ProductId + '","Qty":"' + rows[i].RealQty + '"}}';
                            break;
                        default:
                            break;
                    }
                    //console.log(sData);
                    $.ajax({
                        url: url,
                        type: "post",
                        data: sData,
                        contentType: "application/json; charset=utf-8",
                        beforeSend: function () {
                            totalSave++;
                            if (totalSave == 1) $.messager.progress({ title: '请稍等', msg: '正在执行...' });
                        },
                        complete: function () {
                            totalSave--;
                        },
                        success: function (result) {
                            //console.log('result--' + JSON.stringify(result));
                            if (result.ResCode != 1000) {
                                if (result.Msg != "") {
                                    $.messager.alert('系统提示', result.Msg, 'info');
                                }
                                return false;
                            }
                            jeasyuiFun.show("温馨提示", "操作成功！");
                            setTimeout(function () {
                                switch (stepName) {
                                    case "收货":
                                        ListShelfMissionProduct.LoadDg(1, 50, Id);
                                        break;
                                    case "上架":
                                        ListOrderSendProduct.LoadDg(1, 50, Id);
                                        break;
                                    default:
                                        break;
                                }
                            }, 1000);
                        }
                    });
                }
            }
        }
        catch (e) {
            $.messager.progress('close');
        }
        finally {
            var timer = setInterval(function () {
                if (totalSave < 1) {
                    $.messager.progress('close');
                    clearInterval(timer);
                    DlgProduct.Container.dialog('close');
                }
            }, 1000);
            setTimeout(function () {
                if (totalSave > 0) {
                    clearInterval(timer);
                    $.messager.progress('close');
                    DlgProduct.Container.dialog('close');
                }
            }, 180000);
        }
    }
}