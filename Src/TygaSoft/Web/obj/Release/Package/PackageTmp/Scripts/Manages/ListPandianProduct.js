var ListPandianProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.LoadDg(1, 50);
        var dg = $('#dgPandianProduct');
        var pager = dg.datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListPandianProduct.LoadDg(pageNumber, pageSize);
            }
        });
    },
	SelectRow: null,
	LoadDg: function (pageIndex, pageSize) {
	    var sId = $('[id$=hId]').val();
	    if (sId == '') return false;
	    var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","ParentId":"' + sId + '"}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetPandianProductList",
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
                //console.log('GetPandianProductList--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    if (result.Msg != "") {
                        $.messager.alert('系统提示', result.Msg, 'info');
                    }
                    return false;
                }
                var jData = JSON.parse(result.Data);
                $("#dgPandianProduct").datagrid('loadData', jData);
                $('#lbTotalPan').text(jData.footer.TotalPan);
                $('#lbTotalYPan').text(jData.footer.TotalYpan);
                $('#lbTotalNotPan').text(jData.footer.TotalNotPan);
            }
        });
    },
    OnAdd: function () {
        ListPandianProduct.SelectRow = null;
        var w = $(window).width();
        var h = $(window).height();
        if (w > 750) w = 750;
        else w = w * 0.9;
        if (h > 340) h = 340;
        else h = h * 0.9;
        if ($("body").find("#dlgAddPandianProduct").length == 0) {
            $("body").append("<div id=\"dlgAddPandianProduct\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddPandianProduct").dialog({
            title: '填写信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/a/yyinstore.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddPandianProduct.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddPandianProduct').dialog('close');
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
        ListPandianProduct.SelectRow = rows[0];
        var w = $(window).width();
        var h = $(window).height();
        if (w > 750) w = 750;
        else w = w * 0.9;
        if (h > 340) h = 340;
        else h = h * 0.9;
        if ($("body").find("#dlgAddPandianProduct").length == 0) {
            $("body").append("<div id=\"dlgAddPandianProduct\" style=\"padding:10px;\"></div>");
        }
        $("#dlgAddPandianProduct").dialog({
            title: '编辑信息',
            width: w,
            height: h,
            closed: false,
            href: '/wms/a/yyinstore.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddPandianProduct.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddPandianProduct').dialog('close');
                }
            }]
        })
        return false;
    },
    OnDel: function () {
        var rows = $("#dgPandianProduct").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据再进行此操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].PandianId + '|' + rows[i].ProductId + '|' + rows[i].CustomerId;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/wms/Services/WmsService.svc/DeletePandianProduct",
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
                        ListPandianProduct.LoadDg(1, 50);
                        jeasyuiFun.show("温馨提示", "操作成功！");
                    }
                });
            }
        });
    },
    FUserName: function (value, row, index) {
        if (row.Status != '新建') return value;
        return "";
    },
    OnDlgPandian: function () {
        var rows = $("#dgPandianProduct").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        //console.log('rows[0]--' + JSON.stringify(rows[0]));
        var w = $(window).width();
        var h = $(window).height();
        if (w > 780) w = 780;
        else w = w * 0.9;
        if (h > 700) h = 700;
        else h = h * 0.9;
        if ($("body").find("#dlgPandianProduct").length == 0) {
            $("body").append("<div id=\"dlgPandianProduct\" style=\"padding:10px;\"></div>");
        }
        $("#dlgPandianProduct").dialog({
            title: '盘点',
            width: w,
            height: h,
            closed: false,
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSaveDlgPandianProduct', text: '确定', iconCls: 'icon-ok', handler: function () {
                    ListPandianProduct.SavePandianProduct();
                }
            }, {
                id: 'btnCancelDlgPandianProduct', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgPandianProduct').dialog('close');
                }
            }],
            content: '<table id="dgDlgPandianProduct" data-options="rownumbers:true,singleSelect:true,fit:true,fitColumns:true,striped:true,onClickRow:ListPandianProduct.OnClickRow"></table>',
            onOpen: function () {
                //console.log('rows[0]--' + JSON.stringify(rows[0]));
                var jData = rows[0].UpdatedStockLocations == "" ? JSON.parse(rows[0].StockLocations) : JSON.parse(rows[0].UpdatedStockLocations);
                $('#dgDlgPandianProduct').datagrid({
                    data: jData,
                    columns: [[
                        { field: 'StockLocationId', hidden: true },
                        { field: 'StockLocationCode', title: '库位', width: 200 },
                        { field: 'Qty', title: '实盘数', width: 80, editor: 'numberbox' }
                    ]]
                })
            }
        })
    },
    OnClickRow: function (index, row) {
        $('#dgDlgPandianProduct').datagrid('beginEdit', index);
    },
    SavePandianProduct: function () {
        var dgData = $('#dgDlgPandianProduct').datagrid('acceptChanges').datagrid('getData');
        var qty = 0;
        for (var i = 0; i < dgData.rows.length; i++) {
            qty += parseFloat(dgData.rows[i].Qty);
        }
        var dg = $("#dgPandianProduct");
        var row = dg.datagrid('getSelections')[0];
        row.Qty = qty;
        row.FailQty = row.StayQty - row.Qty;

        var sData = '{"model":{"PandianId":"' + row.PandianId + '","ProductId":"' + row.ProductId + '","CustomerId":"' + row.CustomerId + '","Zones":"' + row.Zones + '","StockLocations":"' + encodeURIComponent(JSON.stringify(dgData.rows)) + '","Qty":"' + row.Qty + '"}}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/SavePandianProduct",
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
                //console.log('SavePandianProduct--result--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                if (!$('#lbtnSave').linkbutton('options').disabled) $('#lbtnSave').linkbutton('disable');
                var jData = JSON.parse(result.Data);
                row.Status = jData.Status;
                row.UpdatedStockLocations = jData.UpdatedStockLocations;
                var selectRowIndex = dg.datagrid('getRowIndex', row);
                dg.datagrid('refreshRow', selectRowIndex);
                dg.datagrid('selectRow', selectRowIndex);
                $('#dlgPandianProduct').dialog('close');
                ListPandianProduct.SetTotal();
                jeasyuiFun.show("温馨提示", "保存成功！");
            }
        });
    },
    SetTotal: function () {
        var totalPan = 0;
        var totalYpan = 0;
        var totalNotPan = 0;
        var rows = $('#dgPandianProduct').datagrid('getRows');
        for (var i = 0; i < rows.length; i++) {
            var row = rows[i];
            if (row.Status != '新建') {
                totalPan++;
                if (row.Qty != 0 && row.FailQty != 0) {
                    totalYpan++;
                }
            }
            else totalNotPan++;
        }
        $('#lbTotalPan').text(totalPan);
        $('#lbTotalYPan').text(totalYpan);
        $('#lbTotalNotPan').text(totalNotPan);
    }
}