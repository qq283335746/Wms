var ListOrderSendProduct = {
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
            if (orderId != '') ListOrderSendProduct.LoadDg(1, 50, orderId);
        }, 100);
    },
    Container: $('#dlgOrderProductFm'),
    InitForm: function () {
        var orderId = $('[id$=hId]').val();
        var dg = $('#dgOrderProduct');
        var pager = dg.datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListOrderSendProduct.LoadDg(pageNumber, pageSize, orderId);
            }
        });
    },
    SelectRow: null,
    LoadDg: function (pageIndex, pageSize, orderId) {
        var dg = $('#dgOrderProduct');
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","OrderId":"' + orderId + '"}}';
        //console.log('sData--' + sData);
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetOrderSendProductList",
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
    OnClickRow: function (index, row) {
        var dg = $('#dgOrderProduct');
        dg.datagrid('beginEdit', index);
    },
    OnBeginEdit: function (index, row) {
        //$('#lbtnSavePick').linkbutton('enable');
    },
    OnSavePick:function(){
        var dg = $('#dgOrderProduct');
        dg.datagrid('acceptChanges');
        var rows = dg.datagrid('getRows');
        if (!rows || rows.length < 1) return false;
        var sAppend = '';
        var appendIndex = 0;
        var errMsgAppend = '';
        var errIndex = 0;
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            if (row.PickQty > row.Qty) {
                if (errIndex > 0) errMsgAppend += '<br />';
                errMsgAppend += '第“' + (i + 1) + '”行的拣货数量超出了范围，请正确操作！';
                dg.datagrid('beginEdit', i);
                errIndex++;
            }
            else {
                if (row.PickQty > 0) {
                    if (appendIndex > 0) sAppend += '|';
                    sAppend += row
                    appendIndex++;
                }
            }
        }
        if (errMsgAppend != '') {
            $.messager.alert('错误提示', errMsgAppend, 'error');
            return false;
        }
        if (!isSave) {
            $.messager.alert('提示', '请输入拣货数量后再执行此操作！', 'error');
            return false;
        }
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
            href: '/wms/u/tproduct.html?stepName=上架&orderId=' + orderId + '',
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
        var rows = $("#dgOrderProduct").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].OrderId + "|" + rows[i].ProductId + "|" + rows[i].CustomerId;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {

                var postData = { "ReqName": "DeleteOrderSendProduct", "ItemAppend": "" + itemAppend + "" };
                Common.AjaxPost("/wms/h/content.html", postData, function (result) {
                    jeasyuiFun.show("温馨提示", "操作成功！");
                    setTimeout(function () {
                        ListOrderSendProduct.LoadDg(1, 10);
                    }, 700);
                })
            }
        });
    }
}