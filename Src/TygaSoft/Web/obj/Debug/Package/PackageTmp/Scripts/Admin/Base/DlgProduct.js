var DlgProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {

    },
    InitData: function () {
        setTimeout(function () {
            DlgProduct.LoadDg(1, 10);
        }, 100);
    },
    InitForm: function () {
        $("#dgDlgProduct").datagrid();
        var pager = $("#dgDlgProduct").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                DlgProduct.LoadDg(pageNumber, pageSize);
            }
        });
    },
    LoadDg: function (pageIndex, pageSize) {
        var sKeyword = $.trim($('#txtDlgKeyword').textbox('getValue'));
        var dg = $("#dgDlgProduct");
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","Keyword":"' + sKeyword + '"}}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetProductList",
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
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                dg.datagrid('loadData', eval("(" + result.Data + ")"));
            }
        });
    },
    OnSearch: function () {
        DlgProduct.LoadDg(1, 10);
    },
    OnDlg: function () {
        if ($("body").find("#dlgProduct").length == 0) {
            $("body").append("<div id=\"dlgProduct\" style=\"padding:0;\"></div>");
        }
        var w = $(window).width();
        var h = $(window).height();
        if (w > 1024) w = 1024;
        else w = w * 0.94;
        if (h > 700) h = 700;
        else h = h * 0.94;
        $("#dlgProduct").dialog({
            title: '货品列表',
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: '/wms/t/product.html',
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSaveProduct', text: '确定', iconCls: 'icon-ok', handler: function () {
                    var dg = $("#dgDlgProduct");
                    var rows = dg.datagrid('getSelections');
                    if (!rows || rows.length != 1) {
                        $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
                        return false;
                    }
                    $("#hProductId").val(rows[0].Id);
                    $('#dlgProduct').dialog('close');
                    $("#txtProduct").textbox('setValue', rows[0].ProductCode);
                }
            }, {
                id: 'btnCancelSaveProduct', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgProduct').dialog('close');
                }
            }]
        })
    }
}