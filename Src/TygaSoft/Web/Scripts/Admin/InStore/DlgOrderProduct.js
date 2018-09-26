var DlgOrderProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {

    },
    InitData: function () {

    },
    InitForm:function(){
        
    },
    DlgDgOrderProduct: function (orderId) {
        var w = $(window).width();
        var h = $(window).height();
        if (w > 960) w = 960;
        else w = w * 0.9;
        if (h > 700) h = 700;
        else h = h * 0.9;
        if ($("body").find("#dlgOrderProduct").length == 0) {
            $("body").append("<div id=\"dlgOrderProduct\"></div>");
        }
        var dlg = $("#dlgOrderProduct");
        //var dgToolbar = '<div id="dgOrderProductToolbar" style="padding:5px;"><a class="easyui-linkbutton" data-options="plain:true,iconCls:\'icon-save\'" onclick="DlgOrderProduct.OnSave()"><span>保存</span></a></div>';
        dlg.dialog({
            title: '明细列表',
            width: w,
            height: h,
            closed: false,
            content: '<div id="dgOrderProduct" data-options="fit:true,fitColumns:true,pagination:true,rownumbers:true,singleSelect:true,border:false"></div>',
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSave', text: '确定', iconCls: 'icon-ok', handler: function () {
                    DlgOrderProduct.OnSave();
                    dlg.dialog('close');
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    dlg.dialog('close');
                }
            }],
            onOpen: function () {
                var bestStockLocationUrl = '/wms/Services/WmsService.svc/GetBestStockLocationList';

                var dg = $("#dgOrderProduct");
                dg.datagrid({
                    columns: [[
                        { field: 'Id', checkbox: true },
                        { field: 'ProductCode', title: '货品', width: 100 },
                        { field: 'ProductName', title: '货品名称', width: 200 },
                        { field: 'PackageCode', title: '包装', width: 100 },
                        { field: 'Unit', title: '单位', width: 100 },
                        { field: 'Status', title: '状态', width: 100 },
                        { field: 'ExpectedAmount', title: '预期量', width: 60 },
                        { field: 'ReceiptAmount', title: '已收货量', width: 80, editor: 'numberbox' },
                        { field: 'StockLocationAppendName', title: '推荐库位', width: 100 }
                        //{
                        //    field: 'StockUnit', title: '推荐库位', width: 100,
                        //    editor: {
                        //        type: 'combobox', options: {
                        //            valueField: 'Id', textField: 'Text'
                        //            //valueField: 'Id', textField: 'Text', onLoadSuccess: function () {
                        //            //    alert('onLoadSuccess--');
                        //            //    var jData = [{ 'Id': '1', 'Text': '库位1', "selected": true }, { 'Id': '2', 'Text': '库位2' }];
                        //            //    $(this).combobox('loadData', jData);
                        //            //}
                        //        }
                        //    }
                        //}
                    ]],
                    //onLoadSuccess: DlgOrderProduct.OnLoadSuccess,
                    onClickCell: function (index, field, value) {
                        DlgOrderProduct.EditIndex = index;
                        var rows = dg.datagrid('getRows');
                        for (var i = 0; i < rows.length; i++) {
                            if (i != index) {
                                dg.datagrid('endEdit', i);
                            }
                        }
                        dg.datagrid('beginEdit', index);
                    },
                    onAfterEdit: DlgOrderProduct.OnAfterEdit
                });
                $("#dgOrderProduct").datagrid('getPager').pagination({
                    onSelectPage: function (pageNumber, pageSize) {
                        DlgOrderProduct.LoadDgOrderProduct(pageNumber, pageSize, orderId);
                    }
                });
                DlgOrderProduct.LoadDgOrderProduct(1, 10, orderId);
            }
        })
    },
    LoadDgOrderProduct: function (pageIndex, pageSize, orderId) {
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
                var jData = JSON.parse(result.Data);

                dg.datagrid('loadData', jData);
            }
        });
    },
    EditIndex:-1,
    OnSave: function () {
        if (DlgOrderProduct.EditIndex > -1) {
            var dg = $("#dgOrderProduct");
            dg.datagrid('endEdit', DlgOrderProduct.EditIndex);
            DlgOrderProduct.EditIndex = -1;
        }
    },
    OnAfterEdit: function (index, row, changes) {
        if (!$.isEmptyObject(changes)) {
            var sData = '{"Id":"' + row.Id + '","ReceiptAmount":"' + changes.ReceiptAmount + '"}';
            $.ajax({
                url: "/wms/Services/WmsService.svc/UpdateOrderReceiptProduct",
                type: "post",
                data: '{"model":' + sData + '}',
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    //$.messager.progress({ title: '请稍等', msg: '正在执行...' });
                },
                complete: function () {
                    //$.messager.progress('close');
                },
                success: function (result) {
                    if (result.ResCode != 1000) {
                        $.messager.alert('系统提示', result.Msg, 'info');
                        return false;
                    }
                }
            });
        }
    },
    OnLoadSuccess: function (data) {
        
        var jData = data.rows;
        var len = jData.length;
        var stockLocationJson = [{ 'Id': '1', 'Text': '库位1', "selected": true }, { 'Id': '2', 'Text': '库位2' }];
        var dg = $("#dgOrderProduct");

        setTimeout(function () {
            var eds = dg.datagrid('getEditors', 1);
            var ed = dg.datagrid('getEditor', { index: 1, field: 'StockUnit' });
        }, 1000);
    }
}