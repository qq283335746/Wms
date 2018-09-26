var ListStockProduct = {
    Init: function () {
        this.InitEvent();
        this.InitData();
        this.InitForm();
    },
    InitEvent: function () {
        
    },
    InitData: function () {
        setTimeout(function () {
            ListStockProduct.LoadDg(1, 10);
        }, 100);
    },
    InitForm:function(){
        var pager = $("#dgStockProduct").datagrid('getPager');
        pager.pagination({
            onSelectPage: function (pageNumber, pageSize) {
                ListStockProduct.LoadDg(pageNumber, pageSize);
            }
        });
    },
    LoadDg: function (pageIndex, pageSize) {
        var sStartDate = $('#txtSStartDate').datebox('getValue');
        var sEndDate = $('#txtSEndDate').datebox('getValue');
        var sKeyword = $.trim($('#txtKeyword').textbox('getValue'));

        var dg = $('#dgStockProduct');
        var sData = '{"model":{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","StartDate":"' + sStartDate + '","EndDate":"' + sEndDate + '","Keyword":"' + sKeyword + '"}}';
        $.ajax({
            url: "/wms/Services/WmsService.svc/GetStockProductList",
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
                //console.log('result.Data--' + result.Data);
                var jData = JSON.parse(result.Data);
                dg.datagrid('loadData', jData);
                if (jData.IsSelfView == 'True') {
                    dg.datagrid('hideColumn', 'Id');
                }
            }
        });
    },
    OnSearch: function () {
        ListStockProduct.LoadDg(1, 10);
    },
    FStockLocation: function (value, row, index) {
        //var jStockLocations = JSON.parse(row.StockLocations);
        //if (jStockLocations.length > 0) {
        //    var sAppend = '';
        //    for (var i = 0; i < jStockLocations.length; i++) {
        //        if (i > 0) sAppend += ',';
        //        sAppend += jStockLocations[i].StockLocationCode;
        //    }
        //    return sAppend;
        //}
        return value;
    },
    FWarnMsg: function (value, row, index) {
        if (value == '') return '正常';
        return '<span class="f-cr">' + value + '</span>';
    },
    OnExport: function () {
        $.messager.confirm('提示', '确定要导出数据吗？', function (r) {
            if (r) {
                window.open("/wms/h/upload.html?ReqName=ExportStockProduct");
            }
        })
    }
}